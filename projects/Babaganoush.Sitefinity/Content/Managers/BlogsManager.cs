// file:	Content\Managers\BlogsManager.cs
//
// summary:	Implements the blogs manager class
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using Babaganoush.Sitefinity.Content.Managers.Interfaces;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Blog manager.
    /// </summary>
    public class BlogsManager : BaseDataManager<
        Telerik.Sitefinity.Modules.Blogs.BlogsManager,
        Blog,
        BlogsManager,
        BlogModel>, IContentManager<BlogModel, Blog>
    {
        /// <summary>
        /// Gets the Sitefinity data.
        /// </summary>
        /// <param name="providerName">(Optional) the provider name to get.</param>
        /// <returns>
        /// An IEnumerable&lt;Blog&gt;
        /// </returns>
        protected override IQueryable<Blog> Get(string providerName = null)
        {
            return GetManager(providerName).GetBlogs();
        }

        /// <summary>
        /// Gets the Sitefinity data by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// A Blog.
        /// </returns>
        protected override Blog Get(Guid id, string providerName = null)
        {
            return GetManager(providerName).GetBlog(id);
        }

        /// <summary>
        /// Creates the Baba instance from the Sitefinity object.
        /// </summary>
        /// <param name="sfContent">Content of the sf.</param>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override BlogModel CreateInstance(Blog sfContent)
        {
            return new BlogModel(sfContent);
        }

        /// <summary>
        /// Gets all item.
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if
        /// you want to override the default constructor.</param>
        /// <returns>
        /// all.
        /// </returns>
        public override IEnumerable<BlogModel> GetAll(
            string providerName = null, 
            Expression<Func<Blog, bool>> filter = null, 
            int take = 0, 
            int skip = 0,
            Expression<Func<Blog, BlogModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(i => i.Status == ContentLifecycleStatus.Master);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        /// <summary>
        /// Gets the item by UrlName.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by name.
        /// </returns>
        public virtual BlogModel GetByName(string value,
            string providerName = null,
            Func<Blog, BlogModel> convert = null)
        {
            var sfItem = Get(providerName)
                .Where(i => i.UrlName.Equals(value, StringComparison.OrdinalIgnoreCase)
                    && i.Status == ContentLifecycleStatus.Master);

            return sfItem.Any()
                ? (convert != null ? convert(sfItem.FirstOrDefault()) : CreateInstance(sfItem.FirstOrDefault()))
                : null;
        }

        /// <summary>
        /// Gets the item by title.
        /// </summary>
        /// <param name="value">The title.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by title.
        /// </returns>
        public virtual BlogModel GetByTitle(string value,
            string providerName = null,
            Func<Blog, BlogModel> convert = null)
        {
            var sfItem = Get(providerName)
                .Where(i => i.Title.Equals(value, StringComparison.OrdinalIgnoreCase)
                    && i.Status == ContentLifecycleStatus.Master);

            return sfItem.Any()
                ? (convert != null ? convert(sfItem.FirstOrDefault()) : CreateInstance(sfItem.FirstOrDefault()))
                : null;
        }

        /// <summary>
        /// Gets the most recent item(s).
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The recent.
        /// </returns>
        public virtual IEnumerable<BlogModel> GetRecent(
            string providerName = null, 
            Expression<Func<Blog, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Blog, BlogModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(s => s.Status == ContentLifecycleStatus.Master);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE SORTING
            sfItems = sfItems
                .OrderByDescending(i => i.PublicationDate);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        /// <summary>
        /// Searches documents' titles and descriptions using the given value.
        /// </summary>
        /// <param name="value">The search string.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// An IQueryable&lt;BlogModel&gt;
        /// </returns>
        public virtual IEnumerable<BlogModel> Search(string value, 
            string providerName = null, 
            Expression<Func<Blog, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Blog, BlogModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(i => (i.Title.ToString().ToLower().Contains(value.ToLower())
                    || i.Description.ToString().ToLower().Contains(value.ToLower()))
                    && i.Status == ContentLifecycleStatus.Master);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        /// <summary>
        /// Gets a collection of content by taxonomy value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by taxonomy.
        /// </returns>
        public virtual IEnumerable<BlogModel> GetByTaxonomy(string key, string value, 
            string providerName = null, 
            Expression<Func<Blog, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Blog, BlogModel>> convert = null)
        {
            //GET TAXONOMY BY NAME
            var taxonomy = TaxonomyManager.GetManager(providerName)
                .GetTaxa<Taxon>()
                .FirstOrDefault(t => t.Name == value);

            //RETURN VALUE
            return taxonomy != null
                ? GetByTaxonomyId(key, taxonomy.Id, providerName, filter, take, skip, convert)
                : Enumerable.Empty<BlogModel>().AsQueryable();
        }

        /// <summary>
        /// Gets a collection of content by taxonomy title.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by taxonomy title.
        /// </returns>
        public virtual IEnumerable<BlogModel> GetByTaxonomyTitle(string key, string value, 
            string providerName = null, 
            Expression<Func<Blog, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Blog, BlogModel>> convert = null)
        {
            //GET TAXONOMY BY NAME
            var taxonomy = TaxonomyManager.GetManager(providerName)
                .GetTaxa<Taxon>()
                .FirstOrDefault(t => t.Title == value);

            //RETURN VALUE
            return taxonomy != null
                ? GetByTaxonomyId(key, taxonomy.Id, providerName, filter, take, skip, convert)
                : Enumerable.Empty<BlogModel>().AsQueryable();
        }

        /// <summary>
        /// Gets a collection of content by taxonomy id.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by taxonomy identifier.
        /// </returns>
        public virtual IEnumerable<BlogModel> GetByTaxonomyId(string key, Guid id, 
            string providerName = null, 
            Expression<Func<Blog, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Blog, BlogModel>> convert = null)
        {
            IQueryable<Blog> sfItems = Enumerable.Empty<Blog>().AsQueryable();

            if (ContentHelper.DoesTypeContainField(typeof(Blog), key))
            {
                sfItems = Get(providerName)
                    .Where(i => i.GetValue<TrackedList<Guid>>(key).Contains(id)
                        && i.Status == ContentLifecycleStatus.Master);

                //ADD OPTIONAL FILTERS IF APPLICABLE
                if (filter != null)
                    sfItems = sfItems.Where(filter);

                //HANDLE PAGING IF APPLICABLE
                if (skip > 0) sfItems = sfItems.Skip(skip);
                if (take > 0) sfItems = sfItems.Take(take);
            }

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        /// <summary>
        /// Gets the items by category name.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by category.
        /// </returns>
        public virtual IEnumerable<BlogModel> GetByCategory(string value, 
            string providerName = null, 
            Expression<Func<Blog, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Blog, BlogModel>> convert = null)
        {
            return GetByTaxonomy("Category", value, providerName, filter, take, skip, convert);
        }

        /// <summary>
        /// Gets the items by category ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by category identifier.
        /// </returns>
        public virtual IEnumerable<BlogModel> GetByCategoryId(Guid id, 
            string providerName = null, 
            Expression<Func<Blog, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Blog, BlogModel>> convert = null)
        {
            return GetByTaxonomyId("Category", id, providerName, filter, take, skip, convert);
        }

        /// <summary>
        /// Gets the items by tag.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by tag.
        /// </returns>
        public virtual IEnumerable<BlogModel> GetByTag(string value, 
            string providerName = null, 
            Expression<Func<Blog, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Blog, BlogModel>> convert = null)
        {
            return GetByTaxonomy("Tags", value, providerName, filter, take, skip, convert);
        }

        /// <summary>
        /// Gets the items by tag id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by tag identifier.
        /// </returns>
        public virtual IEnumerable<BlogModel> GetByTagId(Guid id, 
            string providerName = null, 
            Expression<Func<Blog, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Blog, BlogModel>> convert = null)
        {
            return GetByTaxonomyId("Tag", id, providerName, filter, take, skip, convert);
        }
    }
}