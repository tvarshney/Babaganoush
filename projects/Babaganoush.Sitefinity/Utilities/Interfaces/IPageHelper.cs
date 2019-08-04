// file:	Utilities\Interfaces\IPageHelper.cs
//
// summary:	Declares the IPageHelper interface
using Telerik.Sitefinity.Web;

namespace Babaganoush.Sitefinity.Utilities.Interfaces
{
    /// <summary>
    /// Interface for various methods related to navigation.
    /// </summary>
    public interface IPageHelper
    {
        /// <summary>
        /// Returns the "target" HTML attribute of the link of the given
        /// <paramref name="sitefinitySiteMapNode"/>.
        /// </summary>
        /// <param name="sitefinitySiteMapNode">The SiteMap node where the link resides.</param>
        /// <returns>
        /// The link target, or an empty string if <paramref name="sitefinitySiteMapNode"/> is null.
        /// </returns>
        string GetLinkTarget(ISitefinitySiteMapNode sitefinitySiteMapNode);
    }
}