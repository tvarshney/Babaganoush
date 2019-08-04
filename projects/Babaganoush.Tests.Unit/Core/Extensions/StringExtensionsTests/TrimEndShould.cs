using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class TrimEndShould
    {
        [TestCase(null)]
        [TestCase("")]
        public void ReturnNullOrEmptyResultWhenGivenNullOrEmptyValueToTrim(string nullOrEmptyValue)
        {
            string result = nullOrEmptyValue.TrimEnd("trim");

            Assert.AreEqual(nullOrEmptyValue, result, "Original value should be returned when original is null or empty.");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ReturnOriginalValueWhenStringToTrimIsNullOrEmpty(string stringToTrim)
        {
            const string expected = "expected";

            string result = expected.TrimEnd(stringToTrim);

            Assert.AreEqual(expected, result, "Original string should be returned when string to trim is null or empty.");
        }

        [Test]
        public void RemoveOccurrenceOfStringToTrim()
        {
            const string expected = "pagetitle";
            const string stringToTrim = "controller";
            const string value = expected + stringToTrim;

            string result = value.TrimEnd(stringToTrim);
            
            Assert.AreEqual(expected, result, "String was not trimmed.");
        }

        [Test]
        public void RemoveMultipleOccurrencesOfStringToTrim()
        {
            const string expected = "pagetitle";
            const string stringToTrim = "controller";
            const string value = expected + stringToTrim + stringToTrim + stringToTrim + stringToTrim;

            string result = value.TrimEnd(stringToTrim);

            Assert.AreEqual(expected, result, "String was not trimmed.");
        }
    }
}