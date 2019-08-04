// file:	Web\Controls\ThrowError.cs
//
// summary:	Implements the throw error class
using System;
using System.Web.UI.WebControls;

namespace Babaganoush.Sitefinity.Web.Controls
{
    /// <summary>
    /// A throw error widget.
    /// </summary>
    public class ThrowError : WebControl
    {
        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based
        /// implementation to create any child controls they contain in preparation for posting back or
        /// rendering.
        /// </summary>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        protected override void CreateChildControls()
        {
            throw new Exception("Manually threw exception for testing.");
        }
    }
}
