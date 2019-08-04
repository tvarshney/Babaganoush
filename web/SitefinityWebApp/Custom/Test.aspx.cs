using System;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Libraries.Model;
using Babaganoush.Sitefinity.Extensions;

namespace SitefinityWebApp.Custom
{
    public partial class Test : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var items = DynamicModuleManager.GetManager().GetDataItems(TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Speakers.Speaker"));
            var image = items.First().GetRelatedItems<Image>("Photo");
            var imageex = items.First().GetImage("Photo");
        }
    }
}