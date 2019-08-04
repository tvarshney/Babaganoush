using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Babaganoush.Sitefinity.Content.Fields
{
    /// <summary>
    /// Interface for page selector field definition.
    /// </summary>
    public class PageSelectorDefinition : FieldControlDefinition, IPageSelectorDefinition
    {
        #region Constuctors

        /// <summary>
        /// Initializes a new instance of the <see cref="PageSelectorDefinition"/> class.
        /// </summary>
        public PageSelectorDefinition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageSelectorDefinition"/> class.
        /// </summary>
        /// <param name="element">The configuration element used to persist the control definition.</param>
        public PageSelectorDefinition(ConfigElement element)
            : base(element)
        {
        }

        #endregion

        #region IPageSelectorDefinition members

        /// <summary>
        /// Gets or sets the dynamic module type.
        /// </summary>
        public string DynamicModuleType
        {
            get
            {
                return ResolveProperty("DynamicModuleType", dynamicModuleType);
            }
            set
            {
                dynamicModuleType = value;
            }
        }

        #endregion

        #region Private members

        private string dynamicModuleType;

        #endregion
    }
}