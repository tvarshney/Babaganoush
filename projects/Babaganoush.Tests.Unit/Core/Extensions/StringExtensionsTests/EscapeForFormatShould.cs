using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class EscapeForFormatShould
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ReturnSameStringWhenItIsNullOrWhiteSpace(string nullOrWhiteSpaceString)
        {
            string result = nullOrWhiteSpaceString.EscapeForFormat();

            Assert.AreEqual(nullOrWhiteSpaceString, result, "Exact string should have been returned.");
        }

        [Test]
        public void EscapeCurlyBraces()
        {
            const string expectedResult = "My }} example str{{ing.";
            const string stringWithCurlyBraces = "My } example str{ing.";

            string result = stringWithCurlyBraces.EscapeForFormat();

            Assert.AreEqual(expectedResult, result, "Curly braces should have been escaped.");
        }

        [Test]
        public void NotEscapePlaceholderCurlyBraces()
        {
            const string stringWithPlaceholders = "Here's my {0} placeholders! {1} {93}";

            string result = stringWithPlaceholders.EscapeForFormat();

            Assert.AreEqual(stringWithPlaceholders, result, "Placeholders should not have been escaped.");
        }
    }
}