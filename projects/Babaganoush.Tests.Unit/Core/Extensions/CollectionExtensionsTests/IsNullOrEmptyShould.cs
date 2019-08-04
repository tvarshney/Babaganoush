using System.Collections.Generic;
using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.CollectionExtensionsTests
{
    [TestFixture]
    internal class IsNullOrEmptyShould
    {
        [Test]
        public void ReturnTrueForNullEnumerable()
        {
            bool isNullOrEmpty = ((List<int>) null).IsNullOrEmpty();

            Assert.IsTrue(isNullOrEmpty, "Null should cause IsNullOrEmpty to return true.");
        }

        [Test]
        public void ReturnTrueForEmptyEnumerable()
        {
            var emptyList = new List<string>();

            bool isNullOrEmpty = emptyList.IsNullOrEmpty();

            Assert.IsTrue(isNullOrEmpty, "Empty enumerable should cause IsNullOrEmpty to return true.");
        }

        [Test]
        public void ReturnFalseForNonEmptyCollection()
        {
            var nonEmptyArray = new[] {3, 4, 5};

            bool isNullOrEmpty = nonEmptyArray.IsNullOrEmpty();

            Assert.IsFalse(isNullOrEmpty, "Non-empty enumerable should cause IsNullOrEmpty to return false.");
        }
    }
}