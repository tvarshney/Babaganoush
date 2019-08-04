using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class FirstLetterToUpperShould
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ReturnNullOrWhiteSpaceWhenGivenNullOrWhiteSpaceString(string nullOrWhiteSpace)
        {
            string result = nullOrWhiteSpace.FirstLetterToUpper();

            Assert.AreEqual(nullOrWhiteSpace, result, "Result should not be mutated from original when original is null or empty.");
        }

        [TestCase("a")]
        [TestCase("b")]
        [TestCase("k")]
        [TestCase("y")]
        [TestCase("z")]
        public void CaptializeSingleLetter(string letter)
        {
            string expected = letter.ToUpper();

            string result = letter.FirstLetterToUpper();

            Assert.AreEqual(expected, result, "Single letter should be capitalized.");
        }

        [Test]
        public void OnlyCapitalizeFirstLetterInString()
        {
            const string value = "my first example";
            const string expected = "My first example";

            string result = value.FirstLetterToUpper();

            Assert.AreEqual(result, expected, "Only the first letter should have been capitalized.");
        }

        [TestCase("Example string")]
        [TestCase("EXAMPLE STRING")]
        [TestCase("Example STRING")]
        public void NotMutateStringsAlreadyStartingWithCapitalLetter(string alreadyCapitalized)
        {
            string result = alreadyCapitalized.FirstLetterToUpper();

            Assert.AreEqual(alreadyCapitalized, result, "String was already capitalized. It should have not been mutated.");
        }
    }
}