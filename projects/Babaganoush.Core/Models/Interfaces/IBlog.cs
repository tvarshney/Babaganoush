
namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for blog.
    /// </summary>
    public interface IBlog : IContent
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        ///
        /// <value>
        /// The description.
        /// </value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets URL of the document.
        /// </summary>
        ///
        /// <value>
        /// The URL.
        /// </value>
        string Url { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        ///
        /// <value>
        /// The slug.
        /// </value>
        string Slug { get; set; }

        /// <summary>
        /// Gets or sets the number of posts.
        /// </summary>
        ///
        /// <value>
        /// The number of posts.
        /// </value>
        int PostsCount { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        ///
        /// <value>
        /// The image.
        /// </value>
        string Image { get; set; }
    }
}