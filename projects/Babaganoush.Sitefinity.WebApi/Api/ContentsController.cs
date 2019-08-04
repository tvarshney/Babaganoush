// file:	Api\ContentsController.cs
//
// summary:	Implements the contents controller class
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using System;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for contents.
    /// </summary>
    public class ContentsController : BaseApiController
    {
        /// <summary>
        /// Gets the content item with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// A string.
        /// </returns>
        public virtual string Get(Guid id)
        {
            return BabaManagers.Contents.GetById(id);
        }

        /// <summary>
        /// Gets a live content item's content by its UrlName property (case-insensitive).
        /// </summary>
        /// <param name="value">The name.</param>
        /// <returns>
        /// The by name.
        /// </returns>
        public virtual string GetByName(string value)
        {
            return BabaManagers.Contents.GetByName(value);
        }

        /// <summary>
        /// Gets a live content item's content by its Title property (case insensitive).
        /// </summary>
        /// <param name="value">The title.</param>
        /// <returns>
        /// The by title.
        /// </returns>
        public virtual string GetByTitle(string value)
        {
            return BabaManagers.Contents.GetByTitle(value);
        }
    }
}