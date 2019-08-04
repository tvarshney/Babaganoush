using System;
using System.Collections.Generic;

namespace Babaganoush.Core.Models.Interfaces
{
    /// <summary>
    /// Interface for user.
    /// </summary>
    public interface IUser : IIdentity
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
        /// Gets or sets a value indicating whether this object is authenticated.
        /// </summary>
        ///
        /// <value>
        /// true if this object is authenticated, false if not.
        /// </value>
        bool IsAuthenticated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is backend user.
        /// </summary>
        ///
        /// <value>
        /// true if this object is backend user, false if not.
        /// </value>
        bool IsBackendUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is unrestricted.
        /// </summary>
        ///
        /// <value>
        /// true if this object is unrestricted, false if not.
        /// </value>
        bool IsUnrestricted { get; set; }

        /// <summary>
        /// Gets or sets the last login date.
        /// </summary>
        ///
        /// <value>
        /// The last login date.
        /// </value>
        DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        ///
        /// <value>
        /// The roles.
        /// </value>
        IList<IRole> Roles { get; set; }
    }
}
