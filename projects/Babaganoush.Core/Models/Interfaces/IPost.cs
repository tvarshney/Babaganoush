
namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for post.
    /// </summary>
    public interface IPost : IContent, IClassified
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        ///
        /// <value>
        /// The content.
        /// </value>
        string Content { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        ///
        /// <value>
        /// The summary.
        /// </value>
        string Summary { get; set; }

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
        /// Gets or sets the parent.
        /// </summary>
        ///
        /// <value>
        /// The parent.
        /// </value>
        IBlog Parent { get; set; }

        /// <summary>
        /// Gets or sets the number of comments.
        /// </summary>
        ///
        /// <value>
        /// The number of comments.
        /// </value>
        int CommentsCount { get; set; }

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