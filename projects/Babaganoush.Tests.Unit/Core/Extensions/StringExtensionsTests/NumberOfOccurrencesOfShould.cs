using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class NumberOfOccurrencesOfShould
    {
        [TestCase(null)]
        [TestCase("")]
        public void Return0ForNullOrEmptyString(string nullOrEmptyString)
        {
            const int expected = 0;

            int result = nullOrEmptyString.NumberOfOccurrencesOf('a');

            Assert.AreEqual(expected, result, "No occurrences should be found in a null or empty string.");
        }

        [Test]
        public void Return0ForStringWithNoOccurrencesOfCharacter()
        {
            const int expected = 0;
            const string stringToCount = "foo";

            int result = stringToCount.NumberOfOccurrencesOf('a');

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReturnCountOfWhiteSpaceCharacter()
        {
            const int expected = 3;
            const string stringToCount = "   ";

            int result = stringToCount.NumberOfOccurrencesOf(' ');

            Assert.AreEqual(expected, result, "White space characters should be countable.");
        }

        [Test]
        public void ReturnCorrectCountOfGivenCharacter()
        {
            const int expected = 4;
            const string stringToCount = "abcdabcdabcdabcd";

            int result = stringToCount.NumberOfOccurrencesOf('a');

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DoesNotCountLowercaseVersionOfCapitalCharacter()
        {
            const int expected = 2;
            const string stringToCount = "AaA";

            int result = stringToCount.NumberOfOccurrencesOf('A');

            Assert.AreEqual(expected, result, "Counting occurrences should be case-sensitive.");
        }
    }
}