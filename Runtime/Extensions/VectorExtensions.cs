using UnityEngine;

namespace Tactile.Core.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 Rotate(this Vector2 vec, float angle)
        {
            var rotatedVec = new Vector2(
                vec.x * Mathf.Cos(angle) - vec.y * Mathf.Sin(angle),
                vec.x * Mathf.Sin(angle) + vec.y * Mathf.Cos(angle)
            );

            return rotatedVec;
        }

        public static UIVertex ToSimpleVert(this Vector2 vec, Color color)
        {
            UIVertex uiVert = UIVertex.simpleVert;
            uiVert.position = vec;
            uiVert.color = color;

            return uiVert;
        }

        public static Vector2 Center(this Vector2[] vecs)
        {
            float avgX = 0f;
            float avgY = 0f;

            foreach (var vec in vecs)
            {
                avgX += (vec.x - avgX) / vecs.Length;
                avgY += (vec.y - avgY) / vecs.Length;
            }

            var avgVec = new Vector2(avgX, avgY);

            return avgVec;
        }

        public static Vector2 ToXY(this Vector3 vec)
        {
            var xy = new Vector2(vec.x, vec.y);
            return xy;
        }

        public static Vector2 ToXZ(this Vector3 vec)
        {
            var xy = new Vector2(vec.x, vec.z);
            return xy;
        }

        public static Vector2 ToYZ(this Vector3 vec)
        {
            var xy = new Vector2(vec.y, vec.z);
            return xy;
        }
    }
}