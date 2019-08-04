using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Babaganoush.Sitefinity.Content.Fields
{
    /// <summary>
    /// Interface for user selector field definition.
    /// </summary>
    public class ProductSelectorDefinition : FieldControlDefinition, IProductSelectorDefinition
    {
        #region Constuctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductSelectorDefinition"/> class.
        /// </summary>
        public ProductSelectorDefinition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductSelectorDefinition"/> class.
        /// </summary>
        /// <param name="element">The configuration element used to persist the control definition.</param>
        public ProductSelectorDefinition(ConfigElement element)
            : base(element)
        {
        }

        #endregion

        #region IProductSelectorDefinition members

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