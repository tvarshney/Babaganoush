using System;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for identity.
    /// </summary>
    public interface IIdentity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        ///
        /// <value>
        /// The identifier.
        /// </value>
        Guid Id { get; set; }
    }
}
