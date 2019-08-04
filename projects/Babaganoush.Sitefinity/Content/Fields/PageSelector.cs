using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Abstractions;
using Babaganoush.Sitefinity.Utilities;

namespace Babaganoush.Sitefinity.Content.Fields
{
    /// <summary>
    /// A simple field control used to save a string value.
    /// Use the path to this class when you add the field control
    /// Babaganoush.Sitefinity.Content.Fields.PageSelector
    /// </summary>
    [FieldDefinitionElement(typeof(PageSelectorDefinitionElement))]
    public class PageSelector : FieldControl
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PageSelector" /> class.
        /// </summary>
        public PageSelector()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the reference to the control that represents the title of the field control. Return null
        /// if no such control exists in the template.
        /// </summary>
        ///
        /// <value>
        /// The control displaying the title of the field.
        /// </value>
        protected override WebControl TitleControl
        {
            get
            {
                return TitleLabel;
            }
        }

        /// <summary>
        /// Gets the reference to the control that represents the description of the field control.
        /// Return null if no such control exists in the template.
        /// </summary>
        ///
        /// <value>
        /// The control displaying the description of the field.
        /// </value>
        protected override WebControl DescriptionControl
        {
            get
            {
                return DescriptionLabel;
            }
        }

        /// <summary>
        /// Gets the reference to the control that represents the example of the field control. Return
        /// null if no such control exists in the template.
        /// </summary>
        ///
        /// <value>
        /// The control displaying the sample usage of the field.
        /// </value>
        protected override WebControl ExampleControl
        {
            get
            {
                return ExampleLabel;
            }
        }

        /// <summary>
        /// Obsolete. Use LayoutTemplatePath instead.
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Gets the layout template's relative or virtual path.
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                return layoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        /// <summary>
        /// Gets the title label.
        /// </summary>
        /// <value>The title label.</value>
        protected internal virtual Label TitleLabel
        {
            get
            {
                SitefinityLabel titleLabel = Container.GetControl<SitefinityLabel>("titleLabel", true);
                return titleLabel;
            }
        }

        /// <summary>
        /// Gets the description label.
        /// </summary>
        /// <value>The description label.</value>
        protected internal virtual Label DescriptionLabel
        {
            get
            {
                SitefinityLabel descriptionLabel = Container.GetControl<SitefinityLabel>("descriptionLabel", true);
                return descriptionLabel;
            }
        }

        /// <summary>
        /// Gets the example label.
        /// </summary>
        /// <value>The example label.</value>
        protected internal virtual Label ExampleLabel
        {
            get
            {
                SitefinityLabel exampleLabel = Container.GetControl<SitefinityLabel>("exampleLabel", true);
                return exampleLabel;
            }
        }

        /// <summary>
        /// Get a reference to the page selector
        /// </summary>
        protected PageField ItemsSelector
        {
            get
            {
                return Container.GetControl<PageField>("PageSelector", true);
            }
        }

        /// <summary>
        /// Get or set the dynamic module type 
        /// </summary>
        public string DynamicModuleType
        {
            get
            {
                return dynamicModuleType;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    dynamicModuleType = value;
                }
            }
        }

        #endregion

        #region Overridden Methods

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        ///
        /// <param name="container">The container.</param>
        protected override void InitializeControls(GenericContainer container)
        {
            TitleLabel.Text = Title;
            DescriptionLabel.Text = Description;
            ExampleLabel.Text = Example;
            ItemsSelector.RootNodeID = SiteInitializer.CurrentFrontendRootNodeId;
            ItemsSelector.WebServiceUrl = DynamicModulesDataServicePath;
            ItemsSelector.BindOnLoad = false;
        }

        /// <summary>
        /// Initialize properties of the field implementing
        /// <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField"/> with default
        /// values from the configuration definition object.
        /// </summary>
        ///
        /// <param name="definition">The definition configuration.</param>
        public override void Configure(IFieldDefinition definition)
        {
            base.Configure(definition);

            IPageSelectorDefinition fieldDefinition = definition as IPageSelectorDefinition;

            if (fieldDefinition != null)
            {
                if (!string.IsNullOrEmpty(fieldDefinition.DynamicModuleType))
                {
                    DynamicModuleType = fieldDefinition.DynamicModuleType;
                }
            }
        }

        /// <summary>
        /// Gets required core script libs.
        /// </summary>
        /// <returns></returns>
        protected override ScriptRef GetRequiredCoreScripts()
        {
            return ScriptRef.JQuery |
                ScriptRef.JQueryUI |
                ScriptRef.KendoAll;
        }

        #endregion

        #region IScriptControl Members

        /// <summary>
        /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference"/> objects that define script
        /// resources that the control requires.
        /// </summary>
        ///
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerable"/> collection of
        /// <see cref="T:System.Web.UI.ScriptReference"/> objects.
        /// </returns>
        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var assemblyName = GetType().Assembly.FullName;
            List<ScriptReference> scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(ScriptReference, assemblyName));
            scripts.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js", "Telerik.Sitefinity"));
            scripts.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js", "Telerik.Sitefinity"));
            return scripts;
        }

        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client
        /// components.
        /// </summary>
        ///
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerable"/> collection of
        /// <see cref="T:System.Web.UI.ScriptDescriptor"/> objects.
        /// </returns>
        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var descriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var lastDescriptor = (ScriptControlDescriptor)descriptors.Last();
            lastDescriptor.AddProperty("dynamicModulesDataServicePath", RouteHelper.ResolveUrl(DynamicModulesDataServicePath, UrlResolveOptions.Rooted));
            lastDescriptor.AddProperty("dynamicModuleType", DynamicModuleType);
            lastDescriptor.AddComponentProperty("pageSelector", ItemsSelector.ClientID);

            return descriptors;
        }

        #endregion

        #region Private members

        private const string DynamicModulesDataServicePath = "~/Sitefinity/Services/Pages/PagesService.svc/";

        private string dynamicModuleType = "Telerik.Sitefinity.Pages.Model.PageNode";

        /// <summary>
        /// Full pathname of the layout template file.
        /// </summary>
        public static readonly string layoutTemplatePath = ResourceHelper.ToVirtualPath("Babaganoush.Sitefinity.Content.Fields.PageSelector.ascx");

        /// <summary>
        /// The script reference.
        /// </summary>
        public static readonly string ScriptReference = "Babaganoush.Sitefinity.Content.Fields.PageSelector.js";

        #endregion
    }
}