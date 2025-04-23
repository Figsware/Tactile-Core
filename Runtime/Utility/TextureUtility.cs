using System.IO;
using UnityEngine;

namespace Tactile.Core.Utility
{
    public static class TextureUtility
    {
        public static Texture2D TextureFromFile(string path)
        {
            Texture2D tex = null;

            if (File.Exists(path))
            {
                var data = File.ReadAllBytes(path);
                tex = new Texture2D(1, 1, TextureFormat.RGBA32, false)
                {
                    filterMode = FilterMode.Point
                };
                tex.LoadImage(data);
            }

            return tex;
        }
    }
}