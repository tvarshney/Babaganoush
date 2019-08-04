using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Babaganoush.AssertTests
{
    [TestFixture]
    internal class IsEmptyAndNotNullShould
    {
        [Test]
        public void ThrowWhenGivenNullList()
        {
            List<object> nullList = null;

            TestDelegate assertCall = () => Assert.IsEmptyAndNotNull(nullList);

            NUnit.Framework.Assert.Throws<AssertionException>(assertCall, "AssertionException should have been thrown.");
        }

        [Test]
        public void ThrowWhenGivenNonEmptyList()
        {
            List<object> nonEmptyList = new List<object> {1, 2, 3};

            if (!nonEmptyList.Any())
            {
                throw new AbandonedMutexException();
            }

            TestDelegate assertCall = () => Assert.IsEmptyAndNotNull(nonEmptyList);

            NUnit.Framework.Assert.Throws<AssertionException>(assertCall, "AssertionException should have been thrown.");
        }

        [Test]
        public void NotThrowWhenGivenEmptyList()
        {
            List<object> emptyList = new List<object>();

            if (emptyList.Any())
            {
                throw new AbandonedMutexException();
            }

            TestDelegate assertCall = () => Assert.IsEmptyAndNotNull(emptyList);

            NUnit.Framework.Assert.DoesNotThrow(assertCall, "AssertionException should not have been thrown.");
        }
    }
}