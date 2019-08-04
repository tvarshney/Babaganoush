using System;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for media.
    /// </summary>
    public interface IMedia : IIdentity, IActive, IClassified
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
        /// Gets or sets the author.
        /// </summary>
        ///
        /// <value>
        /// The author.
        /// </value>
        string Author { get; set; }

        /// <summary>
        /// Gets or sets the ordinal.
        /// </summary>
        ///
        /// <value>
        /// The ordinal.
        /// </value>
        float Ordinal { get; set; }

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
        /// Gets or sets the extension.
        /// </summary>
        ///
        /// <value>
        /// The extension.
        /// </value>
        string Extension { get; set; }

        /// <summary>
        /// Gets or sets the type of the mime.
        /// </summary>
        ///
        /// <value>
        /// The type of the mime.
        /// </value>
        string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the total number of size.
        /// </summary>
        ///
        /// <value>
        /// The total number of size.
        /// </value>
        long TotalSize { get; set; }

        /// <summary>
        /// Gets or sets the number of views.
        /// </summary>
        ///
        /// <value>
        /// The number of views.
        /// </value>
        int ViewsCount { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        ///
        /// <value>
        /// The file.
        /// </value>
        string File { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is primary.
        /// </summary>
        ///
        /// <value>
        /// true if this object is primary, false if not.
        /// </value>
        bool IsPrimary { get; set; }

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
        /// Gets or sets the parent.
        /// </summary>
        ///
        /// <value>
        /// The parent.
        /// </value>
        ILibrary Parent { get; set; }
    }
}