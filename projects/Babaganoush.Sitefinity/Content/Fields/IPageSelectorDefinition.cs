using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Babaganoush.Sitefinity.Content.Fields
{
    /// <summary>
    /// A user selector definition element.
    /// </summary>
    public interface IPageSelectorDefinition : IFieldControlDefinition
    {
        /// <summary>
        /// Gets or sets the dynamic module type.
        /// </summary>
        /// <value>The dynamic module type.</value>
        string DynamicModuleType { get; set; }
    }
}