// file:	Models\RoleModel.cs
//
// summary:	Implements the role model class
using System;
using Telerik.Sitefinity.Security;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the role.
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public string Provider { get; set; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected RoleInfo OriginalContent { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public RoleModel(RoleInfo sfContent)
        {
            //SET DEFAULT PROPERTIES
            Id = sfContent.Id;
            Name = sfContent.Name;
            Provider = sfContent.Provider;

            // Store original content
            OriginalContent = sfContent;
        }
    }
}
