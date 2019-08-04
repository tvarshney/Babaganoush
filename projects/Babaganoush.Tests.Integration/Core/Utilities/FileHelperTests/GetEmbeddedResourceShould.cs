using System;
using Babaganoush.Core.Utilities;
using NUnit.Framework;

namespace Babaganoush.Tests.Integration.Core.Utilities.FileHelperTests
{
    [TestFixture]
    internal class GetEmbeddedResourceShould
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ThrowWhenGivenNullOrEmptyResourceName(string resourceName)
        {
            TestDelegate getEmbeddedResourceCall = () => FileHelper.GetEmbeddedResource(resourceName, typeof(GetEmbeddedResourceShould));

            Assert.Throws<ArgumentNullException>(getEmbeddedResourceCall);
        }

        [Test]
        public void ReturnEmbeddedResourceAsString()
        {
            const string embeddedResourceName = "Babaganoush.Tests.Integration.Core.Utilities.FileHelperTests.EmbeddedDocument.txt";

            string embeddedResource = FileHelper.GetEmbeddedResource(embeddedResourceName, typeof(GetEmbeddedResourceShould));

            Assert.IsNotNull(embeddedResource, "Non-null embedded resource should have been returned.");
            Assert.IsNotEmpty(embeddedResource, "Non-empty embedded resource should have been retruned.");
        }

        [Test]
        public void ReturnEmptyStringWhenGivenInvalidResourceName()
        {
            string embeddedResource = FileHelper.GetEmbeddedResource("I.Do.Not.Exist", typeof(GetEmbeddedResourceShould));

            Assert.IsNotNull(embeddedResource, "Non-null embedded resource should have been returned.");
            Assert.IsEmpty(embeddedResource, "Empty string should have been retruned.");
        }
    }
}