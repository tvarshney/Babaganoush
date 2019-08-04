// file:	Utilities\ConfigHelper.cs
//
// summary:	Implements the configuration helper class
using Babaganoush.Core.Utilities;
using Babaganoush.Core.Utilities.Interfaces;
using Babaganoush.Sitefinity.Classes;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;

namespace Babaganoush.Sitefinity.Utilities
{
    /// <summary>
    /// A configuration helper.
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// The web helper.
        /// </summary>
        private static readonly IWebHelper _webHelper = new WebHelper();

        /// <summary>
        /// Registers the virtual path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="assembly">(Optional) The assembly.</param>
        public static void RegisterVirtualPath(string path, string assembly = "Babaganoush.Sitefinity")
        {
            var virtualPathConfig = Config.Get<VirtualPathSettingsConfig>();

            //REGISTER VIRTUAL PATH FOR BABAGANOUSH IF APPLICABLE
            if (!virtualPathConfig.VirtualPaths.ContainsKey(path))
            {
                //CREATE VIRTUAL PATH ELEMENT
                virtualPathConfig.VirtualPaths.Add(new VirtualPathElement(virtualPathConfig.VirtualPaths)
                {
                    VirtualPath = path,
                    ResolverName = "EmbeddedResourceResolver",
                    ResourceLocation = assembly
                });

                var manager = Config.GetManager();
                using (new ElevatedModeRegion(manager))
                {
                    manager.SaveSection(virtualPathConfig);
                }
            }
        }

        /// <summary>
        /// Registers widget to the toolbox. http://www.sitefinity.com/developer-network/forums/general-
        /// discussions-/registering-custom-control-in-toolbox.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="title">The title.</param>
        /// <param name="description">(Optional) The description.</param>
        /// <param name="cssClass">(Optional) The CSS class.</param>
        /// <param name="resourceClassId">(Optional) The resource class identifier.</param>
        /// <param name="layoutTemplate">(Optional) The layout template.</param>
        /// <param name="sectionName">(Optional) Name of the section.</param>
        /// <param name="sectionOrdinal">(Optional) The section ordinal.</param>
        /// <param name="toolboxType">(Optional) Type of the toolbox.</param>
        public static void RegisterToolboxWidget<T>(string title, string description = null, string cssClass = null, string resourceClassId = null, string layoutTemplate = "", string sectionName = Constants.VALUE_TOOLBOX_SECTION_NAME, int? sectionOrdinal = null, ToolboxType toolboxType = ToolboxType.PageControls)
        {
            var manager = Config.GetManager();
            using (new ElevatedModeRegion(manager))
            {
                var config = manager.GetSection<ToolboxesConfig>();

                //GET PAGE TOOLBOX
                var controls = config.Toolboxes[toolboxType.ToString()];
                var section = controls
                    .Sections
                    .FirstOrDefault<ToolboxSection>(x => x.Name == sectionName);

                //CREATE THE SECTION IF APPLICABLE
                if (section == null)
                {
                    section = new ToolboxSection(controls.Sections)
                    {
                        Name = _webHelper.GenerateSafeName(sectionName, true),
                        Title = sectionName,
                        Description = sectionName,
                        Ordinal = sectionOrdinal.GetValueOrDefault(99)
                    };

                    //SET ORDINALS FOR SECTIONS
                    if (sectionOrdinal.HasValue)
                    {
                        //HANDLE ORDINAL CONFLICTS
                        foreach (var item in controls.Sections
                            .Where<ToolboxSection>(x => x.Ordinal == sectionOrdinal.Value))
                        {
                            item.Ordinal += 0.1F;
                        }
                    }

                    controls.Sections.Add(section);
                }

                //REGISTER IN TOOLBOX IF APPLICABLE
                var control = typeof(T);
                if (!section.Tools.Any<ToolboxItem>(t => t.Name == control.Name))
                {
                    //GENERATE NAME FOR TOOLBOX ITEM
                    var name = _webHelper.GenerateSafeName(title, true);

                    //VALIDATE NAME
                    if (Char.IsNumber(name, 0))
                        name = section.Name + name;

                    //CREATE WIDGET OBJECT
                    var widget = new ToolboxItem(section.Tools)
                    {
                        Name = name,
                        Title = title,
                        Description = description ?? title,
                        CssClass = cssClass,
                        ResourceClassId = resourceClassId
                    };

                    //HANDLE WEB FORM AND MVC WIDGETS DIFFERENTLY
                    if (typeof(Controller).IsAssignableFrom(control))
                    {
                        //DEFINE AS MVC WIDGET
                        widget.ControlType = string.Format("{0}, {1}", typeof(MvcControllerProxy).FullName, typeof(MvcControllerProxy).Assembly);
                        widget.ControllerType = control.FullName;
                        widget.Parameters.Add("ControllerName", control.FullName);
                    }
                    else
                    {
                        //DEFINE AS WEB FORM WIDGET
                        widget.ControlType = control.FullName;
                    }

                    //ASSIGN TEMPLATE IF APPLICABLE
                    if (!string.IsNullOrWhiteSpace(layoutTemplate))
                    {
                        widget.LayoutTemplate = layoutTemplate;
                    }

                    //REGISTER TO SYSTEM
                    section.Tools.Add(widget);
                    manager.SaveSection(config);
                }
            }
        }

