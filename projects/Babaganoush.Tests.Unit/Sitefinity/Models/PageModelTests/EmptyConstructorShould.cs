using Babaganoush.Sitefinity.Models;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Sitefinity.Models.PageModelTests
{
    [TestFixture]
    internal class EmptyConstructorShould
    {
        [Test]
        public void SetItemsPropertyToEmptyList()
        {
            var pageModel = new PageModel();

            Babaganoush.Assert.IsEmptyAndNotNull(pageModel.Items);
        }
    }
}