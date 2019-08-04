
namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for role.
    /// </summary>
    public interface IRole : IIdentity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        ///
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        ///
        /// <value>
        /// The provider.
        /// </value>
        string Provider { get; set; }
    }
}
