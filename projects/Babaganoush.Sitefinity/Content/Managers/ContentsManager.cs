// file:	Content\Managers\ContentsManager.cs
//
// summary:	Implements the contents manager class
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using System;
using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Manager for contents.
    /// </summary>
    public class ContentsManager : BaseSingletonManager<ContentManager, ContentsManager>
    {
        /// <summary>
        /// Gets the content item with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// The by identifier.
        /// </returns>
        public virtual string GetById(Guid id, string providerName = null)
        {
            var sfContentItem = GetManager(providerName).GetItems<ContentItem>().SingleOrDefault(i => i.Id == id);

            return sfContentItem != null ? sfContentItem.Content.Value : string.Empty;
        }

        /// <summary>
        /// Gets a live content item's content by its UrlName property (case-insensitive).
        /// </summary>
        /// <param name="value">The UrlName to search for.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// The by name.
        /// </returns>
        public virtual string GetByName(string value, string providerName = null)
        {
            var sfContentItem = GetManager(providerName).GetItems<ContentItem>()
                .SingleOrDefault(i => i.UrlName.Equals(value, StringComparison.OrdinalIgnoreCase)
                    && i.Status == ContentLifecycleStatus.Live
                    && i.Visible);

            return sfContentItem != null ? sfContentItem.Content.Value : string.Empty;
        }

        /// <summary>
        /// Gets a live content item's content by its Title property (case insensitive).
        /// </summary>
        /// <param name="value">The title to search for.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// The by title.
        /// </returns>
        public virtual string GetByTitle(string value, string providerName = null)
        {
            var sfContentItem = GetManager(providerName).GetItems<ContentItem>()
                .FirstOrDefault(i => i.Title.Equals(value, StringComparison.OrdinalIgnoreCase)
                    && i.Status == ContentLifecycleStatus.Live
                    && i.Visible);

            return sfContentItem != null ? sfContentItem.Content.Value : string.Empty;
        }
    }
}