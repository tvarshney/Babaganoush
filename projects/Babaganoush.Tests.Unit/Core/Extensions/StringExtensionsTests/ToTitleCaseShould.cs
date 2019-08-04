using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class ToTitleCaseShould
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ReturnNullorWhiteSpaceWhenGivenNullOrWhiteSpace(string nullOrWhiteSpace)
        {
            string result = nullOrWhiteSpace.ToTitleCase();

            Assert.AreEqual(nullOrWhiteSpace, result, "Null or white space should be returned when given null or white space string.");
        }

        [Test]
        public void ReturnAllLowercaseStringAsTitleCase()
        {
            const string value = "the title";
            const string expected = "The Title";

            string result = value.ToTitleCase();

            Assert.AreEqual(expected, result, "Given all-lowercase value should have been title-cased.");
        }

        [Test]
        public void ReturnAllCapsStringAsTitleCase()
        {
            const string value = "MY GREAT TITLE";
            const string expected = "My Great Title";

            string result = value.ToTitleCase();

            Assert.AreEqual(expected, result, "Given all-caps value should have been title-cased.");
        }
    }
}