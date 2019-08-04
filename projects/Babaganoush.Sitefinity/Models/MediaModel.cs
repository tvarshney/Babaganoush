// file:	Models\MediaModel.cs
//
// summary:	Implements the media model class
using Babaganoush.Core.Utilities;
using Babaganoush.Core.Utilities.Interfaces;
using Babaganoush.Sitefinity.Extensions;
using System.Collections.Generic;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the media.
    /// </summary>
    public class MediaModel : ContentModel
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the ordinal.
        /// </summary>
        /// <value>
        /// The ordinal.
        /// </value>
        public float Ordinal { get; set; }

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
        /// Gets or sets the extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the type of the mime.
        /// </summary>
        /// <value>
        /// The type of the mime.
        /// </value>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the total number of size.
        /// </summary>
        /// <value>
        /// The total number of size.
        /// </value>
        public long TotalSize { get; set; }

        /// <summary>
        /// Gets or sets the number of views.
        /// </summary>
        /// <value>
        /// The number of views.
        /// </value>
        public int ViewsCount { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public string File { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is primary.
        /// </summary>
        /// <value>
        /// true if this object is primary, false if not.
        /// </value>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public LibraryModel Parent { get; set; }

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
        /// Gets or sets a value indicating whether the active.
        /// </summary>
        /// <value>
        /// true if active, false if not.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets URL of the media.
        /// </summary>
        /// <value>
        /// The media URL.
        /// </value>
        public string MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail.
        /// </summary>
        /// <value>
        /// The thumbnail.
        /// </value>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MediaModel()
        {
            Categories = new List<TaxonModel>();
            Tags = new List<TaxonModel>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public MediaModel(MediaContent sfContent)
            : base(sfContent)
        {
            // TODO: Extract to factory class
            IWebHelper webHelper = new WebHelper();
            if (sfContent != null)
            {
                Description = sfContent.Description;
                Author = sfContent.Author;
                Ordinal = sfContent.Ordinal;
                Url = webHelper.ResolveUrl("~" + sfContent.Urls[0].Url);
                Slug = sfContent.UrlName;
                Extension = sfContent.Extension;
                MimeType = sfContent.MimeType;
                TotalSize = sfContent.TotalSize;
                ViewsCount = sfContent.ViewsCount;
                Parent = new LibraryModel(sfContent.Parent);
                Status = sfContent.Status;
                Active = sfContent.Status == ContentLifecycleStatus.Live
                    && sfContent.Visible;

                // TODO: Sometimes gets null exception, find better prevention instead of try/catch. Works when retrieving single item but not collection.
                try { ThumbnailUrl = sfContent.ThumbnailUrl; }
                catch { ThumbnailUrl = Url; }

                // TODO: Sometimes gets null exception, find better prevention instead of try/catch. Works when retrieving single item but not collection.
                try { MediaUrl = sfContent.MediaUrl; }
                catch { MediaUrl = Url; }

                // Populate taxonomies to list
                Categories = sfContent.GetTaxa("Category");
                Tags = sfContent.GetTaxa("Tags");
            }
        }
    }
}