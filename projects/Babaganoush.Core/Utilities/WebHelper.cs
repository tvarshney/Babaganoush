using Babaganoush.Core.Extensions;
using Babaganoush.Core.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Babaganoush.Core.Wrappers;
using Babaganoush.Core.Wrappers.Interfaces;

namespace Babaganoush.Core.Utilities
{
    /// <summary>
    /// A web helper.
    /// </summary>
    public class WebHelper : IWebHelper
    {
        private readonly IVirtualPathUtility _virtualPathUtility;
        private readonly IHttpContext _httpContext;

        /// <summary>
        /// Creates a new instance of <see cref="WebHelper"/>, with default dependencies used.
        /// </summary>
        public WebHelper()
            : this(new VirtualPathUtilityWrapper(), new SystemHttpContextWrapper())
        { }

        /// <summary>
        /// Creates a new instance of <see cref="WebHelper"/>, using the given dependencies.
        /// </summary>
        public WebHelper(IVirtualPathUtility virtualPathUtility, IHttpContext httpContext)
        {
            _virtualPathUtility = virtualPathUtility;
            _httpContext = httpContext;
        }

        /// <summary>
        /// The js tag.
        /// </summary>
        private const string JS_TAG = @"
            <script> 
                {0}
            </script>";

        /// <summary>
        /// The js link template.
        /// </summary>
        private const string JS_LINK_TEMPLATE = @"<script src=""{0}""></script>";

        /// <summary>
        /// The js module template.
        /// </summary>
        private const string JS_MODULE_TEMPLATE = @"define('{0}', [], function () {{ return {1}; }});";

        /// <summary>
        /// The CSS link template.
        /// </summary>
        private const string CSS_LINK_TEMPLATE = @"<link rel=""stylesheet"" href=""{0}"" />";

        /// <summary>
        /// The CSS tag.
        /// </summary>
        private const string CSS_TAG = @"
            <style>
                {0}
            </style>";

        /// <summary>
        /// To the js block.
        /// </summary>
        ///
        /// <param name="value">The value.</param>
        ///
        /// <returns>
        /// value as a string.
        /// </returns>
        public string ToJsBlock(string value)
        {
            return string.Format(JS_TAG, value);
        }

        /// <summary>
        /// To the CSS block.
        /// </summary>
        ///
        /// <param name="value">The value.</param>
        ///
        /// <returns>
        /// value as a string.
        /// </returns>
        public string ToCssBlock(string value)
        {
            return string.Format(CSS_TAG, value);
        }

        /// <summary>
        /// To the js link.
        /// </summary>
        ///
        /// <param name="url">The URL.</param>
        ///
        /// <returns>
        /// URL as a string.
        /// </returns>
        public string ToJsLink(string url)
        {
            return ToLinkFormat(JS_LINK_TEMPLATE, url);
        }

        /// <summary>
        /// To the CSS link.
        /// </summary>
        ///
        /// <param name="url">The URL.</param>
        ///
        /// <returns>
        /// URL as a string.
        /// </returns>
        public string ToCssLink(string url)
        {
            return ToLinkFormat(CSS_LINK_TEMPLATE, url);
        }

        /// <summary>
        /// To the link format.
        /// </summary>
        ///
        /// <param name="format">The format.</param>
        /// <param name="url">The URL.</param>
        ///
        /// <returns>
        /// The given data converted to a string.
        /// </returns>
        private static string ToLinkFormat(string format, string url)
        {
            //VALIDATE INPUT
            if (string.IsNullOrWhiteSpace(url))
                return string.Empty;

            //HANDLE ANY STRANGE CHARACTERS
            url = WebUtility.HtmlEncode(url);

            //RESOLVE PATH IF APPLICABLE
            if (url.StartsWith("~/"))
            {
                url = HostingEnvironment.ApplicationVirtualPath + url.Substring(1);

                //HANDLE NETWORK PATHS
                if (url.StartsWith("//"))
                {
                    url = url.Substring(1);
                }
            }

            //RETURN CONSTRUCTED LINK
            return string.Format(format, url);
        }

