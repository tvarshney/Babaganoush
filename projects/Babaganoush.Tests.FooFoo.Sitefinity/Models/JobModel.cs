using Babaganoush.Sitefinity.Extensions;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.Models.Interfaces;
using Telerik.Sitefinity.DynamicModules.Model;

namespace Babaganoush.Tests.FooFoo.Sitefinity.Models
{
    public class JobModel : DynamicModel, IHierarchy
    {
        public string Description { get; set; }
        public DynamicModel Parent { get; set; }

        public override string MappedType 
        {
            get
            {
                return "Telerik.Sitefinity.DynamicTypes.Model.Applications.Job";
            }
        }

        public JobModel()
        {
        }

        public JobModel(DynamicContent sfContent)
            : base(sfContent)
        {
            if (sfContent != null)
            {
                //SET CUSTOM PROPERTIES
                Description = sfContent.GetStringSafe("Description");
                Parent = new JobModel(sfContent.GetParent());
            }
        }
    }
}
