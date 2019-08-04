using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class ReplaceInsensitiveShould
    {
        [TestCase(null)]
        [TestCase("")]
        public void ReturnOriginalStringWhenGivenNullOrEmptyString(string nullOrEmptyString)
        {
            string result = nullOrEmptyString.ReplaceInsensitive("foo", "bar");

            Assert.AreEqual(nullOrEmptyString, result, "Oriignal string should've been returned.");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ReturnOriginalStringWhenGivenNullOrEmptyValueToReplace(string nullOrEmptyValueToReplace)
        {
            const string originalValue = "foo";

            string result = originalValue.ReplaceInsensitive(nullOrEmptyValueToReplace, "bar");

            Assert.AreEqual(originalValue, result, "Oriignal string should've been returned.");
        }

        [Test]
        public void ReturnOriginalStringWhenGivenNullReplacementValue()
        {
            const string originalValue = "foo";

            string result = originalValue.ReplaceInsensitive("foo", null);

            Assert.AreEqual(originalValue, result, "Oriignal string should've been returned.");
        }

        [Test]
        public void ReplaceExactStringMatch()
        {
            const string valueToReplace = "foo";
            const string newValue = "newValue";
            const string originalValueFormat = "{0}bar";
            string originalValue = string.Format(originalValueFormat, valueToReplace);
            string expected = string.Format(originalValueFormat, newValue);
            
            string result = originalValue.ReplaceInsensitive(valueToReplace, newValue);

            Assert.AreEqual(expected, result, "Value should have been replaced.");
        }

        [TestCase("http://")]
        [TestCase("HTTP://")]
        [TestCase("HttP://")]
        [TestCase("htTP://")]
        public void ReplaceStringRegardlessOfCasing(string originalValue)
        {
            const string valueToReplace = "http://";
            const string newValue = "https://";

            string result = originalValue.ReplaceInsensitive(valueToReplace, newValue);

            Assert.AreEqual(newValue, result, "Old value should have been replaced.");
        }
    }
}