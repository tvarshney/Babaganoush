// file:	Content\Managers\TaxonomiesManager.cs
//
// summary:	Implements the taxonomies manager class
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
    /// Manager for taxonomies.
    /// </summary>
    public class TaxonomiesManager : BaseSingletonManager<TaxonomyManager, TaxonomiesManager>
    {
        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        public virtual IEnumerable<TaxonModel> GetAll(string providerName = null, Expression<Func<Taxon, bool>> filter = null, int take = 0, int skip = 0)
        {
            //GET CLASSIFICATIONS
            var sfItems = GetManager(providerName).GetTaxa<Taxon>();

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(i => new TaxonModel(i));
        }

        /// <summary>
        /// Gets the category by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// The by identifier.
        /// </returns>
        public virtual TaxonModel GetById(Guid id, string providerName = null)
        {
            var sfContent = GetManager(providerName).GetTaxon<Taxon>(id);
            return sfContent != null ? new TaxonModel(sfContent) : null;
        }

        /// <summary>
        /// Gets the category by UrlName.
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// The by name.
        /// </returns>
        public virtual TaxonModel GetByName(string value, string providerName = null)
        {
            var sfContent = GetManager(providerName).GetTaxa<Taxon>()
                .Where(p => p.UrlName.Equals(value, StringComparison.OrdinalIgnoreCase));

            return sfContent.Any()
                ? new TaxonModel(sfContent.SingleOrDefault())
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
        public virtual TaxonModel GetByTitle(string value, string providerName = null)
        {
            var sfContent = GetManager(providerName).GetTaxa<Taxon>()
                .Where(p => p.Title.Equals(value, StringComparison.OrdinalIgnoreCase));

            return sfContent.Any()
                ? new TaxonModel(sfContent.FirstOrDefault())
                : null;
        }

        /// <summary>
        /// Gets the categories by parent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// The by parent.
        /// </returns>
        public virtual IEnumerable<TaxonModel> GetByParent(string value, string providerName = null, Expression<Func<Taxon, bool>> filter = null, int take = 0, int skip = 0)
        {
            var sfItems = GetManager(providerName).GetTaxa<Taxon>()
                .Where(p => p.Parent.Name.Equals(value, StringComparison.OrdinalIgnoreCase));

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(i => new TaxonModel(i));
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
        public virtual IEnumerable<TaxonModel> GetByParentId(Guid id, string providerName = null, Expression<Func<Taxon, bool>> filter = null, int take = 0, int skip = 0)
        {
            var sfItems = GetManager(providerName).GetTaxa<Taxon>()
                .Where(p => p.Parent.Id == id);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(i => new TaxonModel(i));
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
        public virtual IEnumerable<TaxonModel> GetByParentTitle(string value, string providerName = null, Expression<Func<Taxon, bool>> filter = null, int take = 0, int skip = 0)
        {
            var sfItems = GetManager(providerName).GetTaxa<Taxon>()
                .Where(p => p.Parent.Title.Equals(value, StringComparison.OrdinalIgnoreCase));

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(i => new TaxonModel(i));
        }
    }
}