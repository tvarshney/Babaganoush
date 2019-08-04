using System;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for event.
    /// </summary>
    public interface IEvent : IContent, IClassified
    {
        /// <summary>
        /// DEFAULT PROPERTIES.
        /// </summary>
        ///
        /// <value>
        /// The content.
        /// </value>
        string Content { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        ///
        /// <value>
        /// The summary.
        /// </value>
        string Summary { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        ///
        /// <value>
        /// The start date.
        /// </value>
        DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        ///
        /// <value>
        /// The end date.
        /// </value>
        DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        ///
        /// <value>
        /// The street.
        /// </value>
        string Street { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        ///
        /// <value>
        /// The city.
        /// </value>
        string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        ///
        /// <value>
        /// The country.
        /// </value>
        string Country { get; set; }

        /// <summary>
        /// Gets or sets the name of the contact.
        /// </summary>
        ///
        /// <value>
        /// The name of the contact.
        /// </value>
        string ContactName { get; set; }

        /// <summary>
        /// Gets or sets the contact email.
        /// </summary>
        ///
        /// <value>
        /// The contact email.
        /// </value>
        string ContactEmail { get; set; }

        /// <summary>
        /// Gets or sets the contact web.
        /// </summary>
        ///
        /// <value>
        /// The contact web.
        /// </value>
        string ContactWeb { get; set; }

        /// <summary>
        /// Gets or sets the contact phone.
        /// </summary>
        ///
        /// <value>
        /// The contact phone.
        /// </value>
        string ContactPhone { get; set; }

        /// <summary>
        /// Gets or sets the contact cell.
        /// </summary>
        ///
        /// <value>
        /// The contact cell.
        /// </value>
        string ContactCell { get; set; }

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
        /// Gets or sets the number of comments.
        /// </summary>
        ///
        /// <value>
        /// The number of comments.
        /// </value>
        int CommentsCount { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        ///
        /// <value>
        /// The image.
        /// </value>
        string Image { get; set; }
    }
}