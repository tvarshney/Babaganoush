using Babaganoush.Sitefinity.Mvc.Web.Controllers.Abstracts;
using System.Web.Mvc;

namespace Babaganoush.Tests.FooFoo.Sitefinity.Web.Controllers
{
    public class MyTestController : BaseController
    {
        public MyTestController()
            : base(Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH)
        {

        }

        public ActionResult Index()
        {
            return EmbeddedView();
        }
    }
}
