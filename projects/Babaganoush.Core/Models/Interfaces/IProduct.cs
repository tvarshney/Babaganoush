using System;
using System.Collections.Generic;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for product.
    /// </summary>
    public interface IProduct : IContent, IClassified
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        ///
        /// <value>
        /// The description.
        /// </value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        ///
        /// <value>
        /// The price.
        /// </value>
        decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the display price.
        /// </summary>
        ///
        /// <value>
        /// The display price.
        /// </value>
        decimal DisplayPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the featured.
        /// </summary>
        ///
        /// <value>
        /// true if featured, false if not.
        /// </value>
        bool Featured { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is on sale.
        /// </summary>
        ///
        /// <value>
        /// true if this object is on sale, false if not.
        /// </value>
        bool IsOnSale { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is shippable.
        /// </summary>
        ///
        /// <value>
        /// true if this object is shippable, false if not.
        /// </value>
        bool IsShippable { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        ///
        /// <value>
        /// The rating.
        /// </value>
        double Rating { get; set; }

        /// <summary>
        /// Gets or sets the sale price.
        /// </summary>
        ///
        /// <value>
        /// The sale price.
        /// </value>
        decimal? SalePrice { get; set; }

        /// <summary>
        /// Gets or sets the sale start date.
        /// </summary>
        ///
        /// <value>
        /// The sale start date.
        /// </value>
        DateTime? SaleStartDate { get; set; }

        /// <summary>
        /// Gets or sets the sale end date.
        /// </summary>
        ///
        /// <value>
        /// The sale end date.
        /// </value>
        DateTime? SaleEndDate { get; set; }

        /// <summary>
        /// Gets or sets the SKU.
        /// </summary>
        ///
        /// <value>
        /// The SKU.
        /// </value>
        string Sku { get; set; }

        /// <summary>
        /// Gets or sets URL of the document.
        /// </summary>
        ///
        /// <value>
        /// The URL.
        /// </value>
        string Url { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        ///
        /// <value>
        /// The slug.
        /// </value>
        string Slug { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        ///
        /// <value>
        /// The weight.
        /// </value>
        double? Weight { get; set; }

        /// <summary>
        /// Gets or sets the best selling.
        /// </summary>
        ///
        /// <value>
        /// The best selling.
        /// </value>
        int BestSelling { get; set; }

        /// <summary>
        /// Gets or sets the product variations.
        /// </summary>
        ///
        /// <value>
        /// The product variations.
        /// </value>
        IList<IProductVariation> ProductVariations { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        ///
        /// <value>
        /// The images.
        /// </value>
        IList<IImage> Images { get; set; }

        /// <summary>
        /// Gets or sets the documents.
        /// </summary>
        ///
        /// <value>
        /// The documents.
        /// </value>
        IList<IDocument> Documents { get; set; }

        /// <summary>
        /// Gets or sets the type of the product.
        /// </summary>
        ///
        /// <value>
        /// The type of the product.
        /// </value>
        string ProductType { get; set; }
    }
}