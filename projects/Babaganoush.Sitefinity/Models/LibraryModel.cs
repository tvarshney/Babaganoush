// file:	Models\LibraryModel.cs
//
// summary:	Implements the library model class
using Babaganoush.Sitefinity.Extensions;
using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the library.
    /// </summary>
    public class LibraryModel
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
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets URL of the document.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        /// <value>
        /// The slug.
        /// </value>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the number of views.
        /// </summary>
        /// <value>
        /// The number of views.
        /// </value>
        public int ViewsCount { get; set; }

        /// <summary>
        /// Gets or sets the size of the maximum item.
        /// </summary>
        /// <value>
        /// The size of the maximum item.
        /// </value>
        public long MaxItemSize { get; set; }

        /// <summary>
        /// Gets or sets the size of the maximum.
        /// </summary>
        /// <value>
        /// The size of the maximum.
        /// </value>
        public long MaxSize { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ContentLifecycleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        public DateTime DateCreated { get; set; }

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
        /// Gets or sets a value indicating whether the active.
        /// </summary>
        /// <value>
        /// true if active, false if not.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected Library OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LibraryModel()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public LibraryModel(Library sfContent)
        {
            if (sfContent != null)
            {
                Id = sfContent.Id;
                Title = sfContent.Title;
                Description = sfContent.Description;
                ViewsCount = sfContent.ViewsCount;
                MaxItemSize = sfContent.MaxItemSize;
                MaxSize = sfContent.MaxSize;
                Status = sfContent.Status;
                DateCreated = sfContent.DateCreated;
                PublicationDate = sfContent.PublicationDate;
                LastModified = sfContent.LastModified;
                Url = sfContent.GetFullUrl(sfContent.DefaultPageId);
                Slug = sfContent.UrlName;
                Active = sfContent.Status == ContentLifecycleStatus.Live
                    && sfContent.Visible;

                // Store original content
                OriginalContent = sfContent;
            }
        }
    }
}