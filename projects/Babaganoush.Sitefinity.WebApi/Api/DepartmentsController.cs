// file:	Api\DepartmentsController.cs
//
// summary:	Implements the departments controller class
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using Babaganoush.Sitefinity.WebApi.Models;
using System;
using System.Net.Http;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for store departments.
    /// </summary>
    public class DepartmentsController : BaseApiController
    {
        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the matched items.
        /// </returns>
        public virtual HttpResponseMessage Get()
        {
            return new DataResponse(BabaManagers.Departments.GetAll());
        }

        /// <summary>
        /// Gets the category by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// A DepartmentModel.
        /// </returns>
        public virtual HttpResponseMessage Get(Guid id)
        {
            return new DataResponseSingle(BabaManagers.Departments.GetById(id));
        }

        /// <summary>
        /// Gets the category by UrlName.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <returns>
        /// The by name.
        /// </returns>
        public virtual HttpResponseMessage GetByName(string value)
        {
            return new DataResponseSingle(BabaManagers.Departments.GetByName(value));
        }

        /// <summary>
        /// Gets the category by title.
        /// </summary>
        /// <param name="value">The title.</param>
        /// <returns>
        /// The by title.
        /// </returns>
        public virtual HttpResponseMessage GetByTitle(string value)
        {
            return new DataResponseSingle(BabaManagers.Departments.GetByTitle(value));
        }

        /// <summary>
        /// Gets the categories by parent name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the parents in this collection.
        /// </returns>
        public virtual HttpResponseMessage GetByParent(string value)
        {
            return new DataResponse(BabaManagers.Departments.GetByParent(value));
        }

        /// <summary>
        /// Gets the categories by parent id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the parent identifiers in this
        /// collection.
        /// </returns>
        public virtual HttpResponseMessage GetByParentId(Guid id)
        {
            return new DataResponse(BabaManagers.Departments.GetByParentId(id));
        }

        /// <summary>
        /// Gets the categories by parent title.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the parent titles in this collection.
        /// </returns>
        public virtual HttpResponseMessage GetByParentTitle(string value)
        {
            return new DataResponse(BabaManagers.Departments.GetByParentTitle(value));
        }
    }
}