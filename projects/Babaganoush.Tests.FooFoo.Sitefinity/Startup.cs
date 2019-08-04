using Babaganoush.Sitefinity.Application;
using Babaganoush.Sitefinity.Utilities;
using Babaganoush.Tests.FooFoo.Sitefinity;
using Babaganoush.Tests.FooFoo.Sitefinity.Web.Controllers;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;

//Startup and Shutdown code into a web application. This gives a much cleaner
//solution than having to modify global.asax with the startup logic from many packages.
[assembly: PreApplicationStartMethod(typeof(Startup), "PreInit")]
namespace Babaganoush.Tests.FooFoo.Sitefinity
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
                ConfigHelper.RegisterVirtualPath(Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/*", "Babaganoush.Tests.FooFoo.Sitefinity");
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
            //PERFORM LAST ACTIONS IN BOOTSTRAPPER
            if (e.CommandName == "Bootstrapped")
            {
                //REGISTER MVC WIDGETS
                ConfigHelper.RegisterToolboxWidget<SpeakersListController>(
                    title: "Speakers List",
                    description: "Displays list of speakers."
                );

                ConfigHelper.RegisterToolboxWidget<MyTestController>(
                    title: "My Test",
                    description: "Displays test with no model."
                );
            }
        }
    }
}
