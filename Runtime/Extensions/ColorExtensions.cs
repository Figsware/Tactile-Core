using UnityEngine;

namespace Tactile.Core.Extensions
{
    public static class ColorExtensions
    {
        public static string ToHex(this Color color)
        {
            return $"#{ColorUtility.ToHtmlStringRGB(color)}";
        }

        public static Color WithAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
        
        public static TextColor TextColorForBackgroundColor(this Color backgroundColor)
        {
            var r = backgroundColor.r;
            var g = backgroundColor.g;
            var b = backgroundColor.b;
            var useDarkColor = r * 0.299f + g * 0.587f + b * 0.114f > 186;
            return useDarkColor ? TextColor.Dark : TextColor.Light;
        }

        public static Texture2D ToSolidColorTexture(this Color color)
        {
            var tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, color);
            tex.Apply();
            
            return tex;
        }

        public enum TextColor
        {
            Dark,
            Light
        }
    }
}