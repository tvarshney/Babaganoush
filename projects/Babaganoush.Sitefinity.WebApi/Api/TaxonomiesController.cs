// file:	Api\TaxonomiesController.cs
//
// summary:	Implements the taxonomies controller class
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
    public class TaxonomiesController : BaseApiController
    {
        /// <summary>
        /// Gets all taxa.
        /// </summary>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the matched items.
        /// </returns>
        public virtual HttpResponseMessage Get(int take = 0, int skip = 0)
        {
            return new DataResponse(BabaManagers.Taxonomies.GetAll(take: take, skip: skip));
        }

        /// <summary>
        /// Gets the taxa by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// A TaxonModel.
        /// </returns>
        public virtual HttpResponseMessage Get(Guid id)
        {
            return new DataResponseSingle(BabaManagers.Taxonomies.GetById(id));
        }
    }
}