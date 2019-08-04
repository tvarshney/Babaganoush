// file:	Utilities\ResourceHelper.cs
//
// summary:	Implements the resource helper class
using System;

namespace Babaganoush.Sitefinity.Utilities
{
    /// <summary>
    /// A resource helper.
    /// </summary>
    public static class ResourceHelper
    {
        /// <summary>
        /// Converts the given <paramref name="path"/> to a virtual path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// path as a string.
        /// </returns>
        public static string ToVirtualPath(string path)
        {
            //VALIDATE INPUT
            if (String.IsNullOrWhiteSpace(path))
            {
                return String.Empty;
            }

            //PREPEND VIRTUAL PATH AND NAMESPACE FOR SITEFINITY TO RESOLVE:
            //http://www.sitefinity.com/blogs/slavoingilizov/posts/slavo-ingilizovs-blog/2011/04/18/taking_advantage_of_the_virtual_path_provider_in_sitefinity_4_1
            return Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/" + path;
        }
    }
}
