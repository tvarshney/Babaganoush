// file:	Api\ListsController.cs
//
// summary:	Implements the lists controller class
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using Telerik.Sitefinity.Lists.Model;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for lists.
    /// </summary>
    public class ListsController : BaseContentController<ListItemModel, ListItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListsController" /> class.
        /// </summary>
        public ListsController()
            : base(BabaManagers.Lists)
        {

        }
    }
}