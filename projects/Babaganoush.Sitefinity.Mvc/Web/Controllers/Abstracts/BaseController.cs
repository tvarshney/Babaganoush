using Babaganoush.Core.Extensions;
using System.Web.Mvc;

namespace Babaganoush.Sitefinity.Mvc.Web.Controllers.Abstracts
{
    /// <summary>
    /// A controller for handling bases.
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// The embedded virtual path root.
        /// </summary>
        private string _virtualPathRoot = Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BaseController()
        {

        }

        /// <summary>
        /// Constructor that accepts root path for embedded resources.
        /// </summary>
        ///
        /// <param name="virtualPathRoot">The embedded virtual path root.</param>
        public BaseController(string virtualPathRoot)
        {
            _virtualPathRoot = virtualPathRoot;
        }

        /// <summary>
        /// Uses an embedded the view.
        /// </summary>
        ///
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <param name="autoPrefixNamespace">(Optional) if set to <c>true</c> automatically prefixes
        /// namespace to view name based on currently executed controller.</param>
        ///
        /// <returns>
        /// A ViewResult.
        /// </returns>
        public virtual ViewResult EmbeddedView(string viewName, object model, bool autoPrefixNamespace = false)
        {
            string controller = RouteData.Values["controller"].ToString();

            //DETERMINE ROOT OF VIEWS
            string rootPath = "../../../" + _virtualPathRoot
                .TrimStart(new [] { '~', '/' })
                .TrimEnd('/') + "/";

            //CONSTRUCT VIEW FROM CURRENT CONTROLLER NAMESPACE
            string viewNamespace = autoPrefixNamespace
                ? GetType().Namespace.TrimEnd(".Controllers") + ".Views." + controller + "."
                : string.Empty;

            //RETURN CONSTRUCTED VIEW VIRTUAL PATH
            return View(rootPath + viewNamespace + viewName, model);
        }

        /// <summary>
        /// Uses an embedded the view.
        /// </summary>
        ///
        /// <param name="model">The model.</param>
        ///
        /// <returns>
        /// A ViewResult.
        /// </returns>
        public virtual ViewResult EmbeddedView(object model)
        {
            string action = RouteData.Values["action"].ToString();
            return EmbeddedView(action, model, true);
        }

        /// <summary>
        /// Uses an embedded the view.
        /// </summary>
        /// <returns>
        /// A ViewResult.
        /// </returns>
        public virtual ViewResult EmbeddedView()
        {
            return EmbeddedView(null);
        }
    }
}
