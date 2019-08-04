using System;
using System.Web;

namespace Babaganoush.Core.Wrappers.Interfaces
{
    /// <summary>
    /// An interface for exposing <see cref="VirtualPathUtility"/> methods, for dependency exposure and unit testing.
    /// </summary>
    public interface IVirtualPathUtility
    {
        /// <summary>
        /// Converts a virtual path to an application absolute path.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="HttpException"></exception>
        string ToAbsolute(string virtualPath);

        /// <summary>
        /// Retrieves the file name of the file that is referenced in the virtual path.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        string GetFileName(string virtualPath);

        /// <summary>
        /// Converts a virtual path to an application-relative path using the application virtual path this is in the <see cref="HttpRuntime.AppDomainAppVirtualPath" /> property.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        string ToAppRelative(string virtualPath);
    }
}