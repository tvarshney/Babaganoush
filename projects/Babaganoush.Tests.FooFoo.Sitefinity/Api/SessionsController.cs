using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using Babaganoush.Tests.FooFoo.Sitefinity.Data;
using Babaganoush.Tests.FooFoo.Sitefinity.Models;

namespace Babaganoush.Tests.FooFoo.Sitefinity.Api
{
    public class SessionsController : BaseDynamicController<SessionModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionsController" /> class.
        /// </summary>
        public SessionsController()
            : base(FooFooManagers.Sessions)
        {

        }

        /// <summary>
        /// Query if this user is authenticated. This will apply across all web services.
        /// </summary>
        ///
        /// <returns>
        /// true if authenticated, false if not.
        /// </returns>
        //public override bool IsAuthenticated()
        //{
        //    return SecurityManager.IsBackendUser();
        //}
    }
}
