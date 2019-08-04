// file:	Content\Managers\Interfaces\IChildManager.cs
//
// summary:	Declares the IChildManager interface
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Sitefinity.Content.Managers.Interfaces
{
    /// <summary>
    /// Interface for child manager.
    /// </summary>
    /// <typeparam name="TContentModel">Type of the content model.</typeparam>
    /// <typeparam name="TContent">Type of the content.</typeparam>
    public interface IChildManager<TContentModel, TContent> : IContentManager<TContentModel, TContent>
        where TContentModel : ContentModel
        where TContent : IDataItem, IDynamicFieldsContainer
    {
        /// <summary>
        /// Gets documents by parent's UrlName.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by parent.
        /// </returns>
        IEnumerable<TContentModel> GetByParent(string value, 
            string providerName = null,
            Expression<Func<TContent, bool>> filter = null,
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);

        /// <summary>
        /// Gets Documents based off parent's ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by parent identifier.
        /// </returns>
        IEnumerable<TContentModel> GetByParentId(Guid id, 
            string providerName = null,
            Expression<Func<TContent, bool>> filter = null,
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);

        /// <summary>
        /// Gets the blog posts by blog title.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by parent title.
        /// </returns>
        IEnumerable<TContentModel> GetByParentTitle(string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null);
    }
}
