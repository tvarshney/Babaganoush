
namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for hierarchy.
    /// </summary>
    public interface IHierarchy
    {
        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        ///
        /// <value>
        /// The parent.
        /// </value>
        IDynamic Parent { get; set; }
    }
}
