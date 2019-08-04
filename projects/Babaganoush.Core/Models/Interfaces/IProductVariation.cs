using System;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for product variation.
    /// </summary>
    public interface IProductVariation : IActive, IIdentity
    {
        /// <summary>
        /// Gets or sets the SKU.
        /// </summary>
        ///
        /// <value>
        /// The SKU.
        /// </value>
        string Sku { get; set; }

        /// <summary>
        /// Gets or sets the additional price.
        /// </summary>
        ///
        /// <value>
        /// The additional price.
        /// </value>
        decimal AdditionalPrice { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the last modified.
        /// </summary>
        ///
        /// <value>
        /// The last modified.
        /// </value>
        DateTime LastModified { get; set; }

        /// <summary>
        /// ATTRIBUTE PROPERTIES.
        /// </summary>
        ///
        /// <value>
        /// The title.
        /// </value>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        ///
        /// <value>
        /// The description.
        /// </value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal.
        /// </summary>
        ///
        /// <value>
        /// The ordinal.
        /// </value>
        float Ordinal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is visible.
        /// </summary>
        ///
        /// <value>
        /// true if visible, false if not.
        /// </value>
        bool Visible { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the parent.
        /// </summary>
        ///
        /// <value>
        /// The identifier of the parent.
        /// </value>
        Guid ParentId { get; set; }

        /// <summary>
        /// Gets or sets the parent title.
        /// </summary>
        ///
        /// <value>
        /// The parent title.
        /// </value>
        string ParentTitle { get; set; }
    }
}