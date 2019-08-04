
namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for image.
    /// </summary>
    public interface IImage : IMedia
    {
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        ///
        /// <value>
        /// The width.
        /// </value>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        ///
        /// <value>
        /// The height.
        /// </value>
        int Height { get; set; }

        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        ///
        /// <value>
        /// The alternative text.
        /// </value>
        string AlternativeText { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        ///
        /// <value>
        /// The link.
        /// </value>
        string Link { get; set; }
    }
}