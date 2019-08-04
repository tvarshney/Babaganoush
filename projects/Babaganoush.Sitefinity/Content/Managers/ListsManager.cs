// file:	Content\Managers\ListsManager.cs
//
// summary:	Implements the lists manager class
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using Babaganoush.Sitefinity.Models;
using System;
using System.Linq;
using Telerik.Sitefinity.Lists.Model;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Manager for lists.
    /// </summary>
    public class ListsManager : BaseChildManager<
        Telerik.Sitefinity.Modules.Lists.ListsManager,
        ListItem,
        ListsManager,
        ListItemModel>
    {
        /// <summary>
        /// Sort results.
        /// </summary>
        /// <param name="sfContents">The sf contents.</param>
        /// <returns>
        /// The sorted results.
        /// </returns>
        protected override IOrderedQueryable<ListItem> SortResults(IQueryable<ListItem> sfContents)
        {
            return sfContents.OrderBy(i => i.Ordinal);
        }

        /// <summary>
        /// Gets the Sitefinity data.
        /// </summary>
        /// <param name="providerName">(Optional) the provider name to get.</param>
        /// <returns>
        /// An IQueryable&lt;Image&gt;
        /// </returns>
        protected override IQueryable<ListItem> Get(string providerName = null)
        {
            return GetManager(providerName).GetListItems();
        }

        /// <summary>
        /// Gets the Sitefinity data by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// An Image.
        /// </returns>
        protected override ListItem Get(Guid id, string providerName = null)
        {
            return GetManager(providerName).GetListItem(id);
        }

        /// <summary>
        /// Creates the Baba instance from the Sitefinity object.
        /// </summary>
        /// <param name="sfContent">Content of the sf.</param>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override ListItemModel CreateInstance(ListItem sfContent)
        {
            return new ListItemModel(sfContent);
        }
    }
}