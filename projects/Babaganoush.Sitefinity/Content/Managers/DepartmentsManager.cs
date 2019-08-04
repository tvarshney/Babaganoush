// file:	Content\Managers\DepartmentsManager.cs
//
// summary:	Implements the departments manager class
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Manager for departments.
    /// </summary>
    public class DepartmentsManager : BaseSingletonManager<TaxonomyManager, DepartmentsManager>
    {
        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        public virtual IEnumerable<DepartmentModel> GetAll(string providerName = null, int take = 0, int skip = 0)
        {
            //GET CLASSIFICATION
            var taxonomy = GetManager(providerName).GetTaxonomies<HierarchicalTaxonomy>()
                .First(t => t.Name == "Departments");

            //GET TOP-LEVEL DEPARTMENTS ONLY
            var sfItems = taxonomy.Taxa
                .Where(t => t.Parent == null);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(i => new DepartmentModel(i as HierarchicalTaxon));
        }

        /// <summary>
        /// Gets the category by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// The by identifier.
        /// </returns>
        public virtual DepartmentModel GetById(Guid id, string providerName = null)
        {
            var sfDepartment = GetManager(providerName).GetTaxon<HierarchicalTaxon>(id);
            return sfDepartment != null ? new DepartmentModel(sfDepartment) : null;
        }

        /// <summary>
        /// Gets the category by UrlName.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// The by name.
        /// </returns>
        public virtual DepartmentModel GetByName(string value, string providerName = null)
        {
            var sfDepartment = GetManager(providerName).GetTaxa<HierarchicalTaxon>()
                .Where(p => p.UrlName.Equals(value, StringComparison.OrdinalIgnoreCase));

            return sfDepartment.Any()
                ? new DepartmentModel(sfDepartment.SingleOrDefault())
                : null;
        }

        /// <summary>
        /// Gets the category by title.
        /// </summary>
        /// <param name="value">The title.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// The by title.
        /// </returns>
        public virtual DepartmentModel GetByTitle(string value, string providerName = null)
        {
            var sfDepartment = GetManager(providerName).GetTaxa<HierarchicalTaxon>()
                .Where(p => p.Title.Equals(value, StringComparison.OrdinalIgnoreCase));

            return sfDepartment.Any()
                ? new DepartmentModel(sfDepartment.FirstOrDefault())
                : null;
        }

        /// <summary>
        /// Gets the categories by parent name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// The by parent.
        /// </returns>
        public virtual IEnumerable<DepartmentModel> GetByParent(string value, string providerName = null, Expression<Func<HierarchicalTaxon, bool>> filter = null, int take = 0, int skip = 0)
        {
            var sfItems = GetManager(providerName).GetTaxa<HierarchicalTaxon>()
                .Where(p => p.Parent.Name.Equals(value, StringComparison.OrdinalIgnoreCase));

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(i => new DepartmentModel(i));
        }

        /// <summary>
        /// Gets the categories by parent id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// The by parent identifier.
        /// </returns>
        public virtual IEnumerable<DepartmentModel> GetByParentId(Guid id, string providerName = null, Expression<Func<HierarchicalTaxon, bool>> filter = null, int take = 0, int skip = 0)
        {
            var sfItems = GetManager(providerName).GetTaxa<HierarchicalTaxon>()
                .Where(p => p.Parent.Id == id);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(i => new DepartmentModel(i));
        }

        /// <summary>
        /// Gets the categories by parent title.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// The by parent title.
        /// </returns>
        public virtual IEnumerable<DepartmentModel> GetByParentTitle(string value, string providerName = null, Expression<Func<HierarchicalTaxon, bool>> filter = null, int take = 0, int skip = 0)
        {
            var sfItems = GetManager(providerName).GetTaxa<HierarchicalTaxon>()
                .Where(p => p.Parent.Title.Equals(value, StringComparison.OrdinalIgnoreCase));

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(i => new DepartmentModel(i));
        }
    }
}