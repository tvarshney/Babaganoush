using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Babaganoush.Sitefinity.Content.Fields
{
    /// <summary>
    /// A page selector definition element.
    /// </summary>
    public class PageSelectorDefinitionElement : FieldControlDefinitionElement, IPageSelectorDefinition
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PageSelectorDefinitionElement" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public PageSelectorDefinitionElement(ConfigElement parent)
            : base(parent)
        {
        }

        #endregion

        #region FieldControlDefinitionElement members

        /// <summary>
        /// Gets the definition of page selector field.
        /// </summary>
        /// <returns></returns>
        public override DefinitionBase GetDefinition()
        {
            return new PageSelectorDefinition(this);
        }

        #endregion

        #region IFieldDefinition members

        /// <summary>
        /// Gets field's default type.
        /// </summary>
        ///
        /// <value>
        /// The default field type.
        /// </value>
        public override Type DefaultFieldType
        {
            get
            {
                return typeof(PageSelector);
            }
        }

        #endregion

        #region IPageSelectorDefinition

        /// <summary>
        /// Gets or sets the dynamic module type.
        /// </summary>
        ///
        /// <value>
        /// The module type.
        /// </value>
        [ConfigurationProperty("DynamicModuleType")]
        public string DynamicModuleType
        {
            get
            {
                return (string)this["DynamicModuleType"];
            }
            set
            {
                this["DynamicModuleType"] = value;
            }
        }

        #endregion
    }
}