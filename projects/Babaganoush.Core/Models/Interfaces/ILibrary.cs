using System;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for library.
    /// </summary>
    public interface ILibrary : IIdentity, IActive
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
        /// Gets or sets the slug.
        /// </summary>
        ///
        /// <value>
        /// The slug.
        /// </value>
        string Slug { get; set; }

        /// <summary>
        /// Gets or sets the number of views.
        /// </summary>
        ///
        /// <value>
        /// The number of views.
        /// </value>
        int ViewsCount { get; set; }

        /// <summary>
        /// Gets or sets the size of the maximum item.
        /// </summary>
        ///
        /// <value>
        /// The size of the maximum item.
        /// </value>
        long MaxItemSize { get; set; }

        /// <summary>
        /// Gets or sets the size of the maximum.
        /// </summary>
        ///
        /// <value>
        /// The size of the maximum.
        /// </value>
        long MaxSize { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the date created.
        /// </summary>
        ///
        /// <value>
        /// The date created.
        /// </value>
        DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the publication date.
        /// </summary>
        ///
        /// <value>
        /// The publication date.
        /// </value>
        DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the last modified.
        /// </summary>
        ///
        /// <value>
        /// The last modified.
        /// </value>
        DateTime LastModified { get; set; }
    }
}