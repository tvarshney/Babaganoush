// file:	Configuration\BabganoushConfig.cs
//
// summary:	Implements the babganoush configuration class
using Babaganoush.Sitefinity.Configuration.Elements;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Babaganoush.Sitefinity.Configuration
{
    /// <summary>
    /// A babaganoush configuration.
    /// </summary>
    [ObjectInfo(Title = "Babaganoush Configurations", Description = "Settings used for Babaganoush extensions")]
    public class BabaganoushConfig : ConfigSection
    {
        /// <summary>
        /// Gets or sets the identifier of the registration.
        /// </summary>
        /// <value>
        /// The identifier of the registration.
        /// </value>
        [ConfigurationProperty("RegistrationId", DefaultValue = null)]
        [ObjectInfo(Title = "Registration ID", Description = "Enter registration ID if applicable.")]
        public string RegistrationId
        {
            get
            {
                return (string)this["RegistrationId"];
            }
            set
            {
                this["RegistrationId"] = value;
            }
        }

        /// <summary>
        /// Gets the scripts.
        /// </summary>
        /// <value>
        /// The scripts.
        /// </value>
        [ConfigurationProperty("Scripts")]
        [ObjectInfo(Title = "Scripts", Description = "Settings for the scripts.")]
        public ScriptsElement Scripts
        {
            get
            {
                return (ScriptsElement)this["Scripts"];
            }
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        [ConfigurationProperty("Services")]
        [ObjectInfo(Title = "Services", Description = "Settings for the services.")]
        public ServicesElement Services
        {
            get
            {
                return (ServicesElement)this["Services"];
            }
        }

        /// <summary>
        /// Gets the ecommerce.
        /// </summary>
        /// <value>
        /// The ecommerce.
        /// </value>
        [ConfigurationProperty("Ecommerce")]
        [ObjectInfo(Title = "Ecommerce", Description = "Settings for e-commerce.")]
        public EcommerceElement Ecommerce
        {
            get
            {
                return (EcommerceElement)this["Ecommerce"];
            }
        }
    }
}