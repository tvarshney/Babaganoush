using Babaganoush.Core.Utilities;
using Babaganoush.Sitefinity.Mvc.Routes.Abstracts;
using System;
using System.Web;

namespace Babaganoush.Sitefinity.Mvc.Routes
{
    /// <summary>
    /// Outputs config for RequireJS.
    /// </summary>
    public class EmbedScript : BaseScript
    {
        /// <summary>
        /// The contents of the script to embed.
        /// </summary>
        public string FileContents { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbedScript()
        { }

        /// <summary>
        /// Constructor specifying resource name and script type.
        /// </summary>
        /// <param name="resourceName">The resource name.</param>
        /// <param name="type">The script type.</param>
        public EmbedScript(string resourceName, Type type = null)
        {
            // Read file contents from namespace and assembly
            FileContents = FileHelper.GetEmbeddedResource(resourceName, type ?? typeof(Startup));
        }

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
            //MERGE VARIABLES TO SCRIPT
            return FileContents;
        }
    }
}
