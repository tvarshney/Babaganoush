// file:	Api\BlogsController.cs
//
// summary:	Implements the blogs controller class
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using Telerik.Sitefinity.Blogs.Model;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for blogs.
    /// </summary>
    public class BlogsController : BaseContentController<BlogModel, Blog>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlogsController" /> class.
        /// </summary>
        public BlogsController()
            : base(BabaManagers.Blogs)
        {

        }
    }
}