// file:	Models\VideoModel.cs
//
// summary:	Implements the video model class
using Telerik.Sitefinity.Libraries.Model;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the video.
    /// </summary>
    public class VideoModel : MediaModel
    {
        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected Video OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public VideoModel()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf media.</param>
        public VideoModel(Video sfContent)
            : base(sfContent)
        {
            // Store original content
            OriginalContent = sfContent;
        }
    }
}