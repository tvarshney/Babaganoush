// file:	Models\ImageModel.cs
//
// summary:	Implements the image model class
using Babaganoush.Sitefinity.Extensions;
using Babaganoush.Sitefinity.Utilities;
using System;
using System.Linq;
using Telerik.Sitefinity.Ecommerce.Catalog.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Babaganoush.Sitefinity.Models
{
    /// <summary>
    /// A data Model for the image.
    /// </summary>
    public class ImageModel : MediaModel
    {
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        /// <value>
        /// The alternative text.
        /// </value>
        public string AlternativeText { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>
        /// The link.
        /// </value>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the original content.
        /// </summary>
        /// <value>
        /// The original content.
        /// </value>
        protected Image OriginalContent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ImageModel()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public ImageModel(Image sfContent)
            : base(sfContent)
        {
            if (sfContent != null)
            {
                Width = sfContent.Width;
                Height = sfContent.Height;
                AlternativeText = sfContent.AlternativeText;

                // Set custom fields
                if (sfContent.DoesFieldExist("Link"))
                    Link = sfContent.GetValue<string>("Link");

                // Store original content
                OriginalContent = sfContent;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sfContent">The sf content.</param>
        public ImageModel(ProductImage sfContent)
        {
            if (sfContent != null)
            {
                Title = sfContent.Title;
                Width = sfContent.Width;
                Height = sfContent.Height;
                Url = sfContent.Url;
                AlternativeText = sfContent.AlternativeText;
                Ordinal = sfContent.Ordinal;
            }
        }

        /// <summary>
        /// Convert to sitefinity model.
        /// </summary>
        /// <returns>
        /// This object as an Image.
        /// </returns>
        public virtual Image ToSitefinityModel()
        {
            var manager = LibrariesManager.GetManager();
            Image sfContent = null;
            Album sfAlbum = null;

            //CONSTRUCT MODEL FROM SF API
            if (Id == Guid.Empty)
            {
                //CREATE NEW MODEL
                sfContent = manager.CreateImage();

                //SET DEFAULT DATA
                sfContent.DateCreated = DateCreated = DateTime.UtcNow;
                sfContent.LastModified = DateTime.UtcNow;

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
                sfContent = manager.GetImage(Id);

                //CHECK OUT ITEM FOR UPDATE IF APPLICABLE
                if (sfContent != null)
                {
                    //EDIT MODE ON CONTENT AND RETURNED CHECKED OUT ITEM
                    var master = manager.Lifecycle.Edit(sfContent) as Image;
                    sfContent = manager.Lifecycle.CheckOut(master) as Image;
                }
            }

            //MERGE DEFAULT PROPERTIES
            sfContent.Title = Title;
            sfContent.Description = Description;
            sfContent.AlternativeText = AlternativeText;
            sfContent.Author = Author;
            sfContent.Height = Height;
            sfContent.Width = Width;
            sfContent.Ordinal = Ordinal;

            //MERGE CUSTOM PROPERTIES
            if (sfContent.DoesFieldExist("Link"))
                Link = sfContent.GetValue<string>("Link");

            //ASSIGN LIBRARY IF APPLICABLE
            if (Parent != null)
            {
                if (Parent.Id != Guid.Empty)
                {
                    sfAlbum = manager.GetAlbum(Parent.Id);
                }
                else if (!string.IsNullOrWhiteSpace(Parent.Slug))
                {
                    sfAlbum = manager.GetAlbums().FirstOrDefault(a => a.UrlName == Parent.Slug);
                }

                if (sfAlbum == null && !string.IsNullOrWhiteSpace(Parent.Title))
                {
                    sfAlbum = Parent.Id != Guid.Empty
                        ? manager.CreateAlbum(Parent.Id) : manager.CreateAlbum();
                    sfAlbum.Title = Parent.Title;
                    sfAlbum.Description = Parent.Description;
                    sfAlbum.DateCreated = DateTime.UtcNow;
                    sfAlbum.PublicationDate = DateTime.UtcNow;
                    sfAlbum.LastModified = DateTime.UtcNow;
                    if (!string.IsNullOrWhiteSpace(Parent.Slug))
                    {
                        sfAlbum.UrlName = Parent.Slug;
                    }
                    else if (!string.IsNullOrWhiteSpace(Parent.Title))
                    {
                        sfAlbum.UrlName = Parent.Slug = ContentHelper.GenerateUrlName(Parent.Title);
                    }
                    manager.SaveChanges();
                }

                //STORE LIBRARY TO CONTENT
                sfContent.Parent = sfAlbum;
            }

            //MERGE TAXONOMIES
            sfContent.SetTaxa("Category", Categories);
            sfContent.SetTaxa("Tags", Tags);

            //RETURN SITEFINITY MODEL
            return sfContent;
        }
    }
}