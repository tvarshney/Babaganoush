using Babaganoush.Core.Wrappers;
using Babaganoush.Core.Wrappers.Interfaces;

namespace Babaganoush.Sitefinity.Mvc.Routes
{
    /// <summary>
    /// Outputs base services url as JavaScript module for relative pathing.
    /// </summary>
    public class MvcUrlModule : UrlModule
    {
        private readonly IVirtualPathUtility _virtualPathUtility;

        /// <summary>
        /// Creates a new instance of <see cref="MvcUrlModule"/>, with default dependencies used.
        /// </summary>
        public MvcUrlModule()
            : this(new VirtualPathUtilityWrapper())
        { }

        /// <summary>
        /// Creates a new instance of <see cref="MvcUrlModule"/>, using the given dependencies.
        /// </summary>
        public MvcUrlModule(IVirtualPathUtility virtualPathUtility)
        {
            _virtualPathUtility = virtualPathUtility;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        ///
        /// <value>
        /// The key.
        /// </value>
        public override string Key
        {
            get { return "basemvcurl"; }
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
            get { return _virtualPathUtility.ToAbsolute("~/" + Constants.VALUE_CLASSIC_MVC_ROOT_PATH); }
        }
    }
}
