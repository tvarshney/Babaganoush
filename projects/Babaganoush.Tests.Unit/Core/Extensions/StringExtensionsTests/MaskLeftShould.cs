using System.Collections.Generic;
using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class MaskLeftShould
    {
        [TestCase(null)]
        [TestCase("")]
        public void ReturnEmptyStringWhenGivenNullorEmptyValue(string nullOrEmptyValue)
        {
            string result = nullOrEmptyValue.MaskLeft(50);

            Assert.IsNotNull(result, "A null result should never be returned.");
            Assert.IsEmpty(result, "Empty string should be returned when given null or empty value.");
        }

        [Test]
        public void ReturnOriginalStringWhenShowRightCountMatchesOrExceedsValueLength()
        {
            const string value = "SomeTestString";
            var showRightCounts = new List<int> {value.Length, value.Length + 1, value.Length + 50};
            foreach (var showRightCount in showRightCounts)
            {
                string result = value.MaskLeft(showRightCount);
                Assert.AreEqual(value, result, "Original string should be returned when value's length is {0} and showRightCount is {1}.", value.Length, showRightCount);
            }
        }

        [Test]
        public void ReturnWholeStringMaskedWhenShowRightCountIs0()
        {
            const char expectedCharacter = '=';
            const string value = "Something";
            int expectedLength = value.Length;
            
            string result = value.MaskLeft(0, expectedCharacter);

            Assert.AreEqual(expectedLength, result.Length, "Same length should have been returned.");
            foreach (char character in result)
            {
                Assert.AreEqual(expectedCharacter, character, "All characters in the result '{0}' should be replaced with the mask character '{1}'.", result, expectedCharacter);
            }
        }

        [Test]
        public void Leave3CharactersUnmaskedWhenShowRightCountIs3()
        {
            const char maskCharacter = '=';
            const string value = "String";
            const int showRightCount = 3;
            string expectedResult = value.Replace("Str", string.Format("{0}{0}{0}", maskCharacter));

            string result = value.MaskLeft(showRightCount, maskCharacter);

            Assert.AreEqual(expectedResult, result, "The first three characters should have been masked.");
        }
    }
}