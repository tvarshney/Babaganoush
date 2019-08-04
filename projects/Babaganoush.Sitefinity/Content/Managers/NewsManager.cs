// file:	Content\Managers\NewsManager.cs
//
// summary:	Implements the news manager class
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.News.Model;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Manager for news.
    /// </summary>
    public class NewsManager : BaseContentManager<
        Telerik.Sitefinity.Modules.News.NewsManager,
        NewsItem,
        NewsManager,
        NewsItemModel>
    {
        /// <summary>
        /// Gets the Sitefinity data.
        /// </summary>
        /// <param name="providerName">(Optional) the provider name to get.</param>
        /// <returns>
        /// An IQueryable&lt;NewsItem&gt;
        /// </returns>
        protected override IQueryable<NewsItem> Get(string providerName = null)
        {
            return GetManager(providerName).GetNewsItems();
        }

        /// <summary>
        /// Gets the Sitefinity data by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// A sItem.
        /// </returns>
        protected override NewsItem Get(Guid id, string providerName = null)
        {
            return GetManager(providerName).GetNewsItem(id);
        }

        /// <summary>
        /// Creates the Baba instance from the Sitefinity object.
        /// </summary>
        /// <param name="sfContent">Content of the sf.</param>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override NewsItemModel CreateInstance(NewsItem sfContent)
        {
            return new NewsItemModel(sfContent);
        }

        /// <summary>
        /// Searches the item titles and contents.
        /// </summary>
        /// <param name="value">The search string.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// An IQueryable&lt;NewsItemModel&gt;
        /// </returns>
        public override IEnumerable<NewsItemModel> Search(string value, 
            string providerName = null, 
            Expression<Func<NewsItem, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<NewsItem, NewsItemModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(i => (i.Title.ToString().ToLower().Contains(value.ToLower())
                    || i.Content.ToString().ToLower().Contains(value.ToLower()))
                    && i.Status == ContentLifecycleStatus.Live
                    && i.Visible);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }
    }
}