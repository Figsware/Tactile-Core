using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Tactile.Core.Utility;
using UnityEngine;

namespace Tactile.Core.Editor.Utility
{
    public static class EditorIcons
    {
        private static readonly Dictionary<object, Texture2D> Icons = new();

        public static Texture2D GetIconTexture<T>(T enumValue) where T : Enum
        {
            if (Icons.TryGetValue(enumValue, out var cachedTexture) && cachedTexture)
            {
                return cachedTexture;
            }

            var fi = typeof(T).GetField(enumValue.ToString());
            var iconPath = string.Empty;

            var editorIconGroup = typeof(T).GetCustomAttribute<EditorIconGroupAttribute>();
            if (editorIconGroup != null)
            {
                iconPath = editorIconGroup.Path;
            }

            var editorIcon = fi.GetCustomAttribute<EditorIconAttribute>();
            iconPath = Path.Join(iconPath, editorIcon != null ? editorIcon.Path : enumValue.ToString());

            var iconParentDirectory = Path.GetDirectoryName(iconPath);
            if (string.IsNullOrEmpty(iconParentDirectory))
            {
                return null;
            }

            var iconParentDirectoryAbsolutePath = Path.GetFullPath(iconParentDirectory);
            if (!Directory.Exists(iconParentDirectoryAbsolutePath))
            {
                return null;
            }

            var iconFileName = Path.GetFileName(iconPath);
            string resolvedIconFilePath = null;
            foreach (var file in Directory.GetFiles(iconParentDirectoryAbsolutePath))
            {
                var fileName = Path.GetFileName(file);
                if (fileName.StartsWith(iconFileName) && !fileName.EndsWith(".meta"))
                {
                    resolvedIconFilePath = file;
                    break;
                }
            }
            
            if (string.IsNullOrEmpty(resolvedIconFilePath))
            {
                return null;
            }
            
            var tex = TextureUtility.TextureFromFile(resolvedIconFilePath);
            Icons[enumValue] = tex;

            return tex;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class EditorIconAttribute : Attribute
    {
        public readonly string Path;

        public EditorIconAttribute(string path)
        {
            Path = path;
        }
    }

    [AttributeUsage(AttributeTargets.Enum)]
    public class EditorIconGroupAttribute : Attribute
    {
        public readonly string Path;

        public EditorIconGroupAttribute(string path, string packageName = null)
        {
            Path = !string.IsNullOrEmpty(packageName) ? $"Packages/{packageName}/{path}" : path;
        }
    }
}