// file:	Content\Managers\Interfaces\IDynamicManager.cs
//
// summary:	Declares the IDynamicManager interface
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Telerik.Sitefinity.DynamicModules.Model;

namespace Babaganoush.Sitefinity.Content.Managers.Interfaces
{
    /// <summary>
    /// Interface for dynamic manager.
    /// </summary>
    /// <typeparam name="TDynamicModel">Type of the dynamic model.</typeparam>
    public interface IDynamicManager<TDynamicModel> : IDataManager<TDynamicModel, DynamicContent>
        where TDynamicModel : DynamicModel
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
        TDynamicModel GetByName(string value,
            string providerName = null,
            Func<DynamicContent, TDynamicModel> convert = null);

        /// <summary>
        /// Gets the item by title.
        /// </summary>
        /// <param name="value">The title.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by title.
        /// </returns>
        TDynamicModel GetByTitle(string value,
            string providerName = null,
            Func<DynamicContent, TDynamicModel> convert = null);

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
        IEnumerable<TDynamicModel> GetRecent(
            string providerName = null, 
            Expression<Func<DynamicContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<DynamicContent, TDynamicModel>> convert = null);

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
        IEnumerable<TDynamicModel> GetByTaxonomy(string key, string value, 
            string providerName = null, 
            Expression<Func<DynamicContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<DynamicContent, TDynamicModel>> convert = null);

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
        IEnumerable<TDynamicModel> GetByTaxonomyTitle(string key, string value, 
            string providerName = null, 
            Expression<Func<DynamicContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<DynamicContent, TDynamicModel>> convert = null);

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
        IEnumerable<TDynamicModel> GetByTaxonomyId(string key, Guid id, 
            string providerName = null, 
            Expression<Func<DynamicContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<DynamicContent, TDynamicModel>> convert = null);
    }
}
