using System;
using System.Collections.Generic;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for department.
    /// </summary>
    public interface IDepartment : IIdentity
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
        /// Gets or sets URL of the full.
        /// </summary>
        ///
        /// <value>
        /// The full URL.
        /// </value>
        string FullUrl { get; set; }

        /// <summary>
        /// Gets or sets the ordinal.
        /// </summary>
        ///
        /// <value>
        /// The ordinal.
        /// </value>
        float Ordinal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether as link should be rendered.
        /// </summary>
        ///
        /// <value>
        /// true if render as link, false if not.
        /// </value>
        bool RenderAsLink { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the in navigation is shown.
        /// </summary>
        ///
        /// <value>
        /// true if show in navigation, false if not.
        /// </value>
        bool ShowInNavigation { get; set; }

        /// <summary>
        /// Gets or sets the name of the taxon.
        /// </summary>
        ///
        /// <value>
        /// The name of the taxon.
        /// </value>
        string TaxonName { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        ///
        /// <value>
        /// The slug.
        /// </value>
        string Slug { get; set; }

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
        /// Gets or sets the subtaxa.
        /// </summary>
        ///
        /// <value>
        /// The subtaxa.
        /// </value>
        IList<IDepartment> Subtaxa { get; set; }

        /// <summary>
        /// Gets or sets the number of. 
        /// </summary>
        ///
        /// <value>
        /// The count.
        /// </value>
        int Count { get; set; }
    }
}