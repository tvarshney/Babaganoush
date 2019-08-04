// file:	Utilities\ErrorHelper.cs
//
// summary:	Implements the error helper class
using System;
using System.Web;
using Telerik.Sitefinity.Abstractions;

namespace Babaganoush.Sitefinity.Utilities
{
    /// <summary>
    /// An error helper.
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// Logs an exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void LogException(Exception ex)
        {
            Log.Write(ex, ConfigurationPolicy.ErrorLog);
        }

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="configurationPolicy">(Optional) the configuration policy.</param>
        public static void LogMessage(string message, ConfigurationPolicy configurationPolicy = ConfigurationPolicy.Default)
        {
            Log.Write(message, configurationPolicy);
        }

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="file">The file.</param>
        /// <param name="line">The line.</param>
        /// <param name="url">URL of the document.</param>
        /// <param name="userAgent">The user agent.</param>
        public static void LogMessage(string message, string file, string line, string url, string userAgent)
        {
            // BUILD ERROR MESSAGE
            string error = string.Format(
                @"JavaScript Error: 
                    message: {0},
                    file: {1},
                    line: {2},
                    url: {3},
                    userAgent: {4},
                    userName: {5}",
                message,
                file,
                line,
                url,
                userAgent,
                HttpContext.Current.User.Identity.Name);

            //LOG ERROR TO SYSTEM
            LogMessage(error);
        }
    }
}
