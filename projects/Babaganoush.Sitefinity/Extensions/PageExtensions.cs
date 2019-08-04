// file:	Extensions\PageExtensions.cs
//
// summary:	Implements the page extensions class
using Babaganoush.Sitefinity.Utilities;
using System.Collections.Generic;
using Telerik.Sitefinity.Web;

namespace Babaganoush.Sitefinity.Extensions
{
    /// <summary>
    /// Extension methods for Sitefinity's PageSiteNode class.
    /// </summary>
    public static class PageExtensions
    {
        /// <summary>
        /// Retrieves all Sitefinity page custom fields and their values for the given
        /// <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="includeRelatedData">(Optional) true to include, false to exclude the related data.</param>
        /// <returns>
        /// The custom field values.
        /// </returns>
        public static IDictionary<string, object> GetCustomFieldValues(this PageSiteNode item, bool includeRelatedData = true)
        {
            return ContentHelper.GetCustomFieldValues(item, "Telerik.Sitefinity.Web.PageSiteNidePropertyDescriptor, Telerik.Sitefinity", includeRelatedData);
        }
    }
}