using Babaganoush.Sitefinity.Application;
using Babaganoush.Sitefinity.Configuration;
using Babaganoush.Sitefinity.Themes;
using Babaganoush.Sitefinity.Themes.Classes;
using Babaganoush.Sitefinity.Utilities;
using System.Web;
using System.Web.Hosting;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Web.UI;

// Startup and Shutdown code into a web application. This gives a much cleaner solution than having to modify global.asax with the startup logic from many packages.
[assembly: PreApplicationStartMethod(typeof(Startup), "PreInit")]
namespace Babaganoush.Sitefinity.Themes
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

            //REGISTER VIRTUAL PATHS FOR EMBEDDED MASTER PAGES
            HostingEnvironment.RegisterVirtualPathProvider(new MasterPageVirtualPathProvider());

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
                ConfigHelper.RegisterVirtualPath(Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/*", "Babaganoush.Sitefinity.Themes");
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
            if (e.CommandName == "Bootstrapped")
            {
                //REGISTER TWITTER BOOTSTRAP LAYOUTS
                ConfigHelper.RegisterToolboxWidget<LayoutControl>(
                    title: "100%",
                    description: "1 Column (span12)",
                    cssClass: "sfL100",
                    sectionName: "Bootstrap Columns",
                    layoutTemplate: Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/Babaganoush.Sitefinity.Themes.Resources.Layouts.1col_col12.ascx",
                    toolboxType: ToolboxType.PageLayouts
                );

                ConfigHelper.RegisterToolboxWidget<LayoutControl>(
                    title: "33% + 67%",
                    description: "2 Columns (col-4 + col-8)",
                    cssClass: "sfL33_67",
                    sectionName: "Bootstrap Columns",
                    layoutTemplate: Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/Babaganoush.Sitefinity.Themes.Resources.Layouts.2col_col4-8.ascx",
                    toolboxType: ToolboxType.PageLayouts
                );

                ConfigHelper.RegisterToolboxWidget<LayoutControl>(
                    title: "50% + 50%",
                    description: "2 Columns (col-6 + col-6)",
                    cssClass: "sfL50_50",
                    sectionName: "Bootstrap Columns",
                    layoutTemplate: Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/Babaganoush.Sitefinity.Themes.Resources.Layouts.2col_col6-6.ascx",
                    toolboxType: ToolboxType.PageLayouts
                );

                ConfigHelper.RegisterToolboxWidget<LayoutControl>(
                    title: "67% + 33%",
                    description: "2 Columns (col-8 + col-4)",
                    cssClass: "sfL67_33",
                    sectionName: "Bootstrap Columns",
                    layoutTemplate: Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/Babaganoush.Sitefinity.Themes.Resources.Layouts.2col_col8-4.ascx",
                    toolboxType: ToolboxType.PageLayouts
                );

                ConfigHelper.RegisterToolboxWidget<LayoutControl>(
                    title: "33% + 33% + 33%",
                    description: "3 Columns (col-4 + col-4 + col-4)",
                    cssClass: "sfL33_34_33",
                    sectionName: "Bootstrap Columns",
                    layoutTemplate: Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/Babaganoush.Sitefinity.Themes.Resources.Layouts.3col_col4-4-4.ascx",
                    toolboxType: ToolboxType.PageLayouts
                );

                ConfigHelper.RegisterToolboxWidget<LayoutControl>(
                    title: "25% + 50% + 25%",
                    description: "3 Columns (col-3 + col-6 + col-3)",
                    cssClass: "sfL25_50_25",
                    sectionName: "Bootstrap Columns",
                    layoutTemplate: Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/Babaganoush.Sitefinity.Themes.Resources.Layouts.3col_col3-6-3.ascx",
                    toolboxType: ToolboxType.PageLayouts
                );

                ConfigHelper.RegisterToolboxWidget<LayoutControl>(
                    title: "4 x 25%",
                    description: "4 Columns (col-3 + col-3 + col-3 + col-3)",
                    cssClass: "sfL25_25_25_25",
                    sectionName: "Bootstrap Columns",
                    layoutTemplate: Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/Babaganoush.Sitefinity.Themes.Resources.Layouts.4col_col3-3-3-3.ascx",
                    toolboxType: ToolboxType.PageLayouts
                );
            }
        }
    }
}