        /// <summary>
        /// To the js module.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public string ToJsModule(string key, string value)
        {
            return string.Format(JS_MODULE_TEMPLATE, key, value);
        }

        /// <summary>
        /// Determines whether the specified content contains HTML.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content contains HTML; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsHtml(string content)
        {
            var tagRegex = new Regex(@"<\s*([^ >]+)[^>]*>.*?<\s*/\s*\1\s*>");
            return tagRegex.IsMatch(content);
        }

        /// <summary>
        /// Strips the HTML tags. Much faster than RegEx: http://www.dotnetperls.com/remove-html-tags.
        /// </summary>
        ///
        /// <param name="source">The source.</param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public string StripHTML(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        /// <summary>
        /// Returns a site relative HTTP path from a partial path starting out with a ~. Same syntax that
        /// ASP.Net internally supports but this method can be used outside of the Page framework.
        /// 
        /// Works like Control.ResolveUrl including support for ~ syntax but returns an absolute URL.
        /// </summary>
        ///
        /// <param name="url">Any Url including those starting with ~.</param>
        ///
        /// <returns>
        /// relative url.
        /// </returns>
        public string ResolveUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }

            bool urlIsAbsolutePath = url.IndexOf("://") != -1;
            if (urlIsAbsolutePath)
            {
                return url;
            }

