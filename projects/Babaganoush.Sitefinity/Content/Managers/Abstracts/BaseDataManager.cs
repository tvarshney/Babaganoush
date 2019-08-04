// file:	Content\Managers\Abstracts\BaseDataManager.cs
//
// summary:	Implements the base data manager class
using Babaganoush.Sitefinity.Content.Managers.Interfaces;
using Babaganoush.Sitefinity.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Sitefinity.Content.Managers.Abstracts
{
    /// <summary>
    /// Base manager for content that produces several plumbing for retrieval.
    /// </summary>
    /// <typeparam name="TManager">Type of the manager.</typeparam>
    /// <typeparam name="TDataItem">Type of the data item.</typeparam>
    /// <typeparam name="TBaseManager">Type of the base manager.</typeparam>
    /// <typeparam name="TDataModel">Type of the data model.</typeparam>
    public abstract class BaseDataManager<TManager, TDataItem, TBaseManager, TDataModel> : BaseSingletonManager<TManager, TBaseManager>, IDataManager<TDataModel, TDataItem>
        where TManager : class, IManager
        where TDataItem : IDataItem
        where TBaseManager : BaseManager<TManager>, new()
        where TDataModel : IDataModel, new()
    {
        /// <summary>
        /// Gets the Sitefinity data.
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// An IQueryable&lt;TDataItem&gt;
        /// </returns>
        protected abstract IQueryable<TDataItem> Get(string providerName = null);

        /// <summary>
        /// Gets the Sitefinity data by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// A TDataItem.
        /// </returns>
        protected abstract TDataItem Get(Guid id, string providerName = null);

        /// <summary>
        /// Creates the Baba instance from the Sitefinity object.
        /// </summary>
        /// <param name="sfContent">Content of the sf.</param>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected abstract TDataModel CreateInstance(TDataItem sfContent);

        /// <summary>
        /// Gets all item.
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// all.
        /// </returns>
        public abstract IEnumerable<TDataModel> GetAll(
            string providerName = null, 
            Expression<Func<TDataItem, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TDataItem, TDataModel>> convert = null);

        /// <summary>
        /// Gets items matching the given IDs.
        /// </summary>
        /// <param name="ids">The IDs of the items to get.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the identifiers in this collection.
        /// </returns>
        public IEnumerable<TDataModel> GetByIds(IEnumerable<Guid> ids, 
            string providerName = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TDataItem, TDataModel>> convert = null)
        {
            return ids != null
                ? GetAll(providerName, d => ids.Contains(d.Id), take, skip, convert)
                : Enumerable.Empty<TDataModel>();
        }

        /// <summary>
        /// Gets the item by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by identifier.
        /// </returns>
        public virtual TDataModel GetById(Guid id,
            string providerName = null,
            Func<TDataItem, TDataModel> convert = null)
        {
            var sfContent = Get(id, providerName);
            return convert != null ? convert(sfContent) : CreateInstance(sfContent);
        }
    }
}
