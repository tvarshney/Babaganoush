using System;
using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.StringExtensionsTests
{
    [TestFixture]
    internal class GetNextLineShould
    {
        [TestCase(null)]
        [TestCase("")]
        public void ReturnEmptyStringWhenGivenNullOrEmptyValue(string value)
        {
            string nextLine = value.GetNextLine(0);

            Assert.IsNotNull(nextLine, "Next line should not be null.");
            Assert.IsEmpty(nextLine, "Next line should be empty.");
        }

        [Test]
        public void ReturnEmptyStringWhenStartPositionMatchesValueLength()
        {
            const string value = "foobar";

            string nextLine = value.GetNextLine(value.Length);

            Assert.IsNotNull(nextLine, "Next line should not be null.");
            Assert.IsEmpty(nextLine, "Next line should be empty.");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(50)]
        public void ReturnEmptyStringWhenStartPositionExceedsValueLength(int exceedAmount)
        {
            const string value = "foobar";

            string nextLine = value.GetNextLine(value.Length + exceedAmount);

            Assert.IsNotNull(nextLine, "Next line should not be null.");
            Assert.IsEmpty(nextLine, "Next line should be empty.");
        }

        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-50)]
        public void ThrowWhenGivenNegativeStartPosition(int startPosition)
        {
            const string value = "foobar";

            TestDelegate getNextLineCall = () => value.GetNextLine(startPosition);

            Assert.Throws<ArgumentOutOfRangeException>(getNextLineCall, "Exception expected when given negative start position.");
        }

        [Test]
        public void ReturnsSubstringWhenGivenSingleLineString()
        {
            const int startPosition = 5;
            const string value = "This is only one line.";
            string expectedNextLine = value.Substring(startPosition);

            string nextLine = value.GetNextLine(startPosition);

            Assert.AreEqual(expectedNextLine, nextLine);
        }

        [Test]
        public void ReturnFirstLineAtStartPositionOf0()
        {
            const string expectedNextLine = "1\n";
            string value = string.Format("{0}2\n3\n", expectedNextLine);

            string nextLine = value.GetNextLine(0);

            Assert.AreEqual(expectedNextLine, nextLine);
        }

        [Test]
        public void ReturnNextLineAfterGivenStartPosition()
        {
            const string expectedNextLine = "2\n";
            string value = string.Format("1\n{0}3\n", expectedNextLine);

            string nextLine = value.GetNextLine(2);

            Assert.AreEqual(expectedNextLine, nextLine);
        }
    }
}