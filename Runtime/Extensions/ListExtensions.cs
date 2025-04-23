using System;
using System.Collections.Generic;

namespace Tactile.Core.Extensions
{
    public static class ListExtensions
    {
        public static void MapInPlace<T>(this IList<T> items, Func<T, T> mapper)
        {
            MapInPlace(items, (item, _) => mapper(item));
        }
        
        public static void MapInPlace<T>(this IList<T> items, Func<T, int, T> mapper)
        {
            for (var i = 0; i < items.Count; i++)
            {
                items[i] = mapper(items[i], i);
            }
        }

        public static void MapSliceInPlace<T>(this IList<T> items, int stride, Action<T[]> mapper)
        {
            MapSliceInPlace(items, stride, (slice, _) => mapper(slice));
        }

        public static void MapSliceInPlace<T>(this IList<T> items, int stride, Action<T[], int> mapper)
        {
            var slice = new T[stride];
            for (var i = 0; i < items.Count; i += stride)
            {
                // Copy to slice.
                for (var j = 0; j < stride; j++)
                {
                    slice[j] = items[i + j];
                }

                mapper(slice, i);
                
                // Copy from slice.
                for (var j = 0; j < stride; j++)
                {
                    items[i + j] = slice[j];
                }
            }
        }
    }
}