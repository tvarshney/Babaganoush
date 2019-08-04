// file:	Api\PagesController.cs
//
// summary:	Implements the pages controller class
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using Babaganoush.Sitefinity.WebApi.Models;
using System;
using System.Net.Http;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for pages.
    /// </summary>
    public class PagesController : BaseApiController
    {
        /// <summary>
        /// Gets all pages.
        /// </summary>
        /// <returns>
        /// A PageModel.
        /// </returns>
        public virtual HttpResponseMessage Get()
        {
            return new DataResponseSingle(BabaManagers.Pages.GetAll(includeRelatedData: false));
        }

        /// <summary>
        /// Gets the page by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// A PageModel.
        /// </returns>
        public virtual HttpResponseMessage Get(Guid id)
        {
            return new DataResponseSingle(BabaManagers.Pages.GetById(id, includeRelatedData: false));
        }

        /// <summary>
        /// Gets the page by URL.
        /// </summary>
        /// <param name="value">String url name.</param>
        /// <returns>
        /// The by URL.
        /// </returns>
        public virtual HttpResponseMessage GetByUrl(string value)
        {
            return new DataResponseSingle(BabaManagers.Pages.GetByUrl(value, includeRelatedData: false));
        }
    }
}