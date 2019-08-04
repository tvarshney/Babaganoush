using System.IO;
using Babaganoush.Core.Utilities;
using NUnit.Framework;

namespace Babaganoush.Tests.Integration.Core.Utilities.FileHelperTests
{
    internal class CreateEmptyFileShould : FileHelperTestBase
    {
        [Test]
        public void NotPreventAccessAfterCreation()
        {
            FileHelper.CreateEmptyFile(FullFilePath);

            try
            {
                File.OpenRead(FullFilePath).Dispose();
            }
            catch (IOException)
            {
                Assert.Fail("File was not accessible after creation.");
            }
        }

        [Test]
        public void CreateFileWithNoContents()
        {
            FileHelper.CreateEmptyFile(FullFilePath);

            string fileContents = File.ReadAllText(FullFilePath);
            Assert.IsEmpty(fileContents, "File should have no contents.");
        }
    }
}