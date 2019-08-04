// file:	Content\Fields\ICodeEditorDefinition.cs
//
// summary:	Declares the ICodeEditorDefinition interface
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Babaganoush.Sitefinity.Content.Fields
{
    /// <summary>
    /// Interface for code editor definition.
    /// </summary>
    public interface ICodeEditorDefinition : IFieldControlDefinition
    {
        /// <summary>
        /// Gets or sets the sample text.
        /// </summary>
        /// <value>
        /// The sample text.
        /// </value>
        string SampleText { get; set; }
    }
}
