using Babaganoush.Sitefinity.Extensions;
using Babaganoush.Sitefinity.Models;
using System.Linq;
using System.Collections.Generic;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.RelatedData;

namespace Babaganoush.Tests.FooFoo.Sitefinity.Models
{
    public class SpeakerModel : DynamicModel
    {
        public string Email { get; set; }
        public string Bio { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Twitter { get; set; }
        public string Website { get; set; }
        public ImageModel Photo { get; set; }
        public List<DocumentModel> eBooks { get; set; }
        public List<SessionModel> Sessions { get; set; }
        public List<TaxonModel> Categories { get; set; }
        public List<TaxonModel> Tags { get; set; }

        /// <summary>
        /// Gets the name of the dynamic type.
        /// </summary>
        /// <value>
        /// The name of the dynamic type.
        /// </value>
        public override string MappedType 
        {
            get
            {
                return "Telerik.Sitefinity.DynamicTypes.Model.Speakers.Speaker";
            }
        }

        public SpeakerModel()
        {
            Categories = new List<TaxonModel>();
            Tags = new List<TaxonModel>();
        }

        public SpeakerModel(DynamicContent sfContent)
            : base(sfContent)
        {
            if (sfContent != null)
            {
                // Set custom properties
                Email = sfContent.GetStringSafe("Email");
                Bio = sfContent.GetStringSafe("Bio");
                FirstName = sfContent.GetStringSafe("FirstName");
                LastName = sfContent.GetStringSafe("LastName");
                Company = sfContent.GetStringSafe("Company");
                Position = sfContent.GetStringSafe("Position");
                Twitter = sfContent.GetStringSafe("Twitter");
                Website = sfContent.GetStringSafe("Website");
                Photo = sfContent.GetImage("Photo");
                eBooks = sfContent.GetDocuments("Ebooks");

                // TODO: Create Baba extension if possible
                Sessions = sfContent.GetOriginal().GetRelatedItems<DynamicContent>("Sessions")
                    .Select(x => new SessionModel(x))
                    .ToList();

                Categories = sfContent.GetTaxa("Category");
                Tags = sfContent.GetTaxa("Tags");
            }
        }

        public override DynamicContent ToSitefinityModel(bool checkout = true)
        {
            //GET CONTRUCTED CONTENT FROM BASE
            var sfContent = base.ToSitefinityModel();

            //POPULATE MORE FIELDS IF APPLICABLE
            if (sfContent != null)
            {
                //MERGE CUSTOM PROPERTIES
                sfContent.TrySetValue("Email", Email);
                sfContent.TrySetValue("Bio", Bio);
                sfContent.TrySetValue("FirstName", FirstName);
                sfContent.TrySetValue("LastName", LastName);
                sfContent.TrySetValue("Company", Company);
                sfContent.TrySetValue("Position", Position);
                sfContent.TrySetValue("Twitter", Twitter);
                sfContent.TrySetValue("Website", Website);
                sfContent.SetRelation("Photo", Photo);
                //sfContent.SetRelation("EBooks", eBooks);
                sfContent.SetTaxa("Category", Categories);
                sfContent.SetTaxa("Tags", Tags);
            }

            //RETURN SITEFINITY MODEL
            return sfContent;
        }
    }
}
