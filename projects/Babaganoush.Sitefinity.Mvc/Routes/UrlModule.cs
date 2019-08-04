using Babaganoush.Sitefinity.Mvc.Routes.Abstracts;
using System.Web.Hosting;

namespace Babaganoush.Sitefinity.Mvc.Routes
{
    /// <summary>
    /// Outputs base url as JavaScript module for relative pathing.
    /// </summary>
    public class UrlModule : BaseScriptModule
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        ///
        /// <value>
        /// The key.
        /// </value>
        public override string Key
        {
            get { return "baseurl"; }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        ///
        /// <value>
        /// The value.
        /// </value>
        public override string Value
        {
            get { return HostingEnvironment.ApplicationVirtualPath; }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UrlModule()
        {
            IncludeQuotes = true;
        }
    }
}
