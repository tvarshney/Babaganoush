using System;
using System.Collections.Generic;
using System.Linq;

namespace Babaganoush.Core.Extensions
{
    /// <summary>
    /// Extension methods for collection objects.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Shuffles the contents of the given <paramref name="list"/> in-place.
        /// </summary>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="list">The list to act on.</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            if (list == null)
            {
                return;
            }

            var rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Determines whether the given enumerable is null or empty.
        /// </summary>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="value">The enumerable to test.</param>
        ///
        /// <returns>
        /// True if the enumerable is null or empty; false otherwise.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return value == null || !value.Any();
        }
    }
}