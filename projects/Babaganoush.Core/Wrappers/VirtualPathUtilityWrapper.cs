using System;
using System.Web;
using Babaganoush.Core.Wrappers.Interfaces;

namespace Babaganoush.Core.Wrappers
{
    /// <summary>
    /// A wrapper class for encapsulating the <see cref="VirtualPathUtility"/> static class for dependency exposure and unit testing.
    /// </summary>
    public class VirtualPathUtilityWrapper : IVirtualPathUtility
    {
        /// <summary>
        /// Converts a virtual path to an application absolute path.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="HttpException"></exception>
        public string ToAbsolute(string virtualPath)
        {
            return VirtualPathUtility.ToAbsolute(virtualPath);
        }

        /// <summary>
        /// Retrieves the file name of the file that is referenced in the virtual path.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public string GetFileName(string virtualPath)
        {
            return VirtualPathUtility.GetFileName(virtualPath);
        }

        /// <summary>
        /// Converts a virtual path to an application-relative path using the application virtual path this is in the <see cref="HttpRuntime.AppDomainAppVirtualPath" /> property.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public string ToAppRelative(string virtualPath)
        {
            return VirtualPathUtility.ToAppRelative(virtualPath);
        }
    }
}