using Tactile.Core.Utility;
using UnityEngine;

namespace Tactile.Core.Extensions
{
    public static class RectExtensions
    {
        /// <summary>
        /// Adds a specified amount of padding to the rectangle
        /// </summary>
        /// <param name="rect">The rectangle to modify</param>
        /// <param name="top">How much padding to apply on top</param>
        /// <param name="bottom">How much padding to apply on the bottom</param>
        /// <param name="left">How much padding to apply on the left</param>
        /// <param name="right">How much padding to apply on the right</param>
        /// <returns>A new rectangle with padding</returns>
        public static Rect Padding(this Rect rect, float top, float bottom, float left, float right)
        {
            var width = rect.width - (left + right);
            var height = rect.height - (top + bottom);
            var x = rect.x + left;
            var y = rect.y + top;
            var newRect = new Rect(x, y, width, height);

            return newRect;
        }

        public static Rect[] Layout(this Rect rect, RectLayout.LayoutDirection direction, float gap,
            params RectLayout[] layouts) =>
            RectLayout.Layout(rect, direction, gap, layouts);
        
        public static Rect[] Layout(this Rect rect, RectLayout.LayoutDirection direction,
            params RectLayout[] layouts) =>
            RectLayout.Layout(rect, direction, RectLayout.DefaultGap, layouts);

        public static Rect[] HorizontalLayout(this Rect rect, float gap,
            params RectLayout[] layouts) =>
            RectLayout.Layout(rect, RectLayout.LayoutDirection.Horizontal, gap, layouts);
        
        public static Rect[] HorizontalLayout(this Rect rect,
            params RectLayout[] layouts) =>
            RectLayout.Layout(rect, RectLayout.LayoutDirection.Horizontal, RectLayout.DefaultGap, layouts);

        public static Rect[] VerticalLayout(this Rect rect, float gap,
            params RectLayout[] layouts) =>
            RectLayout.Layout(rect, RectLayout.LayoutDirection.Vertical, gap, layouts);
        
        public static Rect[] VerticalLayout(this Rect rect,
            params RectLayout[] layouts) =>
            RectLayout.Layout(rect, RectLayout.LayoutDirection.Vertical, RectLayout.DefaultGap, layouts);

        /// <summary>
        /// Applies padding uniformly throughout the rectangle.
        /// </summary>
        /// <param name="rect">The rectangle to add padding to</param>
        /// <param name="padding">The amount of padding to add throughout the rectangle.</param>
        /// <returns>A new rectangle with padding</returns>
        public static Rect Padding(this Rect rect, float padding) => Padding(rect, padding, padding, padding, padding);
        public static Rect PaddingTop(this Rect rect, float padding) => Padding(rect, padding, 0, 0, 0);
        public static Rect PaddingBottom(this Rect rect, float padding) => Padding(rect, 0, padding, 0, 0);
        public static Rect PaddingLeft(this Rect rect, float padding) => Padding(rect, 0, 0, padding, 0);
        public static Rect PaddingRight(this Rect rect, float padding) => Padding(rect, 0, 0, 0, padding);
        public static Rect PaddingVertical(this Rect rect, float padding) => Padding(rect, padding, padding, 0, 0);
        public static Rect PaddingHorizontal(this Rect rect, float padding) => Padding(rect, 0, 0, padding, padding);

        public static float GetArea(this Rect rect) => rect.width * rect.height;
        public static float GetPerimeter(this Rect rect) => 2 * (rect.width + rect.height);
    }
}