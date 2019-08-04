using System;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for dynamic.
    /// </summary>
    public interface IDynamic : IContent
    {
        /// <summary>
        /// Gets or sets the identifier of the original content.
        /// </summary>
        ///
        /// <value>
        /// The identifier of the original content.
        /// </value>
        Guid OriginalContentId { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        ///
        /// <value>
        /// The slug.
        /// </value>
        string Slug { get; set; }

        /// <summary>
        /// Gets the type of the mapped.
        /// </summary>
        ///
        /// <value>
        /// The type of the mapped.
        /// </value>
        string MappedType { get; }
    }
}
