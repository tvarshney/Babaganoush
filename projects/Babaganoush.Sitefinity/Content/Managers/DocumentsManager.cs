// file:	Content\Managers\DocumentsManager.cs
//
// summary:	Implements the documents manager class
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using Babaganoush.Sitefinity.Models;
using System;
using System.Linq;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Manager for documents.
    /// </summary>
    public class DocumentsManager : BaseMediaManager<
        LibrariesManager,
        Document,
        DocumentsManager,
        DocumentModel>
    {
        /// <summary>
        /// Gets the Sitefinity data.
        /// </summary>
        /// <param name="providerName">(Optional) the provider name to get.</param>
        /// <returns>
        /// An IQueryable&lt;Document&gt;
        /// </returns>
        protected override IQueryable<Document> Get(string providerName = null)
        {
            return GetManager(providerName).GetDocuments();
        }

        /// <summary>
        /// Gets the Sitefinity data by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// A Document.
        /// </returns>
        protected override Document Get(Guid id, string providerName = null)
        {
            return GetManager(providerName).GetDocument(id);
        }

        /// <summary>
        /// Creates the Baba instance from the Sitefinity object.
        /// </summary>
        /// <param name="sfContent">Content of the sf.</param>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override DocumentModel CreateInstance(Document sfContent)
        {
            return new DocumentModel(sfContent);
        }
    }
}