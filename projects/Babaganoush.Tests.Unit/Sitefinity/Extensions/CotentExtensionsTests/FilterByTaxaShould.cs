using System;
using System.Collections.Generic;
using System.Linq;
using Babaganoush.Sitefinity.Extensions;
using NUnit.Framework;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Tests.Unit.Sitefinity.Extensions.CotentExtensionsTests
{
    [TestFixture]
    internal class FilterByTaxaShould
    {
        [Test]
        public void ReturnNullWhenGivenNullItems()
        {
            List<IDynamicFieldsContainer> items = null;

            IEnumerable<IDynamicFieldsContainer> result = items.FilterByTaxa(DummyDynamicFieldsContainer.TaxaFieldName, new List<Guid>());

            Assert.IsNull(result);
        }

        [Test]
        public void ReturnEmptySetWhenGivenEmptySetOfItems()
        {
            var items = new List<IDynamicFieldsContainer>();

            IEnumerable<IDynamicFieldsContainer> result = items.FilterByTaxa(DummyDynamicFieldsContainer.TaxaFieldName, new List<Guid>());

            Assert.IsNotNull(result);
            Assert.IsEmpty(result, "Empty collection should have been returned.");
        }

        [Test]
        public void FilterNoItemsWhenGivenEmptyIdSetAndLeaveFilterBoolUnchanged()
        {
            var items = new List<IDynamicFieldsContainer> { new DummyDynamicFieldsContainer(), new DummyDynamicFieldsContainer()};

            foreach (List<Guid> emptyIdSet in new[] {null, new List<Guid>()})
            {
                IEnumerable<IDynamicFieldsContainer> result = items.FilterByTaxa(DummyDynamicFieldsContainer.TaxaFieldName, emptyIdSet);
                Assert.AreEqual(items.Count, result.Count(), "Same number of items should have been returned when given {0} ID set.", emptyIdSet == null ? "null" : "empty");
            }
        }

        [Test]
        public void FilterNoItemsWhenGivenNullIdSetAndSetFilterBoolToTrue()
        {
            var items = new List<IDynamicFieldsContainer> { new DummyDynamicFieldsContainer(), new DummyDynamicFieldsContainer() };

            IEnumerable<IDynamicFieldsContainer> result = items.FilterByTaxa(DummyDynamicFieldsContainer.TaxaFieldName, null, true);

            Assert.AreEqual(items.Count, result.Count(), "Same number of items should have been returned when given null ID set.");
        }

        [Test]
        public void FilterAllItemsWhenGivenEmptyIdSetAndSetFilterBoolToTrue()
        {
            var items = new List<IDynamicFieldsContainer> { new DummyDynamicFieldsContainer(), new DummyDynamicFieldsContainer() };

            IEnumerable<IDynamicFieldsContainer> result = items.FilterByTaxa(DummyDynamicFieldsContainer.TaxaFieldName, new List<Guid>(), true);
            
            Assert.IsEmpty(result, "Empty result set should have been returned when given empty ID set.");
        }

        [Test]
        public void FilterOutItemsNotMatchingSingleId()
        {
            Guid idToMatch = Guid.NewGuid();
            var expectedItem = new DummyDynamicFieldsContainer(idToMatch);
            var items = new List<IDynamicFieldsContainer> { new DummyDynamicFieldsContainer(), expectedItem, new DummyDynamicFieldsContainer() };

            List<IDynamicFieldsContainer> result = items.FilterByTaxa(DummyDynamicFieldsContainer.TaxaFieldName, new[] {idToMatch}).ToList();

            Assert.AreEqual(1, result.Count, "Only one item should have matched the given filter.");
            Assert.AreEqual(expectedItem, result.Single(), "Incorrect item returned.");
        }

        [Test]
        public void FilterOutItemsNotMatchAnyGivenId()
        {
            Guid idToMatch1 = Guid.NewGuid();
            Guid idToMatch2 = Guid.NewGuid();
            var expectedItem1 = new DummyDynamicFieldsContainer(idToMatch1);
            var expectedItem2 = new DummyDynamicFieldsContainer(idToMatch2);
            var expectedItem3 = new DummyDynamicFieldsContainer(idToMatch1, idToMatch2);
            var items = new List<IDynamicFieldsContainer> { expectedItem1, new DummyDynamicFieldsContainer(), expectedItem2, expectedItem3, new DummyDynamicFieldsContainer() };

            List<IDynamicFieldsContainer> result = items.FilterByTaxa(DummyDynamicFieldsContainer.TaxaFieldName, new[] { idToMatch1, idToMatch2 }).ToList();

            Assert.AreEqual(3, result.Count, "Three items should have matched the given filter.");
            Assert.Contains(expectedItem1, result, "Resulting set should have contained item with first ID.");
            Assert.Contains(expectedItem2, result, "Resulting set should have contained item with second ID.");
            Assert.Contains(expectedItem3, result, "Resulting set should have contained item with both IDs.");
        }
    }
}