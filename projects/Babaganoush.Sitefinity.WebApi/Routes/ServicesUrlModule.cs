// file:	Routes\ServicesUrlModule.cs
//
// summary:	Implements the services URL module class

using Babaganoush.Core.Wrappers;
using Babaganoush.Core.Wrappers.Interfaces;
using Babaganoush.Sitefinity.Mvc.Routes;

namespace Babaganoush.Sitefinity.WebApi.Routes
{
    /// <summary>
    /// Outputs base services url as JavaScript module for relative pathing.
    /// </summary>
    public class ServicesUrlModule : UrlModule
    {
        private readonly IVirtualPathUtility _virtualPathUtility;

        /// <summary>
        /// Creates a new instance of <see cref="ServicesUrlModule"/>, with default dependencies used.
        /// </summary>
        public ServicesUrlModule()
            : this(new VirtualPathUtilityWrapper())
        { }

        /// <summary>
        /// Creates a new instance of <see cref="ServicesUrlModule"/>, using the given dependencies.
        /// </summary>
        public ServicesUrlModule(IVirtualPathUtility virtualPathUtility)
        {
            _virtualPathUtility = virtualPathUtility;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public override string Key
        {
            get { return "baseservicesurl"; }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public override string Value
        {
            get { return _virtualPathUtility.ToAbsolute("~/" + Constants.VALUE_WEBAPI_ROOT_PATH); }
        }
    }
}
