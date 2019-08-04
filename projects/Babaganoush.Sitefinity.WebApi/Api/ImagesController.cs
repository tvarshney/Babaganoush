// file:	Api\ImagesController.cs
//
// summary:	Implements the images controller class
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using Telerik.Sitefinity.Libraries.Model;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for images.
    /// </summary>
    public class ImagesController : BaseMediaController<ImageModel, Image>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagesController" /> class.
        /// </summary>
        public ImagesController()
            : base(BabaManagers.Images)
        {

        }
    }
}