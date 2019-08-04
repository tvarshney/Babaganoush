// file:	Configuration\Elements\ServicesElement.cs
//
// summary:	Implements the services element class
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Babaganoush.Sitefinity.Configuration.Elements
{
    /// <summary>
    /// The scripts element.
    /// </summary>
    public class ServicesElement : ConfigElement
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ServicesElement(ConfigElement parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Gets or sets the enable web API.
        /// </summary>
        /// <value>
        /// The enable web API.
        /// </value>
        [ConfigurationProperty("EnableWebApi", DefaultValue = true)]
        [ObjectInfo(Title = "Enable Web API", Description = "Specify if Web API should be enabled for web services (requires application restart).")]
        public bool EnableWebApi
        {
            get
            {
                return (bool)this["EnableWebApi"];
            }
            set
            {
                this["EnableWebApi"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the JSON p is enabled.
        /// </summary>
        /// <value>
        /// true if enable JSON p, false if not.
        /// </value>
        [ConfigurationProperty("EnableJsonP", DefaultValue = false)]
        [ObjectInfo(Title = "Enable JSONP", Description = "Specify if Web API services should be accessible over JSONP (Web API must be enabled, requires application restart).")]
        public bool EnableJsonP
        {
            get
            {
                return (bool)this["EnableJsonP"];
            }
            set
            {
                this["EnableJsonP"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the default maximum limit for web services.
        /// </summary>
        /// <value>
        /// The default maximum limit for web services.
        /// </value>
        [ConfigurationProperty("DefaultMaxLimit", DefaultValue = 250)]
        [ObjectInfo(Title = "Default Max Limit", Description = "Default maximum records returned by web services when no 'take' parameter specified.")]
        public int DefaultMaxLimit
        {
            get
            {
                return (int)this["DefaultMaxLimit"];
            }
            set
            {
                this["DefaultMaxLimit"] = value;
            }
        }
    }
}