        /// <summary>
        /// Disables the toolbox.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="section">(Optional) the section.</param>
        /// <param name="toolbox">(Optional) the toolbox.</param>
        public static void DisableToolbox(string name, string section = ToolboxesConfig.ContentToolboxSectionName, string toolbox = "PageControls")
        {
            var manager = Config.GetManager();
            using (new ElevatedModeRegion(manager))
            {
                var config = manager.GetSection<ToolboxesConfig>();

                //GET PAGE TOOLBOX
                var toolboxConfig = config.Toolboxes[toolbox];
                var sectionConfig = toolboxConfig
                    .Sections
                    .FirstOrDefault<ToolboxSection>(tb => tb.Name == section);

                //DISABLE IN TOOLBOX IF APPLICABLE
                if (sectionConfig != null)
                {
                    var tool = sectionConfig.Tools.FirstOrDefault<ToolboxItem>(t => t.Name == name);
                    if (tool != null)
                    {
                        tool.Enabled = false;
                        manager.SaveSection(config);
                    }
                }
            }
        }

        /// <summary>
        /// Disables the toolbox section.
        /// </summary>
        /// <param name="section">the section.</param>
        /// <param name="toolbox">(Optional) the toolbox.</param>
        public static void DisableToolboxSection(string section, string toolbox = "PageControls")
        {
            var manager = Config.GetManager();
            using (new ElevatedModeRegion(manager))
            {
                var config = manager.GetSection<ToolboxesConfig>();

                //GET PAGE TOOLBOX
                var toolboxConfig = config.Toolboxes[toolbox];
                var sectionConfig = toolboxConfig
                    .Sections
                    .FirstOrDefault<ToolboxSection>(tb => tb.Name == section);

                //DISABLE SECTION IF APPLICABLE
                if (sectionConfig != null)
                {
                    sectionConfig.Enabled = false;
                    manager.SaveSection(config);
                }
            }
        }

        /// <summary>
        /// Registers the theme.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="path">The path.</param>
        public static void RegisterTheme(string name, string path)
        {
            var manager = Config.GetManager();
            var config = manager.GetSection<AppearanceConfig>();

            //ADD THEME IF APPLIABLE
            if (config.FrontendThemes.ContainsKey(name))
            {
                return;
            }

            //CREATE THEME
            var theme = new ThemeElement(config.FrontendThemes)
            {
                Name = name,
                Path = path
            };

            //ADD TO SYSTEM
            config.FrontendThemes.Add(theme);

            //SAVE TO STORAGE
            using(new ElevatedModeRegion(manager))
            {
                manager.SaveSection(config);
            }
        }

        /// <summary>
        /// Registers the template. Modified from: Telerik.Sitefinity.Samples.Common.SampleUtilities.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="title">The title.</param>
        /// <param name="masterPage">The master page.</param>
        /// <param name="theme">The theme.</param>
        /// <param name="framework">(Optional) The page template framework.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public static bool RegisterTemplate(string name, string title, string masterPage, string theme, PageTemplateFramework framework = PageTemplateFramework.Hybrid)
        {
            bool result = false;

            var pageManager = PageManager.GetManager();
            using (new ElevatedModeRegion(pageManager))
            {
                var template = pageManager.GetTemplates().Where(t => t.Name == name).SingleOrDefault();

                //TODO: REPLACE TRY/CATCH WITH BETTER VALIDATIONS AND ERROR HANDLING BELOW
                try
                {
                    //DEFAULT CULTURE TO ONE IN USE IN BACKEND
                    //http://mejsullivan.wordpress.com/2012/08/19/sitefinity-5-1-en-us-culture-error-updating-dynamic-content/
                    var culture = new CultureInfo(Config.Get<ResourcesConfig>().DefaultBackendCulture.UICulture);

                    if (template == null)
                    {
                        var pageTemplate = pageManager.CreateTemplate();

                        pageTemplate.Name = name;
                        pageTemplate.MasterPage = masterPage;
                        pageTemplate.Category = SiteInitializer.CustomTemplatesCategoryId;
                        pageTemplate.Framework = framework;

                        pageTemplate.Title[culture] = title;

                        var master = pageManager.EditTemplate(pageTemplate.Id, culture);
                        var temp = pageManager.TemplatesLifecycle.CheckOut(master, culture);
                        master = pageManager.TemplatesLifecycle.CheckIn(temp, culture);
                        // set the theme for the particular language version of the template
                        master.Themes.SetString(culture, theme);
                        master.ApprovalWorkflowState.Value = ApprovalWorkflowState.Published;
                        pageManager.TemplatesLifecycle.Publish(master, culture);

                        pageManager.SaveChanges();
                        result = true;
                    }
                    else
                    {
                        if (!template.AvailableCultures.Contains(culture))
                        {
                            template.Title[culture] = title;

                            var master = pageManager.EditTemplate(template.Id, culture);
                            var temp = pageManager.TemplatesLifecycle.CheckOut(master, culture);
                            master = pageManager.TemplatesLifecycle.CheckIn(temp, culture);
                            master.Themes.SetString(culture, theme);
                            master.ApprovalWorkflowState.Value = ApprovalWorkflowState.Published;
                            pageManager.TemplatesLifecycle.Publish(master, culture);

                            pageManager.SaveChanges();
                            result = true;
                        }
                    }
                }
                catch (Exception)
                {
                    //TODO: USE ELMAH TO LOG ERRORS AND NOTIFY
                }
            }
            return result;
        }

        /// <summary>
        /// Determines if we can does section exist.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public static bool DoesSectionExist<T>() where T : ConfigSection
        {
            //TODO: TAGNAME OK TO TEST AGAINST?
            return Config.GetManager().GetAllConfigSections()
                .Any(c => c.TagName.Equals(typeof(T).Name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Determines whether a module is active by the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// <c>true</c> if a module is active by the specified name; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsModuleActive(string name)
        {
            var config = Config.Get<SystemConfig>();
            var module = config.ApplicationModules[name];
            return module.StartupType != StartupType.Disabled;
        }
    }
}