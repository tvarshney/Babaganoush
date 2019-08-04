using System;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for taxon.
    /// </summary>
    public interface ITaxon : IIdentity
    {
        /// <summary>
        /// Gets or sets the title.
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
        /// Gets or sets a value indicating whether the in navigation is shown.
        /// </summary>
        ///
        /// <value>
        /// true if show in navigation, false if not.
        /// </value>
        bool ShowInNavigation { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        ///
        /// <value>
        /// The slug.
        /// </value>
        string Slug { get; set; }

        /// <summary>
        /// Gets or sets the ordinal.
        /// </summary>
        ///
        /// <value>
        /// The ordinal.
        /// </value>
        float Ordinal { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the last modified.
        /// </summary>
        ///
        /// <value>
        /// The last modified.
        /// </value>
        DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        ///
        /// <value>
        /// The parent.
        /// </value>
        IParent Parent { get; set; }

        /// <summary>
        /// Gets or sets the classification.
        /// </summary>
        ///
        /// <value>
        /// The classification.
        /// </value>
        string Classification { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is hierarchy.
        /// </summary>
        ///
        /// <value>
        /// true if this object is hierarchy, false if not.
        /// </value>
        bool IsHierarchy { get; set; }
    }
}
