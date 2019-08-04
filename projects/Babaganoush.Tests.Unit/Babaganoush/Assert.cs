using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Babaganoush
{
    /// <summary>
    /// Static methods that throw the same exceptions that NUnit <see cref="NUnit.Framework.Assert"/> methods throw when the assertions fail.
    /// </summary>
    internal static class Assert
    {
        /// <summary>
        /// Fails when the given list is null or contains any items.
        /// </summary>
        internal static void IsEmptyAndNotNull<T>(IList<T> list)
        {
            if (list == null || list.Any())
            {
                throw new AssertionException("The given list was supposed to be an empty, non-null list.");
            }
        }
    }
}