// file:	Content\Fields\CodeEditor.cs
//
// summary:	Implements the code editor class
using Babaganoush.Sitefinity.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Babaganoush.Sitefinity.Content.Fields
{
    /// <summary>
    /// A simple field control used to save a string value. Use the path to this class when you add
    /// the field control SitefinityWebApp.Custom.Fields.CodeEditor.CodeEditor.
    /// </summary>
    [FieldDefinitionElement(typeof(CodeEditorDefinitionElement))]
    public class CodeEditor : FieldControl
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeEditor" /> class.
        /// </summary>
        public CodeEditor()
        {
            LayoutTemplatePath = layoutTemplatePath;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the reference to the control that represents the title of the field control. Return null
        /// if no such control exists in the template.
        /// </summary>
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
        /// <value>
        /// The name of the layout template.
        /// </value>
        protected override string LayoutTemplateName
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the reference to the label control that represents the title of the field control.
        /// </summary>
        /// <remarks>
        /// This control is mandatory only in write mode.
        /// </remarks>
        /// <value>
        /// The title label.
        /// </value>
        protected internal virtual Label TitleLabel
        {
            get
            {
                return Container.GetControl<Label>("titleLabel", true);
            }
        }

        /// <summary>
        /// Gets the reference to the label control that represents the description of the field control.
        /// </summary>
        /// <remarks>
        /// This control is mandatory only in write mode.
        /// </remarks>
        /// <value>
        /// The description label.
        /// </value>
        protected internal virtual Label DescriptionLabel
        {
            get
            {
                return Container.GetControl<Label>("descriptionLabel", true);
            }
        }

        /// <summary>
        /// Gets the reference to the label control that displays the example for this field control.
        /// </summary>
        /// <remarks>
        /// This control is mandatory only in the write mode.
        /// </remarks>
        /// <value>
        /// The example label.
        /// </value>
        protected internal virtual Label ExampleLabel
        {
            get
            {
                return Container.GetControl<Label>("exampleLabel", true);
            }
        }

        /// <summary>
        /// Gets the text box control.
        /// </summary>
        /// <value>
        /// The text box control.
        /// </value>
        protected virtual TextBox TextBoxControl
        {
            get
            {
                return Container.GetControl<TextBox>("fieldBox", true);
            }
        }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [TypeConverter(typeof(ObjectStringConverter))]
        public override object Value
        {
            get
            {
                return TextBoxControl.Text;
            }
            set
            {
                TextBoxControl.Text = value as string;
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container">The container.</param>
        protected override void InitializeControls(GenericContainer container)
        {
            TitleLabel.Text = Title;
            ExampleLabel.Text = Example;
            DescriptionLabel.Text = Description;

            TextBoxControl.Text = Text;
        }

        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client
        /// components.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerable"/> collection of
        /// <see cref="T:System.Web.UI.ScriptDescriptor"/> objects.
        /// </returns>
        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            List<ScriptDescriptor> descriptors = new List<ScriptDescriptor>();

            ScriptControlDescriptor descriptor = base.GetScriptDescriptors().Last() as ScriptControlDescriptor;

            if (TextBoxControl != null)
            {
                descriptor.AddElementProperty("textBoxElement", TextBoxControl.ClientID);
            }

            descriptors.Add(descriptor);

            return descriptors.ToArray();
        }

        /// <summary>
        /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference"/> objects that define script
        /// resources that the control requires.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerable"/> collection of
        /// <see cref="T:System.Web.UI.ScriptReference"/> objects.
        /// </returns>
        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var assemblyName = GetType().Assembly.FullName;
            var scripts = new List<ScriptReference>(base.GetScriptReferences());

            scripts.Add(new ScriptReference(ScriptReference, assemblyName));

            //ADD CODE MIRROR PLUGIN FOR CODE EDITOR
            scripts.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.codemirror.js", "Telerik.Sitefinity.Resources"));
            scripts.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.htmlmixed.js", "Telerik.Sitefinity.Resources"));
            scripts.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.xml.js", "Telerik.Sitefinity.Resources"));
            scripts.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.css.js", "Telerik.Sitefinity.Resources"));
            scripts.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.javascript.js", "Telerik.Sitefinity.Resources"));
            
            return scripts;
        }

        /// <summary>
        /// Initialize properties of the field implementing
        /// <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField"/>            with default
        /// values from the configuration definition object.
        /// </summary>
        /// <param name="definition">The definition configuration.</param>
        public override void Configure(IFieldDefinition definition)
        {
            base.Configure(definition);

            ICodeEditorDefinition fieldDefinition = definition as ICodeEditorDefinition;

            if (fieldDefinition != null)
            {
                if (!string.IsNullOrEmpty(fieldDefinition.SampleText))
                {
                    Text = fieldDefinition.SampleText;
                }
            }
        }
        #endregion

        #region Private members

        /// <summary>
        /// Full pathname of the layout template file.
        /// </summary>
        public static readonly string layoutTemplatePath = ResourceHelper.ToVirtualPath("Babaganoush.Sitefinity.Content.Fields.CodeEditor.ascx");

        /// <summary>
        /// The script reference.
        /// </summary>
        public static readonly string ScriptReference = "Babaganoush.Sitefinity.Content.Fields.CodeEditor.js";
        #endregion
    }
}