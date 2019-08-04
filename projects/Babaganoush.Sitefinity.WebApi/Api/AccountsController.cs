// file:	Api\AccountsController.cs
//
// summary:	Implements the accounts controller class
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using System.Web;
using System.Web.Http;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for accounts.
    /// </summary>
    public class AccountsController : BaseApiController
    {
        /// <summary>
        /// Gets the current user account.
        /// </summary>
        /// <returns>
        /// The current.
        /// </returns>
        public virtual UserModel GetCurrent()
        {
            return BabaManagers.Accounts.GetCurrent();
        }

        /// <summary>
        /// Logs in <paramref name="username"/>.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="persistent">(Optional) Whether or not the login should be persisted.</param>
        /// <returns>
        /// An UserLoggingReason.
        /// </returns>
        [HttpGet]
        public virtual UserLoggingReason Login(string username, string password, bool persistent = true)
        {
            //INITIALIZE VARIABLES
            var userManager = UserManager.GetManager();
            User currentUser;

            //AUTHENTICATE USER
            var reason = SecurityManager.AuthenticateUser(
                userManager.Provider.Name, username, password, persistent, out currentUser);

            //HANDLE IF ALREADY LOGGED IN
            if (reason == UserLoggingReason.UserAlreadyLoggedIn)
            {
                //LOGOUT AND RE-LOG IN
                ClaimsManager.Logout(HttpContext.Current, ClaimsManager.GetCurrentPrincipal());
                reason = SecurityManager.AuthenticateUser(
                    userManager.Provider.Name, username, password, persistent, out currentUser);
            }

            //RETURN REASON
            return reason;
        }
    }
}