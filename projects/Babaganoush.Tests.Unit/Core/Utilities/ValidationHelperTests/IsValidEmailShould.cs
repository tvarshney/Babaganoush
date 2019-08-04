using Babaganoush.Core.Utilities;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Utilities.ValidationHelperTests
{
    [TestFixture]
    internal class IsValidEmailShould
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void RejectNullOrEmptyStringValues(string nullOrEmptyValue)
        {
            bool isValidEmail = ValidationHelper.IsValidEmail(nullOrEmptyValue);

            Assert.IsFalse(isValidEmail, "String is not a valid email when null or empty.");
        }

        [TestCase("foo@example.com")]
        [TestCase("bottle.cap@example.co.uk")]
        [TestCase("birthday_cake@example.org")]
        public void AcceptNormalEmailAddresses(string normalEmailValue)
        {
            bool isValidEmail = ValidationHelper.IsValidEmail(normalEmailValue);

            Assert.IsTrue(isValidEmail, "The string '{0}' is a valid email address.", normalEmailValue);
        }

        [TestCase("foo")]
        [TestCase("45")]
        [TestCase("83@@@@example.com")]
        [TestCase("test@example.org@")]
        public void RejectNonEmailStrings(string nonEmailValue)
        {
            bool isValidEmail = ValidationHelper.IsValidEmail(nonEmailValue);

            Assert.IsFalse(isValidEmail, "The string '{0}' is not a valid email address.", nonEmailValue);
        }
    }
}