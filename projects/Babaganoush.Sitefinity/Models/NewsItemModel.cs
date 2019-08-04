// file:	Models\NewsItemModel.cs
//
// summary:	Implements the news item model class
using Babaganoush.Sitefinity.Extensions;
using Babaganoush.Sitefinity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the news item.
    /// </summary>
    public class NewsItemModel : ContentModel
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
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the name of the source.
        /// </summary>
        /// <value>
        /// The name of the source.
        /// </value>
        public string SourceName { get; set; }

        /// <summary>
        /// Gets or sets source site.
        /// </summary>
        /// <value>
        /// The source site.
        /// </value>
        public string SourceSite { get; set; }

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
        protected NewsItem OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NewsItemModel()
        {
            Categories = new List<TaxonModel>();
            Tags = new List<TaxonModel>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public NewsItemModel(NewsItem sfContent)
            : base(sfContent)
        {
            if (sfContent != null)
            {
                Content = sfContent.Content;
                Summary = sfContent.Summary;
                Author = sfContent.Author;
                SourceName = sfContent.SourceName;
                SourceSite = sfContent.SourceSite;
                Url = sfContent.GetFullUrl(sfContent.DefaultPageId);
                Slug = sfContent.UrlName;
                Status = sfContent.Status;
                Active = sfContent.Status == ContentLifecycleStatus.Live
                    && sfContent.Visible;

                //POPULATE TAXONOMIES TO LIST
                Categories = sfContent.GetTaxa("Category");
                Tags = sfContent.GetTaxa("Tags");

                //CALCULATE COMMENTS
                CommentsCount = NewsManager.GetManager().GetComments()
                    .Count(c => c.CommentedItemID == sfContent.Id
                        && c.Status == ContentLifecycleStatus.Master);

                //CUSTOM PROPERTIES
                if (sfContent.DoesFieldExist("Image"))
                    Image = sfContent.GetValue<string>("Image");

                // Store original content
                OriginalContent = sfContent;
            }
        }

        /// <summary>
        /// Convert to Sitefinity object.
        /// </summary>
        /// <returns>
        /// This object as a sItem.
        /// </returns>
        public NewsItem ToSitefinityModel()
        {
            var manager = NewsManager.GetManager();
            NewsItem sfContent = null;

            //GET MODEL FROM SF IF EXISTS
            if (Id != Guid.Empty)
            {
                //GET LIVE ITEM FROM STORAGE
                sfContent = manager.GetNewsItem(Id);

                //CHECK OUT ITEM FOR UPDATE IF APPLICABLE
                if (sfContent != null)
                {
                    //EDIT MODE ON CONTENT AND RETURNED CHECKED OUT ITEM
                    var master = manager.Lifecycle.Edit(sfContent) as NewsItem;
                    sfContent = manager.Lifecycle.CheckOut(master) as NewsItem;
                }
            }

            if (sfContent == null)
            {
                //CREATE NEW MODEL
                sfContent = manager.CreateNewsItem();

                //SET DEFAULT DATA
                sfContent.DateCreated = DateCreated = DateTime.UtcNow;

                //GENERATE URL FROM TITLE IF APPLICABLE
                if (!string.IsNullOrWhiteSpace(Slug))
                {
                    sfContent.UrlName = Slug;
                }
                else if (!string.IsNullOrWhiteSpace(Title))
                {
                    sfContent.UrlName = Slug = ContentHelper.GenerateUrlName(Title);
                }
            }

            //MERGE CUSTOM PROPERTIES
            sfContent.Title = Title;
            sfContent.Content = Content;
            sfContent.Summary = Summary;
            sfContent.Author = Author;
            sfContent.SourceName = SourceName;
            sfContent.SourceSite = SourceSite;

            sfContent.SetTaxa("Category", Categories);
            sfContent.SetTaxa("Tags", Tags);

            //RETURN SITEFINITY MODEL
            return sfContent;
        }
    }
}