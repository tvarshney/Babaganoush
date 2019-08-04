using System;
using Babaganoush.Sitefinity.Extensions;
using NUnit.Framework;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Tests.Unit.Sitefinity.Extensions.CotentExtensionsTests
{
    [TestFixture]
    internal class GetBooleanShould
    {
        [Test]
        public void ThrowWhenGivenNullItem()
        {
            IDynamicFieldsContainer nullContainer = null;

            TestDelegate getBooleanCall = () => nullContainer.GetBoolean(DummyDynamicFieldsContainer.BooleanFieldName);

            Assert.Throws<ArgumentNullException>(getBooleanCall);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ReturnFalseWhenGivenNullOrWhiteSpaceField(string nullOrEmptyFieldName)
        {
            IDynamicFieldsContainer container = new DummyDynamicFieldsContainer();

            bool result = container.GetBoolean(nullOrEmptyFieldName);

            Assert.IsFalse(result, "Null or empty field should always come back false.");
        }

        [Test]
        public void ReturnFalseWhenFieldDoesNotExist()
        {
            IDynamicFieldsContainer container = new DummyDynamicFieldsContainer();

            bool result = container.GetBoolean(DummyDynamicFieldsContainer.NonexistentFieldName);

            Assert.IsFalse(result, "False should be returned for field that doesn't exist.");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ReturnValueOfBooleanField(bool expectedValue)
        {
            IDynamicFieldsContainer container = new DummyDynamicFieldsContainer { Boolean = expectedValue};

            bool result = container.GetBoolean(DummyDynamicFieldsContainer.BooleanFieldName);

            Assert.AreEqual(expectedValue, result, "Boolean value was not returned.");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ReturnValueOfBooleanString(bool expectedValue)
        {
            IDynamicFieldsContainer container = new DummyDynamicFieldsContainer { String = expectedValue.ToString() };

            bool result = container.GetBoolean(DummyDynamicFieldsContainer.StringFieldName);

            Assert.AreEqual(expectedValue, result, "String boolean value was not returned.");
        }

        [Test]
        public void ReturnFalseWhenValueIsNull()
        {
            IDynamicFieldsContainer container = new DummyDynamicFieldsContainer { String = null };

            bool result = container.GetBoolean(DummyDynamicFieldsContainer.StringFieldName);

            Assert.IsFalse(result, "A null field should return false.");
        }
    }
}