// file:	Models\CalendarModel.cs
//
// summary:	Implements the calendar model class
using Babaganoush.Sitefinity.Extensions;
using System;
using Telerik.Sitefinity.Events.Model;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the calendar.
    /// </summary>
    public class CalendarModel
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
        /// Gets or sets the last modified.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        public DateTime DateCreated { get; set; }

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
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected Calendar OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CalendarModel()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public CalendarModel(Calendar sfContent)
        {
            if (sfContent != null)
            {
                Id = sfContent.Id;
                Title = sfContent.Title;
                PublicationDate = sfContent.PublicationDate;
                LastModified = sfContent.LastModified;
                DateCreated = sfContent.DateCreated;
                Description = sfContent.Description;
                Color = sfContent.Color;
                Url = sfContent.GetFullUrl();
                Slug = sfContent.UrlName;

                // Store original content
                OriginalContent = sfContent;
            }
        }
    }
}