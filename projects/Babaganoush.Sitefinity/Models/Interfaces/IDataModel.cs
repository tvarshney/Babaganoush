// file:	Models\Interfaces\IDataModel.cs
//
// summary:	Declares the IDataModel interface
using System;

namespace Babaganoush.Sitefinity.Models.Interfaces
{
    /// <summary>
    /// Interface for data model.
    /// </summary>
    public interface IDataModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the publication date.
        /// </summary>
        /// <value>
        /// The publication date.
        /// </value>
        DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the last modified.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        Guid Owner { get; set; }
    }
}
