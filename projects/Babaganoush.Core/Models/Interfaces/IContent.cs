using System;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for content.
    /// </summary>
    public interface IContent : IIdentity
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

        /// <summary>
        /// Gets or sets the Date/Time of the date created.
        /// </summary>
        ///
        /// <value>
        /// The date created.
        /// </value>
        DateTime DateCreated { get; set; }
    }
}