using UnityEngine;

namespace Tactile.Core.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Destroys all children of a Transform.
        /// </summary>
        /// <param name="transform">The transform whose children to destroy</param>
        public static void DestroyAllChildren(this Transform transform)
        {
            int totalChildren = transform.childCount;
            for (var i = 0; i < totalChildren; i++)
            {
                var child = transform.GetChild(i).gameObject;
                Object.Destroy(child);
            }
        }
    }
}