// file:	Models\DataResponseSingle.cs
//
// summary:	Implements the data response single class
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace Babaganoush.Sitefinity.WebApi.Models
{
    /// <summary>
    /// A data response.
    /// </summary>
    public class DataResponseSingle : HttpResponseMessage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="status">(Optional) the status.</param>
        public DataResponseSingle(object data, HttpStatusCode status = HttpStatusCode.OK)
        {
            Content = new ObjectContent<object>(data,
                new JsonMediaTypeFormatter());
            StatusCode = status;
        }
    }
}
