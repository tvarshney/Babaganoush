using Babaganoush.Sitefinity.Application;
using Babaganoush.Sitefinity.Configuration;
using Babaganoush.Sitefinity.Utilities;
using Babaganoush.Sitefinity.WebApi;
using Babaganoush.Sitefinity.WebApi.Configuration;
using Babaganoush.Sitefinity.WebApi.Routes;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;

// Startup and Shutdown code into a web application. This gives a much cleaner solution than having to modify global.asax with the startup logic from many packages.
[assembly: PreApplicationStartMethod(typeof(Startup), "PreInit")]
namespace Babaganoush.Sitefinity.WebApi
{
    /// <summary>
    /// Application_Start of the target site.
    /// </summary>
    public class Startup : BaseStartup<Startup>
    {
        /// <summary>
        /// Will run when the application is starting (same as Application_Start)
        /// Called by the assembly PreApplicationStartMethod attribute.
        /// </summary>
        public static void PreInit()
        {
            //CALL BASE REGISTERATION
            RegisterStartup();

            //SUBSCRIBE TO SITEFINITY BOOTSTRAP EVENTS
            Bootstrapper.Initialized += OnBootstrapperInitialized;
        }

        /// <summary>
        /// Handles the Initialized event of the Bootstrapper.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedEventArgs"/> instance containing the event data.</param>
        protected static void OnBootstrapperInitialized(object sender, ExecutedEventArgs e)
        {
            // Register app-wide custom settings
            Config.RegisterSection<BabaganoushConfig>();

            //PERFORM LAST ACTIONS IN BOOTSTRAPPER
            if (e.CommandName == "RegisterRoutes")
            {
                //REGISTER WEBAPI MVC ROUTES IF APPLICABLE
                if (!ConfigHelper.DoesSectionExist<BabaganoushConfig>() //DEFAULT IN CASE RUNS TOO SOON FIRST TIME
                    || Config.Get<BabaganoushConfig>().Services.EnableWebApi)
                {
                    //REGISTER DYNAMIC SCRIPTS FOR REQUIREJS MODULES
                    var scriptsPath = PageHelper.GetScriptsPath().TrimStart(new[] { '/' });
                    RouteTable.Routes.Add(new Route(scriptsPath + "/baseservicesurl", new ServicesUrlModule()));

                    //REGISTER WEBAPI MVC ROUTES
                    WebApiConfig.RegisterAll();
                }
            }
        }
    }
}