// file:	Configuration\WebApiConfig.cs
//
// summary:	Implements the web API configuration class
using Babaganoush.Sitefinity.Configuration;
using Babaganoush.Sitefinity.Mvc.Constraints;
using Babaganoush.Sitefinity.Mvc.Formatters;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Telerik.Sitefinity.Configuration;

namespace Babaganoush.Sitefinity.WebApi.Configuration
{
    /// <summary>
    /// Configurations for Web API.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers all MVC routes.
        /// </summary>
        public static void RegisterAll()
        {
            //REGISTER WEBAPI SERVICES
            RegisterRoutes(GlobalConfiguration.Configuration);

            //ENABLE JSONP IF APPLICABLE
            if (Config.Get<BabaganoushConfig>().Services.EnableJsonP)
            {
                //ADD JSONP FORMATTER
                GlobalConfiguration.Configuration.Formatters.Insert(0, new JsonpFormatter());
            }
        }

        /// <summary>
        /// Register routess for the specified WebApi configurations.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void RegisterRoutes(HttpConfiguration config)
        {
            //FOR GENERIC API'S
            config.Routes.MapHttpRoute(
                name: "BabaganoushWebApiControllerActionId",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}/{action}/{id}",
                defaults: null,
                constraints: new { action = Core.Constants.VALUE_ALPHANUMERIC_REGEX, id = new GuidConstraint() } // action must start with character
            );

            config.Routes.MapHttpRoute(
                name: "BabaganoushWebApiControllerActionName",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}/{action}/{name}",
                defaults: null,
                constraints: new { action = Core.Constants.VALUE_ALPHANUMERIC_REGEX, name = Core.Constants.VALUE_ALPHANUMERIC_REGEX } // action and name must start with character
            );

            config.Routes.MapHttpRoute(
                name: "BabaganoushWebApiControllerId",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}/{id}",
                defaults: new { action = "Get" },
                constraints: new { id = new GuidConstraint() } // id must be a guid
            );

            config.Routes.MapHttpRoute(
                name: "BabaganoushWebApiControllerAction",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}/{action}",
                defaults: null,
                constraints: new { action = Core.Constants.VALUE_ALPHANUMERIC_REGEX } // action must start with character
            );

            //TODO: FOR HTTP VERB BASED ROUTING
            config.Routes.MapHttpRoute(
                name: "BabaganoushWebApiControllerGet",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}",
                defaults: new { action = "Get" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "BabaganoushWebApiControllerPost",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}",
                defaults: null,
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) }
            );

            config.Routes.MapHttpRoute(
                name: "BabaganoushWebApiControllerPut",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}",
                defaults: null,
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) }
            );

            config.Routes.MapHttpRoute(
                name: "BabaganoushWebApiControllerDelete",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}",
                defaults: new { action = "Delete" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) }
            );
        }
    }
}
