using System.Collections.Generic;
using System.Linq;
using Tactile.Core.Extensions;
using UnityEngine;

namespace Tactile.Core.Utility.MeshPart
{
    public class MeshPart
    {
        public readonly List<Vector3> Positions = new();
        public readonly List<int> Triangles = new();
        public readonly List<Vector2> UV = new();
        public readonly List<Vector3> Normals = new();

        public void ApplyTransformationMatrix(Matrix4x4 transformationMatrix)
        {
            Positions.MapInPlace(vert => transformationMatrix * vert);
        }

        public void Translate(Vector3 offset)
        {
            Positions.MapInPlace(vert => vert + offset);
        }

        public void Scale(float scale)
        {
            Positions.MapInPlace(vert => scale * vert);
        }

        public void Scale(Vector3 scale)
        {
            Positions.MapInPlace(vert => Vector3.Scale(scale, vert));
        }

        public void TransformUV(Vector2 offset, Vector2 scale)
        {
            UV.MapInPlace(uv => Vector2.Scale(uv, scale) + offset);
        }

        public void FlipTriangleFaces()
        {
            Triangles.MapSliceInPlace(3, slice => (slice[0], slice[1], slice[2]) = (slice[2], slice[1], slice[0]));
        }

        public void AddTriangle(int a, int b, int c)
        {
            Triangles.Add(a);
            Triangles.Add(b);
            Triangles.Add(c);
        }

        public void AddTriangle((int a, int b, int c) triangle)
        {
            AddTriangle(triangle.a, triangle.b, triangle.c);
        }

        public void AddTriangles(params (int a, int b, int c)[] triangles)
        {
            foreach (var (a, b, c) in triangles)
            {
                AddTriangle(a,b,c);
            }
        }

        public void AddQuad(int topLeft, int topRight, int bottomLeft, int bottomRight)
        {
            AddTriangle(topLeft, topRight, bottomLeft);
            AddTriangle(bottomLeft, topRight, bottomRight);
        }
        
        public void AddQuad((int topLeft, int topRight, int bottomLeft, int bottomRight) quad)
        {
            AddQuad(quad.topLeft, quad.topRight, quad.bottomLeft, quad.bottomRight);
        }
        
        
        public void AddQuads(params (int topLeft, int topRight, int bottomLeft, int bottomRight)[] quads)
        {
            foreach (var (topLeft, topRight, bottomLeft, bottomRight) in quads)
            {
                AddQuad(topLeft, topRight, bottomLeft, bottomRight);
            }
        }

        public MeshPart Copy()
        {
            var copiedPart = new MeshPart();
            copiedPart.Positions.AddRange(Positions);
            copiedPart.Triangles.AddRange(Triangles);
            copiedPart.UV.AddRange(UV);
            copiedPart.Normals.AddRange(Normals);

            return copiedPart;
        }

        public Mesh CreateMesh()
        {
            var mesh = new Mesh();
            ApplyToMesh(mesh);
            
            return mesh;
        }

        public void ApplyToMesh(Mesh mesh)
        {
            mesh.vertices = Positions.ToArray();
            mesh.triangles = Triangles.ToArray();
            mesh.normals = Normals.ToArray();
            mesh.uv = UV.ToArray();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();
        }
        
        public static MeshPart Combine(params MeshPart[] parts)
        {
            var combinedPart = new MeshPart();
            combinedPart.Positions.AddRange(parts.SelectMany(part => part.Positions));
            combinedPart.UV.AddRange(parts.SelectMany(part => part.UV));
            combinedPart.Normals.AddRange(parts.SelectMany(part => part.Normals));

            var triangleOffset = 0;
            foreach (var part in parts)
            {
                combinedPart.Triangles.AddRange(part.Triangles.Select(triangleIndex => triangleOffset + triangleIndex));
                triangleOffset += part.Positions.Count;
            }

            return combinedPart;
        }
    }
}