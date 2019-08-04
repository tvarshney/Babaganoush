using System;
using System.Linq;
using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class SplitShould
    {
        [Test]
        public void ReturnEmptyArrayWhenGivenNullValue()
        {
            string[] result = StringExtensions.Split(null, ",", StringSplitOptions.None);
            
            Assert.IsNotNull(result, "Non-null array should have been returned.");
            Assert.IsEmpty(result, "Empty array should have been returned.");
        }

        [Test]
        public void SplitGivenStringByGivenDelimiter()
        {
            const string expectedSubstring1 = "foo";
            const string expectedSubstring2 = "bar";
            string stringToSplit = string.Format("{0},{1}", expectedSubstring1, expectedSubstring2);
            
            string[] strings = stringToSplit.Split(",", StringSplitOptions.None);

            Assert.AreEqual(expectedSubstring1, strings[0], "The substring '{0}' was not returned.", expectedSubstring1);
            Assert.AreEqual(expectedSubstring2, strings[1], "The substring '{0}' was not returned.", expectedSubstring1);
        }

        [Test]
        public void RemoveEmptyEntriesWhenGivenTheOption()
        {
            string[] strings = " ,,   ,foo,,bar,,buzz, ,,".Split(",", StringSplitOptions.RemoveEmptyEntries);

            Assert.IsFalse(strings.Any(string.IsNullOrEmpty), "Empty entries were included in result.");
        }
    }
}