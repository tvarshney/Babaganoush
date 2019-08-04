using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class GetFirstCharsShould
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ReturnEmptyStringWhenGivenNullOrEmptyValue(string nullorEmptyValue)
        {
            string firstChars = nullorEmptyValue.GetFirstChars(5);

            Assert.IsNotNull(firstChars, "Null string should never be returned.");
            Assert.IsEmpty(firstChars, "Empty string should be returned for null or empty value.");
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        public void ReturnEmptyStringWhenCountIs0OrLess(int count)
        {
            string firstChars = "foo".GetFirstChars(count);

            Assert.IsNotNull(firstChars, "Null string should never be returned.");
            Assert.IsEmpty(firstChars, "Empty string should be returned when count is 0 or less (was {0}).", count);
        }

        [Test]
        public void ReturnValueWhenCountIsEqualToValueLength()
        {
            const string value = "example string";
            int count = value.Length;

            string firstChars = value.GetFirstChars(count);

            Assert.AreEqual(value, firstChars, "Same string should be returned when count matches string value length.");
        }

        [Test]
        public void ReturnValueWhenCountIsGreaterThanValueLength()
        {
            const string value = "example string";
            int count = value.Length + 17;

            string firstChars = value.GetFirstChars(count);

            Assert.AreEqual(value, firstChars, "Same string should be returned when count is greater than string value length.");
        }

        [Test]
        public void ReturnCountCharactersOfGivenString()
        {
            const string value = "example";
            const string expected = "exa";
            int count = expected.Length;

            string firstChars = value.GetFirstChars(count);

            Assert.AreEqual(expected, firstChars, "Only the first {0} characters of the given string value should be returned.", count);
        }
    }
}