// file:	Content\Managers\Abstracts\BaseChildManager.cs
//
// summary:	Implements the base child manager class
using Babaganoush.Sitefinity.Content.Managers.Interfaces;
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Sitefinity.Content.Managers.Abstracts
{
    /// <summary>
    /// BaseChildManager.
    /// </summary>
    /// <typeparam name="TManager">Type of the manager.</typeparam>
    /// <typeparam name="TContent">Type of the content.</typeparam>
    /// <typeparam name="TBaseManager">Type of the base manager.</typeparam>
    /// <typeparam name="TContentModel">Type of the content model.</typeparam>
    public abstract class BaseChildManager<TManager, TContent, TBaseManager, TContentModel> : BaseContentManager<TManager, TContent, TBaseManager, TContentModel>, IChildManager<TContentModel, TContent>
        where TManager : class, IManager
        where TContent : IContent, IDynamicFieldsContainer, ILifecycleDataItem, ILocatable, ILocalizable, IHasParent
        where TBaseManager : BaseManager<TManager>, new()
        where TContentModel : ContentModel, new()
    {
        /// <summary>
        /// Sort results.
        /// </summary>
        /// <param name="sfContents">The sf contents.</param>
        /// <returns>
        /// The sorted results.
        /// </returns>
        protected virtual IOrderedQueryable<TContent> SortResults(IQueryable<TContent> sfContents)
        {
            return sfContents.OrderByDescending(i => i.PublicationDate);
        }

        /// <summary>
        /// Gets items by parent's UrlName.
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
        public virtual IEnumerable<TContentModel> GetByParent(string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(s => s.Parent.UrlName.Equals(value, StringComparison.OrdinalIgnoreCase)
                    && s.Status == ContentLifecycleStatus.Live && (s as IContent).Visible);

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

        /// <summary>
        /// Gets items based off parent's ID.
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
        public virtual IEnumerable<TContentModel> GetByParentId(Guid id, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(s => (s.Parent.Id == id)
                    && s.Status == ContentLifecycleStatus.Live
                    && (s as IContent).Visible);

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

        /// <summary>
        /// Gets the items by parent title.
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
        public virtual IEnumerable<TContentModel> GetByParentTitle(string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(s => s.Parent.Title.Equals(value, StringComparison.OrdinalIgnoreCase)
                    && s.Status == ContentLifecycleStatus.Live
                    && (s as IContent).Visible);

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
