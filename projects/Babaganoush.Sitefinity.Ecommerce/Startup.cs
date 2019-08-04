using Babaganoush.Sitefinity.Application;
using Babaganoush.Sitefinity.Configuration;
using Babaganoush.Sitefinity.Ecommerce;
using Babaganoush.Sitefinity.Ecommerce.Payments;
using Babaganoush.Sitefinity.Ecommerce.Utilities;
using Babaganoush.Sitefinity.Utilities;
using System.Web;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Ecommerce;
using Telerik.Sitefinity.Modules.Ecommerce.BusinessServices.Orders.Interfaces;
using Telerik.Sitefinity.Modules.Ecommerce.Events;
using Telerik.Sitefinity.Services;

// Startup and Shutdown code into a web application. This gives a much cleaner solution than having to modify global.asax with the startup logic from many packages.
[assembly: PreApplicationStartMethod(typeof(Startup), "PreInit")]
namespace Babaganoush.Sitefinity.Ecommerce
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
                ConfigHelper.RegisterVirtualPath(Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/*", "Babaganoush.Sitefinity.Ecommerce");
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
            if (e.CommandName != "Bootstrapped")
            {
                return;
            }
            //HANDLE ECOMMERCE IF APPLICABLE
            IModule ecommerceModule = SystemManager.GetModule(EcommerceModule.moduleName);
            if (ecommerceModule == null || ecommerceModule.Startup == StartupType.Disabled)
            {
                return;
            }
            //ENABLE CUSTOM FIELDS FOR ORDER CHECK OUT IF APPLICABLE
            if (!string.IsNullOrWhiteSpace(Config.Get<BabaganoushConfig>().Ecommerce.CustomCheckoutFields))
            {
                OrderHelper.CreateCustomFields();
                EventHub.Subscribe<IEcommerceCheckoutPageChangingEvent>(CheckoutHelper.PageChangingHandler);
            }

            //SUBSCRIBE TO ORDER PLACED HOOK FOR FURTHER PROCESSING
            EcommerceEvents.OrderPlaced += new EcommerceEvents.OnOrderPlaced(OrderHelper.CompletedHandler);

            //ENABLE SPECIAL PAYMENT PROCESSING
            if (Config.Get<BabaganoushConfig>().Ecommerce.DisableOnlinePaymentForFreeOrders)
            {
                ObjectFactory.Container.RegisterType<IEcommercePaymentMethodService, FreeOrdersMethodService>(new ContainerControlledLifetimeManager());
            }
        }
    }
}
