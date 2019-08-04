// file:	Api\BlogPostsController.cs
//
// summary:	Implements the blog posts controller class
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using Telerik.Sitefinity.Blogs.Model;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for blog posts.
    /// </summary>
    public class BlogPostsController : BaseChildController<BlogPostModel, BlogPost>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlogPostsController" /> class.
        /// </summary>
        public BlogPostsController()
            : base(BabaManagers.BlogPosts)
        {

        }
    }
}