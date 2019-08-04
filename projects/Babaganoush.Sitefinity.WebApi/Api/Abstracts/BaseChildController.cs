// file:	Api\Abstracts\BaseChildController.cs
//
// summary:	Implements the base child controller class
using Babaganoush.Sitefinity.Content.Managers.Interfaces;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.WebApi.Models;
using System;
using System.Net.Http;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Sitefinity.WebApi.Api.Abstracts
{
    /// <summary>
    /// Base Web API controller.
    /// </summary>
    /// <typeparam name="TContentModel">Type of the content model.</typeparam>
    /// <typeparam name="TContent">Type of the content.</typeparam>
    public abstract class BaseChildController<TContentModel, TContent> : BaseContentController<TContentModel, TContent>
        where TContentModel : ContentModel, new()
        where TContent : IDataItem, IDynamicFieldsContainer
    {
        /// <summary>
        /// Gets or sets the content manager.
        /// </summary>
        /// <value>
        /// The manager.
        /// </value>
        public IChildManager<TContentModel, TContent> ChildManager
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController"/> class.
        /// </summary>
        public BaseChildController()
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public BaseChildController(IChildManager<TContentModel, TContent> manager)
            : base(manager)
        {
            ChildManager = manager;
        }

        /// <summary>
        /// Gets items by parent's UrlName.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the parents in this collection.
        /// </returns>
        public virtual HttpResponseMessage GetByParent(string value, int take = 0, int skip = 0)
        {
            return new DataResponse(ChildManager.GetByParent(value, take: take, skip: skip));
        }

        /// <summary>
        /// Gets items based off parent's ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the parent identifiers in this
        /// collection.
        /// </returns>
        public virtual HttpResponseMessage GetByParentId(Guid id, int take = 0, int skip = 0)
        {
            return new DataResponse(ChildManager.GetByParentId(id, take: take, skip: skip));
        }

        /// <summary>
        /// Gets items based off parent's title.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the parent titles in this collection.
        /// </returns>
        public virtual HttpResponseMessage GetByParentTitle(string value, int take = 0, int skip = 0)
        {
            return new DataResponse(ChildManager.GetByParentTitle(value, take: take, skip: skip));
        }
    }
}
