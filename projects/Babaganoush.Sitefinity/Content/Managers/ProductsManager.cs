// file:	Content\Managers\ProductsManager.cs
//
// summary:	Implements the products manager class
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Babaganoush.Sitefinity.Models.Factories;
using Babaganoush.Sitefinity.Models.Interfaces;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Ecommerce.Catalog.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Manager for products.
    /// </summary>
    public class ProductsManager : BaseContentManager<
        CatalogManager,
        Product,
        ProductsManager,
        ProductModel>
    {
        private readonly IProductFactory _productFactory = new ProductFactory();

        /// <summary>
        /// Gets the Sitefinity data.
        /// </summary>
        /// <param name="providerName">(Optional) the provider name to get.</param>
        /// <returns>
        /// An IQueryable&lt;Product&gt;
        /// </returns>
        protected override IQueryable<Product> Get(string providerName = null)
        {
            return GetManager(providerName).GetProducts();
        }

        /// <summary>
        /// Gets the Sitefinity data by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// A Product.
        /// </returns>
        protected override Product Get(Guid id, string providerName = null)
        {
            return GetManager(providerName).GetProduct(id);
        }

        /// <summary>
        /// Gets the specified sku.
        /// </summary>
        /// <param name="sku">The sku.</param>
        /// <param name="providerName">name of the provider.</param>
        /// <returns>
        /// A Product.
        /// </returns>
        protected virtual Product Get(string sku, string providerName)
        {
            return GetManager(providerName).GetProduct(sku);
        }

        /// <summary>
        /// Creates the Baba instance from the Sitefinity object.
        /// </summary>
        /// <param name="sfContent">Content of the sf.</param>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override ProductModel CreateInstance(Product sfContent)
        {
            return _productFactory.Create(sfContent);
        }

        /// <summary>
        /// Gets the product by sku.
        /// </summary>
        /// <param name="value">The sku.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by SKU.
        /// </returns>
        public virtual ProductModel GetBySku(string value,
            string providerName = null,
            Func<Product, ProductModel> convert = null)
        {
            var sfContent = Get(value, providerName);
            return convert != null ? convert(sfContent) : CreateInstance(sfContent);
        }

        /// <summary>
        /// Gets the featured product.
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The featured.
        /// </returns>
        public virtual IEnumerable<ProductModel> GetFeatured(
            string providerName = null, 
            Expression<Func<Product, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Product, ProductModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(p => p.Featured
                    && p.Status == ContentLifecycleStatus.Live
                    && p.Visible);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        /// <summary>
        /// Gets the by category.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by category.
        /// </returns>
        public override IEnumerable<ProductModel> GetByCategory(string value, 
            string providerName = null, 
            Expression<Func<Product, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Product, ProductModel>> convert = null)
        {
            return GetByCategory(value, false, providerName, filter, take, skip, convert);
        }

        /// <summary>
        /// Gets the by category identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by category identifier.
        /// </returns>
        public override IEnumerable<ProductModel> GetByCategoryId(Guid id, 
            string providerName = null, 
            Expression<Func<Product, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Product, ProductModel>> convert = null)
        {
            return GetByCategoryId(id, false, providerName, filter, take, skip, convert);
        }

        /// <summary>
        /// Gets the products by category.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="featured">if set to <c>true</c> featured.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if
        /// you want to override the default constructor.</param>
        /// <returns>
        /// The by category.
        /// </returns>
        public virtual IEnumerable<ProductModel> GetByCategory(string value, bool featured, 
            string providerName = null, 
            Expression<Func<Product, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Product, ProductModel>> convert = null)
        {
            //GET TAXON
            var taxon = TaxonomyManager.GetManager(providerName)
                .GetTaxa<HierarchicalTaxon>()
                .FirstOrDefault(t => t.Name == value);

            //CONVERT PRODUCT TO MODEL
            return taxon != null
                ? GetByCategoryId(taxon.Id, featured, providerName, filter, take, skip, convert)
                : Enumerable.Empty<ProductModel>().AsQueryable();
        }

        /// <summary>
        /// Gets the products by category.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="featured">if set to <c>true</c> featured.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if
        /// you want to override the default constructor.</param>
        /// <returns>
        /// The by category identifier.
        /// </returns>
        public virtual IEnumerable<ProductModel> GetByCategoryId(Guid id, bool featured, 
            string providerName = null, 
            Expression<Func<Product, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Product, ProductModel>> convert = null)
        {
            //GET PRODUCTS BY TAXON
            var sfItems = GetManager(providerName).GetProducts()
                .Where(p => p.GetValue<TrackedList<Guid>>("Department").Contains(id)
                    && p.Status == ContentLifecycleStatus.Live
                    && p.Visible);

            //FILTER BY FEATURED IF APPLICABLE
            if (featured)
                sfItems = sfItems.Where(p => p.Featured);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            //CONVERT PRODUCTS TO MODEL
            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }
    }
}