// file:	Models\PageModel.cs
//
// summary:	Implements the page model class
using System;
using System.Collections.Generic;
using System.Web;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Pages.Model;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the page.
    /// </summary>
    public class PageModel
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
        /// Gets or sets URL of the document.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is crawlable.
        /// </summary>
        /// <value>
        /// true if crawlable, false if not.
        /// </value>
        public bool Crawlable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object is backend.
        /// </summary>
        /// <value>
        /// true if this object is backend, false if not.
        /// </value>
        public bool IsBackend { get; set; }

        /// <summary>
        /// Gets or sets the NodeType of the given page.
        /// </summary>
        /// <value>
        /// Standard, Group, External, Inner/Outer Redirect, or Rewriting.
        /// </value>
        public NodeType NodeType { get; set; }

        /// <summary>
        /// Gets or sets the LinkTarget of the given page.
        /// </summary>
        /// <value>
        /// This value typically resolves to "_blank" when a Sitefinity page is set to open in a new
        /// window.
        /// </value>
        public string LinkTarget { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the in navigation is shown.
        /// </summary>
        /// <value>
        /// true if show in navigation, false if not.
        /// </value>
        public bool ShowInNavigation { get; set; }

        /// <summary>
        /// Gets or sets the name of the safe.
        /// </summary>
        /// <value>
        /// The name of the safe.
        /// </value>
        public string SafeName { get; set; }

        /// <summary>
        /// Gets or sets the ordinal.
        /// </summary>
        /// <value>
        /// The ordinal.
        /// </value>
        public float Ordinal { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ContentLifecycleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        /// <value>
        /// The theme.
        /// </value>
        public string Theme { get; set; }

        /// <summary>
        /// Gets or sets the framework.
        /// </summary>
        /// <value>
        /// The framework.
        /// </value>
        public string Framework { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        /// <value>
        /// The slug.
        /// </value>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public PageModel Parent { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<PageModel> Items { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the active.
        /// </summary>
        /// <value>
        /// true if active, false if not.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Contains the Sitefinity page custom fields and values for this PageModel.
        /// </summary>
        /// <value>
        /// The custom fields.
        /// </value>
        public IDictionary<string, object> CustomFields { get; set; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected SiteMapNode OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PageModel()
            : this(null)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public PageModel(SiteMapNode sfContent)
        {
            Items = new List<PageModel>();

            OriginalContent = sfContent;
        }
    }
}