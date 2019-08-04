// file:	Models\UserModel.cs
//
// summary:	Implements the user model class
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Security.Claims;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the user.
    /// </summary>
    public class UserModel
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
        /// Gets or sets a value indicating whether this object is authenticated.
        /// </summary>
        /// <value>
        /// true if this object is authenticated, false if not.
        /// </value>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is backend user.
        /// </summary>
        /// <value>
        /// true if this object is backend user, false if not.
        /// </value>
        public bool IsBackendUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is unrestricted.
        /// </summary>
        /// <value>
        /// true if this object is unrestricted, false if not.
        /// </value>
        public bool IsUnrestricted { get; set; }

        /// <summary>
        /// Gets or sets the last login date.
        /// </summary>
        /// <value>
        /// The last login date.
        /// </value>
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public List<RoleModel> Roles { get; set; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected ClaimsIdentityProxy OriginalContent { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public UserModel(ClaimsIdentityProxy sfContent)
        {
            if (sfContent != null)
            {
                //SET DEFAULT PROPERTIES
                Id = sfContent.UserId;
                Name = sfContent.Name;
                IsAuthenticated = sfContent.IsAuthenticated;
                IsBackendUser = sfContent.IsBackendUser;
                IsUnrestricted = sfContent.IsUnrestricted;
                LastLoginDate = sfContent.LastLoginDate;

                //GET ROLES
                Roles = new List<RoleModel>();
                if (sfContent.Roles != null && sfContent.Roles.Count() > 0)
                {
                    sfContent.Roles.ToList().ForEach(
                        r => Roles.Add(new RoleModel(r)));
                }

                // Store original content
                OriginalContent = sfContent;
            }
        }
    }
}
