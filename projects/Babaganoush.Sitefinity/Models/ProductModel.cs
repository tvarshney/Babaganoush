// file:	Models\ProductModel.cs
//
// summary:	Implements the product model class

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Ecommerce.Catalog.Model;
using Telerik.Sitefinity.GenericContent.Model;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the product.
    /// </summary>
    public class ProductModel : ContentModel
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the display price.
        /// </summary>
        /// <value>
        /// The display price.
        /// </value>
        public decimal DisplayPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the featured.
        /// </summary>
        /// <value>
        /// true if featured, false if not.
        /// </value>
        public bool Featured { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is on sale.
        /// </summary>
        /// <value>
        /// true if this object is on sale, false if not.
        /// </value>
        public bool IsOnSale { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is shippable.
        /// </summary>
        /// <value>
        /// true if this object is shippable, false if not.
        /// </value>
        public bool IsShippable { get; set; }

        /// <summary>
        /// Gets or sets the sale price.
        /// </summary>
        /// <value>
        /// The sale price.
        /// </value>
        public decimal? SalePrice { get; set; }

        /// <summary>
        /// Gets or sets the sale start date.
        /// </summary>
        /// <value>
        /// The sale start date.
        /// </value>
        public DateTime? SaleStartDate { get; set; }

        /// <summary>
        /// Gets or sets the sale end date.
        /// </summary>
        /// <value>
        /// The sale end date.
        /// </value>
        public DateTime? SaleEndDate { get; set; }

        /// <summary>
        /// Gets or sets the SKU.
        /// </summary>
        /// <value>
        /// The SKU.
        /// </value>
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets URL of the document.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        /// <value>
        /// The slug.
        /// </value>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public double? Weight { get; set; }

        /// <summary>
        /// Gets or sets the best selling.
        /// </summary>
        /// <value>
        /// The best selling.
        /// </value>
        public int BestSelling { get; set; }

        /// <summary>
        /// Gets or sets the product variations.
        /// </summary>
        /// <value>
        /// The product variations.
        /// </value>
        public List<ProductVariationModel> ProductVariations { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        public List<ImageModel> Images { get; set; }

        /// <summary>
        /// Gets or sets the documents.
        /// </summary>
        /// <value>
        /// The documents.
        /// </value>
        public List<DocumentModel> Documents { get; set; }

        /// <summary>
        /// Gets or sets the type of the product.
        /// </summary>
        /// <value>
        /// The type of the product.
        /// </value>
        public string ProductType { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ContentLifecycleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public List<TaxonModel> Categories { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<TaxonModel> Tags { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the active.
        /// </summary>
        /// <value>
        /// true if active, false if not.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the isbn.
        /// </summary>
        /// <value>
        /// The isbn.
        /// </value>
        public string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        /// <value>
        /// The release date.
        /// </value>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the publisher.
        /// </summary>
        /// <value>
        /// The publisher.
        /// </value>
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or sets the number of pages.
        /// </summary>
        /// <value>
        /// The total number of pages.
        /// </value>
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>
        /// The platform.
        /// </value>
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the release.
        /// </summary>
        /// <value>
        /// The release.
        /// </value>
        public string Release { get; set; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected Product OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProductModel()
            : this(null)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public ProductModel(Product sfContent)
            : base(sfContent)
        {
            ProductVariations = new List<ProductVariationModel>();
            Images = new List<ImageModel>();
            Documents = new List<DocumentModel>();
            Categories = new List<TaxonModel>();
            Tags = new List<TaxonModel>();

            OriginalContent = sfContent;
        }
    }
}