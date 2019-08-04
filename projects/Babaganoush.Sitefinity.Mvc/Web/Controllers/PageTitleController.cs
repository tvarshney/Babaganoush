using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.Mvc.Web.Controllers.Abstracts;
using Babaganoush.Sitefinity.Mvc.Web.ViewModels;
using Babaganoush.Sitefinity.Utilities;
using System;
using System.Web.Mvc;

namespace Babaganoush.Sitefinity.Mvc.Web.Controllers
{
    /// <summary>
    /// Dynamically displays current page title.
    /// </summary>
    public class PageTitleController : BaseController
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        ///
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the text tag.
        /// </summary>
        ///
        /// <value>
        /// The text tag.
        /// </value>
        public string TextTag { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the parent title is shown.
        /// </summary>
        ///
        /// <value>
        /// true if show parent title, false if not.
        /// </value>
        public bool ShowParentTitle { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PageTitleController()
        {
            TextTag = "h1";
        }

        /// <summary>
        /// Gets the index.
        /// </summary>
        ///
        /// <returns>
        /// An ActionResult.
        /// </returns>
        public ActionResult Index()
        {
            var model = new PageTitleViewModel
            {
                TextTag = TextTag
            };

            //CONSTUCT TITLE
            if (string.IsNullOrEmpty(Text))
            {
                try
                {
                    //USE CURRENT PAGE TO DETERMINE TITLE
                    var currentPage = BabaManagers.Pages.GetCurrentSiteMapNode();
                    if (currentPage != null)
                    {
                        //DETERMINE TEXT TO DISPLAY FROM CURRENT NODE
                        model.Title = ShowParentTitle && currentPage.ParentNode != null
                            ? currentPage.ParentNode.Title
                            : currentPage.Title;
                    }
                }
                catch (Exception ex)
                {
                    //TODO: SAFELY GET RID OF TRY/CATCH FOR PERFORMANCE
                    LogHelper.LogException(ex);
                }
            }
            else
            {
                //RENDER DEFAULT TEXT TO PAGE
                model.Title = Text;
            }

            return EmbeddedView(model);
        }
    }
}