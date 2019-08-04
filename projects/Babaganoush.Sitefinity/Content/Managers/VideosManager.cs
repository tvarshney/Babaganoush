// file:	Content\Managers\VideosManager.cs
//
// summary:	Implements the videos manager class
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using Babaganoush.Sitefinity.Models;
using System;
using System.Linq;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Manager for videos.
    /// </summary>
    public class VideosManager : BaseMediaManager<
        LibrariesManager,
        Video,
        VideosManager,
        VideoModel>
    {
        /// <summary>
        /// Gets the Sitefinity data.
        /// </summary>
        /// <param name="providerName">(Optional) the provider name to get.</param>
        /// <returns>
        /// An IQueryable&lt;Video&gt;
        /// </returns>
        protected override IQueryable<Video> Get(string providerName = null)
        {
            return GetManager(providerName).GetVideos();
        }

        /// <summary>
        /// Gets the Sitefinity data by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// A Video.
        /// </returns>
        protected override Video Get(Guid id, string providerName = null)
        {
            return GetManager(providerName).GetVideo(id);
        }

        /// <summary>
        /// Creates the Baba instance from the Sitefinity object.
        /// </summary>
        /// <param name="sfContent">Content of the sf.</param>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override VideoModel CreateInstance(Video sfContent)
        {
            return new VideoModel(sfContent);
        }
    }
}