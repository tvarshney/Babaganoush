// file:	Extensions\TaxonomyManagerExtensions.cs
//
// summary:	Implements the taxonomy manager extensions class
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Web;

namespace Babaganoush.Sitefinity.Extensions
{
    /// <summary>
    /// Extensions for the Sitefinity Taxonomy manager.
    /// </summary>
    public static class TaxonomyManagerExtensions
    {
        /// <summary>
        /// Deletes any tags in Sitefinity that are not used by anything.
        /// </summary>
        /// <param name="taxonomyManager">The manager to use to delete the unused tags.</param>
        public static void DeleteAllUnusedTags(this ITaxonomyManager taxonomyManager)
        {
            if (taxonomyManager == null)
            {
                return;
            }
            List<Taxon> existingTags = taxonomyManager.GetTaxonomy<FlatTaxonomy>(TaxonomyManager.TagsTaxonomyId).Taxa.ToList();
            List<Guid> existingTagIds = existingTags.Select(t => t.Id).ToList();
            var tagIdsToDelete = taxonomyManager.GetUnusedTaxonGuids(existingTagIds);
            foreach (Guid tagIdToDelete in tagIdsToDelete)
            {
                Taxon existingTagToDelete = existingTags.SingleOrDefault(t => t.Id == tagIdToDelete);
                if (existingTagToDelete != null)
                {
                    taxonomyManager.Delete(existingTagToDelete);
                }
            }
            taxonomyManager.SaveChanges();
        }

        /// <summary>
        /// Gets a collection of IDs of taxons that are no longer in use, or have never been used.
        /// </summary>
        /// <param name="taxonomyManager">The manager used to fetch the unused IDs.</param>
        /// <param name="existingTaxonIds">A collection of taxon IDs that exist in Sitefinity.</param>
        /// <returns>
        /// The unused taxon guids.
        /// </returns>
        public static IList<Guid> GetUnusedTaxonGuids(this ITaxonomyManager taxonomyManager, IList<Guid> existingTaxonIds)
        {
            if (taxonomyManager == null || existingTaxonIds == null || !existingTaxonIds.Any())
            {
                return new Guid[0];
            }
            IQueryable<TaxonomyStatistic> tagStats = ((TaxonomyManager)taxonomyManager).GetStatistics()
                .Where(stat => stat.StatisticType == ContentLifecycleStatus.Master && existingTaxonIds.Contains(stat.TaxonId));

            // Grab taxons with 0 items using them (i.e. taxon is no longer used).
            List<Guid> unusedTaxonGuids = tagStats.Where(stat => stat.MarkedItemsCount == 0).Select(stat => stat.TaxonId).ToList();

            // Also grab taxons with no statistics (i.e. taxon was never used).
            unusedTaxonGuids.AddRange(existingTaxonIds.Where(guid => !tagStats.Select(s => s.TaxonId).Contains(guid)));

            return unusedTaxonGuids.Distinct().ToList();
        }
    }
}