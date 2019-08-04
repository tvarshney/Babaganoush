using Babaganoush.Sitefinity.Application;
using Babaganoush.Sitefinity.Configuration;
using Babaganoush.Sitefinity.Mvc;
using Babaganoush.Sitefinity.Mvc.Configuration;
using Babaganoush.Sitefinity.Mvc.Routes;
using Babaganoush.Sitefinity.Mvc.Web.Controllers;
using Babaganoush.Sitefinity.Utilities;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using AppSettings = Babaganoush.Core.Configuration.AppSettings;

//Startup and Shutdown code into a web application. This gives a much cleaner
//solution than having to modify global.asax with the startup logic from many packages.
[assembly: PreApplicationStartMethod(typeof(Startup), "PreInit")]
namespace Babaganoush.Sitefinity.Mvc
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

            //REGISTER CLASSIC MVC ROUTES IF APPLICABLE
            if (AppSettings.Get<bool>(Constants.KEY_MVC_ENABLE_CLASSIC_ROUTES))
                RouteConfig.RegisterAll(RouteTable.Routes);

            //SUBSCRIBE TO SITEFINITY BOOTSTRAP EVENTS
            Bootstrapper.Initializing += OnBootstrapperInitializing;
            Bootstrapper.Initialized += OnBootstrapperInitialized;
        }

        /// <summary>
        /// Handles the Initializing event of the Bootstrapper.
        /// </summary>
        ///
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutingEventArgs"/> instance containing the event data.</param>
        protected static void OnBootstrapperInitializing(object sender, ExecutingEventArgs e)
        {
            if (e.CommandName == "RegisterRoutes")
            {
                //REGISTER CUSTOM VIRTUAL PATH FOR SITEFINITY TEMPLATES
                ConfigHelper.RegisterVirtualPath(Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/*", "Babaganoush.Sitefinity.Mvc");
            }
        }

        /// <summary>
        /// Handles the Initialized event of the Bootstrapper.
        /// </summary>
        ///
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedEventArgs"/> instance containing the event data.</param>
        protected static void OnBootstrapperInitialized(object sender, ExecutedEventArgs e)
        {
            // Register app-wide custom settings
            Config.RegisterSection<BabaganoushConfig>();

            //PERFORM LAST ACTIONS IN BOOTSTRAPPER
            if (e.CommandName == "RegisterRoutes")
            {
                //REGISTER DYNAMIC SCRIPTS FOR REQUIREJS MODULES
                var scriptsPath = PageHelper.GetScriptsPath().TrimStart(new [] { '/' });
                RouteTable.Routes.Add(new Route(scriptsPath + "/baseurl", new UrlModule()));
                RouteTable.Routes.Add(new Route(scriptsPath + "/basemvcurl", new MvcUrlModule()));
                RouteTable.Routes.Add(new Route(scriptsPath + "/basescriptsurl", new ScriptsUrlModule()));
                RouteTable.Routes.Add(new Route(scriptsPath + "/main", new RequireJsConfigModule()));
            }
            
            //PERFORM LAST ACTIONS IN BOOTSTRAPPER
            if (e.CommandName == "Bootstrapped")
            {
                //REGISTER MVC WIDGETS
                ConfigHelper.RegisterToolboxWidget<PageTitleController>("Page Title", "Dynamically displays title of the currently viewed page.");
            }
        }
    }
}
