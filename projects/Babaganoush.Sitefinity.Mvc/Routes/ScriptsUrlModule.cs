using Babaganoush.Sitefinity.Utilities;

namespace Babaganoush.Sitefinity.Mvc.Routes
{
    /// <summary>
    /// Outputs base services url as JavaScript module for relative pathing.
    /// </summary>
    public class ScriptsUrlModule : UrlModule
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
            get { return "basescriptsurl"; }
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
            get { return PageHelper.GetScriptsPath(); }
        }
    }
}
