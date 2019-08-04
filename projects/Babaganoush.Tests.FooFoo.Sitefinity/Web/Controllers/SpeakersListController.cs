using Babaganoush.Sitefinity.Mvc.Web.Controllers.Abstracts;
using Babaganoush.Tests.FooFoo.Sitefinity.Web.ViewModels;
using System.Web.Mvc;

namespace Babaganoush.Tests.FooFoo.Sitefinity.Web.Controllers
{
    public class SpeakersListController : BaseController
    {
        public SpeakersListController()
            : base(Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH)
        {

        }

        public ActionResult Index()
        {
            var model = new SpeakersListViewModel
            {
                Title = "Speakers listed below:"
            };

            return EmbeddedView(model);
        }
    }
}
