// file:	Models\ContentModel.cs
//
// summary:	Implements the content model class
using Babaganoush.Sitefinity.Models.Interfaces;
using System;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the content.
    /// </summary>
    public class ContentModel : IDataModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the publication date.
        /// </summary>
        /// <value>
        /// The publication date.
        /// </value>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the last modified.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public Guid Owner { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ContentModel()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public ContentModel(IContent sfContent)
        {
            if (sfContent != null)
            {
                Id = sfContent.Id;
                Title = sfContent.Title;
                PublicationDate = sfContent.PublicationDate;
                LastModified = sfContent.LastModified;
                DateCreated = sfContent.DateCreated;
                Owner = sfContent.Owner;
            }
        }
    }
}