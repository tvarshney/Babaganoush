// file:	Utilities\PageHelper.cs
//
// summary:	Implements the page helper class
using Babaganoush.Core.Utilities;
using Babaganoush.Core.Utilities.Interfaces;
using Babaganoush.Core.Wrappers;
using Babaganoush.Core.Wrappers.Interfaces;
using Babaganoush.Sitefinity.Configuration;
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.Utilities.Interfaces;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Resources;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.NavigationControls;

namespace Babaganoush.Sitefinity.Utilities
{
    /// <summary>
    /// Page helper utilities.
    /// </summary>
    public class PageHelper : IPageHelper
    {
        private static readonly IWebHelper _webHelper;
        private static readonly IHttpContext _httpContext;
        private static readonly IVirtualPathUtility _virtualPathUtility;

        static PageHelper()
        {
            _virtualPathUtility = new VirtualPathUtilityWrapper();
            _httpContext = new SystemHttpContextWrapper();
            _webHelper = new WebHelper(_virtualPathUtility, _httpContext);
        }

        /// <summary>
        /// Gets a cached page object when needed too early, such as resolving web resource URL's in
        /// routes. 
        /// http://weblog.west-wind.com/posts/2011/Oct/05/Getting-a-Web-Resource-Url-in-non-WebForms-Applications.
        /// </summary>
        /// <value>
        /// The cached page.
        /// </value>
        private static Page CachedPage
        {
            get { return _cachedPage ?? (_cachedPage = new Page()); }
        }

        /// <summary>
        /// The cached page.
        /// </summary>
        private static Page _cachedPage;

        /// <summary>
        /// Gets the current theme path.
        /// </summary>
        /// <returns>
        /// The current theme path.
        /// </returns>
        public static string GetCurrentThemePath()
        {
            //GET CURRENT THEME
            var theme = BabaManagers.Pages.GetCurrentTheme();

            //GET THEME PATH IF APPLIABLE
            if (theme != null)
            {
                //GET PATH
                string path = theme.Path;

                //GET WEB PATH OR RETURN NAMESPACE
                if (!string.IsNullOrWhiteSpace(path))
                {
                    //REMOVE APP_DATA FROM CLIENT URL IF APPLICABLE
                    if (path.StartsWith("~/App_Data", StringComparison.OrdinalIgnoreCase))
                        path = "~/" + path.Substring(11);

                    //RETURN RESOLVED URL
                    return _virtualPathUtility.ToAbsolute(path);
                }
                else
                {
                    //TODO: VIRTUAL PATH FOR NAMESPACE?
                    return theme.Namespace;
                }
            }

            //NO THEME SELECTED OR FOUND
            return string.Empty;
        }

        /// <summary>
        /// Gets the scripts path.
        /// </summary>
        /// <returns>
        /// The scripts path.
        /// </returns>
        public static string GetScriptsPath()
        {
            //GET CUSTOM PATH FROM CONFIG
            string path = ConfigHelper.DoesSectionExist<BabaganoushConfig>()
                ? Config.Get<BabaganoushConfig>().Scripts.ScriptsPath
                : string.Empty;

            //RETURN RESOLVED PATH OR DEFAULT TO SCRIPTS FOLDER IN CURRENT THEME
            return !string.IsNullOrWhiteSpace(path)
                ? _virtualPathUtility.ToAbsolute(path)
                : _virtualPathUtility.ToAbsolute(Constants.VALUE_DEFAULT_SCRIPTS_PATH);
        }

        /// <summary>
        /// Gets the pages by control.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="id">(Optional) the identifier.</param>
        /// <returns>
        /// The pages by control.
        /// </returns>
        public static IQueryable<PageNode> GetPagesByControl<T>(Guid? id = null)
        {
            //FIND CONTROLS ON PAGES
            return App.WorkWith().Pages()
                .Where(p => p.GetPageData() != null && p.GetPageData().Status == ContentLifecycleStatus.Live && p.GetPageData().Visible
                    //RETRIEVE PAGE IF CONTROL TYPE EXISTS MORE THAN ZERO TIMES
                    && p.GetPageData().Controls.Any(c => c.ObjectType.StartsWith(typeof(T).FullName)
                        //GET ALL CONTROLS OF TYPE OR ONLY BY OPTIONAL PARAMETER
                        && (id == null || c.Id == id)))
                        .Get();
        }

