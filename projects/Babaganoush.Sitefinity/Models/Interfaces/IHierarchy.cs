// file:	Models\Interfaces\IHierarchy.cs
//
// summary:	Declares the IHierarchy interface
namespace Babaganoush.Sitefinity.Models.Interfaces
{
    /// <summary>
    /// Interface for hierarchy.
    /// </summary>
    public interface IHierarchy
    {
        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        DynamicModel Parent { get; set; }
    }
}
