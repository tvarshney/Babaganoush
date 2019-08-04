// file:	Api\VideosController.cs
//
// summary:	Implements the videos controller class
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using Telerik.Sitefinity.Libraries.Model;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for videos.
    /// </summary>
    public class VideosController : BaseMediaController<VideoModel, Video>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VideosController" /> class.
        /// </summary>
        public VideosController()
            : base(BabaManagers.Videos)
        {

        }
    }
}