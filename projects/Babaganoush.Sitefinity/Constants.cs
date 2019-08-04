// file:	Constants.cs
//
// summary:	Implements the constants class
namespace Babaganoush.Sitefinity
{
    /// <summary>
    /// Constants ad keys needed throughout the site.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Full pathname of the value custom virtual root file.
        /// </summary>
        public const string VALUE_CUSTOM_VIRTUAL_ROOT_PATH = "~/BabaganoushSF";

        /// <summary>
        /// Full pathname of the value default scripts file.
        /// </summary>
        public const string VALUE_DEFAULT_SCRIPTS_PATH = "~/Scripts";

        /// <summary>
        /// The value top styles placeholder.
        /// </summary>
        public const string VALUE_TOP_STYLES_PLACEHOLDER = "TopStylesPlaceHolder";

        /// <summary>
        /// The value top scripts placeholder.
        /// </summary>
        public const string VALUE_TOP_SCRIPTS_PLACEHOLDER = "TopScriptsPlaceHolder";

        /// <summary>
        /// The value kendo dataviz CSS.
        /// </summary>
        public const string VALUE_KENDO_DATAVIZ_CSS = "http://cdn.kendostatic.com/2014.1.318/styles/kendo.dataviz.min.css";

        /// <summary>
        /// Name of the value toolbox section.
        /// </summary>
        public const string VALUE_TOOLBOX_SECTION_NAME = "Extras";
    }

    /// <summary>
    /// Toolbox types for registering new controls.
    /// </summary>
    public enum ToolboxType
    {
        /// <summary>
        /// An enum constant representing the page controls option.
        /// </summary>
        PageControls,
        /// <summary>
        /// An enum constant representing the page layouts option.
        /// </summary>
        PageLayouts,
        /// <summary>
        /// An enum constant representing the form controls option.
        /// </summary>
        FormControls
    }

    /// <summary>
    /// Values that represent HierarchicalTaxonCompareType.
    /// </summary>
    public enum HierarchicalTaxonCompareType
    {
        /// <summary>
        /// An enum constant representing the equals option.
        /// </summary>
        Equals,
        /// <summary>
        /// An enum constant representing the starts with option.
        /// </summary>
        StartsWith,
        /// <summary>
        /// An enum constant representing the ends with option.
        /// </summary>
        EndsWith,
        /// <summary>
        /// An enum constant representing the contains option.
        /// </summary>
        Contains
    }

}
