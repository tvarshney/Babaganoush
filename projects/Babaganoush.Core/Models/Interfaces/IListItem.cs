using System;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for list item.
    /// </summary>
    public interface IListItem : IIdentity, IActive, IClassified
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
        /// Gets or sets the content.
        /// </summary>
        ///
        /// <value>
        /// The content.
        /// </value>
        string Content { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        ///
        /// <value>
        /// The slug.
        /// </value>
        string Slug { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the date created.
        /// </summary>
        ///
        /// <value>
        /// The date created.
        /// </value>
        DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the last modified.
        /// </summary>
        ///
        /// <value>
        /// The last modified.
        /// </value>
        DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or sets the publication date.
        /// </summary>
        ///
        /// <value>
        /// The publication date.
        /// </value>
        DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the ordinal.
        /// </summary>
        ///
        /// <value>
        /// The ordinal.
        /// </value>
        float Ordinal { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        ///
        /// <value>
        /// The parent.
        /// </value>
        IParent Parent { get; set; }
    }
}