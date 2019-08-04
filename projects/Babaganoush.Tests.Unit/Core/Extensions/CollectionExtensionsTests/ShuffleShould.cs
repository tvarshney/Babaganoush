using System.Collections.Generic;
using System.Linq;
using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.CollectionExtensionsTests
{
    [TestFixture]
    internal class ShuffleShould
    {
        [Test]
        public void NotThrowWhenGivenNullList()
        {
            TestDelegate shuffleCall = () => ((IList<int>)null).Shuffle();

            Assert.DoesNotThrow(shuffleCall);
        }

        [Test]
        public void NotThrowWhenGivenEmptyList()
        {
            IList<int> emptyList = new List<int>();

            TestDelegate shuffleCall = emptyList.Shuffle;

            Assert.DoesNotThrow(shuffleCall);
        }

        [Test]
        public void NotMutateListWithSingleItem()
        {
            const int originalItem = 5;
            IList<int> listWithOneItem = new List<int> {originalItem};

            listWithOneItem.Shuffle();

            Assert.AreEqual(1, listWithOneItem.Count, "List should still have only one item.");
            Assert.AreEqual(originalItem, listWithOneItem.Single(), "List should still have its original item.");
        }
    }
}