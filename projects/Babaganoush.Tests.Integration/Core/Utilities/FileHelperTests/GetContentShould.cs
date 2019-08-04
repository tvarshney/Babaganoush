using System;
using System.IO;
using Babaganoush.Core.Utilities;
using NUnit.Framework;

namespace Babaganoush.Tests.Integration.Core.Utilities.FileHelperTests
{
    internal class GetContentShould : FileHelperTestBase
    {
        [Test]
        public void ReturnContentsOfSpecifiedFile()
        {
            string expectedContents = string.Format("Foo{0}Bar{0}Baz", Environment.NewLine);
            File.WriteAllText(FullFilePath, expectedContents);

            string content = FileHelper.GetContent(FullFilePath);

            Assert.AreEqual(expectedContents, content);
        }
    }
}