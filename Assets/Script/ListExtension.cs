using System;
using System.Collections.Generic;

namespace Tomino
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

        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static T TakeFirst<T>(this IList<T> list)
        {
            var value = list[0];
            list.RemoveAt(0);
            return value;
        }

        public static void Add<T>(this IList<T> list, T value, int numDuplicates)
        {
            for (int n = 0; n < numDuplicates; n++)
            {
                list.Add(value);
            }
        }
    }
};