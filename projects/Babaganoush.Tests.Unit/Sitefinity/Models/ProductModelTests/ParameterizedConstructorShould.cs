using Babaganoush.Sitefinity.Models;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Sitefinity.Models.ProductModelTests
{
    [TestFixture]
    internal class ParameterizedConstructorShould
    {
        [Test]
        public void SetListPropertiesToEmptyList()
        {
            var productModel = new ProductModel(null);

            Babaganoush.Assert.IsEmptyAndNotNull(productModel.Categories);
            Babaganoush.Assert.IsEmptyAndNotNull(productModel.Documents);
            Babaganoush.Assert.IsEmptyAndNotNull(productModel.Images);
            Babaganoush.Assert.IsEmptyAndNotNull(productModel.ProductVariations);
            Babaganoush.Assert.IsEmptyAndNotNull(productModel.Tags);
        }
    }
}