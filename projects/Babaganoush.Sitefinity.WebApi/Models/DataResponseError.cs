// file:	Models\DataResponseError.cs
//
// summary:	Implements the data response error class
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace Babaganoush.Sitefinity.WebApi.Models
{
    /// <summary>
    /// A data response.
    /// </summary>
    public class DataResponseError : HttpResponseMessage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <param name="status">(Optional) the status.</param>
        public DataResponseError(object errors, HttpStatusCode status = HttpStatusCode.InternalServerError)
        {
            Content = new ObjectContent<object>(new { Errors = errors },
                new JsonMediaTypeFormatter());
            StatusCode = status;
        }
    }
}
