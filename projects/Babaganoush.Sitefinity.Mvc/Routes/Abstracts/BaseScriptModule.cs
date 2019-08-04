using Babaganoush.Core.Utilities;
using Babaganoush.Core.Utilities.Interfaces;
using System.Web;

namespace Babaganoush.Sitefinity.Mvc.Routes.Abstracts
{
    /// <summary>
    /// Outputs base url as JavaScript module for relative pathing.
    /// </summary>
    public class BaseScriptModule : BaseScript
    {
        private readonly IWebHelper _webHelper;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BaseScriptModule()
            : this(new WebHelper())
        { }

        /// <summary>
        /// Constructor with settable dependencies.
        /// </summary>
        public BaseScriptModule(IWebHelper webHelper)
        {
            _webHelper = webHelper;
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        ///
        /// <value>
        /// The key.
        /// </value>
        public virtual string Key { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the quotes should be included.
        /// </summary>
        ///
        /// <value>
        /// true if include quotes, false if not.
        /// </value>
        public virtual bool IncludeQuotes { get; set; }

        /// <summary>
        /// Process the value described by context.
        /// </summary>
        ///
        /// <param name="context">The context.</param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public override string ProcessValue(HttpContext context)
        {
            return _webHelper.ToJsModule(Key, IncludeQuotes
                ? "'" + Value + "'" : Value);
        }
    }
}
