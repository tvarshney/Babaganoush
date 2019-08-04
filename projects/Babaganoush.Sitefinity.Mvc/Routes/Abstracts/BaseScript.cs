using System.Web;
using System.Web.Routing;

namespace Babaganoush.Sitefinity.Mvc.Routes.Abstracts
{
    /// <summary>
    /// Outputs base url as JavaScript module for relative pathing.
    /// </summary>
    public class BaseScript : IHttpHandler, IRouteHandler
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        ///
        /// <value>
        /// The value.
        /// </value>
        public virtual string Value { get; set; }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the
        /// <see cref="T:System.Web.IHttpHandler" /> interface.
        /// </summary>
        ///
        /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides
        /// references to the intrinsic server objects (for example, Request, Response, Session, and
        /// Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context)
        {
            //RENDER REQUIREJS MODULE REPRESENTING THE ROOT APP PATH
            context.Response.ContentType = "text/javascript";
            context.Response.Write(ProcessValue(context));
        }

        /// <summary>
        /// Process the value described by context.
        /// </summary>
        ///
        /// <param name="context">The context.</param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public virtual string ProcessValue(HttpContext context)
        {
            return Value;
        }

        /// <summary>
        /// Provides the object that processes the request.
        /// </summary>
        ///
        /// <param name="requestContext">An object that encapsulates information about the request.</param>
        ///
        /// <returns>
        /// An object that processes the request.
        /// </returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return this;
        }

        /// <summary>
        /// Gets a value indicating whether another request can use the
        /// <see cref="T:System.Web.IHttpHandler" /> instance.
        /// </summary>
        ///
        /// <value>
        /// true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.
        /// </value>
        public bool IsReusable
        {
            get { return true; }
        }
    }
}
