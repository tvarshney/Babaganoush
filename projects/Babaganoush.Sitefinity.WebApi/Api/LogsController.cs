// file:	Api\LogsController.cs
//
// summary:	Implements the logs controller class
using Babaganoush.Sitefinity.Utilities;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using System.Web.Http;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for logs.
    /// </summary>
    public class LogsController : BaseApiController
    {
        /// <summary>
        /// Creates the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="file">The file name.</param>
        /// <param name="line">The line.</param>
        /// <param name="url">The URL.</param>
        /// <param name="userAgent">The user agent.</param>
        [HttpPost]
        //TODO: FIX ROUTING SINCE GIVE 404 NOT FOUND
        public virtual void Create(string message, string file, string line, string url, string userAgent)
        {
            LogHelper.LogMessage(message, file, line, url, userAgent);
        }
    }
}