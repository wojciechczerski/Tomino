using System;
using System.Collections.Generic;

namespace Tomino.Shared
{
    public static class ListExtension
    {
        public static void Shuffle<T>(this IList<T> list, Random rnd)
        {
            for (var i = 0; i < list.Count - 1; i++)
            {
                list.Swap(i, rnd.Next(i, list.Count));
            }
        }

        private static void Swap<T>(this IList<T> list, int i, int j)
        {
            (list[j], list[i]) = (list[i], list[j]);
        }

        public static T TakeFirst<T>(this IList<T> list)
        {
            var value = list[0];
            list.RemoveAt(0);
            return value;
        }

        public static void Add<T>(this IList<T> list, T value, int numDuplicates)
        {
            for (var n = 0; n < numDuplicates; n++)
            {
                list.Add(value);
            }
        }
    }
}
