
namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for parent.
    /// </summary>
    public interface IParent
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        ///
        /// <value>
        /// The title.
        /// </value>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        ///
        /// <value>
        /// The description.
        /// </value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        ///
        /// <value>
        /// The slug.
        /// </value>
        string Slug { get; set; }

        /// <summary>
        /// Gets or sets the ordinal.
        /// </summary>
        ///
        /// <value>
        /// The ordinal.
        /// </value>
        float Ordinal { get; set; }
    }
}
