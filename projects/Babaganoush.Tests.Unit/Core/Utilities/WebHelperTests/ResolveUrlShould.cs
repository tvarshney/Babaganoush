using Babaganoush.Core.Utilities;
using Babaganoush.Core.Utilities.Interfaces;
using Babaganoush.Core.Wrappers.Interfaces;
using Moq;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Utilities.WebHelperTests
{
    [TestFixture]
    internal class ResolveUrlShould
    {
        private IWebHelper _webHelper;
        private Mock<IVirtualPathUtility> _mockedVirtualPathUtility;

        [SetUp]
        public void SetUp()
        {
            _mockedVirtualPathUtility = new Mock<IVirtualPathUtility>();
            _webHelper = new WebHelper(_mockedVirtualPathUtility.Object, null);
        }

        [TestCase(null)]
        [TestCase("")]
        public void ReturnGivenUrlWhenGivenUrlIsNullOrEmpty(string url)
        {
            string result = _webHelper.ResolveUrl(url);

            Assert.AreEqual(url, result, "Result should have matched given url.");
        }

        [Test]
        public void NotResolveAbsoluteUrl()
        {
            const string absoluteUrl = "http://www.example.com/";

            _webHelper.ResolveUrl(absoluteUrl);

            _mockedVirtualPathUtility.Verify(v => v.ToAbsolute(It.IsAny<string>()), Times.Never, "Absolute URLs should not be transformed into an absolute URL again.");
        }

        [Test]
        public void ResolveVirtualUrlToAbsolute()
        {
            const string virtualUrl = "~/foo/bar";
            const string expected = "expected";
            _mockedVirtualPathUtility.Setup(v => v.ToAbsolute(virtualUrl)).Returns(expected);

            var result = _webHelper.ResolveUrl(virtualUrl);

            Assert.AreEqual(result, expected, "Given URL should have resolved to expected absolute URL.");
        }
    }
}