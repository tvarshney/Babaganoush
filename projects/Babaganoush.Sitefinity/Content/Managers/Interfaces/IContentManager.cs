// file:	Content\Managers\Interfaces\IContentManager.cs
//
// summary:	Declares the IContentManager interface
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Sitefinity.Content.Managers.Interfaces
{
    /// <summary>
    /// Interface for content manager.
    /// </summary>
    /// <typeparam name="TContentModel">Type of the content model.</typeparam>
    /// <typeparam name="TContent">Type of the content.</typeparam>
    public interface IContentManager<TContentModel, TContent> : IDataManager<TContentModel, TContent>
        where TContentModel : ContentModel
        where TContent : IDataItem, IDynamicFieldsContainer
    {
        /// <summary>
        /// Gets the item by UrlName.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by name.
        /// </returns>
        TContentModel GetByName(string value,
            string providerName = null,
            Func<TContent, TContentModel> convert = null);

        /// <summary>
        /// Gets the item by title.
        /// </summary>
        /// <param name="value">The title.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by title.
        /// </returns>
        TContentModel GetByTitle(string value,
            string providerName = null,
            Func<TContent, TContentModel> convert = null);

        /// <summary>
        /// Gets the most recent item(s).
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The recent.
        /// </returns>
        IEnumerable<TContentModel> GetRecent(
            string providerName = null,
            Expression<Func<TContent, bool>> filter = null,
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);

        /// <summary>
        /// Searches the item titles and contents.
        /// </summary>
        /// <param name="value">The search string.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// An IQueryable&lt;T&gt;
        /// </returns>
        IEnumerable<TContentModel> Search(string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);

        /// <summary>
        /// Gets the items by category name.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by category.
        /// </returns>
        IEnumerable<TContentModel> GetByCategory(string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);

        /// <summary>
        /// Gets the items by tag.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by tag.
        /// </returns>
        IEnumerable<TContentModel> GetByTag(string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);

        /// <summary>
        /// Gets the items by category ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by category identifier.
        /// </returns>
        IEnumerable<TContentModel> GetByCategoryId(Guid id,
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);

        /// <summary>
        /// Gets the items by tag id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by tag identifier.
        /// </returns>
        IEnumerable<TContentModel> GetByTagId(Guid id, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);

        /// <summary>
        /// Gets by taxonomy.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The name.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by taxonomy.
        /// </returns>
        IEnumerable<TContentModel> GetByTaxonomy(string key, string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);

        /// <summary>
        /// Gets by taxonomy title.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The name.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by taxonomy title.
        /// </returns>
        IEnumerable<TContentModel> GetByTaxonomyTitle(string key, string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);

        /// <summary>
        /// Gets by taxonomy identifier.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by taxonomy identifier.
        /// </returns>
        IEnumerable<TContentModel> GetByTaxonomyId(string key, Guid id, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);
    }
}