            return _virtualPathUtility.ToAbsolute(url);
        }

        /// <summary>
        /// This method returns a fully qualified absolute server Url which includes the protocol, server,
        /// port in addition to the server relative Url.
        /// 
        /// Works like Control.ResolveUrl including support for ~ syntax but returns an absolute URL.
        /// </summary>
        ///
        /// <param name="url">Any Url, either App relative or fully qualified.</param>
        /// <param name="forceHttps">(Optional) if true forces the url to use https.</param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public string ResolveServerUrl(string url, bool forceHttps = false)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }

            bool urlIsAlreadyAbsolute = url.IndexOf("://") > -1;
            if (urlIsAlreadyAbsolute)
            {
                return forceHttps ? url.ReplaceInsensitive("http://", "https://") : url;
            }

            var result = new Uri(HttpContext.Current.Request.Url, ResolveUrl(url));
            return forceHttps
                ? new UriBuilder(result) { Scheme = Uri.UriSchemeHttps, Port = 443 }.Uri.ToString()
                : result.ToString();
        }

        /// <summary>
        /// Converts the provided app-relative path into an absolute Url containing the full host name.
        /// </summary>
        ///
        /// <param name="relativeUrl">App-Relative path.</param>
        ///
        /// <returns>
        /// Provided relativeUrl parameter as fully qualified Url.
        /// </returns>
        ///
        /// <example>
        /// ~/path/to/foo to http://www.web.com/path/to/foo
        /// </example>
        public string ToAbsoluteUrl(string relativeUrl)
        {
            //VALIDATE INPUT
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            //VALIDATE INPUT FOR ALREADY ABSOLUTE URL
            if (relativeUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                || relativeUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                return relativeUrl;

            //VALIDATE CONTEXT
            if (HttpContext.Current == null)
                return relativeUrl;

            //PAD FOR RELATIVE PATHING
            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl.Insert(0, "~");
            if (!relativeUrl.StartsWith("~/"))
                relativeUrl = relativeUrl.Insert(0, "~/");

            //GET CURRENT URL AND PORT
            var url = HttpContext.Current.Request.Url;
            var port = url.Port != 80 ? (":" + url.Port) : string.Empty;

            //CONSTRUCT FULL URL
            return string.Format("{0}://{1}{2}{3}",
                url.Scheme, url.Host, port, _virtualPathUtility.ToAbsolute(relativeUrl));
        }

        /// <summary>
        /// Strips out unsafe characters and returns a safe name.
        /// </summary>
        ///
        /// <param name="value">The value.</param>
        /// <param name="titleCase">(Optional) if set to <c>true</c> [title case].</param>
        ///
        /// <returns>
        /// The safe name.
        /// </returns>
        public string GenerateSafeName(string value, bool titleCase = false)
        {
            //VALIDATE INPUT
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            //HANDLING CASING IF APPLICABLE
            value = titleCase ? value.ToTitleCase() : value.ToLower();

            const string pattern = @"[!""#$%&'()\*\+,\./:;<=>\?@\[\\\]^`{\|}~ ]";
            return Regex.Replace(value, pattern, string.Empty);
        }

        /// <summary>
        /// Includes the js.
        /// </summary>
        ///
        /// <param name="path">The path.</param>
        /// <param name="type">(Optional) The type.</param>
        /// <param name="head">(Optional) if place into page head.</param>
        /// <param name="placeholder">(Optional) The placeholder.</param>
        /// <param name="id">(Optional) The identifier.</param>
        public void IncludeJs(string path, Type type = null, bool head = false, string placeholder = null, string id = null)
        {
            Page page = _httpContext.GetCurrentHandler();
            if (page == null)
            {
                return;
            }
            //GENERATE CONTROL ID IF APPLICABLE
            if (string.IsNullOrWhiteSpace(id))
            {
                id = GenerateSafeName(path);
            }

            //RESOLVE URL IF APPLICABLE
            if (type != null)
            {
                //USE EMBEDDED RESOURCE
                path = page.ClientScript.GetWebResourceUrl(type, path);
            }
            else
            {
                //HANDLE ANY STRANGE CHARACTERS AND RESOLVE
                path = WebUtility.HtmlEncode(path.StartsWith("~/") ? page.ResolveUrl(path) : path);
            }
                
            //ADD SCRIPT IF APPLICABLE
            if (!head && string.IsNullOrWhiteSpace(placeholder))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), id, ToJsLink(path), false);
                return;
            }
            
            Control control;
            if (head)
            {
                //HANDLE PLACEMENT OF STYLESHEETS IF AVAILABLE
                control = !string.IsNullOrWhiteSpace(placeholder)
                    ? (page.Header.FindControl(placeholder) ?? page.Header)
                    : page.Header;
            }
            else
            {
                //GET PLACEMENT OF SCRIPTS IF AVAILABLE
                control = page.FindControl(placeholder);
            }

            //DETERMINE IF CONTROL AVAILABLE AND SCRIPT NOT ADDED BEFORE
            if (control != null && control.FindControl(id) == null)
            {
                var jslink = new HtmlGenericControl("script");
                jslink.ID = id;
                jslink.Attributes.Add("src", path);

                //ADD STYLESHEET TO PAGE HEAD
                control.Controls.Add(jslink);
            }
        }

        /// <summary>
        /// Includes the CSS.
        /// </summary>
        ///
        /// <param name="path">The path.</param>
        /// <param name="type">(Optional) The type.</param>
        /// <param name="placeholder">(Optional) The placeholder.</param>
        /// <param name="id">(Optional) The identifier.</param>
        public void IncludeCss(string path, Type type = null, string placeholder = null, string id = null)
        {
            Page page = _httpContext.GetCurrentHandler();
            if (page != null && page.Header != null)
            {
                //HANDLE PLACEMENT OF STYLESHEETS IF AVAILABLE
                var control = !string.IsNullOrWhiteSpace(placeholder)
                    ? (page.Header.FindControl(placeholder) ?? page.Header) : page.Header;

                //GENERATE CONTROL ID IF APPLICABLE
                if (string.IsNullOrWhiteSpace(id))
                {
                    id = GenerateSafeName(path);
                }

                //CHECK FOR DUPLICATES FIRST
                if (control.FindControl(id) != null)
                {
                    return;
                }

                //RESOLVE URL IF APPLICABLE
                if (type != null)
                {
                    //USE EMBEDDED RESOURCE
                    path = page.ClientScript.GetWebResourceUrl(type, path);
                }
                else
                {
                    //HANDLE ANY STRANGE CHARACTERS AND RESOLVE
                    path = WebUtility.HtmlEncode(path.StartsWith("~/")
                        ? page.ResolveUrl(path) : path);
                }

                //CONSTRUCT CSS LINK
                var csslink = new HtmlGenericControl("link");
                csslink.ID = id;
                csslink.Attributes.Add("href", path);
                csslink.Attributes.Add("rel", "stylesheet");

                //ADD STYLESHEET TO PAGE HEAD
                control.Controls.Add(csslink);
            }
        }

        /// <summary>
        /// Gets the IP address.
        /// </summary>
        ///
        /// <returns>
        /// The IP address.
        /// </returns>
        public string GetIPAddress()
        {
            //HANDLE PROXIES, VPN, ETC IF APPLICABLE
            string ipAddress = _httpContext.GetServerVariable("HTTP_X_FORWARDED_FOR");
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            //RETURN IP FROM HEADER REQUEST
            return _httpContext.GetServerVariable("REMOTE_ADDR");
        }

        /// <summary>
        /// Streams to browser.
        /// </summary>
        ///
        /// <param name="fileLocation">The file location.</param>
        /// <param name="contentType">Type of the content.</param>
        public void StreamToBrowser(string fileLocation, string contentType)
        {
            var context = HttpContext.Current;
            string attachment = string.Format("attachment; filename={0}", Path.GetFileName(fileLocation));
            SetHeadersAndContentType(context.Response, contentType, attachment);
            context.Response.WriteFile(_httpContext.MapPath(fileLocation));
            context.Response.End();
        }

        /// <summary>
        /// Streams to browser.
        /// </summary>
        ///
        /// <param name="content">The content.</param>
        /// <param name="downloadName">Name of the download.</param>
        /// <param name="contentType">Type of the content.</param>
        public void StreamToBrowser(string content, string downloadName, string contentType)
        {
            var context = HttpContext.Current;
            string attachment = string.Format("attachment; filename={0}", downloadName);
            SetHeadersAndContentType(context.Response, contentType, attachment);
            context.Response.Write(content);
            context.Response.End();
        }

        /// <summary>
        /// Streams to browser.
        /// </summary>
        ///
        /// <param name="data">The data.</param>
        /// <param name="downloadName">Name of the download.</param>
        /// <param name="contentType">Type of the content.</param>
        public void StreamToBrowser(byte[] data, string downloadName, string contentType)
        {
            var context = HttpContext.Current;
            string attachment = string.Format("inline; filename={0}", downloadName);
            context.Response.Buffer = true;
            SetHeadersAndContentType(context.Response, contentType, attachment);
            context.Response.AddHeader("Content-Length", data.Length.ToString());
            context.Response.BinaryWrite(data);
            context.Response.Flush();
            context.Response.End();
        }

        private static void SetHeadersAndContentType(HttpResponse response, string contentType, string attachment)
        {
            response.AddHeader("Pragma", "public");
            response.AddHeader("content-disposition", attachment);
            response.ContentType = contentType;
        }

        /// <summary>
        /// Posts to remote.
        /// </summary>
        ///
        /// <param name="url">The URL.</param>
        /// <param name="inputs">The inputs.</param>
        public void PostToRemote(string url, Dictionary<string, string> inputs)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            var context = HttpContext.Current;

            //CREATE UNIQUE FORM NAME
            const string formName = "remoteform1";

            //ERASE ENTER RENDER OF CURRENT PAGE
            context.Response.Clear();

            //OUTPUT SINGLE FORM TO POST DATA
            context.Response.Write("<html><head></head>");

            //ON LOAD, PAGE WILL POST FORM TO NEW URL
            context.Response.Write(string.Format("<body onload=\"document.{0}.submit()\">", formName));
            context.Response.Write(string.Format("<form name=\"{0}\" method=\"post\" action=\"{1}\" >", formName, url));

            //ADD PARAMETERS TO PAGE TO POST
            foreach (var item in inputs)
            {
                context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", item.Key, item.Value));
            }

            context.Response.Write("</form>");
            context.Response.Write("</body></html>");

            context.Response.End();
        }
    }
}
