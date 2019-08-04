using System;
using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class ScrubDelimitedFieldShould
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ReturnOriginalValueWhenGivenNullOrWhiteSpace(string nullOrWhiteSpaceValue)
        {
            string result = nullOrWhiteSpaceValue.ScrubDelimitedField();

            Assert.AreEqual(nullOrWhiteSpaceValue, result);
        }

        [Test]
        public void ReplaceEnvironmentNewLineWithSpace()
        {
            const string expected = "Foo Bar";
            string value = string.Format("Foo{0}Bar", Environment.NewLine);

            string result = value.ScrubDelimitedField();

            Assert.AreEqual(expected, result, "Environment.NewLine values should have been replaced.");
        }

        [Test]
        public void ReplaceNewLineCharacterWithSpace()
        {
            const string expected = "Foo Bar";
            string value = string.Format("Foo{0}Bar", "\n");

            string result = value.ScrubDelimitedField();

            Assert.AreEqual(expected, result, "'\n' values should have been replaced.");
        }

        [Test]
        public void ReplaceSpecifiedDelimiterWithSpecifiedReplacementValue()
        {
            const string delimiter = "asdf";
            const string replacement = "jjjj";
            string expected = string.Format("Foo{0}Bar", replacement);
            string value = string.Format("Foo{0}Bar", delimiter);

            string result = value.ScrubDelimitedField(delimiter, replacement);

            Assert.AreEqual(expected, result, "Specified delimited values should be replaced by specified replacement string.");
        }
    }
}