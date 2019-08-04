// file:	Api\DocumentsController.cs
//
// summary:	Implements the documents controller class
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using Telerik.Sitefinity.Libraries.Model;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for documents.
    /// </summary>
    public class DocumentsController : BaseMediaController<DocumentModel, Document>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentsController" /> class.
        /// </summary>
        public DocumentsController()
            : base(BabaManagers.Documents)
        {

        }
    }
}