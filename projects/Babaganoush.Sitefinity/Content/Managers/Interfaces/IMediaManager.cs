// file:	Content\Managers\Interfaces\IMediaManager.cs
//
// summary:	Declares the IMediaManager interface
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Sitefinity.Content.Managers.Interfaces
{
    /// <summary>
    /// Interface for media manager.
    /// </summary>
    /// <typeparam name="TContentModel">Type of the content model.</typeparam>
    /// <typeparam name="TContent">Type of the content.</typeparam>
    public interface IMediaManager<TContentModel, TContent> : IChildManager<TContentModel, TContent>
        where TContentModel : MediaModel
        where TContent : IDataItem, IDynamicFieldsContainer
    {
        /// <summary>
        /// Gets the documents by extension.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by extension.
        /// </returns>
        IEnumerable<TContentModel> GetByExtension(string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);
    }
}
