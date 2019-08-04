// file:	Models\DocumentModel.cs
//
// summary:	Implements the document model class
using Telerik.Sitefinity.Libraries.Model;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the document.
    /// </summary>
    public class DocumentModel : MediaModel
    {
        /// <summary>
        /// Gets or sets the other.
        /// </summary>
        /// <value>
        /// The other.
        /// </value>
        public string Other { get; set; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected Document OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DocumentModel()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf media.</param>
        public DocumentModel(Document sfContent)
            : base(sfContent)
        {
            if (sfContent != null)
            {
                Other = sfContent.Parts;

                // Store original content
                OriginalContent = sfContent;
            }
        }
    }
}