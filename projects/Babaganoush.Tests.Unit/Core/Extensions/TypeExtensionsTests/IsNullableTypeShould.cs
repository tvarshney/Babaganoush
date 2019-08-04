using System;
using System.Collections.Generic;
using Babaganoush.Core.Extensions;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Extensions.TypeExtensionsTests
{
    [TestFixture]
    internal class IsNullableTypeShould
    {
        [Test]
        public void ReturnFalseWhenGivenTypeIsNotGeneric()
        {
            Type nonGenericType = typeof(int);

            bool isNullableType = nonGenericType.IsNullableType();

            Assert.IsFalse(isNullableType, "Non-generic type should not be considered nullable.");
        }

        [Test]
        public void ReturnFalseWhenGivenListOfIntAsType()
        {
            Type listOfInt = typeof(List<int>);

            bool isNullableType = listOfInt.IsNullableType();

            Assert.IsFalse(isNullableType, "Generic list type should not be considered nullable.");
        }

        [Test]
        public void ReturnFalseWhenGivenOpenGeneric()
        {
            Type listOfInt = typeof(List<>);

            bool isNullableType = listOfInt.IsNullableType();

            Assert.IsFalse(isNullableType, "Generic list type should not be considered nullable.");
        }

        [Test]
        public void ReturnTrueWhenGivenNullableBoolAsType()
        {
            Type nullableBool = typeof(bool?);

            bool isNullableType = nullableBool.IsNullableType();

            Assert.IsTrue(isNullableType, "Nullable bool type should be considered nullable.");
        }

        [Test]
        public void ReturnFalseForNullType()
        {
            Type type = null;

            bool isNullableType = type.IsNullableType();

            Assert.IsFalse(isNullableType, "Null type should not be considered nullable.");
        }
    }
}