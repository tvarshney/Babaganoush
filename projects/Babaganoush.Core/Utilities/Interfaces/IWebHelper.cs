using System;
using System.Collections.Generic;

namespace Babaganoush.Core.Utilities.Interfaces
{
    /// <summary>
    /// A web helper interface.
    /// </summary>
    public interface IWebHelper
    {
        /// <summary>
        /// To the js block.
        /// </summary>
        ///
        /// <param name="value">The value.</param>
        ///
        /// <returns>
        /// value as a string.
        /// </returns>
        string ToJsBlock(string value);

        /// <summary>
        /// To the CSS block.
        /// </summary>
        ///
        /// <param name="value">The value.</param>
        ///
        /// <returns>
        /// value as a string.
        /// </returns>
        string ToCssBlock(string value);

        /// <summary>
        /// To the js link.
        /// </summary>
        ///
        /// <param name="url">The URL.</param>
        ///
        /// <returns>
        /// URL as a string.
        /// </returns>
        string ToJsLink(string url);

        /// <summary>
        /// To the CSS link.
        /// </summary>
        ///
        /// <param name="url">The URL.</param>
        ///
        /// <returns>
        /// URL as a string.
        /// </returns>
        string ToCssLink(string url);

        /// <summary>
        /// To the js module.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        string ToJsModule(string key, string value);

        /// <summary>
        /// Determines whether the specified content contains HTML.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content contains HTML; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsHtml(string content);

        /// <summary>
        /// Strips the HTML tags. Much faster than RegEx: http://www.dotnetperls.com/remove-html-tags.
        /// </summary>
        ///
        /// <param name="source">The source.</param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        string StripHTML(string source);

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
        string ResolveUrl(string url);

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
        string ResolveServerUrl(string url, bool forceHttps = false);

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
        string ToAbsoluteUrl(string relativeUrl);

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
        string GenerateSafeName(string value, bool titleCase = false);

        /// <summary>
        /// Includes the js.
        /// </summary>
        ///
        /// <param name="path">The path.</param>
        /// <param name="type">(Optional) The type.</param>
        /// <param name="head">(Optional) if place into page head.</param>
        /// <param name="placeholder">(Optional) The placeholder.</param>
        /// <param name="id">(Optional) The identifier.</param>
        void IncludeJs(string path, Type type = null, bool head = false, string placeholder = null, string id = null);

        /// <summary>
        /// Includes the CSS.
        /// </summary>
        ///
        /// <param name="path">The path.</param>
        /// <param name="type">(Optional) The type.</param>
        /// <param name="placeholder">(Optional) The placeholder.</param>
        /// <param name="id">(Optional) The identifier.</param>
        void IncludeCss(string path, Type type = null, string placeholder = null, string id = null);

        /// <summary>
        /// Gets the IP address.
        /// </summary>
        ///
        /// <returns>
        /// The IP address.
        /// </returns>
        string GetIPAddress();

        /// <summary>
        /// Streams to browser.
        /// </summary>
        ///
        /// <param name="fileLocation">The file location.</param>
        /// <param name="contentType">Type of the content.</param>
        void StreamToBrowser(string fileLocation, string contentType);

        /// <summary>
        /// Streams to browser.
        /// </summary>
        ///
        /// <param name="content">The content.</param>
        /// <param name="downloadName">Name of the download.</param>
        /// <param name="contentType">Type of the content.</param>
        void StreamToBrowser(string content, string downloadName, string contentType);

        /// <summary>
        /// Streams to browser.
        /// </summary>
        ///
        /// <param name="data">The data.</param>
        /// <param name="downloadName">Name of the download.</param>
        /// <param name="contentType">Type of the content.</param>
        void StreamToBrowser(byte[] data, string downloadName, string contentType);

        /// <summary>
        /// Posts to remote.
        /// </summary>
        ///
        /// <param name="url">The URL.</param>
        /// <param name="inputs">The inputs.</param>
        void PostToRemote(string url, Dictionary<string, string> inputs);
    }
}