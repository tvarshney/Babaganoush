using Babaganoush.Core.Models.Attributes;

namespace Babaganoush.Tests.Unit.Core.Extensions.EnumExtensionsTests
{
    internal enum DummyEnum
    {
        [StringValue(GetStringValueShould.DUMMY_STRING_VALUE)]
        HasAString,
        HasNoString
    }
}