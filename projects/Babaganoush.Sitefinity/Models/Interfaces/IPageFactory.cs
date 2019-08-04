// file:	Models\Interfaces\IPageFactory.cs
//
// summary:	Declares the IPageFactory interface
using System.Web;

namespace Babaganoush.Sitefinity.Models.Interfaces
{
    /// <summary>
    /// Interface for creating <see cref="PageModel" /> objects.
    /// </summary>
    public interface IPageFactory
    {
        /// <summary>
        /// Creates a <see cref="PageModel"/> object based off of then given <see cref="SiteMapNode"/>
        /// object.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        /// <param name="includeParents">true to include, false to exclude the parents.</param>
        /// <param name="includeChildren">true to include, false to exclude the children.</param>
        /// <param name="includeRelatedData">true to include, false to exclude the related data.</param>
        /// <returns>
        /// A new <see cref="PageModel"/> object.
        /// </returns>
        PageModel Create(SiteMapNode sfContent, bool includeParents, bool includeChildren, bool includeRelatedData);
    }
}