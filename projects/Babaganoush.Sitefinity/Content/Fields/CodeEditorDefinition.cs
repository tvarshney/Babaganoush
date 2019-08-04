// file:	Content\Fields\CodeEditorDefinition.cs
//
// summary:	Implements the code editor definition class
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Babaganoush.Sitefinity.Content.Fields
{
    /// <summary>
    /// A code editor definition.
    /// </summary>
    public class CodeEditorDefinition : FieldControlDefinition, ICodeEditorDefinition
    {
        #region Constuctors

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeEditorDefinition" /> class.
        /// </summary>
        public CodeEditorDefinition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeEditorDefinition" /> class.
        /// </summary>
        /// <param name="configDefinition">The config definition.</param>
        public CodeEditorDefinition(ConfigElement configDefinition)
            : base(configDefinition)
        {
        }
        #endregion

        #region ICodeEditorDefinition members

        /// <summary>
        /// Gets or sets the sample text.
        /// </summary>
        /// <value>
        /// The sample text.
        /// </value>
        public string SampleText
        {
            get
            {
                return ResolveProperty("SampleText", sampleText);
            }
            set
            {
                sampleText = value;
            }
        }
        #endregion

        #region Private members

        /// <summary>
        /// The sample text.
        /// </summary>
        private string sampleText;
        #endregion
    }
}