using Babaganoush.Core.Extensions;
using Babaganoush.Core.Utilities;
using Babaganoush.Sitefinity.Mvc.Routes.Abstracts;
using System.Web;
using PageHelper = Babaganoush.Sitefinity.Utilities.PageHelper;

namespace Babaganoush.Sitefinity.Mvc.Routes
{
    /// <summary>
    /// Outputs config for RequireJS.
    /// </summary>
    public class RequireJsConfigModule : BaseScript
    {
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
            //GET MAIN SCRIPT FROM EMBEDDED RESOURCE AND ESCAPE CURLY BRACE
            var fileContents = FileHelper.GetEmbeddedResource(
                "Babaganoush.Sitefinity.Resources.Scripts.main.js", typeof(Sitefinity.Startup));

            //MERGE VARIABLES TO SCRIPT
            return string.Format(fileContents.EscapeForFormat(),
                PageHelper.GetScriptsPath(),
                PageHelper.GetWebResourceUrl("Telerik.Sitefinity.Resources.Scripts.jquery.cookie.js"),
                PageHelper.GetWebResourceUrl("Telerik.Sitefinity.Resources.Scripts.RequireJS.text.js"),
                PageHelper.GetWebResourceUrl("Babaganoush.Sitefinity.Resources.Scripts.require.css.min.js"),
                PageHelper.GetWebResourceUrl("Babaganoush.Sitefinity.Resources.Scripts.moment.moment.min.js"),
                PageHelper.GetWebResourceUrl("Babaganoush.Sitefinity.Resources.Scripts.moment.moment-timezone-with-data.js"),
                PageHelper.GetWebResourceUrl("Babaganoush.Sitefinity.Resources.Scripts.underscore.lodash.underscore.min.js"),
                PageHelper.GetWebResourceUrl("Babaganoush.Sitefinity.Resources.Scripts.underscore.underscore.string.min.js"),
                PageHelper.GetWebResourceUrl("Babaganoush.Sitefinity.Resources.Scripts.glide.jquery.glide.min.js"),
                PageHelper.GetWebResourceUrl("Babaganoush.Sitefinity.Resources.Scripts.url.url.min.js"),
                PageHelper.GetWebResourceUrl("Babaganoush.Sitefinity.Resources.Scripts.lostorage.loStorage.min.js"),
                PageHelper.GetWebResourceUrl("Babaganoush.Sitefinity.Resources.Scripts.baba.api.js"),
                PageHelper.GetWebResourceUrl("Babaganoush.Sitefinity.Resources.Scripts.baba.helpers.js"),
                PageHelper.GetWebResourceUrl("Babaganoush.Sitefinity.Resources.Scripts.baba.alerts.js"),
                PageHelper.GetWebResourceUrl("Telerik.Sitefinity.Resources.Scripts.jquery.blockUI.js"));
        }
    }
}
