using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class IsExternalUrlShould
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ReturnFalseForNullOrWhiteSpace(string nullOrWhiteSpaceString)
        {
            bool result = nullOrWhiteSpaceString.IsExternalUrl();

            Assert.IsFalse(result);
        }

        [TestCase("http")]
        [TestCase("HTTP")]
        [TestCase("Http")]
        [TestCase("HtTp")]
        public void ReturnTrueWhenStringStartsWithHTTP(string httpPrefix)
        {
            string stringToTest = httpPrefix + "://example.com/";
            
            bool result = stringToTest.IsExternalUrl();

            Assert.IsTrue(result, "String starting with HTTP should return true.");
        }

        [TestCase("https")]
        [TestCase("HTTPS")]
        [TestCase("Https")]
        [TestCase("HtTpS")]
        public void ReturnTrueWhenStringStartsWithHTTPS(string httpsPrefix)
        {
            string stringToTest = httpsPrefix + "://foobar";
            
            bool result = stringToTest.IsExternalUrl();

            Assert.IsTrue(result, "String starting with HTTPS should return true.");
        }

        [TestCase("www.example.com")]
        [TestCase("foobar")]
        [TestCase("123")]
        [TestCase("../file.txt")]
        [TestCase("C:\\Foo\\Bar.htm")]
        public void ReturnFalseWhenStringDoesNotStartWithURLPrefix(string stringWithoutUrlPrefix)
        {
            bool result = stringWithoutUrlPrefix.IsExternalUrl();

            Assert.IsFalse(result, "String {0} should not be considered an external URL.", stringWithoutUrlPrefix);
        }
    }
}