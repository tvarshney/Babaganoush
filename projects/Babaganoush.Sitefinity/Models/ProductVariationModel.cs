// file:	Models\ProductVariationModel.cs
//
// summary:	Implements the product variation model class
using Newtonsoft.Json.Linq;
using System;
using Telerik.Sitefinity.Ecommerce.Catalog.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the product variation.
    /// </summary>
    public class ProductVariationModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the SKU.
        /// </summary>
        /// <value>
        /// The SKU.
        /// </value>
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets the additional price.
        /// </summary>
        /// <value>
        /// The additional price.
        /// </value>
        public decimal AdditionalPrice { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the last modified.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the active.
        /// </summary>
        /// <value>
        /// true if active, false if not.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal.
        /// </summary>
        /// <value>
        /// The ordinal.
        /// </value>
        public float Ordinal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is visible.
        /// </summary>
        /// <value>
        /// true if visible, false if not.
        /// </value>
        public bool Visible { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the parent.
        /// </summary>
        /// <value>
        /// The identifier of the parent.
        /// </value>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Gets or sets the parent title.
        /// </summary>
        /// <value>
        /// The parent title.
        /// </value>
        public string ParentTitle { get; set; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected ProductVariation OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProductVariationModel()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf product variation.</param>
        public ProductVariationModel(ProductVariation sfContent)
        {
            if (sfContent != null)
            {
                Id = sfContent.Id;
                Sku = sfContent.Sku;
                AdditionalPrice = sfContent.AdditionalPrice;
                LastModified = sfContent.LastModified;
                Active = sfContent.IsActive;

                //GET ATTRIBUTE DETAILS
                if (!string.IsNullOrWhiteSpace(sfContent.Variant))
                {
                    // Variant is stored as an array of an object. Get array, select first (only) object, then pull property from that object.
                    JToken variantData = JArray.Parse(sfContent.Variant).First;
                    Guid attributeId = new Guid(variantData["AttributeValueId"].Value<string>());

                    //GET ATTRIBUTE VALUE FOR VARIANT
                    var manager = CatalogManager.GetManager();
                    var attribute = manager.GetProductAttributeValue(attributeId);

                    //STORE PROPERTY VALUES TO MODEL
                    Title = attribute.Title;
                    Description = attribute.Description;
                    Ordinal = attribute.Ordinal;
                    Visible = attribute.Visible;
                    ParentId = attribute.Parent.Id;
                    ParentTitle = attribute.Parent.Title;
                }

                // Store original content
                OriginalContent = sfContent;
            }
        }
    }
}