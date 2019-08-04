using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using Babaganoush.Core.Wrappers;
using Babaganoush.Core.Wrappers.Interfaces;

namespace Babaganoush.Sitefinity.Themes.Classes
{
    /// <summary>
    /// MasterPage Virtual File.
    /// </summary>
    public class MasterPageVirtualFile : VirtualFile
    {
        /// <summary>
        /// Full pathname of the virtual file.
        /// </summary>
        private readonly string _virtualPath;

        private readonly IVirtualPathUtility _virtualPathUtility;

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterPageVirtualFile"/> class.
        /// </summary>
        ///
        /// <param name="virtualPath">The virtual path to the resource represented by this instance.</param>
        public MasterPageVirtualFile(string virtualPath)
            : this(virtualPath, new VirtualPathUtilityWrapper())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterPageVirtualFile"/> class.
        /// </summary>
        ///
        /// <param name="virtualPath">The virtual path to the resource represented by this instance.</param>
        /// <param name="virtualPathUtility">The object used to resolve virtual paths.</param>
        public MasterPageVirtualFile(string virtualPath, IVirtualPathUtility virtualPathUtility)
            : base(virtualPath)
        {
            _virtualPath = virtualPath;
            _virtualPathUtility = virtualPathUtility;
        }

        /// <summary>
        /// When overridden in a derived class, returns a read-only stream to the virtual resource.
        /// </summary>
        ///
        /// <returns>
        /// A read-only stream to the virtual file.
        /// </returns>
        public override Stream Open()
        {
            if (HttpContext.Current != null)
            {
                //CACHE STREAM IF APPLICABLE
                if (HttpContext.Current.Cache[_virtualPath] == null)
                {
                    HttpContext.Current.Cache.Insert(_virtualPath, ReadResource(_virtualPath));
                }

                //USE CACHED STREAM
                return (Stream)HttpContext.Current.Cache[_virtualPath];
            }

            return ReadResource(_virtualPath);
        }

        /// <summary>
        /// Reads the resource stream.
        /// </summary>
        ///
        /// <param name="embeddedFileName">Name of the embedded file.</param>
        ///
        /// <returns>
        /// The resource.
        /// </returns>
        private Stream ReadResource(string embeddedFileName)
        {
            string resourceFileName = _virtualPathUtility.GetFileName(embeddedFileName);
            string path = Constants.VALUE_VIRTUAL_MASTERPAGE_NAMESPACE + "." + resourceFileName;
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
        }
    }
}
