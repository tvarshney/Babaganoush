// file:	Models\BlogPostModel.cs
//
// summary:	Implements the blog post model class
using Babaganoush.Sitefinity.Extensions;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Blogs;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the blog post.
    /// </summary>
    public class BlogPostModel : ContentModel
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
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public BlogModel Parent { get; set; }

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
        protected BlogPost OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BlogPostModel()
        {
            Categories = new List<TaxonModel>();
            Tags = new List<TaxonModel>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public BlogPostModel(BlogPost sfContent)
            : base(sfContent)
        {
            if (sfContent != null)
            {
                Content = sfContent.Content;
                Summary = sfContent.Summary;
                Status = sfContent.Status;
                Slug = sfContent.UrlName;
                Active = sfContent.Status == ContentLifecycleStatus.Live
                    && sfContent.Visible;

                //GET PARENT BLOG
                Parent = new BlogModel(sfContent.Parent);

                //CONSTRUCT URL BASED ON PARENT BLOG
                Url = sfContent.GetFullUrl(sfContent.DefaultPageId);
                if (sfContent.Parent.DefaultPageId.HasValue)
                {
                    Url = Parent.Url + Url;
                }

                //POPULATE TAXONOMIES TO LIST
                Categories = sfContent.GetTaxa("Category");
                Tags = sfContent.GetTaxa("Tags");

                //CALCULATE COMMENTS
                CommentsCount = BlogsManager.GetManager().GetComments()
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