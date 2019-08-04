using Babaganoush.Sitefinity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Babaganoush.Sitefinity.Content.Fields
{
    /// <summary>
    /// A simple field control used to save a string value.
    /// Use the path to this class when you add the field control
    /// Babaganoush.Sitefinity.Content.Fields.ProductSelector
    /// </summary>
    [FieldDefinitionElement(typeof(ProductSelectorDefinitionElement))]
    public class ProductSelector : FieldControl
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductSelector" /> class.
        /// </summary>
        public ProductSelector()
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
        /// Gets the name of the embedded layout template.
        /// </summary>
        ///
        /// <value>
        /// The name of the layout template.
        /// </value>
        protected override string LayoutTemplateName
        {
            get
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Gets the path of the embedded layout template.
        /// </summary>
        ///
        /// <value>
        /// The path of the layout template.
        /// </value>
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
        /// Gets the reference to the label control that represents the title of the field control.
        /// </summary>
        ///
        /// <remarks>
        /// This control is mandatory only in write mode.
        /// </remarks>
        ///
        /// <value>
        /// The title label.
        /// </value>
        protected internal virtual Label TitleLabel
        {
            get
            {
                SitefinityLabel titleLabel = Container.GetControl<SitefinityLabel>("titleLabel", true);
                return titleLabel;
            }
        }

        /// <summary>
        /// Gets the reference to the label control that represents the description of the field control.
        /// </summary>
        ///
        /// <remarks>
        /// This control is mandatory only in write mode.
        /// </remarks>
        ///
        /// <value>
        /// The description label.
        /// </value>
        protected internal virtual Label DescriptionLabel
        {
            get
            {
                SitefinityLabel descriptionLabel = Container.GetControl<SitefinityLabel>("descriptionLabel", true);
                return descriptionLabel;
            }
        }

        /// <summary>
        /// Gets the reference to the label control that displays the example for this field control.
        /// </summary>
        ///
        /// <remarks>
        /// This control is mandatory only in the write mode.
        /// </remarks>
        ///
        /// <value>
        /// The example label.
        /// </value>
        protected internal virtual Label ExampleLabel
        {
            get
            {
                SitefinityLabel exampleLabel = Container.GetControl<SitefinityLabel>("exampleLabel", true);
                return exampleLabel;
            }
        }

        /// <summary>
        /// Get a reference to the content selector
        /// </summary>
        protected virtual FlatSelector ItemsSelector
        {
            get
            {
                return Container.GetControl<FlatSelector>("itemsSelector", true);
            }
        }

        /// <summary>
        /// Get a reference to the selected items list
        /// </summary>
        protected virtual HtmlGenericControl SelectedItemsList
        {
            get
            {
                return Container.GetControl<HtmlGenericControl>("selectedItemsList", true);
            }
        }

        /// <summary>
        /// Get a reference to the selector wrapper
        /// </summary>
        protected virtual HtmlGenericControl SelectorWrapper
        {
            get
            {
                return Container.GetControl<HtmlGenericControl>("selectorWrapper", true);
            }
        }

        /// <summary>
        /// The LinkButton for "Done"
        /// </summary>
        protected virtual LinkButton DoneButton
        {
            get
            {
                return Container.GetControl<LinkButton>("doneButton", true);
            }
        }

        /// <summary>
        /// The LinkButton for "Cancel"
        /// </summary>
        protected virtual LinkButton CancelButton
        {
            get
            {
                return Container.GetControl<LinkButton>("cancelButton", true);
            }
        }

        /// <summary>
        /// The button area control
        /// </summary>
        protected virtual Control ButtonArea
        {
            get
            {
                return Container.GetControl<Control>("buttonAreaPanel", false);
            }
        }

        /// <summary>
        /// Get a reference to the link that opens the selector
        /// </summary>
        protected virtual HyperLink SelectButton
        {
            get
            {
                return Container.GetControl<HyperLink>("selectButton", true);
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
            ItemsSelector.ServiceUrl = DynamicModulesDataServicePath;
            ItemsSelector.ItemType = DynamicModuleType;
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

            IProductSelectorDefinition fieldDefinition = definition as IProductSelectorDefinition;

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
            var scripts = new List<ScriptReference>(base.GetScriptReferences());

            scripts.Add(new ScriptReference(ScriptReference, assemblyName));
            scripts.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.8.8.custom.min.js", "Telerik.Sitefinity.Resources"));
            scripts.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js",
                Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.FullName));

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
            lastDescriptor.AddElementProperty("selectButton", SelectButton.ClientID);
            lastDescriptor.AddComponentProperty("itemsSelector", ItemsSelector.ClientID);
            lastDescriptor.AddElementProperty("selectorWrapper", SelectorWrapper.ClientID);
            lastDescriptor.AddElementProperty("selectedItemsList", SelectedItemsList.ClientID);
            lastDescriptor.AddElementProperty("doneButton", DoneButton.ClientID);
            lastDescriptor.AddElementProperty("cancelButton", CancelButton.ClientID);
            lastDescriptor.AddProperty("dynamicModulesDataServicePath", RouteHelper.ResolveUrl(DynamicModulesDataServicePath, UrlResolveOptions.Rooted));
            lastDescriptor.AddProperty("dynamicModuleType", DynamicModuleType);
            return descriptors;
        }

        #endregion

        #region Private members

        private const string DynamicModulesDataServicePath = "~/Sitefinity/Services/Ecommerce/Catalog/ProductService.svc/";

        private string dynamicModuleType = "Telerik.Sitefinity.Ecommerce.Catalog.Model.Product";

        /// <summary>
        /// Full pathname of the layout template file.
        /// </summary>
        public static readonly string layoutTemplatePath = ResourceHelper.ToVirtualPath("Babaganoush.Sitefinity.Content.Fields.ProductSelector.ascx");

        /// <summary>
        /// The script reference.
        /// </summary>
        public static readonly string ScriptReference = "Babaganoush.Sitefinity.Content.Fields.ProductSelector.js";

        #endregion
    }
}