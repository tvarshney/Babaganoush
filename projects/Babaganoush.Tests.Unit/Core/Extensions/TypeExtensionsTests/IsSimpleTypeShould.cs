using System;
using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.TypeExtensionsTests
{
    [TestFixture]
    internal class IsSimpleTypeShould
    {
        [TestCase(typeof(String))]
        [TestCase(typeof(Decimal))]
        [TestCase(typeof(DateTime))]
        [TestCase(typeof(DateTimeOffset))]
        [TestCase(typeof(TimeSpan))]
        [TestCase(typeof(Guid))]
        public void ReturnTrueForSimpleTypes(Type simpleType)
        {
            bool result = simpleType.IsSimpleType();

            AssertIsSimpleType(true, result, simpleType);
        }

        [TestCase(typeof(int))]
        [TestCase(typeof(float))]
        public void ReturnTrueForPrimitiveType(Type primitiveType)
        {
            if (!primitiveType.IsPrimitive)
            {
                Assert.Fail("Setup failure: {0} is not a primitive type.", primitiveType.FullName);
            }

            bool result = primitiveType.IsSimpleType();

            AssertIsSimpleType(true, result, primitiveType);
        }

        [TestCase(typeof(int))]
        [TestCase(typeof(float))]
        public void ReturnTrueForValueType(Type valueType)
        {
            if (!valueType.IsValueType)
            {
                Assert.Fail("Setup failure: {0} is not a value type.", valueType.FullName);
            }

            bool result = valueType.IsSimpleType();

            AssertIsSimpleType(true, result, valueType);
        }

        [Test]
        public void ReturnFalseForObjectType()
        {
            Type objectType = typeof (object);

            bool result = objectType.IsSimpleType();

            AssertIsSimpleType(false, result, objectType);
        }

        [Test]
        public void ReturnFalseForClassType()
        {
            Type classType = typeof(IsSimpleTypeShould);

            bool result = classType.IsSimpleType();

            AssertIsSimpleType(false, result, classType);
        }

        private static void AssertIsSimpleType(bool expected, bool actual, Type testedType)
        {
            Assert.AreEqual(expected, actual, "{0} should{1} be considered a simple type.", testedType.FullName, !expected ? " not" : "");
        }
    }
}