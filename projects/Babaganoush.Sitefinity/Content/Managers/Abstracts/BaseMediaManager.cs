// file:	Content\Managers\Abstracts\BaseMediaManager.cs
//
// summary:	Implements the base media manager class
using Babaganoush.Sitefinity.Content.Managers.Interfaces;
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;

namespace Babaganoush.Sitefinity.Content.Managers.Abstracts
{
    /// <summary>
    /// Manager for base medias.
    /// </summary>
    /// <typeparam name="TManager">Type of the manager.</typeparam>
    /// <typeparam name="TContent">Type of the content.</typeparam>
    /// <typeparam name="TBaseManager">Type of the base manager.</typeparam>
    /// <typeparam name="TContentModel">Type of the content model.</typeparam>
    public abstract class BaseMediaManager<TManager, TContent, TBaseManager, TContentModel> : BaseChildManager<TManager, TContent, TBaseManager, TContentModel>, IMediaManager<TContentModel, TContent>
        where TManager : class, IManager
        where TContent : MediaContent
        where TBaseManager : BaseManager<TManager>, new()
        where TContentModel : MediaModel, new()
    {
        /// <summary>
        /// Sort results.
        /// </summary>
        /// <param name="sfContents">The sf contents.</param>
        /// <returns>
        /// The sorted results.
        /// </returns>
        protected override IOrderedQueryable<TContent> SortResults(IQueryable<TContent> sfContents)
        {
            return sfContents.OrderBy(i => i.Ordinal);
        }

        /// <summary>
        /// Gets the media by extension.
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
        public virtual IEnumerable<TContentModel> GetByExtension(string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(s => s.Extension.Equals(value, StringComparison.OrdinalIgnoreCase)
                    && s.Status == ContentLifecycleStatus.Live
                    && (s as MediaContent).Visible);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE SORTING
            sfItems = SortResults(sfItems);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }
    }
}
