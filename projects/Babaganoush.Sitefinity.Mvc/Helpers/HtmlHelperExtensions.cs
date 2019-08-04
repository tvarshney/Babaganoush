using Babaganoush.Core.Utilities;
using Babaganoush.Core.Utilities.Interfaces;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Babaganoush.Core.Wrappers;
using Babaganoush.Core.Wrappers.Interfaces;
using Telerik.Sitefinity.Modules.Pages;
using PageHelper = Babaganoush.Sitefinity.Utilities.PageHelper;

namespace Babaganoush.Sitefinity.Mvc.Helpers
{
    /// <summary>
    /// Extension methods for HtmlHelper.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        private static readonly IWebHelper _webHelper = new WebHelper();
        private static readonly IFileSystem _fileSystem = new FileSystemWrapper();
        private static readonly IHttpContext _httpContext = new SystemHttpContextWrapper();

        /// <summary>
        /// Adds the CSS file.
        /// </summary>
        ///
        /// <param name="html">The HTML.</param>
        /// <param name="path">The path.</param>
        /// <param name="id">(Optional) The identifier to prevent duplicates on the page.</param>
        public static void AddCssFile(this HtmlHelper html, string path, string id = null)
        {
            _webHelper.IncludeCss(path, null, null, id);
        }

        /// <summary>
        /// Adds the CSS resource.
        /// </summary>
        ///
        /// <param name="html">The HTML.</param>
        /// <param name="resource">The resource.</param>
        public static void AddCssResource(this HtmlHelper html, string resource)
        {
            //ADD STYLESHEET TO PAGE HEAD
            html.AddCssFile(PageHelper.GetWebResourceUrl(resource), resource);
        }

        /// <summary>
        /// Adds the script file.
        /// </summary>
        ///
        /// <param name="html">The HTML.</param>
        /// <param name="path">The path.</param>
        /// <param name="head">(Optional) Specify to add to the page head.</param>
        /// <param name="id">(Optional) The identifier to prevent duplicates on the page.</param>
        public static void AddScriptFile(this HtmlHelper html, string path, bool head = false, string id = null)
        {
            _webHelper.IncludeJs(path, null, head, null, id);
        }

        /// <summary>
        /// Adds the script resource.
        /// </summary>
        ///
        /// <param name="html">The HTML.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="head">(Optional) if set to <c>true</c> [head].</param>
        /// <param name="id">(Optional) The identifier.</param>
        public static void AddScriptResource(this HtmlHelper html, string resource, bool head = false, string id = null)
        {
            //ADD STYLESHEET TO PAGE HEAD
            html.AddScriptFile(PageHelper.GetWebResourceUrl(resource), head, resource);
        }

        /// <summary>
        /// Adds the script reference.
        /// </summary>
        ///
        /// <param name="html">The HTML.</param>
        /// <param name="reference">The reference.</param>
        public static void AddScriptReference(this HtmlHelper html, ScriptRef reference)
        {
            Page page = _httpContext.GetCurrentHandler();
            if (page != null)
            {
                PageManager.ConfigureScriptManager(page, reference);
            }
        }

        /// <summary>
        /// A HtmlHelper extension method that include view file.
        /// </summary>
        /// <param name="html">The HTML to act on.</param>
        /// <param name="extension">The extension of the file.</param>
        /// <returns>
        /// A string.
        /// </returns>
        public static string IncludeViewFile(this HtmlHelper html, string extension)
        {
            string controller = html.ViewContext.RouteData.Values["controller"].ToString();
            string action = html.ViewContext.RouteData.Values["action"].ToString();
            string path = string.Format("~/Views/{0}/{1}{2}", controller, action, extension);

            return _fileSystem.Exists(html.ViewContext.HttpContext.Server.MapPath(path))
                ? path : string.Empty;
        }
        /// <summary>
        /// A HtmlHelper extension method that include view style.
        /// </summary>
        /// <param name="html">The HTML to act on.</param>
        public static HtmlString IncludeViewStyle(this HtmlHelper html)
        {
            return new HtmlString(_webHelper.ToCssLink(html.IncludeViewFile(".css")));
        }

        /// <summary>
        /// A HtmlHelper extension method that include view script.
        /// </summary>
        /// <param name="html">The HTML to act on.</param>
        public static HtmlString IncludeViewScript(this HtmlHelper html)
        {
            return new HtmlString(_webHelper.ToJsLink(html.IncludeViewFile(".js")));
        }
    }
}
