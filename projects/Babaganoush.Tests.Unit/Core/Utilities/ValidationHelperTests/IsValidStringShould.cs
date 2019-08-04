using Babaganoush.Core.Utilities;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Utilities.ValidationHelperTests
{
    [TestFixture]
    internal class IsValidStringShould
    {
        [Test]
        public void RejectNullStringWhenCheckingForNull()
        {
            const string nullStringValue = null;
            const bool checkForNull = true;

            bool isValidString = ValidationHelper.IsValidString(nullStringValue, checkForNull, false, false, 0, 0);

            Assert.IsFalse(isValidString, "Null string is invalid when checking for null.");
        }

        [Test]
        public void AcceptNullStringWhenNotCheckingForNull()
        {
            const string nullStringValue = null;
            const bool checkForNull = false;

            bool isValidString = ValidationHelper.IsValidString(nullStringValue, checkForNull, false, false, 0, 0);

            Assert.IsTrue(isValidString, "Null string is valid when not checking for null.");
        }

        [TestCase('#')]
        [TestCase(' ')]
        [TestCase('/')]
        public void RejectStringWithNonNumberLetterWhenCheckingForSpecialCharacters(char specialCharacter)
        {
            string valueWithSpecialCharacter = "foo" + specialCharacter + "bar";
            const bool checkForSpecialCharacters = true;
            
            bool isValidString = ValidationHelper.IsValidString(valueWithSpecialCharacter, false, false, checkForSpecialCharacters, 0, 0);

            Assert.IsFalse(isValidString, "String value '{0}' is invalid when checking for special characters.", valueWithSpecialCharacter);
        }

        [TestCase('#')]
        [TestCase(' ')]
        [TestCase('/')]
        public void AcceptStringWithNonNumberLetterWhenNotCheckingForSpecialCharacters(char specialCharacter)
        {
            string valueWithSpecialCharacter = "foo" + specialCharacter + "bar";
            const bool checkForSpecialCharacters = false;
            
            bool isValidString = ValidationHelper.IsValidString(valueWithSpecialCharacter, false, false, checkForSpecialCharacters, 0, 0);

            Assert.IsTrue(isValidString, "String value '{0}' is valid when checking for special characters.", valueWithSpecialCharacter);
        }

        [Test]
        public void RejectEmptyStringWhenCheckingForEmptyStrings()
        {
            const string emptyString = "";
            const bool checkIfEmpty = true;

            bool isValidString = ValidationHelper.IsValidString(emptyString, false, checkIfEmpty, false, 0, 0);

            Assert.IsFalse(isValidString, "Empty string values are invalid when checking for empty strings.");
        }

        [Test]
        public void AcceptEmptyStringWhenNotCheckingForEmptyStrings()
        {
            const string emptyString = "";
            const bool checkIfEmpty = false;

            bool isValidString = ValidationHelper.IsValidString(emptyString, false, checkIfEmpty, false, 0, 0);

            Assert.IsTrue(isValidString, "Empty string values are valid when not checking for empty strings.");
        }

        [Test]
        public void RejectStringThatIsShorterThanMinLength()
        {
            const string value = "abc";
            int minLength = value.Length + 5;
            
            bool isValidString = ValidationHelper.IsValidString(value, false, false, false, minLength, int.MaxValue);

            Assert.IsFalse(isValidString, "String value of length {0} is invalid when checking for min length {1}.", value.Length, minLength);
        }

        [Test]
        public void AcceptStringThatIsEqualToMinLength()
        {
            const string value = "abc";
            int minLength = value.Length;

            bool isValidString = ValidationHelper.IsValidString(value, false, false, false, minLength, int.MaxValue);

            Assert.IsTrue(isValidString, "String value of length {0} is valid when checking for min length {1}.", value.Length, minLength);
        }

        [Test]
        public void AcceptStringThatIsLongerThanMinLength()
        {
            const string value = "abc123";
            int minLength = value.Length - 1;

            bool isValidString = ValidationHelper.IsValidString(value, false, false, false, minLength, int.MaxValue);

            Assert.IsTrue(isValidString, "String value of length {0} is valid when checking for min length {1}.", value.Length, minLength);
        }

        [Test]
        public void RejectStringThatIsLongerThanMaxLength()
        {
            const string value = "abc123";
            int maxLength = value.Length - 1;

            bool isValidString = ValidationHelper.IsValidString(value, false, false, false, 0, maxLength);

            Assert.IsFalse(isValidString, "String value of length {0} is invalid when checking for max length {1}.", value.Length, maxLength);
        }

        [Test]
        public void AcceptStringThatIsEqualToMaxLength()
        {
            const string value = "abc123";
            int maxLength = value.Length;

            bool isValidString = ValidationHelper.IsValidString(value, false, false, false, 0, maxLength);

            Assert.IsTrue(isValidString, "String value of length {0} is valid when checking for max length {1}.", value.Length, maxLength);
        }

        [Test]
        public void AcceptStringThatIsShorterThanMaxLength()
        {
            const string value = "abc123";
            int maxLength = value.Length + 15;

            bool isValidString = ValidationHelper.IsValidString(value, false, false, false, 0, maxLength);

            Assert.IsTrue(isValidString, "String value of length {0} is valid when checking for max length {1}.", value.Length, maxLength);
        }

        [Test]
        public void AcceptStringThatIsInRange()
        {
            const string value = "abc123";
            int minLength = value.Length - 3;
            int maxLength = value.Length + 3;

            bool isValidString = ValidationHelper.IsValidString(value, false, false, false, minLength, maxLength);

            Assert.IsTrue(isValidString, "String value of length {0} is valid when checking for min length {1} and max length {2}.", value.Length, minLength, maxLength);
        }

        [Test]
        public void AcceptStringThatIsEqualToMinAndMaxLength()
        {
            const string value = "abc123";
            int minLength = value.Length;
            int maxLength = value.Length;

            bool isValidString = ValidationHelper.IsValidString(value, false, false, false, minLength, maxLength);

            Assert.IsTrue(isValidString, "String value of length {0} is valid when checking for min length {1} and max length {2}.", value.Length, minLength, maxLength);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-50)]
        public void AcceptStringLongerThanMaxWhenMaxLengthIsLessThan1(int maxLength)
        {
            const string value = "abc";

            bool isValidString = ValidationHelper.IsValidString(value, false, false, false, 0, maxLength);

            Assert.IsTrue(isValidString, "String value of length {0} is valid when checking for max length {1}.", value.Length, maxLength);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-50)]
        public void AcceptAnyStringWhenMinLengthIsLessThan1(int minLength)
        {
            const string value = "";

            bool isValidString = ValidationHelper.IsValidString(value, false, false, false, minLength, int.MaxValue);

            Assert.IsTrue(isValidString, "String value of length {0} is valid when checking for min length {1}.", value.Length, minLength);
        }

        [Test]
        public void RejectStringShorterThanMinLengthWhenMaxLengthIsLessThanMin()
        {
            const string value = "abc123";
            int minLength = value.Length + 5;
            int maxLength = minLength - 2;

            bool isValidString = ValidationHelper.IsValidString(value, false, false, false, minLength, maxLength);

            Assert.IsFalse(isValidString, "String value of length {0} is invalid due to minimum length {1}, despite maximum length being smaller at {2}.",
                value.Length, minLength, maxLength);
        }

        [Test]
        public void RejectStringLongerThanMaxLengthWhenMinLengthIsGreaterThanMax()
        {
            const string value = "abc123";
            int maxLength = value.Length - 2;
            int minLength = maxLength + 5;

            bool isValidString = ValidationHelper.IsValidString(value, false, false, false, minLength, maxLength);

            Assert.IsFalse(isValidString, "String value of length {0} is invalid due to maximum length {1}, despite minium length being greater at {2}.",
                value.Length, maxLength, minLength);
        }
    }
}