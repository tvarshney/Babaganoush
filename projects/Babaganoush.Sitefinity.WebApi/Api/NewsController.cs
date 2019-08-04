// file:	Api\NewsController.cs
//
// summary:	Implements the news controller class
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using Telerik.Sitefinity.News.Model;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for news.
    /// </summary>
    public class NewsController : BaseContentController<NewsItemModel, NewsItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsController" /> class.
        /// </summary>
        public NewsController()
            : base(BabaManagers.News)
        {

        }
    }
}