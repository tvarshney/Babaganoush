// file:	Models\DataResponse.cs
//
// summary:	Implements the data response class
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace Babaganoush.Sitefinity.WebApi.Models
{
    /// <summary>
    /// A data response.
    /// </summary>
    public class DataResponse : HttpResponseMessage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="status">(Optional) the status.</param>
        public DataResponse(IEnumerable<object> data, HttpStatusCode status = HttpStatusCode.OK)
        {
            Content = new ObjectContent<DataResult>(
                new DataResult(data),
                new JsonMediaTypeFormatter());
            StatusCode = status;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="errors">The errors.</param>
        /// <param name="status">(Optional) the status.</param>
        public DataResponse(IEnumerable<object> data, object errors, HttpStatusCode status = HttpStatusCode.OK)
        {
            Content = new ObjectContent<DataResult>(
                new DataResult(data, errors),
                new JsonMediaTypeFormatter());

            StatusCode = errors != null && status == HttpStatusCode.OK
                ? HttpStatusCode.InternalServerError : status;
        }
    }
}
