using System.Web.Mvc;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;

namespace Babaganoush.Sitefinity.Mvc.Configuration
{
    /// <summary>
    /// A route configuration.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers all described by routes.
        /// </summary>
        ///
        /// <param name="routes">The routes.</param>
        public static void RegisterAll(RouteCollection routes)
        {
			RouteTable.Routes.MapRoute(
                name: "BabaganoushClassicDefault",
                url: Constants.VALUE_CLASSIC_MVC_ROOT_PATH,
                defaults: new { controller = "Home", action = "Index" }
            );

			RouteTable.Routes.MapRoute(
                name: "BabaganoushClassicControllerActionId",
                url: Constants.VALUE_CLASSIC_MVC_ROOT_PATH + "/{controller}/{action}/{id}",
                defaults: null,
                constraints: new { action = Core.Constants.VALUE_ALPHANUMERIC_REGEX, id = Core.Constants.VALUE_NUMERIC_REGEX }
            );

			RouteTable.Routes.MapRoute(
                name: "BabaganoushClassicControllerAction",
                url: Constants.VALUE_CLASSIC_MVC_ROOT_PATH + "/{controller}/{action}",
                defaults: null,
                constraints: new { action = Core.Constants.VALUE_ALPHANUMERIC_REGEX }
            );

			RouteTable.Routes.MapRoute(
                name: "BabaganoushClassicControllerId",
                url: Constants.VALUE_CLASSIC_MVC_ROOT_PATH + "/{controller}/{id}",
                defaults: new { action = "Detail" },
                constraints: new { id = Core.Constants.VALUE_NUMERIC_REGEX }
            );

			RouteTable.Routes.MapRoute(
                name: "BabaganoushClassicControllerGet",
                url: Constants.VALUE_CLASSIC_MVC_ROOT_PATH + "/{controller}",
                defaults: new { action = "Index" },
                constraints: new { controller = Core.Constants.VALUE_ALPHANUMERIC_REGEX, httpMethod = new HttpMethodConstraint("GET") }
            );
        }
    }
}
