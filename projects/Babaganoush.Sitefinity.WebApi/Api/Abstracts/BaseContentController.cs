// file:	Api\Abstracts\BaseContentController.cs
//
// summary:	Implements the base content controller class
using Babaganoush.Sitefinity.Configuration;
using Babaganoush.Sitefinity.Content.Managers.Interfaces;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.WebApi.Models;
using System;
using System.Net.Http;
using System.Web.Http;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Sitefinity.WebApi.Api.Abstracts
{
    /// <summary>
    /// Base Web API controller.
    /// </summary>
    /// <typeparam name="TContentModel">Type of the content model.</typeparam>
    /// <typeparam name="TContent">Type of the content.</typeparam>
    public abstract class BaseContentController<TContentModel, TContent> : BaseApiController
        where TContentModel : ContentModel, new()
        where TContent : IDataItem, IDynamicFieldsContainer
    {
        /// <summary>
        /// Gets or sets the content manager.
        /// </summary>
        /// <value>
        /// The manager.
        /// </value>
        public IContentManager<TContentModel, TContent> Manager
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController"/> class.
        /// </summary>
        public BaseContentController()
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public BaseContentController(IContentManager<TContentModel, TContent> manager)
        {
            Manager = manager;
        }

        /// <summary>
        /// Gets all news.
        /// </summary>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the matched items.
        /// </returns>
        public virtual HttpResponseMessage Get(int take = 0, int skip = 0)
        {
            return new DataResponse(Manager.GetAll(
                take: take > 0 ? take : Config.Get<BabaganoushConfig>().Services.DefaultMaxLimit,
                skip: skip));
        }

        /// <summary>
        /// Gets the news by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// A T.
        /// </returns>
        public virtual HttpResponseMessage Get(Guid id)
        {
            return new DataResponseSingle(Manager.GetById(id));
        }

        /// <summary>
        /// Gets the news by UrlName.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <returns>
        /// The by name.
        /// </returns>
        public virtual HttpResponseMessage GetByName(string value)
        {
            return new DataResponseSingle(Manager.GetByName(value));
        }

        /// <summary>
        /// Gets the news item by title.
        /// </summary>
        /// <param name="value">The title.</param>
        /// <returns>
        /// The by title.
        /// </returns>
        public virtual HttpResponseMessage GetByTitle(string value)
        {
            return new DataResponseSingle(Manager.GetByTitle(value));
        }

        /// <summary>
        /// Gets the top <paramref name="take"/> most recent news item(s).
        /// </summary>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the recents in this collection.
        /// </returns>
        public virtual HttpResponseMessage GetRecent(int take = 25, int skip = 0)
        {
            return new DataResponse(Manager.GetRecent(take: take, skip: skip));
        }

        /// <summary>
        /// Searches the news titles and contents.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the matched items.
        /// </returns>
        [HttpGet]
        public virtual HttpResponseMessage Search(string value, int take = 0, int skip = 0)
        {
            return new DataResponse(Manager.Search(value, take: take, skip: skip));
        }

        /// <summary>
        /// Gets the news items by category name.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the categories in this collection.
        /// </returns>
        public virtual HttpResponseMessage GetByCategory(string value, int take = 0, int skip = 0)
        {
            return new DataResponse(Manager.GetByCategory(value, take: take, skip: skip));
        }

        /// <summary>
        /// Gets the news items by tag.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the tags in this collection.
        /// </returns>
        public virtual HttpResponseMessage GetByTag(string value, int take = 0, int skip = 0)
        {
            return new DataResponse(Manager.GetByTag(value, take: take, skip: skip));
        }

        /// <summary>
        /// Gets the news items by category ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the category identifiers in this
        /// collection.
        /// </returns>
        public virtual HttpResponseMessage GetByCategoryId(Guid id, int take = 0, int skip = 0)
        {
            return new DataResponse(Manager.GetByCategoryId(id, take: take, skip: skip));
        }

        /// <summary>
        /// Gets the news items by tag id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the tag identifiers in this
        /// collection.
        /// </returns>
        public virtual HttpResponseMessage GetByTagId(Guid id, int take = 0, int skip = 0)
        {
            return new DataResponse(Manager.GetByTagId(id, take: take, skip: skip));
        }

        /// <summary>
        /// Gets the taxonomies in this collection.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The name.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the taxonomies in this collection.
        /// </returns>
        public virtual HttpResponseMessage GetByTaxonomy(string key, string value, int take = 0, int skip = 0)
        {
            return new DataResponse(Manager.GetByTaxonomy(key, value, take: take, skip: skip));
        }

        /// <summary>
        /// Gets the taxonomy identifiers in this collection.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="id">The id.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the taxonomy identifiers in this
        /// collection.
        /// </returns>
        public virtual HttpResponseMessage GetByTaxonomyId(string key, Guid id, int take = 0, int skip = 0)
        {
            return new DataResponse(Manager.GetByTaxonomyId(key, id, take: take, skip: skip));
        }

        /// <summary>
        /// Gets the taxonomy titles in this collection.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The name.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the taxonomy titles in this
        /// collection.
        /// </returns>
        public virtual HttpResponseMessage GetByTaxonomyTitle(string key, string value, int take = 0, int skip = 0)
        {
            return new DataResponse(Manager.GetByTaxonomyTitle(key, value, take: take, skip: skip));
        }
    }
}
