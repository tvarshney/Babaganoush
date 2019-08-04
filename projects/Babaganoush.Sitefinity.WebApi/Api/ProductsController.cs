// file:	Api\ProductsController.cs
//
// summary:	Implements the products controller class
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using Babaganoush.Sitefinity.WebApi.Models;
using System;
using System.Net.Http;
using Telerik.Sitefinity.Ecommerce.Catalog.Model;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for products.
    /// </summary>
    public class ProductsController : BaseContentController<ProductModel, Product>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController" /> class.
        /// </summary>
        public ProductsController()
            : base(BabaManagers.Products)
        {

        }

        /// <summary>
        /// Gets the product by sku.
        /// </summary>
        /// <param name="value">The sku.</param>
        /// <returns>
        /// The by SKU.
        /// </returns>
        public virtual HttpResponseMessage GetBySku(string value)
        {
            return new DataResponseSingle(BabaManagers.Products.GetBySku(value));
        }

        /// <summary>
        /// Gets the featured products.
        /// </summary>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the featured in this collection.
        /// </returns>
        public virtual HttpResponseMessage GetFeatured(int take = 0, int skip = 0)
        {
            return new DataResponse(BabaManagers.Products.GetFeatured(take: take, skip: skip));
        }

        /// <summary>
        /// Gets the products by category.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="featured">if set to <c>true</c> featured.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the categories in this collection.
        /// </returns>
        public virtual HttpResponseMessage GetByCategory(string value, bool featured, int take = 0, int skip = 0)
        {
            return new DataResponse(BabaManagers.Products.GetByCategory(value, featured, take: take, skip: skip));
        }

        /// <summary>
        /// Gets the products by category.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="featured">if set to <c>true</c> featured.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the category identifiers in this
        /// collection.
        /// </returns>
        public virtual HttpResponseMessage GetByCategoryId(Guid id, bool featured, int take = 0, int skip = 0)
        {
            return new DataResponse(BabaManagers.Products.GetByCategoryId(id, featured, take: take, skip: skip));
        }
    }
}