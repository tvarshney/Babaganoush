using Telerik.Sitefinity.Ecommerce.Catalog.Model;

namespace Babaganoush.Sitefinity.Models.Interfaces
{
    /// <summary>
    /// Interface for creating <see cref="ProductModel"/> objects from Sitefinity <see cref="Product"/> objects.
    /// </summary>
    public interface IProductFactory
    {
        /// <summary>
        /// Creates a <see cref="ProductModel"/> object based off of then given <see cref="Product"/> object.
        /// </summary>
        ProductModel Create(Product sfContent);
    }
}