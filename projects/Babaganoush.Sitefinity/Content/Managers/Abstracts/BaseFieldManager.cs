// file:	Content\Managers\Abstracts\BaseFieldManager.cs
//
// summary:	Implements the base field manager class
using Babaganoush.Sitefinity.Models.Interfaces;
using Babaganoush.Sitefinity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Babaganoush.Sitefinity.Content.Managers.Abstracts
{
    /// <summary>
    /// Base manager for dynamic field content that produces several plumbing for retrieval.
    /// </summary>
    /// <typeparam name="TManager">Type of the manager.</typeparam>
    /// <typeparam name="TContent">Type of the content.</typeparam>
    /// <typeparam name="TBaseManager">Type of the base manager.</typeparam>
    /// <typeparam name="TContentModel">Type of the content model.</typeparam>
    public abstract class BaseFieldManager<TManager, TContent, TBaseManager, TContentModel> : BaseDataManager<TManager, TContent, TBaseManager, TContentModel>
        where TManager : class, IManager
        where TContent : IDynamicFieldsContainer, ILifecycleDataItem, ILocatable, ILocalizable
        where TBaseManager : BaseManager<TManager>, new()
        where TContentModel : IDataModel, new()
    {
        /// <summary>
        /// Gets the type of the dynamic.
        /// </summary>
        /// <returns>
        /// The dynamic type.
        /// </returns>
        public virtual Type GetDynamicType()
        {
            return typeof(TContent);
        }

        /// <summary>
        /// Gets collection of <typeparamref name="TFieldType"/> by field value.
        /// </summary>
        /// <typeparam name="TFieldType">Type of the field type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by field.
        /// </returns>
        public virtual IEnumerable<TContentModel> GetByField<TFieldType>(string key, TFieldType value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null) where TFieldType : class
        {
            IQueryable<TContent> sfItems = Enumerable.Empty<TContent>().AsQueryable();

            if (ContentHelper.DoesTypeContainField(GetDynamicType(), key))
            {
                //HANDLE STRING SPECIALLY
                if (typeof(TFieldType) == typeof(string))
                {
                    //VALIDATE INPUT
                    if (value == null)
                        value = (TFieldType)(object)string.Empty;

                    //IMPLEMENT CASE INSENSITIVE
                    sfItems = Get(providerName).Where(
                       i => i.GetValue<string>(key).ToLower()
                           == value.ToString().ToLower());
                }
                else
                {
                    sfItems = Get(providerName).Where(i => i.GetValue<TFieldType>(key) == value);
                }

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
        /// Gets a collection of <typeparamref name="TContentModel"/> by taxonomy value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by taxonomy.
        /// </returns>
        public virtual IEnumerable<TContentModel> GetByTaxonomy(string key, string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
        {
            //GET TAXONOMY BY NAME
            var taxonomy = TaxonomyManager.GetManager(providerName)
                .GetTaxa<Taxon>()
                .FirstOrDefault(t => t.Name == value);

            //RETURN VALUE
            return taxonomy != null
                ? GetByTaxonomyId(key, taxonomy.Id, providerName, filter, take, skip, convert)
                : Enumerable.Empty<TContentModel>().AsQueryable();
        }

        /// <summary>
        /// Gets a collection of <typeparamref name="TContentModel"/> by taxonomy title.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by taxonomy title.
        /// </returns>
        public virtual IEnumerable<TContentModel> GetByTaxonomyTitle(string key, string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
        {
            //GET TAXONOMY BY NAME
            var taxonomy = TaxonomyManager.GetManager(providerName)
                .GetTaxa<Taxon>()
                .FirstOrDefault(t => t.Title == value);

            //RETURN VALUE
            return taxonomy != null
                ? GetByTaxonomyId(key, taxonomy.Id, providerName, filter, take, skip, convert)
                : Enumerable.Empty<TContentModel>().AsQueryable();
        }

        /// <summary>
        /// Gets a collection of <typeparamref name="TContentModel"/> by taxonomy id.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by taxonomy identifier.
        /// </returns>
        public virtual IEnumerable<TContentModel> GetByTaxonomyId(string key, Guid id, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
        {
            IQueryable<TContent> sfItems = Enumerable.Empty<TContent>().AsQueryable();

            if (ContentHelper.DoesTypeContainField(GetDynamicType(), key))
            {
                sfItems = Get(providerName)
                    .Where(i => i.GetValue<TrackedList<Guid>>(key).Contains(id)
                        && i.Status == ContentLifecycleStatus.Live);

                // Handle visibility of content items if applicable
                if (sfItems.FirstOrDefault() is IContent)
                {
                    sfItems = sfItems.Where(i => (i as IContent).Visible);
                }

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
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by category.
        /// </returns>
        public virtual IEnumerable<TContentModel> GetByCategory(string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
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
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by category identifier.
        /// </returns>
        public virtual IEnumerable<TContentModel> GetByCategoryId(Guid id, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
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
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by tag.
        /// </returns>
        public virtual IEnumerable<TContentModel> GetByTag(string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
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
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by tag identifier.
        /// </returns>
        public virtual IEnumerable<TContentModel> GetByTagId(Guid id, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
        {
            return GetByTaxonomyId("Tag", id, providerName, filter, take, skip, convert);
        }
    }
}
