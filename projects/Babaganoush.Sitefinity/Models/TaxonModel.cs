// file:	Models\TaxonModel.cs
//
// summary:	Implements the taxon model class
using System;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the taxon.
    /// </summary>
    public class TaxonModel
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
        /// Gets or sets a value indicating whether the in navigation is shown.
        /// </summary>
        /// <value>
        /// true if show in navigation, false if not.
        /// </value>
        public bool ShowInNavigation { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        /// <value>
        /// The slug.
        /// </value>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the ordinal.
        /// </summary>
        /// <value>
        /// The ordinal.
        /// </value>
        public float Ordinal { get; set; }

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
        /// Gets or sets the classification.
        /// </summary>
        /// <value>
        /// The classification.
        /// </value>
        public string Classification { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is hierarchy.
        /// </summary>
        /// <value>
        /// true if this object is hierarchy, false if not.
        /// </value>
        public bool IsHierarchy { get; set; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected ITaxon OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TaxonModel()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public TaxonModel(ITaxon sfContent)
        {
            if (sfContent != null)
            {
                Id = sfContent.Id;
                Title = sfContent.Title;
                Description = sfContent.Description;
                ShowInNavigation = sfContent.ShowInNavigation;
                Slug = sfContent.UrlName;
                Ordinal = sfContent.Ordinal;
                LastModified = sfContent.LastModified;
                Classification = sfContent.Taxonomy.Name;

                //HANDLE HIERARCHY IF APPLICABLE
                IsHierarchy = sfContent is HierarchicalTaxon;
                if (IsHierarchy)
                {
                    var item = sfContent as HierarchicalTaxon;
                    if (item.Parent != null)
                    {
                        Parent = new ParentModel
                        {
                            Id = item.Parent.Id,
                            Title = item.Parent.Title,
                            Description = item.Parent.Description,
                            Ordinal = item.Parent.Ordinal,
                            Slug = item.Parent.UrlName
                        };
                    }
                }

                // Store original content
                OriginalContent = sfContent;
            }
        }
    }
}
