using Babaganoush.Sitefinity.Models;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Sitefinity.Models.PageModelTests
{
    [TestFixture]
    internal class ParameterizedConstructorShould
    {
        [Test]
        public void SetItemsPropertyToEmptyList()
        {
            var pageModel = new PageModel(null);

            Babaganoush.Assert.IsEmptyAndNotNull(pageModel.Items);
        }
    }
}