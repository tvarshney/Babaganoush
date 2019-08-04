using System.Web;
using System.Web.UI;
using Babaganoush.Core.Wrappers.Interfaces;

namespace Babaganoush.Core.Wrappers
{
    /// <summary>
    /// A wrapper class for encapsulating the <see cref="HttpContext"/> static class for dependency exposure and unit testing.
    /// Named "SystemHttpContextWrapper" instead of HttpContextWrapper since the latter already exists in .NET.
    /// </summary>
    public class SystemHttpContextWrapper : IHttpContext
    {
        /// <summary>
        /// Returns the physical file path that corresponds to the specified virtual path on the Web server.
        /// </summary>
        /// <exception cref="HttpException"></exception>
        public string MapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        /// <summary>
        /// Returns the server variable with <paramref name="name"/> from the current HttpContext Request object.
        /// </summary>
        public string GetServerVariable(string name)
        {
            return HttpContext.Current.Request.ServerVariables[name];
        }

        /// <summary>
        /// Gets the <see cref="IHttpHandler" /> <see cref="Page"/> object that represents the currently executing handler.
        /// </summary>
        public Page GetCurrentHandler()
        {
            return HttpContext.Current.CurrentHandler as Page;
        }

        /// <summary>
        /// Gets the physical file system path of the currently executing server application's root directory.
        /// </summary>
        public string GetPhysicalApplicationPath()
        {
            return HttpContext.Current.Request.PhysicalApplicationPath;
        }
    }
}