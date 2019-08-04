// file:	Models\Factories\PageFactory.cs
//
// summary:	Implements the page factory class
using Babaganoush.Core.Utilities;
using Babaganoush.Core.Utilities.Interfaces;
using Babaganoush.Sitefinity.Extensions;
using Babaganoush.Sitefinity.Models.Interfaces;
using Babaganoush.Sitefinity.Utilities.Interfaces;
using System.Web;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;
using PageHelper = Babaganoush.Sitefinity.Utilities.PageHelper;

namespace Babaganoush.Sitefinity.Models.Factories
{
    /// <summary>
    /// Factory class implementation for creating <see cref="PageModel" /> objects.
    /// </summary>
    public class PageFactory : IPageFactory
    {
        /// <summary>
        /// The page helper.
        /// </summary>
        private readonly IPageHelper _pageHelper;

        /// <summary>
        /// The web helper.
        /// </summary>
        private readonly IWebHelper _webHelper;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PageFactory()
            : this(new PageHelper(), new WebHelper())
        { }

        /// <summary>
        /// Constructor with settable dependencies.
        /// </summary>
        /// <param name="pageHelper">The page helper.</param>
        /// <param name="webHelper">The web helper.</param>
        public PageFactory(IPageHelper pageHelper, IWebHelper webHelper)
        {
            _pageHelper = pageHelper;
            _webHelper = webHelper;
        }

        /// <summary>
        /// Creates a <see cref="PageModel"/> object based off of then given <see cref="SiteMapNode"/>
        /// object.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        /// <param name="includeParents">true to include, false to exclude the parents.</param>
        /// <param name="includeChildren">true to include, false to exclude the children.</param>
        /// <param name="includeRelatedData">true to include, false to exclude the related data.</param>
        /// <returns>
        /// A new <see cref="PageModel"/> object.
        /// </returns>
        public PageModel Create(SiteMapNode sfContent, bool includeParents, bool includeChildren, bool includeRelatedData)
        {
            var pageModel = new PageModel(sfContent);

            var pageNode = sfContent as PageSiteNode;
            if (pageNode == null)
            {
                return pageModel;
            }

            pageModel.Id = pageNode.Id;
            pageModel.Title = pageNode.Title;
            pageModel.Description = pageNode.Description;
            pageModel.Url = _webHelper.ResolveUrl(pageNode.Url);
            pageModel.Crawlable = pageNode.Crawlable;
            pageModel.IsBackend = pageNode.IsBackend;
            pageModel.NodeType = pageNode.NodeType;
            pageModel.LinkTarget = _pageHelper.GetLinkTarget(pageNode);
            pageModel.ShowInNavigation = pageNode.ShowInNavigation;
            pageModel.SafeName = _webHelper.GenerateSafeName(pageNode.UrlName);
            pageModel.Ordinal = pageNode.Ordinal;
            pageModel.Status = pageNode.Status;
            pageModel.Theme = pageNode.Theme;
            pageModel.Framework = pageNode.Framework.ToString();
            pageModel.Slug = pageNode.UrlName;
            pageModel.Active = pageNode.Status == ContentLifecycleStatus.Live && pageNode.Visible;
            pageModel.CustomFields = pageNode.GetCustomFieldValues(includeRelatedData);

            if (includeParents)
            {
                pageModel.Parent = Create(sfContent.ParentNode, false, false, includeRelatedData);
            }

            //GET CHILDREN PAGES IF APPLICABLE
            if (!includeChildren || !pageNode.HasChildNodes)
            {
                return pageModel;
            }
            foreach (PageSiteNode item in pageNode.ChildNodes)
            {
                bool itemIsLiveStandard = item.NodeType == NodeType.Standard && item.Status == ContentLifecycleStatus.Live;
                bool itemIsMasterNonStandard = item.NodeType != NodeType.Standard && item.Status == ContentLifecycleStatus.Master;
				bool itemIsPublishedButHasDraft = item.NodeType == NodeType.Standard && item.Status == ContentLifecycleStatus.Master && item.Visible;

                if (item.ShowInNavigation && (itemIsLiveStandard || itemIsMasterNonStandard || itemIsPublishedButHasDraft))
                {
                    //ADD PAGE AND LEAVE OUT PARENT NODE
                    pageModel.Items.Add(Create(item, false, true, includeRelatedData));
                }
            }

            return pageModel;
        }
    }
}