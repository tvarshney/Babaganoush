using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace Babaganoush.Tests.Integration.Core.Utilities.FileHelperTests
{
    [TestFixture]
    internal abstract class FileHelperTestBase
    {
        private readonly string _directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        protected string FullFilePath;

        [SetUp]
        public void SetUpBase()
        {
            string uniqueFileName = Guid.NewGuid().ToString();

            FullFilePath = string.Format(@"{0}\{1}.txt", _directoryName, uniqueFileName);
        }

        [TearDown]
        public void TearDownBase()
        {
            File.Delete(FullFilePath);
        }
    }
}