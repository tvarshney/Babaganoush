// file:	Content\Fields\CodeEditorDefinitionElement.cs
//
// summary:	Implements the code editor definition element class
using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Babaganoush.Sitefinity.Content.Fields
{
    /// <summary>
    /// A code editor definition element.
    /// </summary>
    public class CodeEditorDefinitionElement : FieldControlDefinitionElement, ICodeEditorDefinition
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeEditorDefinitionElement" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public CodeEditorDefinitionElement(ConfigElement parent)
            : base(parent)
        {
        }
        #endregion

        #region FieldControlDefinitionElement members

        /// <summary>
        /// Gets the definition.
        /// </summary>
        /// <returns>
        /// The definition.
        /// </returns>
        public override DefinitionBase GetDefinition()
        {
            return new CodeEditorDefinition(this);
        }
        #endregion

        #region IFieldDefinition members

        /// <summary>
        /// Gets the default type of the field.
        /// </summary>
        /// <value>
        /// The default type of the field.
        /// </value>
        public override Type DefaultFieldType
        {
            get
            {
                return typeof(CodeEditor);
            }
        }
        #endregion

        #region ICodeEditorDefinition

        /// <summary>
        /// Gets or sets the sample text.
        /// </summary>
        /// <value>
        /// The sample text.
        /// </value>
        [ConfigurationProperty("SampleText")]
        public string SampleText
        {
            get
            {
                return (string)this["SampleText"];
            }
            set
            {
                this["SampleText"] = value;
            }
        }
        #endregion
    }
}
