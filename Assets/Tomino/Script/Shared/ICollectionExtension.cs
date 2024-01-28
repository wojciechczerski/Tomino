using System;
using System.Collections.Generic;

namespace Tomino.Shared
{
    public static class CollectionExtension
    {
        public static T FindFirst<T>(this IEnumerable<T> collection, Func<T, bool> condition)
        {
            foreach (var element in collection)
            {
                if (condition(element))
                {
                    return element;
                }
            }
            return default;
        }

        public static T[] First<T>(this IEnumerable<T> collection, int count)
        {
            var result = new T[count];
            var index = 0;
            foreach (var element in collection)
            {
                if (index >= count)
                {
                    break;
                }

                result[index++] = element;
            }
            return result;
        }

        private static void Iterate<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            var index = 0;
            foreach (var element in collection)
            {
                action(element, index++);
            }
        }

        public static TU[] Map<T, TU>(this ICollection<T> collection, Func<T, TU> map)
        {
            var result = new TU[collection.Count];
            collection.Iterate((element, index) => result[index] = map(element));
            return result;
        }

        public static T Min<T>(this ICollection<T> collection) where T : IComparable
        {
            return collection.CompareAll((a, b) => a.CompareTo(b) < 0);
        }

        public static T Max<T>(this ICollection<T> collection) where T : IComparable
        {
            return collection.CompareAll((a, b) => a.CompareTo(b) > 0);
        }

        private static T CompareAll<T>(this IEnumerable<T> collection, Func<T, T, bool> compare) where T : IComparable
        {
            T result = default;
            var hasValue = false;
            foreach (var element in collection)
            {
                if (hasValue && !compare(element, result)) continue;

                result = element;
                hasValue = true;
            }
            return result;
        }
    }
}
