// file:	Models\ListItemModel.cs
//
// summary:	Implements the list item model class
using Babaganoush.Sitefinity.Extensions;
using System.Collections.Generic;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lists.Model;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the list item.
    /// </summary>
    public class ListItemModel : ContentModel
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

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
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public ParentModel Parent { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public List<TaxonModel> Categories { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<TaxonModel> Tags { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ContentLifecycleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the active.
        /// </summary>
        /// <value>
        /// true if active, false if not.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected ListItem OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ListItemModel()
        {
            Categories = new List<TaxonModel>();
            Tags = new List<TaxonModel>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public ListItemModel(ListItem sfContent)
            : base(sfContent)
        {
            if (sfContent != null)
            {
                Content = sfContent.Content;
                Slug = sfContent.UrlName;
                Ordinal = sfContent.Ordinal;
                Status = sfContent.Status;
                Active = sfContent.Status == ContentLifecycleStatus.Live
                    && sfContent.Visible;

                if (sfContent.Parent != null)
                {
                    Parent = new ParentModel
                    {
                        Id = sfContent.Parent.Id,
                        Title = sfContent.Parent.Title,
                        Description = sfContent.Parent.Description,
                        Slug = sfContent.Parent.UrlName
                    };
                }

                //POPULATE TAXONOMIES TO LIST
                Categories = sfContent.GetTaxa("Category");
                Tags = sfContent.GetTaxa("Tags");

                // Store original content
                OriginalContent = sfContent;
            }
        }
    }
}