        /// <summary>
        /// Gets the web resource URL.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="type">(Optional) The type.</param>
        /// <returns>
        /// The web resource URL.
        /// </returns>
        public static string GetWebResourceUrl(string resourceName, Type type = null)
        {
            Page page = _httpContext.GetCurrentHandler() ?? CachedPage;

            //DETERMINE ASSEMBLY IF APPLICABLE
            if (type == null)
            {
                //HANDLE IF COMING FROM TELERIK RESOURCE ASSEMBLY
                if (resourceName.StartsWith("Telerik.Sitefinity.Resources."))
                {
                    type = typeof(Reference);
                }
                else
                {
                    //USE LOCAL RESOURCE BY DEFAULT
                    type = typeof(PageHelper);
                }
            }

            return page != null ? page.ClientScript.GetWebResourceUrl(type, resourceName) : string.Empty;
        }

        /// <summary>
        /// Registers the startup scripts.
        /// </summary>
        /// <param name="includeJquery">(Optional) the include jquery.</param>
        /// <param name="includeRequireJS">(Optional) the include require js.</param>
        /// <param name="includeKendoUI">(Optional) the include kendo user interface.</param>
        /// <param name="includeAngularJS">(Optional) the include angular js.</param>
        /// <param name="includeTwitterBootstrap">(Optional) the include twitter bootstrap.</param>
        /// <param name="sfPage">(Optional) the sf page.</param>
        /// <param name="page">(Optional) the page.</param>
        public static void RegisterClientSideStartup(bool? includeJquery = null, bool? includeRequireJS = null, bool? includeKendoUI = null, bool? includeAngularJS = null, bool? includeTwitterBootstrap = null, PageNode sfPage = null, Page page = null)
        {
            //VALIDATE INPUT
            if (page == null)
            {
                page = _httpContext.GetCurrentHandler();
            }

            //VALIDATE SCRIPT MANAGER ON PAGE
            if (ScriptManager.GetCurrent(page) == null)
                return;

            if (sfPage == null)
            {
                //TODO: MUST PUT IN TRY/CATCH OTHERWISE I GET SF ERROR ON ACTIVATING MODULES (Cannot access a disposed object. Object name: 'DynamicModule.ns.Wrapped_OpenAccessPageProvider_***'.)
                try
                {
                    sfPage = BabaManagers.Pages.GetCurrentPage();
                }
                catch (Exception)
                {
                    //TODO: LOG ERROR
                }
            }

            //HANDLE PAGE IF APPLICABLE
            if (page != null && sfPage != null && !sfPage.IsBackend)
            {
                var scriptsManager = PageManager.ConfigureScriptManager(page, ScriptRef.Empty);

                //REGISTER JQUERY IF APPLICABLE
                if (includeJquery.GetValueOrDefault(Config.Get<BabaganoushConfig>().Scripts.IncludeJquery))
                {
                    PageManager.ConfigureScriptManager(page, ScriptRef.JQuery);
                }

                //REGISTER TWITTER BOOTSTRAP IF APPLICABLE
                if (includeTwitterBootstrap.GetValueOrDefault(Config.Get<BabaganoushConfig>().Scripts.IncludeTwitterBootstrap))
                {
                    scriptsManager.Scripts.Add(new ScriptReference("Babaganoush.Sitefinity.Resources.Scripts.bootstrap.js.bootstrap.min.js", "Babaganoush.Sitefinity"));
                    _webHelper.IncludeCss("Babaganoush.Sitefinity.Resources.Scripts.bootstrap.css.bootstrap.min.css", typeof(PageHelper));
                }

                //REGISTER KENDO UI IF APPLICABLE
                if (Config.Get<BabaganoushConfig>().Scripts.IncludeKendoWeb)
                {
                    PageManager.ConfigureScriptManager(page, ScriptRef.KendoWeb);
                }

                if (includeKendoUI.GetValueOrDefault(Config.Get<BabaganoushConfig>().Scripts.IncludeKendoAll))
                {
                    PageManager.ConfigureScriptManager(page, ScriptRef.KendoAll);
                }
                
                //ADD KENDO STYLES IF APPLICABLE
                if (Config.Get<BabaganoushConfig>().Scripts.IncludeKendoStyles)
                {
                    _webHelper.IncludeCss("Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css", typeof(Reference), Constants.VALUE_TOP_STYLES_PLACEHOLDER);
                    _webHelper.IncludeCss(string.Format(
                        "Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_{0}_min.css", Config.Get<BabaganoushConfig>().Scripts.KendoTheme.ToLower()),
                        typeof(Reference), Constants.VALUE_TOP_STYLES_PLACEHOLDER);

                    if (Config.Get<BabaganoushConfig>().Scripts.IncludeKendoAll)
                    {
                        _webHelper.IncludeCss(Constants.VALUE_KENDO_DATAVIZ_CSS, null, Constants.VALUE_TOP_STYLES_PLACEHOLDER);
                    }
                }

                //REGISTER ANGULARJS IF APPLICABLE
                if (includeAngularJS.GetValueOrDefault(Config.Get<BabaganoushConfig>().Scripts.IncludeAngularJS))
                {
                    scriptsManager.Scripts.Add(new ScriptReference("Babaganoush.Sitefinity.Resources.Scripts.angular.angular.min.js", "Babaganoush.Sitefinity"));
                }

                //REGISTER REQUIREJS IF APPLICABLE
                if (includeRequireJS.GetValueOrDefault(Config.Get<BabaganoushConfig>().Scripts.IncludeRequireJS))
                {
                    //USE EXISTING SCRIPT MANAGER FOR PLACEMENT UNDER JQUERY
                    var scriptsPath = GetScriptsPath();

                    //ADD REQUIREJS LIBRARY
                    scriptsManager.Scripts.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.RequireJS.require.min.js", "Telerik.Sitefinity.Resources"));

                    //ADD BASE URL PATHS FOR APP FOR CLIENT-SIDE RELATIVE PATHING
                    scriptsManager.Scripts.Add(new ScriptReference(scriptsPath + "/baseurl"));
                    scriptsManager.Scripts.Add(new ScriptReference(scriptsPath + "/basemvcurl"));
                    scriptsManager.Scripts.Add(new ScriptReference(scriptsPath + "/basescriptsurl"));
                    scriptsManager.Scripts.Add(new ScriptReference(scriptsPath + "/baseservicesurl"));

                    //CONFIGURE REQUIRE JS
                    scriptsManager.Scripts.Add(new ScriptReference(scriptsPath + "/main"));
                }

                //REGISTER STARTUP SCRIPT IF APPLICABLE
                if (!string.IsNullOrWhiteSpace(Config.Get<BabaganoushConfig>().Scripts.StartupScript))
                {
                    var rootPath = string.Empty;

                    //HANDLE RELATIVE PATH IF APPLICABLE
                    if (!Config.Get<BabaganoushConfig>().Scripts.StartupScript.StartsWith("~/")
                        && !Config.Get<BabaganoushConfig>().Scripts.StartupScript.StartsWith("/")
                        && !Config.Get<BabaganoushConfig>().Scripts.StartupScript.ToLower().StartsWith("http"))
                        rootPath = GetScriptsPath() + "/";

                    //USE EXISTING SCRIPT MANAGER FOR PLACEMENT UNDER JQUERY
                    scriptsManager.Scripts.Add(new ScriptReference(rootPath + Config.Get<BabaganoushConfig>().Scripts.StartupScript));
                }
            }
        }

        /// <summary>
        /// Determines whether or not the current page site node is an MVC page.
        /// </summary>
        /// <returns>
        /// true if mvc page, false if not.
        /// </returns>
        public static bool IsMvcPage()
        {
            var pageNode = BabaManagers.Pages.GetCurrentSiteMapNode() as PageSiteNode;
            if (pageNode != null)
            {
                return pageNode.Framework == PageTemplateFramework.Mvc;
            }

            return false;
        }

        /// <summary>
        /// Returns the "target" HTML attribute of the link of the given
        /// <paramref name="sitefinitySiteMapNode"/>.
        /// </summary>
        /// <param name="sitefinitySiteMapNode">The SiteMap node where the link resides.</param>
        /// <returns>
        /// The link target, or an empty string if <paramref name="sitefinitySiteMapNode"/> is null.
        /// </returns>
        public string GetLinkTarget(ISitefinitySiteMapNode sitefinitySiteMapNode)
        {
            return NavigationUtilities.GetLinkTarget(sitefinitySiteMapNode);
        }
    }
}
