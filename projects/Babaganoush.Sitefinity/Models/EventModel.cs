// file:	Models\EventModel.cs
//
// summary:	Implements the event model class
using Babaganoush.Sitefinity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Events;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the event.
    /// </summary>
    public class EventModel : ContentModel
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>
        /// The street.
        /// </value>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the name of the contact.
        /// </summary>
        /// <value>
        /// The name of the contact.
        /// </value>
        public string ContactName { get; set; }

        /// <summary>
        /// Gets or sets the contact email.
        /// </summary>
        /// <value>
        /// The contact email.
        /// </value>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Gets or sets the contact web.
        /// </summary>
        /// <value>
        /// The contact web.
        /// </value>
        public string ContactWeb { get; set; }

        /// <summary>
        /// Gets or sets the contact phone.
        /// </summary>
        /// <value>
        /// The contact phone.
        /// </value>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Gets or sets the contact cell.
        /// </summary>
        /// <value>
        /// The contact cell.
        /// </value>
        public string ContactCell { get; set; }

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
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public CalendarModel Parent { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ContentLifecycleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public List<TaxonModel> Categories { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<TaxonModel> Tags { get; set; }

        /// <summary>
        /// Gets or sets the number of comments.
        /// </summary>
        /// <value>
        /// The number of comments.
        /// </value>
        public int CommentsCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the active.
        /// </summary>
        /// <value>
        /// true if active, false if not.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected Event OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EventModel()
        {
            Categories = new List<TaxonModel>();
            Tags = new List<TaxonModel>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public EventModel(Event sfContent)
            : base(sfContent)
        {
            if (sfContent != null)
            {
                Content = sfContent.Content;
                Summary = sfContent.Summary;
                StartDate = sfContent.EventStart;
                EndDate = sfContent.EventEnd;
                Street = sfContent.Street;
                City = sfContent.City;
                Country = sfContent.Country;
                ContactName = sfContent.ContactName;
                ContactEmail = sfContent.ContactEmail;
                ContactWeb = sfContent.ContactWeb;
                ContactPhone = sfContent.ContactPhone;
                ContactCell = sfContent.ContactCell;
                Url = sfContent.GetFullUrl(sfContent.DefaultPageId);
                Slug = sfContent.UrlName;
                Status = sfContent.Status;
                Active = sfContent.Status == ContentLifecycleStatus.Live
                    && sfContent.Visible;

                //GET PARENT CALENDAR
                if (sfContent.Parent != null)
                {
                    Parent = new CalendarModel(sfContent.Parent);
                }

                //POPULATE TAXONOMIES TO LIST
                Categories = sfContent.GetTaxa("Category");
                Tags = sfContent.GetTaxa("Tags");

                //CALCULATE COMMENTS
                CommentsCount = EventsManager.GetManager().GetComments()
                    .Count(c => c.CommentedItemID == sfContent.Id
                        && c.Status == ContentLifecycleStatus.Master);

                //CUSTOM PROPERTIES
                if (sfContent.DoesFieldExist("Image"))
                    Image = sfContent.GetValue<string>("Image");

                // Store original content
                OriginalContent = sfContent;
            }
        }
    }
}