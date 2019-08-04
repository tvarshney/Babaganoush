using System.Collections.Generic;
using Babaganoush.Core.Classes;
using Babaganoush.Core.Utilities;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Utilities.DataHelperTests
{
    [TestFixture]
    internal class GetReadableSizeShould
    {
        [Test]
        public void ReturnBytesForEverythingLessThanAKiloByte()
        {
            const string expectedSuffix = "B";
            for (ulong numberOfBytes = 0; numberOfBytes < 1024; numberOfBytes++)
            {
                string expectedReadableSize = string.Format("{0} {1}", numberOfBytes, expectedSuffix);

                string readableSize = DataHelper.GetReadableSize(numberOfBytes);
                
                Assert.AreEqual(expectedReadableSize, readableSize, "Bytes should be returned without being mutated when less than a KB.");
            }
        }

        [Test]
        public void ReturnBytesSuffixedWithB()
        {
            const string expectedSuffix = " B";

            string readableSize = DataHelper.GetReadableSize(349);

            StringAssert.EndsWith(expectedSuffix, readableSize, "Bytes should be suffixed with '{0}'.", expectedSuffix);
        }

        [Test]
        public void ReturnKiloBytesWhenGivenBetween1KBAnd1MB()
        {
            AssertRangeOfBytesReturnsCorrectReadableSize(FileSizeUnit.KiloByte);
        }

        [Test]
        public void ReturnMegaBytesWhenGivenBetween1MBAnd1GB()
        {
            AssertRangeOfBytesReturnsCorrectReadableSize(FileSizeUnit.MegaByte);
        }

        [Test]
        public void ReturnGigaBytesWhenGivenBetween1GBAnd1TB()
        {
            AssertRangeOfBytesReturnsCorrectReadableSize(FileSizeUnit.GigaByte);
        }

        [Test]
        public void ReturnTeraBytesWhenGivenBetween1TBAnd1PB()
        {
            AssertRangeOfBytesReturnsCorrectReadableSize(FileSizeUnit.TeraByte);
        }

        [Test]
        public void ReturnPetaBytesWhenGivenBetween1PBAnd1EB()
        {
            AssertRangeOfBytesReturnsCorrectReadableSize(FileSizeUnit.PetaByte);
        }

        [Test]
        public void ReturnExaBytesWhenGiven1EBOrMore()
        {
            AssertRangeOfBytesReturnsCorrectReadableSize(FileSizeUnit.ExaByte);
        }

        private static void AssertRangeOfBytesReturnsCorrectReadableSize(FileSizeUnit fileSizeUnit)
        {
            var bytesToTest = new List<ulong>
            {
                fileSizeUnit.Size,
                fileSizeUnit.Size + 1,
                fileSizeUnit.Size * 2,
                fileSizeUnit.Size * 500,
                fileSizeUnit.Size * 1023,
                (fileSizeUnit.Size * 1024) - 1,
            };

            foreach (var numberOfBytes in bytesToTest)
            {
                string readableSize = DataHelper.GetReadableSize(numberOfBytes);

                StringAssert.EndsWith(fileSizeUnit.Suffix, readableSize, "{0} should've been used for {1} bytes.", fileSizeUnit.Suffix, numberOfBytes);
            }
        }
    }
}