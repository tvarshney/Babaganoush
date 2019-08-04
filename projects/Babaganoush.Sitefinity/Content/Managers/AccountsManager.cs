// file:	Content\Managers\AccountsManager.cs
//
// summary:	Implements the accounts manager class
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using Babaganoush.Sitefinity.Models;
using System;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Manager for accounts.
    /// </summary>
    public class AccountsManager : BaseSingletonManager<UserManager, AccountsManager>
    {
        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <returns>
        /// The current.
        /// </returns>
        public UserModel GetCurrent()
        {
            return new UserModel(ClaimsManager.GetCurrentIdentity());
        }

        /// <summary>
        /// Checks if current user is in role.
        /// </summary>
        /// <returns>Is user in role.</returns>
        public bool IsCurrentUserInRole(string roleName)
        {
            bool isUserInRole = false;
            var user = GetCurrent();
            if (user.Id == Guid.Empty)
            {
                return isUserInRole;
            }

            var roleManager = RoleManager.GetManager(SecurityManager.ApplicationRolesProviderName);
            isUserInRole = roleManager.IsUserInRole(user.Id, roleName);

            return isUserInRole;
        }
    }
}