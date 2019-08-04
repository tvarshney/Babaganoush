using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.EnumExtensionsTests
{
    [TestFixture]
    internal class GetStringValueShould
    {
        internal const string DUMMY_STRING_VALUE = "Test Value";

        [Test]
        public void ReturnEmptyStringWhenGivenNullEnum()
        {
            var stringValue = EnumExtensions.GetStringValue(null);

            Assert.IsNotNull(stringValue, "Non-null string should have been retruned.");
            Assert.IsEmpty(stringValue, "Empty string should have been returned.");
        }

        [Test]
        public void ReturnStringValueOfGivenEnum()
        {
            var stringValue = DummyEnum.HasAString.GetStringValue();

            Assert.AreEqual(DUMMY_STRING_VALUE, stringValue);
        }

        [Test]
        public void ReturnEmptyStringWhenEnumHasNoStringValue()
        {
            var stringValue = DummyEnum.HasNoString.GetStringValue();

            Assert.IsEmpty(stringValue);
        }
    }
}