using Babaganoush.Sitefinity;
using Babaganoush.Sitefinity.Application;
using Babaganoush.Sitefinity.Configuration;
using Babaganoush.Sitefinity.Utilities;
using System;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Services;

// Startup and Shutdown code into a web application. This gives a much cleaner solution than having to modify global.asax with the startup logic from many packages.
[assembly: PreApplicationStartMethod(typeof(Startup), "PreInit")]
namespace Babaganoush.Sitefinity
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
            SystemManager.ApplicationStart += OnBootstrapperApplicationStart;
            Bootstrapper.Initializing += OnBootstrapperInitializing;
            Bootstrapper.Initialized += OnBootstrapperInitialized;
        }

        /// <summary>
        /// Handles the ApplicationStart event of Sitefinity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected static void OnBootstrapperApplicationStart(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Initializing event of the Bootstrapper.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutingEventArgs"/> instance containing the event data.</param>
        protected static void OnBootstrapperInitializing(object sender, ExecutingEventArgs e)
        {
            if (e.CommandName == "RegisterRoutes")
            {
                //REGISTER CUSTOM VIRTUAL PATH FOR SITEFINITY TEMPLATES
                ConfigHelper.RegisterVirtualPath(Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/*");
            }
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
            if (e.CommandName == "Bootstrapped")
            {

            }
        }

        /// <summary>
        /// Called when [page pre render].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnPagePreRender(object sender, EventArgs e)
        {
            //CALL BASE METHOD
            base.OnPagePreRender(sender, e);

            //AUTOMATICALLY ADD GLOBAL SCRIPTS AND STYLES
            PageHelper.RegisterClientSideStartup();
        }
    }
}
