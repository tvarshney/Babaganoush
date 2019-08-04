using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using SitefinityWebApp.Mvc.Models;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "MyWidget1", Title = "MyWidget1", SectionName = "MvcWidgets")]
    public class MyWidget1Controller : Controller
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [Category("String Properties")]
        public string Message { get; set; }

        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            var model = new MyWidget1Model();
            if (string.IsNullOrEmpty(Message))
            {
                model.Message = "Hello, World!";
            }
            else
            {
                model.Message = Message;
            }

            return View("Default", model);
        }
    }
}