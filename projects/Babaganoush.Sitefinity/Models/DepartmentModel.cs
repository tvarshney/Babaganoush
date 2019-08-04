// file:	Models\DepartmentModel.cs
//
// summary:	Implements the department model class
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the department.
    /// </summary>
    public class DepartmentModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets URL of the full.
        /// </summary>
        /// <value>
        /// The full URL.
        /// </value>
        public string FullUrl { get; set; }

        /// <summary>
        /// Gets or sets the ordinal.
        /// </summary>
        /// <value>
        /// The ordinal.
        /// </value>
        public float Ordinal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether as link should be rendered.
        /// </summary>
        /// <value>
        /// true if render as link, false if not.
        /// </value>
        public bool RenderAsLink { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the in navigation is shown.
        /// </summary>
        /// <value>
        /// true if show in navigation, false if not.
        /// </value>
        public bool ShowInNavigation { get; set; }

        /// <summary>
        /// Gets or sets the name of the taxon.
        /// </summary>
        /// <value>
        /// The name of the taxon.
        /// </value>
        public string TaxonName { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        /// <value>
        /// The slug.
        /// </value>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the last modified.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public ParentModel Parent { get; set; }

        /// <summary>
        /// Gets or sets the subtaxa.
        /// </summary>
        /// <value>
        /// The subtaxa.
        /// </value>
        public List<DepartmentModel> Subtaxa { get; set; }

        /// <summary>
        /// Gets or sets the number of.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected HierarchicalTaxon OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DepartmentModel()
        {
            Subtaxa = new List<DepartmentModel>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public DepartmentModel(HierarchicalTaxon sfContent)
        {
            var manager = TaxonomyManager.GetManager();

            Id = sfContent.Id;
            Title = sfContent.Title;
            Description = sfContent.Description;
            FullUrl = sfContent.FullUrl;
            Ordinal = sfContent.Ordinal;
            RenderAsLink = sfContent.RenderAsLink;
            ShowInNavigation = sfContent.ShowInNavigation;
            TaxonName = sfContent.Taxonomy.TaxonName;
            Slug = sfContent.UrlName;
            LastModified = sfContent.LastModified;

            //STORE PARENT DETAILS IF APPLICABLE
            if (sfContent.Parent != null)
            {
                Parent = new ParentModel
                {
                    Id = sfContent.Parent.Id,
                    Title = sfContent.Parent.Title,
                    Description = sfContent.Parent.Description,
                    Ordinal = sfContent.Parent.Ordinal,
                    Slug = sfContent.Parent.UrlName
                };
            }

            //BUILD CHILDREN CATEGORIES
            Subtaxa = new List<DepartmentModel>();
            sfContent.Subtaxa.ToList().ForEach(c => 
                Subtaxa.Add(new DepartmentModel(c)));

            //GET NUMBER OF ITEMS IN CATEGORY
            Count = (int)manager.GetTaxonItemsCount(sfContent.Id, ContentLifecycleStatus.Live);

            // Store original content
            OriginalContent = sfContent;
        }
    }
}