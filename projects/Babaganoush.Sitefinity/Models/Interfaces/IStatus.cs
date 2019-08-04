// file:	Models\Interfaces\IStatus.cs
//
// summary:	Declares the IStatus interface
using Telerik.Sitefinity.GenericContent.Model;

namespace Babaganoush.Sitefinity.Models.Interfaces
{
    /// <summary>
    /// Interface for status.
    /// </summary>
    public interface IStatus
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        ContentLifecycleStatus Status { get; set; }
    }
}
