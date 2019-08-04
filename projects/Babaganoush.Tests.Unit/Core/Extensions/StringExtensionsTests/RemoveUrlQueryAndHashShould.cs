using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class RemoveUrlQueryAndHashShould
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ReturnEmptyWhenGivenNullorEmptyValue(string nullOrEmptyValue)
        {
            string result = nullOrEmptyValue.RemoveUrlQueryAndHash();

            Assert.IsEmpty(result, "Empty string should be returned when given null or empty value.");
        }

        [Test]
        public void ReturnURLWithoutQuery()
        {
            const string expectedUrl = "http://www.example.com/foo.html";
            const string originalUrl = expectedUrl + "?bar=5";

            string result = originalUrl.RemoveUrlQueryAndHash();

            Assert.AreEqual(expectedUrl, result, "Query should be removed from resulting URL.");
        }

        [Test]
        public void ReturnURLWithoutHash()
        {
            const string expectedUrl = "http://www.example.com/foo.html";
            const string originalUrl = expectedUrl + "#top";

            string result = originalUrl.RemoveUrlQueryAndHash();

            Assert.AreEqual(expectedUrl, result, "Hash should be removed from resulting URL.");
        }

        [Test]
        public void ReturnURLWithoutQueryAndHash()
        {
            const string expectedUrl = "http://www.example.com/foo.html";
            const string originalUrl = expectedUrl + "?bar=5#top";

            string result = originalUrl.RemoveUrlQueryAndHash();

            Assert.AreEqual(expectedUrl, result, "Query and hash should be removed from resulting URL.");
        }

        [Test]
        public void ReturnValueWhenGivenValueHasNoQueryOrHash()
        {
            const string expectedUrl = "http://www.example.com/foo.html";

            string result = expectedUrl.RemoveUrlQueryAndHash();

            Assert.AreEqual(expectedUrl, result, "Result should match original URL when there was no query or hash present.");
        }
    }
}