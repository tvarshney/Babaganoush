// file:	Models\DataResult.cs
//
// summary:	Implements the data result class
using System.Collections.Generic;
using System.Linq;

namespace Babaganoush.Sitefinity.WebApi.Models
{
    /// <summary>
    /// Encapsulates the result of a data.
    /// </summary>
    public class DataResult
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public IEnumerable<object> Data { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public object Errors { get; set; }

        /// <summary>
        /// Gets or sets the number of. 
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public int Total { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataResult()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The data.</param>
        public DataResult(IEnumerable<object> data)
        {
            Data = data.ToList();
            Total = data.Count();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="errors">The errors.</param>
        public DataResult(IEnumerable<object> data, object errors)
        {
            Data = data.ToList();
            Errors = errors;
            Total = data.Count();
        }
    }
}
