// file:	Content\Managers\Abstracts\BaseContentManager.cs
//
// summary:	Implements the base content manager class
using Babaganoush.Sitefinity.Content.Managers.Interfaces;
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Sitefinity.Content.Managers.Abstracts
{
    /// <summary>
    /// Base manager for content that produces several plumbing for retrieval.
    /// </summary>
    /// <typeparam name="TManager">Type of the manager.</typeparam>
    /// <typeparam name="TContent">Type of the content.</typeparam>
    /// <typeparam name="TBaseManager">Type of the base manager.</typeparam>
    /// <typeparam name="TContentModel">Type of the content model.</typeparam>
    public abstract class BaseContentManager<TManager, TContent, TBaseManager, TContentModel> : BaseFieldManager<TManager, TContent, TBaseManager, TContentModel>, IContentManager<TContentModel, TContent>
        where TManager : class, IManager
        where TContent : IContent, IDynamicFieldsContainer, ILifecycleDataItem, ILocatable, ILocalizable
        where TBaseManager : BaseManager<TManager>, new()
        where TContentModel : ContentModel, new()
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
        public override IEnumerable<TContentModel> GetAll(
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(i => i.Status == ContentLifecycleStatus.Live
                    && (i as IContent).Visible);

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
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by name.
        /// </returns>
        public virtual TContentModel GetByName(string value,
            string providerName = null,
            Func<TContent, TContentModel> convert = null)
        {
            var sfItem = Get(providerName)
                .Where(i => i.UrlName.Equals(value, StringComparison.OrdinalIgnoreCase)
                    && i.Status == ContentLifecycleStatus.Live
                    && (i as IContent).Visible);

            return sfItem.Any()
                ? (convert != null ? convert(sfItem.FirstOrDefault()) : CreateInstance(sfItem.FirstOrDefault()))
                : null;
        }

        /// <summary>
        /// Gets the item by title.
        /// </summary>
        /// <param name="value">The title.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by title.
        /// </returns>
        public virtual TContentModel GetByTitle(string value,
            string providerName = null,
            Func<TContent, TContentModel> convert = null)
        {
            var sfItem = Get(providerName)
                .Where(i => i.Title.Equals(value, StringComparison.OrdinalIgnoreCase)
                    && i.Status == ContentLifecycleStatus.Live
                    && (i as IContent).Visible);

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
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The recent.
        /// </returns>
        public virtual IEnumerable<TContentModel> GetRecent(
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(s => s.Status == ContentLifecycleStatus.Live
                    && (s as IContent).Visible);

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
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// An IQueryable&lt;TContentModel&gt;
        /// </returns>
        public virtual IEnumerable<TContentModel> Search(string value, 
            string providerName = null, 
            Expression<Func<TContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<TContent, TContentModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(i => (i.Title.ToString().ToLower().Contains(value.ToLower())
                    || i.Description.ToString().ToLower().Contains(value.ToLower()))
                    && i.Status == ContentLifecycleStatus.Live
                    && (i as IContent).Visible);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        //MAYBE NOT NEEDED SINCE SITEFINITY'S FLUENT API WORKS OK IN THIS SCENARIO
        //public virtual V Create(V value)
        //{
        //    try
        //    {
        //        //CONVERT CURRENT MODEL TO SITEFINITY MODEL
        //        var sfContent = value.ToSitefinityModel();

        //        //UPDATE PUBLICATION DATE
        //        sfContent.PublicationDate = DateTime.UtcNow;

        //        //Recompiles and validates the url of the item.
        //        this.Manager.RecompileAndValidateUrls(sfContent);

        //        // We can now call the following to publish the item
        //        this.Manager.Lifecycle.Publish(sfContent);

        //        //You need to set appropriate workflow status
        //        sfContent.SetWorkflowStatus(this.Manager.Provider.ApplicationName, "Published");

        //        //Save the changes.
        //        this.Manager.SaveChanges();

        //        //UPDATE ID
        //        return CreateInstance(i);
        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO: LOG ERROR
        //        return null;
        //    }
        //}

    }
}
