using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class FindLineContainingStringShould
    {
        [TestCase(null)]
        [TestCase("")]
        public void ReturnNullWhenGivenNullOrEmptyValue(string value)
        {
            string result = value.FindLineContainingString("foo");

            Assert.IsNull(result, "Null result should have been returned.");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ReturnNullWhenGivenNullOrEmptySearchString(string stringToSearchFor)
        {
            const string value = "foobar";

            string result = value.FindLineContainingString(stringToSearchFor);

            Assert.IsNull(result, "Null result should have been returned.");
        }

        [Test]
        public void ReturnFirstLineWithSearchString()
        {
            const string searchString = "bar";
            string value = string.Format("foo\n{0}1\nfoo\n{0}2", searchString);

            string result = value.FindLineContainingString(searchString);

            Assert.AreEqual(searchString + "1\n", result, "The first line containing the search string should have been returned.");
        }

        [Test]
        public void ReturnNullWhenSearchStringNotFound()
        {
            const string value = "foo\nbar\nfizz\nbuzz\n";

            string result = value.FindLineContainingString("widget");

            Assert.IsNull(result, "Null string should have been returned.");
        }
    }
}