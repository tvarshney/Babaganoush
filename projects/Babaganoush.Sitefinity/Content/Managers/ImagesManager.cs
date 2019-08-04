// file:	Content\Managers\ImagesManager.cs
//
// summary:	Implements the images manager class

using Babaganoush.Core.Wrappers;
using Babaganoush.Core.Wrappers.Interfaces;
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Workflow;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Manager for images.
    /// </summary>
    public class ImagesManager : BaseMediaManager<LibrariesManager, Image, ImagesManager, ImageModel>
    {
        private readonly IFileSystem _fileSystem;
        private readonly IHttpContext _httpContext;

        /// <summary>
        /// Creates a new instance of <see cref="ImagesManager"/>, with default dependencies used.
        /// </summary>
        public ImagesManager()
            : this(new FileSystemWrapper(), new SystemHttpContextWrapper())
        { }

        /// <summary>
        /// Creates a new instance of <see cref="ImagesManager"/>, using the given dependencies.
        /// </summary>
        public ImagesManager(IFileSystem fileSystem, IHttpContext httpContext)
        {
            _fileSystem = fileSystem;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Gets the Sitefinity data.
        /// </summary>
        /// <param name="providerName">(Optional) the provider name to get.</param>
        /// <returns>
        /// An IQueryable&lt;Image&gt;
        /// </returns>
        protected override IQueryable<Image> Get(string providerName = null)
        {
            return GetManager(providerName).GetImages();
        }

        /// <summary>
        /// Gets the Sitefinity data by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// An Image.
        /// </returns>
        protected override Image Get(Guid id, string providerName = null)
        {
            return GetManager(providerName).GetImage(id);
        }

        /// <summary>
        /// Creates the Baba instance from the Sitefinity object.
        /// </summary>
        /// <param name="sfContent">Content of the sf.</param>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override ImageModel CreateInstance(Image sfContent)
        {
            return new ImageModel(sfContent);
        }

        /// <summary>
        /// Creates the specified image.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// An ImageModel.
        /// </returns>
        public virtual ImageModel Create(ImageModel value, string providerName = null)
        {
            using (new ElevatedModeRegion(GetManager(providerName)))
            {
                try
                {
                    //CONVERT CURRENT MODEL TO SITEFINITY MODEL
                    var sfContent = value.ToSitefinityModel();
                    sfContent.PublicationDate = DateTime.UtcNow;

                    //UPLOAD FILE IF APPLICABLE
                    if (!string.IsNullOrWhiteSpace(value.File))
                    {
                        var path = _httpContext.MapPath(value.File);
                        if (_fileSystem.Exists(path))
                        {
                            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                            {
                                GetManager(providerName).Upload(sfContent, stream, Path.GetExtension(path));
                            }
                        }
                    }

                    //PUBLISH AND PERSIST TO SITEFINITY STORAGE
                    if (sfContent.OriginalContentId == Guid.Empty)
                    {
                        GetManager(providerName).SaveChanges();

                        var bag = new Dictionary<string, string>();
                        bag.Add("ContentType", typeof(Image).FullName);
                        WorkflowManager.MessageWorkflow(sfContent.Id, typeof(Image), null, "Publish", false, bag);
                    }
                    else //HANDLE UPDATES ON EXISTING ITEM
                    {
                        // Now we need to check in, so the changes apply
                        var master = GetManager(providerName).Lifecycle.CheckIn(sfContent) as Image;

                        GetManager(providerName).SaveChanges();

                        //Publish the image.
                        var bag = new Dictionary<string, string>();
                        bag.Add("ContentType", typeof(Image).FullName);
                        WorkflowManager.MessageWorkflow(master.Id, typeof(Image), null, "Publish", false, bag);
                    }

                    // You need to call SaveChanges() in order for the items to be actually persisted to data store
                    GetManager(providerName).SaveChanges();

                    //UPDATE ANY GENERATED PROPERTIES
                    value.Id = sfContent.Id;

                    //RETURN ITEM
                    return value;
                }
                catch (Exception)
                {
                    //TODO: LOG ERROR
                    return null;
                }
            }
        }
    }
}