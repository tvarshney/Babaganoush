using System.Collections.Generic;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for page.
    /// </summary>
    public interface IPage : IIdentity, IActive
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
        /// Gets or sets URL of the document.
        /// </summary>
        ///
        /// <value>
        /// The URL.
        /// </value>
        string Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is crawlable.
        /// </summary>
        ///
        /// <value>
        /// true if crawlable, false if not.
        /// </value>
        bool Crawlable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is backend.
        /// </summary>
        ///
        /// <value>
        /// true if this object is backend, false if not.
        /// </value>
        bool IsBackend { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the in navigation is shown.
        /// </summary>
        ///
        /// <value>
        /// true if show in navigation, false if not.
        /// </value>
        bool ShowInNavigation { get; set; }

        /// <summary>
        /// Gets or sets the name of the safe.
        /// </summary>
        ///
        /// <value>
        /// The name of the safe.
        /// </value>
        string SafeName { get; set; }

        /// <summary>
        /// Gets or sets the ordinal.
        /// </summary>
        ///
        /// <value>
        /// The ordinal.
        /// </value>
        float Ordinal { get; set; }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        ///
        /// <value>
        /// The theme.
        /// </value>
        string Theme { get; set; }

        /// <summary>
        /// Gets or sets the framework.
        /// </summary>
        ///
        /// <value>
        /// The framework.
        /// </value>
        string Framework { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        ///
        /// <value>
        /// The slug.
        /// </value>
        string Slug { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        ///
        /// <value>
        /// The parent.
        /// </value>
        IPage Parent { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        ///
        /// <value>
        /// The items.
        /// </value>
        IList<IPage> Items { get; set; }
    }
}