// file:	Configuration\Elements\ScriptsElement.cs
//
// summary:	Implements the scripts element class
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Babaganoush.Sitefinity.Configuration.Elements
{
    /// <summary>
    /// The scripts element.
    /// </summary>
    public class ScriptsElement : ConfigElement
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ScriptsElement(ConfigElement parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Gets or sets the full pathname of the scripts file.
        /// </summary>
        /// <value>
        /// The full pathname of the scripts file.
        /// </value>
        [ConfigurationProperty("ScriptsPath", DefaultValue = Constants.VALUE_DEFAULT_SCRIPTS_PATH)]
        [ObjectInfo(Title = "Scripts Path", Description = "Root path used for scripts folder.")]
        public string ScriptsPath
        {
            get
            {
                return (string)this["ScriptsPath"];
            }
            set
            {
                this["ScriptsPath"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the startup script.
        /// </summary>
        /// <value>
        /// The startup script.
        /// </value>
        [ConfigurationProperty("StartupScript", DefaultValue = "")]
        [ObjectInfo(Title = "Startup Script", Description = "Specify script file to load on page startup")]
        public string StartupScript
        {
            get
            {
                return (string)this["StartupScript"];
            }
            set
            {
                this["StartupScript"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the jquery should be included.
        /// </summary>
        /// <value>
        /// true if include jquery, false if not.
        /// </value>
        [ConfigurationProperty("IncludeJquery", DefaultValue = true)]
        [ObjectInfo(Title = "Include jQuery", Description = "Includes jQuery on every page")]
        public bool IncludeJquery
        {
            get
            {
                return (bool)this["IncludeJquery"];
            }
            set
            {
                this["IncludeJquery"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the kendo all should be included.
        /// </summary>
        /// <value>
        /// true if include kendo all, false if not.
        /// </value>
        [ConfigurationProperty("IncludeKendoAll", DefaultValue = true)]
        [ObjectInfo(Title = "Include Kendo UI (All)", Description = "Includes Kendo UI Web, DataViz, and Mobile on every page")]
        public bool IncludeKendoAll
        {
            get
            {
                return (bool)this["IncludeKendoAll"];
            }
            set
            {
                this["IncludeKendoAll"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the kendo web should be included.
        /// </summary>
        /// <value>
        /// true if include kendo web, false if not.
        /// </value>
        [ConfigurationProperty("IncludeKendoWeb", DefaultValue = true)]
        [ObjectInfo(Title = "Include Kendo UI (Web)", Description = "Includes Kendo UI Web on every page")]
        public bool IncludeKendoWeb
        {
            get
            {
                return (bool)this["IncludeKendoWeb"];
            }
            set
            {
                this["IncludeKendoWeb"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the kendo styles should be included.
        /// </summary>
        /// <value>
        /// true if include kendo styles, false if not.
        /// </value>
        [ConfigurationProperty("IncludeKendoStyles", DefaultValue = true)]
        [ObjectInfo(Title = "Include Kendo UI Styles", Description = "Includes Kendo UI styles on every page")]
        public bool IncludeKendoStyles
        {
            get
            {
                return (bool)this["IncludeKendoStyles"];
            }
            set
            {
                this["IncludeKendoStyles"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the kendo theme should be included.
        /// </summary>
        /// <value>
        /// theme name to include kendo theme.
        /// </value>
        [ConfigurationProperty("KendoTheme", DefaultValue = "default")]
        [ObjectInfo(Title = "Specify Kendo UI Theme", Description = "Specify Kendo UI theme to use (only themes embedded in Sitefinity are available)")]
        public string KendoTheme
        {
            get
            {
                return (string)this["KendoTheme"];
            }
            set
            {
                this["KendoTheme"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the twitter bootstrap should be included.
        /// </summary>
        /// <value>
        /// true if include twitter bootstrap, false if not.
        /// </value>
        [ConfigurationProperty("IncludeTwitterBootstrap", DefaultValue = false)]
        [ObjectInfo(Title = "Include Twitter Bootstrap", Description = "Includes Twitter Bootstrap on every page")]
        public bool IncludeTwitterBootstrap
        {
            get
            {
                return (bool)this["IncludeTwitterBootstrap"];
            }
            set
            {
                this["IncludeTwitterBootstrap"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the angular js should be included.
        /// </summary>
        /// <value>
        /// true if include angular js, false if not.
        /// </value>
        [ConfigurationProperty("IncludeAngularJS", DefaultValue = false)]
        [ObjectInfo(Title = "Include AngularJS", Description = "Includes AngularJS on every page")]
        public bool IncludeAngularJS
        {
            get
            {
                return (bool)this["IncludeAngularJS"];
            }
            set
            {
                this["IncludeAngularJS"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the require js should be included.
        /// </summary>
        /// <value>
        /// true if include require js, false if not.
        /// </value>
        [ConfigurationProperty("IncludeRequireJS", DefaultValue = true)]
        [ObjectInfo(Title = "Include RequireJS", Description = "Includes RequireJS on every page")]
        public bool IncludeRequireJS
        {
            get
            {
                return (bool)this["IncludeRequireJS"];
            }
            set
            {
                this["IncludeRequireJS"] = value;
            }
        }
    }
}
