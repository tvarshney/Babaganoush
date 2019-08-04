// file:	Api\Abstracts\BaseMediaController.cs
//
// summary:	Implements the base media controller class
using Babaganoush.Sitefinity.Content.Managers.Interfaces;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.WebApi.Models;
using System.Net.Http;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Sitefinity.WebApi.Api.Abstracts
{
    /// <summary>
    /// Base Web API controller.
    /// </summary>
    /// <typeparam name="TContentModel">Type of the content model.</typeparam>
    /// <typeparam name="TContent">Type of the content.</typeparam>
    public abstract class BaseMediaController<TContentModel, TContent> : BaseChildController<TContentModel, TContent>
        where TContentModel : MediaModel, new()
        where TContent : IDataItem, IDynamicFieldsContainer
    {
        /// <summary>
        /// Gets or sets the content manager.
        /// </summary>
        /// <value>
        /// The manager.
        /// </value>
        public IMediaManager<TContentModel, TContent> MediaManager
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController"/> class.
        /// </summary>
        public BaseMediaController()
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public BaseMediaController(IMediaManager<TContentModel, TContent> manager)
            : base(manager)
        {
            MediaManager = manager;
        }

        /// <summary>
        /// Gets the media by extension.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the extensions in this collection.
        /// </returns>
        public virtual HttpResponseMessage GetByExtension(string value, int take = 0, int skip = 0)
        {
            return new DataResponse(MediaManager.GetByExtension(value, take: take, skip: skip));
        }
    }
}
