// file:	Models\DynamicModel.cs
//
// summary:	Implements the dynamic model class
using Babaganoush.Sitefinity.Extensions;
using Babaganoush.Sitefinity.Models.Interfaces;
using Babaganoush.Sitefinity.Utilities;
using System;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the dynamic.
    /// </summary>
    public abstract class DynamicModel : IDataModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the original content.
        /// </summary>
        /// <value>
        /// The identifier of the original content.
        /// </value>
        public Guid OriginalContentId { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        /// <value>
        /// The slug.
        /// </value>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the publication date.
        /// </summary>
        /// <value>
        /// The publication date.
        /// </value>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time of the last modified.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ContentLifecycleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the active.
        /// </summary>
        /// <value>
        /// true if active, false if not.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public Guid Owner { get; set; }

        /// <summary>
        /// The Sitefinity dynamic type of the mapped content Must be declared in derviced classes.
        /// </summary>
        /// <value>
        /// The type of the mapped.
        /// </value>
        public abstract string MappedType { get; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected DynamicContent OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DynamicModel()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public DynamicModel(DynamicContent sfContent)
        {
            if (sfContent != null)
            {
                //SET PROPERTIES
                Id = sfContent.Id;
                OriginalContentId = sfContent.OriginalContentId;
                Slug = sfContent.UrlName;
                DateCreated = sfContent.DateCreated;
                PublicationDate = sfContent.PublicationDate;
                LastModified = sfContent.LastModified;
                Status = sfContent.Status;
                Active = sfContent.Status == ContentLifecycleStatus.Live
                    && sfContent.Visible;
                Owner = sfContent.Owner;

                //SET CUSTOM PROPERTIES
                Title = sfContent.GetTitle();

                // Store original content
                OriginalContent = sfContent;
            }
        }

        /// <summary>
        /// Convert to sitefinity model.
        /// </summary>
        /// <param name="checkout">whether to get temp version of sitefinity model.</param>
        /// <returns>
        /// This object as a DynamicContent.
        /// </returns>
        public virtual DynamicContent ToSitefinityModel(bool checkout = true)
        {
            var manager = DynamicModuleManager.GetManager();
            DynamicContent sfContent;
            
            //CONSTRUCT MODEL FROM SF API
            if (Id == Guid.Empty)
            {
                //CREATE NEW MODEL
                sfContent = manager.CreateDataItem(
                    TypeResolutionService.ResolveType(MappedType));

                //SET DEFAULT DATA
                sfContent.DateCreated = DateCreated = DateTime.UtcNow;

                //GENERATE URL FROM TITLE IF APPLICABLE
                if (!string.IsNullOrWhiteSpace(Slug))
                {
                    sfContent.UrlName = Slug;
                }
                else if (!string.IsNullOrWhiteSpace(Title))
                {
                    sfContent.UrlName = Slug = ContentHelper.GenerateUrlName(Title);
                }
            }
            else
            {
                //GET LIVE ITEM FROM STORAGE
                sfContent = manager.GetDataItem(
                    TypeResolutionService.ResolveType(MappedType), Id);

                //CHECK OUT ITEM FOR UPDATE IF APPLICABLE
                if (sfContent != null && checkout)
                {
                    //EDIT MODE ON CONTENT AND RETURNED CHECKED OUT ITEM
                    var master = manager.Lifecycle.Edit(sfContent) as DynamicContent;
                    sfContent = manager.Lifecycle.CheckOut(master) as DynamicContent;
                }
            }

            //MERGE CUSTOM PROPERTIES
            sfContent.TrySetValue("Title", Title);

            //RETURN SITEFINITY MODEL
            return sfContent;
        }

        /// <summary>
        /// Get live version of sitefinity model.
        /// </summary>
        /// <returns>
        /// This object as a DynamicContent.
        /// </returns>
        public virtual DynamicContent Live()
        {
            var manager = DynamicModuleManager.GetManager();
            var sfContent = this.ToSitefinityModel(false);

            if (sfContent != null)
            {
                if (sfContent.Status == ContentLifecycleStatus.Live)
                {
                    return sfContent;
                }
                else if (sfContent.Status == ContentLifecycleStatus.Master)
                {
                    sfContent = manager.Lifecycle.GetLive(sfContent) as DynamicContent;
                }
                else
                {
                    return null;
                }
            }

            //RETURN SITEFINITY MODEL
            return sfContent;
        }

        /// <summary>
        /// Get master version of sitefinity model.
        /// </summary>
        /// <returns>
        /// This object as a DynamicContent.
        /// </returns>
        public virtual DynamicContent Master()
        {
            var manager = DynamicModuleManager.GetManager();
            var sfContent = this.ToSitefinityModel(false);

            if (sfContent != null)
            {
                if (sfContent.Status == ContentLifecycleStatus.Live)
                {
                    sfContent = manager.Lifecycle.GetMaster(sfContent) as DynamicContent;
                }
                else if (sfContent.Status == ContentLifecycleStatus.Master)
                {
                    return sfContent;
                }
                else
                {
                    return null;
                }
            }

            //RETURN SITEFINITY MODEL
            return sfContent;
        }
    }
}
