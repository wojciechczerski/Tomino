using System;
using System.Collections;
using System.Collections.Generic;

namespace Tomino
{
    public static class ICollectionExtension
    {
        public static T FindFirst<T>(this ICollection<T> collection, Func<T, bool> condition)
        {
            foreach (var element in collection)
            {
                if (condition(element)) return element;
            }
            return default(T);
        }

        public static T[] First<T>(this ICollection<T> collection, int count)
        {
            var result = new T[count];
            var index = 0;
            foreach (var element in collection)
            {
                if (index >= count) break;
                result[index++] = element;
            }
            return result;
        }

        public static void Iterate<T>(this ICollection<T> collection, Action<T, int> action)
        {
            var index = 0;
            foreach (var element in collection) action(element, index++);
        }

        public static U[] Map<T, U>(this ICollection<T> collection, Func<T, U> map)
        {
            var result = new U[collection.Count];
            collection.Iterate((element, index) => result[index] = map(element));
            return result;
        }
    }
}
