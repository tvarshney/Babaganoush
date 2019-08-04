using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Babaganoush.Sitefinity.Content.Fields
{
    /// <summary>
    /// Interface for user selector field definition.
    /// </summary>
    public class UserSelectorDefinition : FieldControlDefinition, IUserSelectorDefinition
    {
        #region Constuctors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSelectorDefinition"/> class.
        /// </summary>
        public UserSelectorDefinition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSelectorDefinition"/> class.
        /// </summary>
        /// <param name="element">The configuration element used to persist the control definition.</param>
        public UserSelectorDefinition(ConfigElement element)
            : base(element)
        {
        }

        #endregion

        #region IUserSelectorDefinition members

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