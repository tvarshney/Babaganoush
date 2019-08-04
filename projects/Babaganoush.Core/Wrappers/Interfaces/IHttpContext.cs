using System.Web;
using System.Web.UI;

namespace Babaganoush.Core.Wrappers.Interfaces
{
    /// <summary>
    /// An interface for exposing <see cref="HttpContext"/> methods and properties for dependency exposure and unit testing.
    /// </summary>
    public interface IHttpContext
    {
        /// <summary>
        /// Returns the physical file path that corresponds to the specified virtual path on the Web server.
        /// </summary>
        /// <exception cref="HttpException"></exception>
        string MapPath(string path);

        /// <summary>
        /// Returns the server variable with <paramref name="name"/> from the current HttpContext Request object.
        /// </summary>
        string GetServerVariable(string name);

        /// <summary>
        /// Gets the <see cref="IHttpHandler" /> <see cref="Page"/> object that represents the currently executing handler.
        /// </summary>
        Page GetCurrentHandler();

        /// <summary>
        /// Gets the physical file system path of the currently executing server application's root directory.
        /// </summary>
        string GetPhysicalApplicationPath();
    }
}