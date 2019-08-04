using System;
using System.Collections;
using System.Web.Caching;
using System.Web.Hosting;
using Babaganoush.Core.Wrappers;
using Babaganoush.Core.Wrappers.Interfaces;

namespace Babaganoush.Sitefinity.Themes.Classes
{
    /// <summary>
    /// Master Page Virtual Path Provider.
    /// </summary>
    public class MasterPageVirtualPathProvider : VirtualPathProvider
    {
        private readonly IVirtualPathUtility _virtualPathUtility;

        /// <summary>
        /// Creates a new instance of <see cref="MasterPageVirtualPathProvider"/>, with default dependencies used.
        /// </summary>
        public MasterPageVirtualPathProvider()
            : this(new VirtualPathUtilityWrapper())
        { }

        /// <summary>
        /// Creates a new instance of <see cref="MasterPageVirtualPathProvider"/>, using the given dependencies.
        /// </summary>
        public MasterPageVirtualPathProvider(IVirtualPathUtility virtualPathUtility)
        {
            _virtualPathUtility = virtualPathUtility;
        }

        /// <summary>
        /// Gets a value that indicates whether a file exists in the virtual file system.
        /// </summary>
        ///
        /// <param name="virtualPath">The path to the virtual file.</param>
        ///
        /// <returns>
        /// true if the file exists in the virtual file system; otherwise, false.
        /// </returns>
        public override bool FileExists(string virtualPath)
        {
            if (IsProviderPath(virtualPath))
            {
                var file = (MasterPageVirtualFile)GetFile(virtualPath);
                return file != null;
            }
            return Previous.FileExists(virtualPath);
        }

        /// <summary>
        /// Gets a virtual file from the virtual file system.
        /// </summary>
        ///
        /// <param name="virtualPath">The path to the virtual file.</param>
        ///
        /// <returns>
        /// A descendent of the <see cref="T:System.Web.Hosting.VirtualFile"></see> class that represents
        /// a file in the virtual file system.
        /// </returns>
        public override VirtualFile GetFile(string virtualPath)
        {
            return IsProviderPath(virtualPath)
                ? new MasterPageVirtualFile(virtualPath, _virtualPathUtility) : Previous.GetFile(virtualPath);
        }

        /// <summary>
        /// Creates a cache dependency based on the specified virtual paths.
        /// </summary>
        ///
        /// <param name="virtualPath">The path to the primary virtual resource.</param>
        /// <param name="virtualPathDependencies">An array of paths to other resources required by the
        /// primary virtual resource.</param>
        /// <param name="utcStart">The UTC time at which the virtual resources were read.</param>
        ///
        /// <returns>
        /// A <see cref="T:System.Web.Caching.CacheDependency"></see> object for the specified virtual
        /// resources.
        /// </returns>
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            return null;
        }

        /// <summary>
        /// Determines whether [is path virtual] [the specified virtual path].
        /// </summary>
        ///
        /// <param name="virtualPath">The virtual path.</param>
        ///
        /// <returns>
        /// <c>true</c> if [is path virtual] [the specified virtual path]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsProviderPath(string virtualPath)
        {
            string path = _virtualPathUtility.ToAppRelative(virtualPath);
            return path.StartsWith(Constants.VALUE_VIRTUAL_MASTERPAGE_PATH, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
