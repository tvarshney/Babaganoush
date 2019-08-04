using Babaganoush.Sitefinity.Extensions;
using NUnit.Framework;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Tests.Unit.Sitefinity.Extensions.CotentExtensionsTests
{
    [TestFixture]
    internal class TrySetValueShould
    {
        [Test]
        public void NotThrowWhenGivenNullItem()
        {
            IDynamicFieldsContainer nullItem = null;

            TestDelegate trySetValueCall = () => nullItem.TrySetValue("Foo", "Bar");

            Assert.DoesNotThrow(trySetValueCall);
        }

        [Test]
        public void NotThrowWhenGivenFieldThatDoesNotExistToSet()
        {
            var item = new DummyDynamicFieldsContainer();

            TestDelegate trySetValueCall = () => item.TrySetValue(DummyDynamicFieldsContainer.NonexistentFieldName, "foo");

            Assert.DoesNotThrow(trySetValueCall);
        }

        // TODO: Update this test when exceptions are supposed to be logged.
        [Test]
        public void NotThrowWhenSettingFieldThatThrows()
        {
            var item = new DummyDynamicFieldsContainer();

            TestDelegate trySetValueCall = () => item.TrySetValue(DummyDynamicFieldsContainer.FieldThatThrowsName, "foo");

            Assert.DoesNotThrow(trySetValueCall);
        }

        [Test]
        public void SetsValueOfValidField()
        {
            const string expected = "my new value";
            var item = new DummyDynamicFieldsContainer();

            item.TrySetValue(DummyDynamicFieldsContainer.StringFieldName, expected);
            
            Assert.AreEqual(expected, item.String);
        }
    }
}