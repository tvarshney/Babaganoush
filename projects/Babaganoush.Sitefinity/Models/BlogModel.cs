// file:	Models\BlogModel.cs
//
// summary:	Implements the blog model class
using Babaganoush.Sitefinity.Extensions;
using System.Linq;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Blogs;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the blog.
    /// </summary>
    public class BlogModel : ContentModel
    {
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
        /// Gets or sets the number of posts.
        /// </summary>
        /// <value>
        /// The number of posts.
        /// </value>
        public int PostsCount { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ContentLifecycleStatus Status { get; set; }

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
        protected Blog OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BlogModel()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public BlogModel(Blog sfContent)
            : base(sfContent)
        {
            if (sfContent != null)
            {
                Description = sfContent.Description;
                Status = sfContent.Status;
                Url = sfContent.GetFullUrl(sfContent.DefaultPageId);
                Slug = sfContent.UrlName;
                Active = sfContent.Status == ContentLifecycleStatus.Live
                    && sfContent.Visible;

                //CALCULATE COMMENTS
                PostsCount = BlogsManager.GetManager().GetBlogPosts()
                    .Count(c => c.Parent.Id == sfContent.Id
                        && c.Status == ContentLifecycleStatus.Live
                        && c.Visible);


                //CUSTOM PROPERTIES
                if (sfContent.DoesFieldExist("Image"))
                    Image = sfContent.GetValue<string>("Image");

                // Store original content
                OriginalContent = sfContent;
            }
        }
    }
}