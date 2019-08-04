
namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for widget.
    /// </summary>
    public interface IWidget : IDynamic
    {
        /// <summary>
        /// Gets or sets the styles.
        /// </summary>
        ///
        /// <value>
        /// The styles.
        /// </value>
        string Styles { get; set; }

        /// <summary>
        /// Gets or sets the scripts.
        /// </summary>
        ///
        /// <value>
        /// The scripts.
        /// </value>
        string Scripts { get; set; }

        /// <summary>
        /// Gets or sets the templates.
        /// </summary>
        ///
        /// <value>
        /// The templates.
        /// </value>
        string Templates { get; set; }

        /// <summary>
        /// Gets or sets the usage.
        /// </summary>
        ///
        /// <value>
        /// The usage.
        /// </value>
        string Usage { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        ///
        /// <value>
        /// The description.
        /// </value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the HTML footer.
        /// </summary>
        ///
        /// <value>
        /// The HTML footer.
        /// </value>
        string HtmlFooter { get; set; }

        /// <summary>
        /// Gets or sets the script target.
        /// </summary>
        ///
        /// <value>
        /// The script target.
        /// </value>
        string ScriptTarget { get; set; }

        /// <summary>
        /// Gets or sets the style files.
        /// </summary>
        ///
        /// <value>
        /// The style files.
        /// </value>
        string StyleFiles { get; set; }

        /// <summary>
        /// Gets or sets the HTML.
        /// </summary>
        ///
        /// <value>
        /// The HTML.
        /// </value>
        string Html { get; set; }

        /// <summary>
        /// Gets or sets the HTML head.
        /// </summary>
        ///
        /// <value>
        /// The HTML head.
        /// </value>
        string HtmlHead { get; set; }

        /// <summary>
        /// Gets or sets the script files.
        /// </summary>
        ///
        /// <value>
        /// The script files.
        /// </value>
        string ScriptFiles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the jquery should be included.
        /// </summary>
        ///
        /// <value>
        /// true if include jquery, false if not.
        /// </value>
        bool IncludeJquery { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the require js should be included.
        /// </summary>
        ///
        /// <value>
        /// true if include require js, false if not.
        /// </value>
        bool IncludeRequireJS { get; set; }
    }
}
