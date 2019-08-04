
namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for document.
    /// </summary>
    public interface IDocument : IMedia
    {
        /// <summary>
        /// Gets or sets the other.
        /// </summary>
        ///
        /// <value>
        /// The other.
        /// </value>
        string Other { get; set; }
    }
}