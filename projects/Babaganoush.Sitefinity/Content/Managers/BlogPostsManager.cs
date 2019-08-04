// file:	Content\Managers\BlogPostsManager.cs
//
// summary:	Implements the blog posts manager class
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.GenericContent.Model;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Manager for blog posts.
    /// </summary>
    public class BlogPostsManager : BaseChildManager<
        Telerik.Sitefinity.Modules.Blogs.BlogsManager,
        BlogPost,
        BlogPostsManager,
        BlogPostModel>
    {
        /// <summary>
        /// Gets the Sitefinity data.
        /// </summary>
        /// <param name="providerName">(Optional) the provider name to get.</param>
        /// <returns>
        /// An IQueryable&lt;BlogPost&gt;
        /// </returns>
        protected override IQueryable<BlogPost> Get(string providerName = null)
        {
            return GetManager(providerName).GetBlogPosts();
        }

        /// <summary>
        /// Gets the Sitefinity data by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// A BlogPost.
        /// </returns>
        protected override BlogPost Get(Guid id, string providerName = null)
        {
            return GetManager(providerName).GetBlogPost(id);
        }

        /// <summary>
        /// Creates the Baba instance from the Sitefinity object.
        /// </summary>
        /// <param name="sfContent">Content of the sf.</param>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override BlogPostModel CreateInstance(BlogPost sfContent)
        {
            return new BlogPostModel(sfContent);
        }

        /// <summary>
        /// Searches blog posts' title and content.
        /// </summary>
        /// <param name="value">The search string.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// An IQueryable&lt;BlogPostModel&gt;
        /// </returns>
        public override IEnumerable<BlogPostModel> Search(string value, 
            string providerName = null, 
            Expression<Func<BlogPost, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<BlogPost, BlogPostModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(i => (i.Title.ToString().ToLower().Contains(value.ToLower())
                    || i.Content.ToString().ToLower().Contains(value.ToLower()))
                    && i.Status == ContentLifecycleStatus.Live
                    && i.Visible);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }
    }
}