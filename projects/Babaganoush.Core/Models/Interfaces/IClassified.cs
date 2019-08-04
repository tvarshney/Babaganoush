using System.Collections.Generic;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for classified.
    /// </summary>
    public interface IClassified
    {
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        ///
        /// <value>
        /// The categories.
        /// </value>
        IList<ITaxon> Categories { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        ///
        /// <value>
        /// The tags.
        /// </value>
        IList<ITaxon> Tags { get; set; }
    }
}
