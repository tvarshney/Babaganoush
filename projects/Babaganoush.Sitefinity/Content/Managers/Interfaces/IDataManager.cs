// file:	Content\Managers\Interfaces\IDataManager.cs
//
// summary:	Declares the IDataManager interface
using Babaganoush.Sitefinity.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Sitefinity.Content.Managers.Interfaces
{
    /// <summary>
    /// Interface for data manager.
    /// </summary>
    /// <typeparam name="TContentModel">Type of the content model.</typeparam>
    /// <typeparam name="TContent">Type of the content.</typeparam>
    public interface IDataManager<TContentModel, TContent>
        where TContentModel : IDataModel
        where TContent : IDataItem
    {
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
        IEnumerable<TContentModel> GetAll(
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);

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
        IEnumerable<TContentModel> GetByIds(IEnumerable<Guid> ids, 
            string providerName = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);

        /// <summary>
        /// Gets the item by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by identifier.
        /// </returns>
        TContentModel GetById(Guid id,
            string providerName = null,
            Func<TContent, TContentModel> convert = null);
    }
}
