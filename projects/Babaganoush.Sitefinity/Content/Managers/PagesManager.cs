// file:	Content\Managers\PagesManager.cs
//
// summary:	Implements the pages manager class

using Babaganoush.Core.Wrappers;
using Babaganoush.Core.Wrappers.Interfaces;
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.Models.Factories;
using Babaganoush.Sitefinity.Models.Interfaces;
using System;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Manager for pages.
    /// </summary>
    public class PagesManager : BaseSingletonManager<PageManager, PagesManager>
    {
        /// <summary>
        /// The page model factory.
        /// </summary>
        private readonly IPageFactory _pageModelFactory = new PageFactory();
        private readonly IVirtualPathUtility _virtualPathUtility = new VirtualPathUtilityWrapper();

        /// <summary>
        /// Gets the current site map node.
        /// </summary>
        /// <returns>
        /// The current site map node.
        /// </returns>
        public virtual SiteMapNode GetCurrentSiteMapNode()
        {
            return SiteMapBase.GetActualCurrentNode();
        }

        /// <summary>
        /// Gets the current site map node id.
        /// </summary>
        /// <returns>
        /// The current site map node identifier.
        /// </returns>
        public virtual Guid GetCurrentSiteMapNodeId()
        {
            //DECLARE VARIABLES
            var currentPage = SiteMapBase.GetActualCurrentNode();
            Guid pageId = Guid.Empty;

            //GET ID FROM CURRENT PAGE IF APPLICABLE
            if (currentPage != null)
            {
                Guid.TryParse(currentPage.Key, out pageId);
            }

            //RETURN PAGE ID
            return pageId;
        }

        /// <summary>
        /// Gets the current page.
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// The current page.
        /// </returns>
        public virtual PageNode GetCurrentPage(string providerName = null)
        {
            //GET CURENT SITE MAP PAGE
            Guid sitemapId = GetCurrentSiteMapNodeId();

            //QUERY STORAGE FOR MORE PAGE DATA IF APPLICABLE
            return sitemapId != Guid.Empty ? GetManager(providerName).GetPageNode(sitemapId) : null;
        }

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// The current theme.
        /// </returns>
        public virtual ThemeElement GetCurrentTheme(string providerName = null)
        {
            var currentPage = GetCurrentPage(providerName);
            if (currentPage != null)
            {
                //GET CURRENT TEMPLATE OF Page
                var pageData = currentPage.GetPageData();
                var template = pageData.Template;

                //GET THEME IF APPLICABLE
                if (template != null)
                {
                    string theme = template.Theme;
                    if (!string.IsNullOrWhiteSpace(theme) && theme != "notheme")
                    {
                        //RETURN THEME BY NAME
                        return Config.Get<AppearanceConfig>().FrontendThemes[theme];
                    }
                }
            }

            //NO TEMPLATE OF THEME SELECTED
            return null;
        }

        /// <summary>
        /// Gets all pages.
        /// </summary>
        /// <param name="includeChildren">(Optional) if set to <c>true</c> [include children].</param>
        /// <param name="includeRelatedData">(Optional) true to include, false to exclude the related data.</param>
        /// <returns>
        /// all.
        /// </returns>
        public virtual PageModel GetAll(bool includeChildren = true, bool includeRelatedData = true)
        {
            return GetById(SiteInitializer.CurrentFrontendRootNodeId, includeChildren, includeRelatedData);
        }

        /// <summary>
        /// Gets the page by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="includeChildren">(Optional) if set to <c>true</c> [include children].</param>
        /// <param name="includeRelatedData">(Optional) true to include, false to exclude the related data.</param>
        /// <returns>
        /// The by identifier.
        /// </returns>
        public virtual PageModel GetById(Guid id, bool includeChildren = true, bool includeRelatedData = true)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var siteMapNodeById = SiteMapBase.GetCurrentProvider()
                .FindSiteMapNodeFromKey(id.ToString());

            return _pageModelFactory.Create(siteMapNodeById, true, includeChildren, includeRelatedData);
        }

        /// <summary>
        /// Gets the page by URL.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="includeChildren">(Optional) if set to <c>true</c> [include children].</param>
        /// <param name="includeRelatedData">(Optional) true to include, false to exclude the related data.</param>
        /// <returns>
        /// The by URL.
        /// </returns>
        public virtual PageModel GetByUrl(string value, bool includeChildren = true, bool includeRelatedData = true)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var siteMapNodeByUrl = SiteMapBase.GetCurrentProvider()
                .FindSiteMapNode(_virtualPathUtility.ToAbsolute("~/" + value.TrimStart('~', '/')));

            return _pageModelFactory.Create(siteMapNodeByUrl, true, includeChildren, includeRelatedData);
        }
    }
}