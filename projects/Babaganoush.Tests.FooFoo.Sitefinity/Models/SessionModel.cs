using Babaganoush.Sitefinity.Extensions;
using Babaganoush.Sitefinity.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.RelatedData;

namespace Babaganoush.Tests.FooFoo.Sitefinity.Models
{
    public class SessionModel : DynamicModel
    {
        public string Description { get; set; }
        public string Room { get; set; }
        public bool IsKeynote { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MaxAttendees { get; set; }
        public int CurrentAttendees { get; set; }
        public List<ImageModel> Images { get; set; }
        public List<DocumentModel> Docs { get; set; }
        public EventModel Event { get; set; }
        public List<TaxonModel> Tracks { get; set; }
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
                return "Telerik.Sitefinity.DynamicTypes.Model.Sessions.Session";
            }
        }

        public SessionModel()
        {
            Tracks = new List<TaxonModel>();
            Categories = new List<TaxonModel>();
            Tags = new List<TaxonModel>();
        }

        public SessionModel(DynamicContent sfContent)
            : base(sfContent)
        {
            if (sfContent != null)
            {
                // Set custom properties
                Description = sfContent.GetStringSafe("Description");
                Room = sfContent.GetStringSafe("Room");
                IsKeynote = sfContent.GetBoolean("IsKeynote");
                StartTime = sfContent.GetDateTime("StartTime");
                EndTime = sfContent.GetDateTime("EndTime");
                MaxAttendees = sfContent.GetInteger("MaxAttendees");
                CurrentAttendees = sfContent.GetInteger("CurrentAttendees");
                Images = sfContent.GetImages("Images");
                Docs = sfContent.GetDocuments("Docs");

                // TODO: Create Baba extension if possible
                var sfEvent = sfContent.GetOriginal().GetRelatedItems<Event>("Event").FirstOrDefault();
                if (sfEvent != null)
                {
                    Event = new EventModel(sfEvent);
                }

                Tracks = sfContent.GetTaxa("tracks");
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
                // TODO: MERGE CUSTOM PROPERTIES ONLY IF SAVING
            }

            //RETURN SITEFINITY MODEL
            return sfContent;
        }
    }
}
