using System;
using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.ObjectExtensionsTests
{
    [TestFixture]
    internal class ChangeTypeToShould
    {
        [Test]
        public void ThrowInvalidCastExceptionWhenAttemptingToCastStringToInt()
        {
            const string originalValue = "hello";

            TestDelegate methodThatShouldThrow = () => originalValue.ChangeTypeTo<int>();

            Assert.Throws<InvalidCastException>(methodThatShouldThrow);
        }

        [TestCase("true")]
        [TestCase("True")]
        [TestCase("false")]
        [TestCase("False")]
        public void ReturnTrueFalseStringAsBoolean(string trueFalseString)
        {
            bool expected = bool.Parse(trueFalseString);

            bool value = trueFalseString.ChangeTypeTo<bool>();

            Assert.AreEqual(expected, value);
        }

        [Test]
        public void ThrowWhenTryingToChangeNonBoolStringIntoBool()
        {
            TestDelegate changeCall = () => "foobar".ChangeTypeTo<bool>();

            Assert.Throws<InvalidCastException>(changeCall);
        }

        [Test]
        public void ReturnValueOfIntegerElementAsInteger()
        {
            const int expectedValue = 5;

            int value = expectedValue.ChangeTypeTo<int>();

            Assert.AreEqual(expectedValue, value, "Expected integer value was not returned from element.");
        }

        [TestCase(5)]
        [TestCase(null)]
        public void ReturnValueOfIntegerElementAsNullableInteger(int? expectedValue)
        {
            int? value = expectedValue.ChangeTypeTo<int?>();

            Assert.AreEqual(expectedValue, value, "Expected nullable integer value was not returned from element.");
        }

        [Test]
        public void ReturnValueOfBooleanElementAsBool()
        {
            const bool expectedValue = true;

            bool value = expectedValue.ChangeTypeTo<bool>();

            Assert.AreEqual(expectedValue, value, "Expected boolean value was not returned from element.");
        }

        [Test]
        public void ReturnValueOfStringElementAsString()
        {
            const string expectedValue = "expected value";

            string value = expectedValue.ChangeTypeTo<string>();

            Assert.AreEqual(expectedValue, value, "Expected string value was not returned from element.");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ReturnNullableIntWhenGivenNullOrEmptyString(string nullOrEmptyString)
        {
            int? value = nullOrEmptyString.ChangeTypeTo<int?>();

            Assert.IsNull(value, "Null int? should have been returned.");
        }

        [Test]
        public void ReturnSameObjectWhenSourceAndDestinationTypesMatch()
        {
            const string value = "some value";

            string result = value.ChangeTypeTo<string>();

            Assert.AreEqual(value, result, "Same string should have been returned.");
        }
    }
}