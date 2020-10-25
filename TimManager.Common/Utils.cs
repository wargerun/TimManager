using System;
using System.Collections.Generic;

namespace TimManager.Common
{
    public static class Utils
    {
        public static bool Has(this string str) => !string.IsNullOrWhiteSpace(str);

        public static bool Has(this object str) => str != null;

        public static bool Has<T>(this ICollection<T> collection) => collection.Count > 0;

        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            return collection.Foreach((item, _) => action(item));
        }

        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            int indexCounter = 0;

            foreach (var item in collection)
            {
                action(item, indexCounter++);
            }

            return collection;
        }
    }
}
