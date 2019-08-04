using Babaganoush.Core.Utilities;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Utilities.ValidationHelperTests
{
    [TestFixture]
    internal class IsValidNumberShould
    {
        [Test]
        public void Reject0When0IsNotAllowed()
        {
            const bool zeroAllowed = false;

            bool isValidNumber = ValidationHelper.IsValidNumber(0, zeroAllowed, true);

            Assert.IsFalse(isValidNumber, "0 is invalid when not allowed.");
        }

        [TestCase(-10)]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(253)]
        public void AcceptNonZeroValueWhen0IsNotAllowed(int nonZeroValue)
        {
            const bool zeroAllowed = false;

            bool isValidNumber = ValidationHelper.IsValidNumber(nonZeroValue, zeroAllowed, true);

            Assert.IsTrue(isValidNumber, "Non-zero value {0} is valid even when 0 is not allowed.", nonZeroValue);
        }

        [Test]
        public void Accept0When0IsAllowed()
        {
            const bool zeroAllowed = true;

            bool isValidNumber = ValidationHelper.IsValidNumber(0, zeroAllowed, true);

            Assert.IsTrue(isValidNumber, "0 is valid when allowed.");
        }

        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-5689)]
        public void RejectNegativeNumberWhenNegativesAreNotAllowed(int negativeValue)
        {
            const bool negativeAllowed = false;

            bool isValidNumber = ValidationHelper.IsValidNumber(negativeValue, true, negativeAllowed);

            Assert.IsFalse(isValidNumber, "{0} is invalid when negative numbers are not allowed.", negativeValue);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(14)]
        public void AcceptNonNegativeNumberWhenNegativesAreNotAllowed(int nonNegativeValue)
        {
            const bool negativeAllowed = false;

            bool isValidNumber = ValidationHelper.IsValidNumber(nonNegativeValue, true, negativeAllowed);

            Assert.IsTrue(isValidNumber, "{0} is valid even when negative numbers are not allowed.", nonNegativeValue);
        }

        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-235)]
        public void AcceptNegativeNumberWhenAllowed(int negativeValue)
        {
            const bool negativeAllowed = true;

            bool isValidNumber = ValidationHelper.IsValidNumber(negativeValue, true, negativeAllowed);

            Assert.IsTrue(isValidNumber, "{0} is valid when negative numbers are allowed.", negativeValue);
        }

        [TestCase(-500)]
        [TestCase(0)]
        [TestCase(654)]
        public void RejectNumberLessThanMinValue(int value)
        {
            int minValue = value + 29;

            bool isValidNumber = ValidationHelper.IsValidNumber(value, true, true, minValue);

            Assert.IsFalse(isValidNumber, "{0} is invalid when the minimum value is {1}.", value, minValue);
        }

        [Test]
        public void AcceptNumberEqualToMinValue()
        {
            const int value = 2398;
            const int minValue = value;

            bool isValidNumber = ValidationHelper.IsValidNumber(value, true, true, minValue);

            Assert.IsTrue(isValidNumber, "{0} is valid when the minimum value is {1}.", value, minValue);
        }

        [TestCase(-500)]
        [TestCase(0)]
        [TestCase(654)]
        public void AcceptNumberGreaterThanMinValue(int value)
        {
            int minValue = value - 5;

            bool isValidNumber = ValidationHelper.IsValidNumber(value, true, true, minValue);

            Assert.IsTrue(isValidNumber, "{0} is valid when the minimum value is {1}.", value, minValue);
        }

        [TestCase(-500)]
        [TestCase(0)]
        [TestCase(654)]
        public void RejectNumberGreaterThanMaxValue(int value)
        {
            int maxValue = value - 23;

            bool isValidNumber = ValidationHelper.IsValidNumber(value, true, true, null, maxValue);

            Assert.IsFalse(isValidNumber, "{0} is invalid when the maximum value is {1}.", value, maxValue);
        }

        [Test]
        public void AcceptNumberEqualToMaxValue()
        {
            const int value = 2398;
            const int maxValue = value;

            bool isValidNumber = ValidationHelper.IsValidNumber(value, true, true, null, maxValue);

            Assert.IsTrue(isValidNumber, "{0} is valid when the maximum value is {1}.", value, maxValue);
        }

        [TestCase(-500)]
        [TestCase(0)]
        [TestCase(654)]
        public void AcceptNumberLessThanMaxValue(int value)
        {
            int maxValue = value + 74;

            bool isValidNumber = ValidationHelper.IsValidNumber(value, true, true, null, maxValue);

            Assert.IsTrue(isValidNumber, "{0} is valid when the maximum value is {1}.", value, maxValue);
        }

        [TestCase(-3489)]
        [TestCase(0)]
        [TestCase(6542)]
        public void AcceptNumberBetweenMinAndMaxValue(int value)
        {
            int minValue = value - 22;
            int maxValue = value + 74;

            bool isValidNumber = ValidationHelper.IsValidNumber(value, true, true, minValue, maxValue);

            Assert.IsTrue(isValidNumber, "{0} is valid when the minimum value is {1} and the maximum value is {2}.", value, minValue, maxValue);
        }

        [Test]
        public void AcceptNumberEqualToMinAndMaxValue()
        {
            const int value = 2398;
            const int minValue = value;
            const int maxValue = value;

            bool isValidNumber = ValidationHelper.IsValidNumber(value, true, true, minValue, maxValue);

            Assert.IsTrue(isValidNumber, "{0} is valid when the minimum value is {1} and the maximum value is {2}.", value, minValue, maxValue);
        }
    }
}