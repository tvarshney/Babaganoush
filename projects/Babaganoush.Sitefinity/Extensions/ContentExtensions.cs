using Babaganoush.Core.Utilities;
using Babaganoush.Core.Utilities.Interfaces;
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using ContentModel = Telerik.Sitefinity.GenericContent.Model.Content;

namespace Babaganoush.Sitefinity.Extensions
{
    /// <summary>
    /// Extensions for content.
    /// </summary>
    public static class ContentExtensions
    {
        /// <summary>
        /// The web helper.
        /// </summary>
        private static readonly IWebHelper _webHelper = new WebHelper();

        /// <summary>
        /// Filters the given collection so that only Live and Visible objects are returned.
        /// </summary>
        /// <typeparam name="T">The content type.</typeparam>
        /// <param name="value">The collection to filter.</param>
        /// <returns></returns>
        public static IEnumerable<T> LiveAndVisible<T>(this IEnumerable<T> value)
            where T : ContentModel
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", "The given collection of content cannot be null.");
            }
            return value.Where(c => c.Status == ContentLifecycleStatus.Live && c.Visible);
        }

        /// <summary>
        /// Tries to set value.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        public static void TrySetValue(this IDynamicFieldsContainer dataItem, string field, object value)
        {
            //HANDLE FIELD ONLY IF APPLICABLE
            if (dataItem.DoesFieldExist(field))
            {
                try
                {
                    dataItem.SetValue(field, value);
                }
                catch (Exception)
                {
                    //TODO: LOG ERROR
                }
            }
        }

        /// <summary>
        /// A DynamicContent extension method that gets the parent of this item.
        /// Since Sitefinity broke SystemParentItem.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>
        /// The parent.
        /// </returns>
        public static DynamicContent GetParent(this DynamicContent dataItem)
        {
            return dataItem.SystemParentId != Guid.Empty && !string.IsNullOrEmpty(dataItem.SystemParentType)
                ? DynamicModuleManager.GetManager().GetDataItem(
                    TypeResolutionService.ResolveType(dataItem.SystemParentType), dataItem.SystemParentId)
                : null;
        }

        /// <summary>
        /// Adds the media.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <param name="field">The field.</param>
        /// <param name="media">The media.</param>
        public static void SetRelation(this DynamicContent dataItem, string field, MediaModel media)
        {
            //HANDLE FIELD ONLY IF APPLICABLE
            if (!dataItem.DoesFieldExist(field))
            {
                return;
            }
            var manager = LibrariesManager.GetManager();

            if (media is ImageModel)
            {
                Image image;

                //GET AVAILABLE IMAGE
                if (media.Id != Guid.Empty)
                {
                    image = manager.GetImage(media.Id);
                }
                else
                {
                    image = manager.GetImages().LiveAndVisible().FirstOrDefault(i => i.UrlName == media.Slug);
                }

                //ADD IMAGE IF AVAILABLE
                if (image != null)
                {
                    dataItem.CreateRelation(image, field);
                }
            }
            else if (media is VideoModel)
            {
                Video video;

                //GET AVAILABLE VIDEO
                if (media.Id != Guid.Empty)
                {
                    video = manager.GetVideo(media.Id);
                }
                else
                {
                    video = manager.GetVideos().LiveAndVisible().FirstOrDefault(v => v.UrlName == media.Slug);
                }

                //ADD VIDEO IF AVAILABLE
                if (video != null)
                {
                    dataItem.CreateRelation(video, field);
                }
            }
            else if (media is DocumentModel)
            {
                Document document;

                //GET AVAILABLE DOCUMENT
                if (media.Id != Guid.Empty)
                {
                    document = manager.GetDocument(media.Id);
                }
                else
                {
                    document = manager.GetDocuments().LiveAndVisible().FirstOrDefault(d => d.UrlName == media.Slug);
                }

                //ADD DOCUMENT IF AVAILABLE
                if (document != null)
                {
                    dataItem.CreateRelation(document, field);
                }
            }
        }

        /// <summary>
        /// Sets a list to the taxonomies.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <param name="field">The field.</param>
        /// <param name="list">The list.</param>
        public static void SetTaxa(this IOrganizable dataItem, string field, List<TaxonModel> list)
        {
            dataItem.Organizer.Clear(field);

            if (list.Any())
            {
                using (var taxonomyManager = TaxonomyManager.GetManager())
                {
                    //FIND EXISTING TAXA IF APPLICABLE
                    var taxa = taxonomyManager.GetTaxa<ITaxon>()
                        .Where(t => list.Any(x => x.Id == t.Id || x.Slug == t.UrlName));

                    //ADD TO DATA
                    dataItem.Organizer.AddTaxa(field, taxa.Select(t => t.Id).ToArray());
                }
            }
        }

        /// <summary>
        /// Gets the taxons.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="taxonomyField">The taxonomy field.</param>
        /// <returns>
        /// The taxa.
        /// </returns>
        public static List<TaxonModel> GetTaxa(this IDynamicFieldsContainer content, string taxonomyField)
        {
            var taxonomyManager = TaxonomyManager.GetManager();
            var list = new List<TaxonModel>();

            //POPULATE CATEGORIES IF APPLICABLE
            if (content.DoesFieldExist(taxonomyField))
            {
                var ids = content.GetValue<TrackedList<Guid>>(taxonomyField);
                if (ids.Any())
                {
                    //BUILD COLLECTION OF TAXONS
                    foreach (Guid item in ids)
                    {
                        list.Add(new TaxonModel(taxonomyManager.GetTaxon(item)));
                    }
                }
            }

            //RETURN CONSTRUCTED LIST
            return list;
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="defaultPageId">(Optional) The default page ID.</param>
        /// <returns>
        /// The full URL.
        /// </returns>
        public static string GetFullUrl(this ILocatable content, Guid? defaultPageId = null)
        {
            string url = string.Empty;

            //HANDLE DEFAULT PAGE FOR URL CONSTRUCTION
            if (defaultPageId.HasValue)
            {
                //GET DEFAULT PAGE
                var defaultPage = BabaManagers.Pages.GetById(defaultPageId.GetValueOrDefault());
                if (defaultPage != null)
                {
                    url = defaultPage.Url;
                }
            }
            else
            {
                //NO DEFAULT PAGE SO USE GENERATED URL
                url = content.Urls.FirstOrDefault().Url;
            }

            //RESOLVE AND RETURN CONSTRUCTED URL
            return _webHelper.ResolveUrl(url);
        }

        /// <summary>
        /// Get a single image from a content.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// The image.
        /// </returns>
        public static ImageModel GetImage(this DynamicContent item, string fieldName)
        {
            //VALIDATE INPUT
            if (!item.DoesFieldExist(fieldName))
                return null;

            var sfContent = item.GetOriginal().GetRelatedItems<Image>(fieldName).FirstOrDefault();
            return sfContent != null ? new ImageModel(sfContent) : null;
        }

        /// <summary>
        /// Gets the images collection from content.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// IQueryable Telerik.Sitefinity.Libraries.Model.Image.
        /// </returns>
        public static List<ImageModel> GetImages(this DynamicContent item, string fieldName)
        {
            return item.DoesFieldExist(fieldName)
                ? item.GetOriginal().GetRelatedItems<Image>(fieldName).Select(x => new ImageModel(x)).ToList()
                : Enumerable.Empty<ImageModel>().ToList();
        }

        /// <summary>
        /// Get a single image from a content.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// The image.
        /// </returns>
        public static ImageModel GetImage(this ContentModel item, string fieldName)
        {
            //VALIDATE INPUT
            if (!item.DoesFieldExist(fieldName))
                return null;

            var sfContent = item.GetOriginal().GetRelatedItems<Image>(fieldName).FirstOrDefault();
            return sfContent != null ? new ImageModel(sfContent) : null;
        }

        /// <summary>
        /// Gets the images collection from content.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// IQueryable Telerik.Sitefinity.Libraries.Model.Image.
        /// </returns>
        public static List<ImageModel> GetImages(this ContentModel item, string fieldName)
        {
            return item.DoesFieldExist(fieldName)
                ? item.GetOriginal().GetRelatedItems<Image>(fieldName).Select(x => new ImageModel(x)).ToList()
                : Enumerable.Empty<ImageModel>().ToList();
        }

        /// <summary>
        /// Get a video from the given item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// The video.
        /// </returns>
        public static VideoModel GetVideo(this DynamicContent item, string fieldName)
        {
            if (item == null || !item.DoesFieldExist(fieldName))
            {
                return null;
            }

            Video sfContent = item.GetOriginal().GetRelatedItems<Video>(fieldName).FirstOrDefault();
            return sfContent != null ? new VideoModel(sfContent) : null;
        }

        /// <summary>
        /// Gets the videos collection from content.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// IQueryable Telerik.Sitefinity.Libraries.Model.Video.
        /// </returns>
        public static IList<VideoModel> GetVideos(this DynamicContent item, string fieldName)
        {
            return item.DoesFieldExist(fieldName)
                ? item.GetOriginal().GetRelatedItems<Video>(fieldName).Select(x => new VideoModel(x)).ToList()
                : Enumerable.Empty<VideoModel>().ToList();
        }

        /// <summary>
        /// Get a video from the given item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// The video.
        /// </returns>
        public static VideoModel GetVideo(this ContentModel item, string fieldName)
        {
            if (item == null || !item.DoesFieldExist(fieldName))
            {
                return null;
            }

            Video sfContent = item.GetOriginal().GetRelatedItems<Video>(fieldName).FirstOrDefault();
            return sfContent != null ? new VideoModel(sfContent) : null;
        }

        /// <summary>
        /// Gets the videos collection from content.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// IQueryable Telerik.Sitefinity.Libraries.Model.Video.
        /// </returns>
        public static IList<VideoModel> GetVideos(this ContentModel item, string fieldName)
        {
            return item.DoesFieldExist(fieldName)
                ? item.GetOriginal().GetRelatedItems<Video>(fieldName).Select(x => new VideoModel(x)).ToList()
                : Enumerable.Empty<VideoModel>().ToList();
        }

        /// <summary>
        /// Get a single image from a content.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// Telerik.Sitefinity.Libraries.Model.Image object.
        /// </returns>
        public static DocumentModel GetDocument(this DynamicContent item, string fieldName)
        {
            if (!item.DoesFieldExist(fieldName))
                return null;

            var sfContent = item.GetOriginal().GetRelatedItems<Document>(fieldName).FirstOrDefault();
            return sfContent != null ? new DocumentModel(sfContent) : null;
        }

        /// <summary>
        /// Gets the images collection from content.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// IQueryable Telerik.Sitefinity.Libraries.Model.Image.
        /// </returns>
        public static List<DocumentModel> GetDocuments(this DynamicContent item, string fieldName)
        {
            return item.DoesFieldExist(fieldName)
                ? item.GetOriginal().GetRelatedItems<Document>(fieldName).Select(x => new DocumentModel(x)).ToList()
                : Enumerable.Empty<DocumentModel>().ToList();
        }

        /// <summary>
        /// Get a single image from a content.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// Telerik.Sitefinity.Libraries.Model.Image object.
        /// </returns>
        public static DocumentModel GetDocument(this ContentModel item, string fieldName)
        {
            if (!item.DoesFieldExist(fieldName))
                return null;

            var sfContent = item.GetOriginal().GetRelatedItems<Document>(fieldName).FirstOrDefault();
            return sfContent != null ? new DocumentModel(sfContent) : null;
        }

        /// <summary>
        /// Gets the images collection from content.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// IQueryable Telerik.Sitefinity.Libraries.Model.Image.
        /// </returns>
        public static List<DocumentModel> GetDocuments(this ContentModel item, string fieldName)
        {
            return item.DoesFieldExist(fieldName)
                ? item.GetOriginal().GetRelatedItems<Document>(fieldName).Select(x => new DocumentModel(x)).ToList()
                : Enumerable.Empty<DocumentModel>().ToList();
        }

        /// <summary>
        /// Gets Dynamic Contents Title ("Title") being the field name.
        /// </summary>
        /// <param name="item">Current Dynamic Content Object.</param>
        /// <returns>
        /// Item Title.
        /// </returns>
        public static string GetTitle(this DynamicContent item)
        {
            return GetStringSafe(item, "Title");
        }

        /// <summary>
        /// Gets Dynamic Contents Description ("Description") being the field name.
        /// </summary>
        /// <param name="item">Current Dynamic Content Object.</param>
        /// <returns>
        /// Item Description.
        /// </returns>
        public static string GetDescription(this DynamicContent item)
        {
            return GetStringSafe(item, "Description");
        }

        /// <summary>
        /// Gets Dynamic Contents String Value for a given field, with current culture.
        /// </summary>
        /// <param name="item">Current Dynamic Content Object.</param>
        /// <param name="fieldName">Name of the string field.</param>
        /// <param name="defaultValue">(Optional) the default value.</param>
        /// <returns>
        /// Item value.
        /// </returns>
        public static string GetStringSafe(this IDynamicFieldsContainer item, string fieldName, string defaultValue = "")
        {
            if (item.DoesFieldExist(fieldName))
            {
                //CONVERT VALUE AND RETURN DEFAULT VALUE IF APPLICABLE
                string value = Convert.ToString(item.GetValue(fieldName), CultureInfo.CurrentCulture);
                return !string.IsNullOrEmpty(value) ? value : defaultValue;
            }
            return defaultValue;
        }

        /// <summary>
        /// Gets Dynamic Contents DateTime Value, with current culture. If the field is not found returns
        /// minimum datetime value.
        /// </summary>
        /// <param name="item">Current Dynamic Content Object.</param>
        /// <param name="fieldName">Name of the string field.</param>
        /// <returns>
        /// Item value.
        /// </returns>
        public static DateTime GetDateTime(this IDynamicFieldsContainer item, string fieldName)
        {
            if (item.DoesFieldExist(fieldName))
            {
                var dateTime = item.GetValue<DateTime?>(fieldName);
                return dateTime.HasValue ? dateTime.Value : DateTime.MinValue;
            }
            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets Dynamic Contents DateTime Value, which can be nullable. If the value is minimum date
        /// time method returns null.
        /// </summary>
        /// <param name="item">Current Dynamic Content Object.</param>
        /// <param name="fieldName">Name of the string field.</param>
        /// <returns>
        /// Item value.
        /// </returns>
        public static DateTime? GetDateTimeSafe(this IDynamicFieldsContainer item, string fieldName)
        {
            if (item.DoesFieldExist(fieldName))
            {
                var dateTime = item.GetValue<DateTime?>(fieldName);
                if (dateTime.HasValue && dateTime.Value == DateTime.MinValue)
                {
                    return null;
                }
                return dateTime.Value;
            }
            return null;
        }

        /// <summary>
        /// Gets Dynamic Contents Bool Value for a given field, with current culture. If the field is not
        /// found, returns false.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="item">Current Dynamic Content Object.</param>
        /// <param name="fieldName">Name of the string field.</param>
        /// <returns>
        /// Item value.
        /// </returns>
        public static bool GetBoolean(this IDynamicFieldsContainer item, string fieldName)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (fieldName != null && item.DoesFieldExist(fieldName))
            {
                return Convert.ToBoolean(item.GetValue(fieldName), CultureInfo.CurrentCulture);
            }
            return false;
        }

        /// <summary>
        /// Gets Dynamic Contents Int32 value for a given field, with current culture. If the field is not found, returns 0
        /// </summary>
        /// <param name="item">Current Dynamic Content Object</param>
        /// <param name="fieldName">Name of the string field</param>
        /// <returns>Item value</returns>
        public static Int32 GetInteger(this IDynamicFieldsContainer item, string fieldName)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (fieldName != null && item.DoesFieldExist(fieldName))
            {
                return Convert.ToInt32(item.GetValue(fieldName), CultureInfo.CurrentCulture);
            }
            return 0;
        }

        /// <summary>
        /// Gets Dynamic Contents long value for a given field, with current culture. If the field is not found, returns 0
        /// </summary>
        /// <param name="item">Current Dynamic Content Object</param>
        /// <param name="fieldName">Name of the string field</param>
        /// <returns>Item value</returns>
        public static long GetLong(this IDynamicFieldsContainer item, string fieldName)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (fieldName != null && item.DoesFieldExist(fieldName))
            {
                return Convert.ToInt64(item.GetValue(fieldName), CultureInfo.CurrentCulture);
            }
            return 0;
        }

        /// <summary>
        /// Creates a relation between DynamicModel item and relatedItem by field name
        /// </summary>
        /// <param name="item">Current Dynamic Content Object</param>
        /// <param name="relatedItem">Dynamic Content Object to relate</param>
        /// <param name="fieldName">Name of the string field</param>
        public static void CreateRelation(this DynamicModel item, DynamicModel relatedItem, string fieldName)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            else if (relatedItem == null)
            {
                throw new ArgumentException("relatedItem");
            }
            if (fieldName != null)
            {
                var sfContent = item.ToSitefinityModel();
                var sfRelatedContent = relatedItem.ToSitefinityModel(false);

                if (sfContent.DoesFieldExist(fieldName))
                {
                    DynamicModuleManager manager = DynamicModuleManager.GetManager();
                    sfContent.CreateRelation(sfRelatedContent, fieldName);

                    manager.Lifecycle.Publish(sfContent);
                    manager.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Removes relation between DynamicModel item and relatedItem by field name
        /// </summary>
        /// <param name="item">Current Dynamic Content Object</param>
        /// <param name="relatedItem">Related Dynamic Content Object</param>
        /// <param name="fieldName">Name of the string field</param>
        public static void DeleteRelation(this DynamicModel item, DynamicModel relatedItem, string fieldName)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            else if (relatedItem == null)
            {
                throw new ArgumentException("relatedItem");
            }
            if (fieldName != null)
            {
                var sfContent = item.ToSitefinityModel();
                var sfRelatedContent = relatedItem.ToSitefinityModel(false);

                if (sfContent.DoesFieldExist(fieldName))
                {
                    DynamicModuleManager manager = DynamicModuleManager.GetManager();
                    sfContent.DeleteRelation(sfRelatedContent, fieldName);

                    manager.Lifecycle.Publish(sfContent);
                    manager.SaveChanges();
                }
            }
        }
        
        /// <summary>
        /// Gets related Dynamic Models for a given field name
        /// </summary>
        /// <typeparam name="T">The content type.</typeparam>
        /// <param name="item">Current Dynamic Content Object</param>
        /// <param name="fieldName">Name of the string field</param>
        public static List<T> GetRelated<T>(this DynamicModel item, string fieldName) 
            where T : DynamicModel, new()
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (fieldName != null)
            {
                var sfContent = item.ToSitefinityModel();

                if (sfContent.DoesFieldExist(fieldName))
                {
                    var items = sfContent.GetRelatedItems<DynamicContent>(fieldName)
                        .Select(i => (T)Activator.CreateInstance(typeof(T), i))
                        .ToList();
                    return items;
                }
            }
            return null;
        }

        /// <summary>
        /// A DynamicModel extension method that gets the dynamic type.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The dynamic type.
        /// </returns>
        public static Type GetDynamicType(this DynamicModel item)
        {
            return TypeResolutionService.ResolveType(item.MappedType);
        }

        /// <summary>
        /// Filters out <paramref name="items"/> that do not have any of the given <paramref name="ids"/>
        /// in <paramref name="fieldName"/>.
        /// </summary>
        /// <remarks>
        /// This will cause SQL queries, etc. to execute when called. It does not go back to the database
        /// to do its filtering.
        /// </remarks>
        /// <param name="items">The items to filter.</param>
        /// <param name="fieldName">The name of the classification field to search.</param>
        /// <param name="ids">The IDs of the classifications to filter by.</param>
        /// <param name="filterIfEmptyIds">(Optional) Whether or not to apply filtering if
        /// <paramref name="ids"/> is an empty collection.</param>
        /// <returns>
        /// The filtered collection of <paramref name="items"/>. Returns original collection if any
        /// parameters are null.
        /// </returns>
        public static IEnumerable<IDynamicFieldsContainer> FilterByTaxa(this IEnumerable<IDynamicFieldsContainer> items, string fieldName, IList<Guid> ids, bool filterIfEmptyIds = false)
        {
            bool hasInvalidParameters = items == null || ids == null || string.IsNullOrWhiteSpace(fieldName);
            bool skipEmptyParameters = ids != null && !ids.Any() && !filterIfEmptyIds;

            if (hasInvalidParameters || skipEmptyParameters)
            {
                return items;
            }

            return items.Where(h => h.GetValue<TrackedList<Guid>>(fieldName).Intersect(ids).Any());
        }
        
        /// <summary>
        /// A DynamicContent extension method that gets an original.
        /// </summary>
        /// <param name="item">Current Dynamic Content Object.</param>
        /// <returns>
        /// The original item.
        /// </returns>
        public static DynamicContent GetOriginal(this DynamicContent item)
        {
            if (item.OriginalContentId != Guid.Empty)
            {
                var manager = item.GetManager();
                return manager.GetItem(item.GetType(), item.OriginalContentId) as DynamicContent;
            }

            return item;
        }

        /// <summary>
        /// A content extension method that gets an original.
        /// </summary>
        /// <param name="item">Current Content Object.</param>
        /// <returns>
        /// The original item.
        /// </returns>
        public static ContentModel GetOriginal(this ContentModel item)
        {
            if (item.OriginalContentId != Guid.Empty)
            {
                var manager = item.GetManager();
                return manager.GetItem(item.GetType(), item.OriginalContentId) as ContentModel;
            }

            return item;
        }

        /// <summary>
        /// An IDataItem extension method that gets a manager.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The manager.
        /// </returns>
        public static IManager GetManager(this IDataItem item)
        {
            return ManagerBase.GetMappedManager(item.GetType());
        }
    }
}
