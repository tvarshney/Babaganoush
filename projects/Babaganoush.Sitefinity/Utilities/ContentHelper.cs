// file:	Utilities\ContentHelper.cs
//
// summary:	Implements the content helper class
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Modules.Blogs;
using Telerik.Sitefinity.Modules.Blogs.Configuration;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Modules.Events.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Modules.News.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Babaganoush.Sitefinity.Utilities
{
    /// <summary>
    /// A content helper.
    /// </summary>
    public static class ContentHelper
    {
        /// <summary>
        /// The URL name characters to replace.
        /// </summary>
        public const string URL_NAME_CHARS_TO_REPLACE = @"[^\w\-\!\$\'\(\)\=\@\d_]+";

        /// <summary>
        /// The URL name replace string.
        /// </summary>
        public const string URL_NAME_REPLACE_STRING = "-";

        /// <summary>
        /// Gets an objects associated manager
        /// ** Sitefinitysteve.com Extension **.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// An instance of the manager of an object.
        /// </returns>
        public static IManager GetContentManager(object item)
        {
            return ManagerBase.GetMappedManager(item.GetType());
        }

        /// <summary>
        /// Generates the name of the URL.
        /// </summary>
        /// <param name="value">The title.</param>
        /// <returns>
        /// The URL name.
        /// </returns>
        public static string GenerateUrlName(string value)
        {
            return !string.IsNullOrWhiteSpace(value)
                ? Regex.Replace(value.ToLower(), URL_NAME_CHARS_TO_REPLACE, URL_NAME_REPLACE_STRING)
                : string.Empty;
        }

        /// <summary>
        /// Checks if the type contains a field.
        /// </summary>
        /// <param name="dynamicType">Type of the dynamic.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public static bool DoesTypeContainField(Type dynamicType, string fieldName)
        {
            return App.WorkWith()
                .DynamicData()
                .Type(dynamicType)
                .Get()
                .Fields
                .Any(f => f.FieldName == fieldName);
        }

        /// <summary>
        /// Checks if the dynamic type contains a field.
        /// </summary>
        /// <param name="dynamicType">Type of the dynamic.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public static bool DoesTypeContainsField(string dynamicType, string fieldName)
        {
            return DoesTypeContainField(TypeResolutionService.ResolveType(dynamicType), fieldName);
        }

        /// <summary>
        /// Returns an IList of Meta Fields for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The meta fields for type.
        /// </returns>
        public static IList<MetaField> GetMetaFieldsForType(string type)
        {
            var existingType = TypeResolutionService.ResolveType(type);
            var existingClassName = existingType.Name;
            var existingNamespace = existingType.Namespace;
            var mgr = MetadataManager.GetManager();
            var types = mgr.GetMetaTypes()
                .Where(dt => dt.ClassName == existingClassName
                    && dt.Namespace == existingNamespace)
                .FirstOrDefault();
            var fields = types.Fields;
            return fields;
        }

        /// <summary>
        /// Gets the Config section for a Content module supporting <paramref name="itemType"/>
        /// </summary>
        /// <param name="itemType">the Content item type (e.g. Telerik.Sitefinity.Blogs.Model.BlogPost)</param>
        /// <returns>
        /// ContentModuleConfigBase.
        /// </returns>
        ///
        /// ### <exception cref="NotImplementedException">Thrown when the requested operation is
        /// unimplemented.</exception>
        public static ContentModuleConfigBase GetModuleConfigSection(string itemType)
        {
            ContentModuleConfigBase section;
            switch (itemType)
            {
                case "Telerik.Sitefinity.Blogs.Model.BlogPost":
                    section = Config.Get<BlogsConfig>();
                    break;
                case "Telerik.Sitefinity.Events.Model.Event":
                    section = Config.Get<EventsConfig>();
                    break;
                case "Telerik.Sitefinity.News.Model.NewsItem":
                    section = Config.Get<NewsConfig>();
                    break;
                case "Telerik.Sitefinity.Libraries.Model.Document":
                case "Telerik.Sitefinity.Libraries.Model.Image":
                case "Telerik.Sitefinity.Libraries.Model.Video":
                    section = Config.Get<LibrariesConfig>();
                    break;
                default:
                    throw new NotImplementedException(String.Format("Unsupported type {0}", itemType));
            }
            return section;
        }

        /// <summary>
        /// Gets the BackendDefinition section name for a Content module supporting
        /// <paramref name="itemType"/>
        /// </summary>
        /// <param name="itemType">the Content item type (e.g. BlogPost)</param>
        /// <returns>
        /// BackendDefinition section name as string.
        /// </returns>
        ///
        /// ### <exception cref="NotImplementedException">Thrown when the requested operation is
        /// unimplemented.</exception>
        public static string GetModuleBackendDefinitionName(string itemType)
        {
            string name;
            switch (itemType)
            {
                case "Telerik.Sitefinity.Blogs.Model.BlogPost":
                    name = BlogsDefinitions.BackendPostsDefinitionName;
                    break;
                case "Telerik.Sitefinity.Events.Model.Event":
                    name = EventsDefinitions.BackendDefinitionName;
                    break;
                case "Telerik.Sitefinity.News.Model.NewsItem":
                    name = NewsDefinitions.BackendDefinitionName;
                    break;
                case "Telerik.Sitefinity.Libraries.Model.Document":
                    name = DocumentsDefinitions.BackendDefinitionName;
                    break;
                case "Telerik.Sitefinity.Libraries.Model.Image":
                    name = ImagesDefinitions.BackendImagesDefinitionName;
                    break;
                case "Telerik.Sitefinity.Libraries.Model.Video":
                    name = VideosDefinitions.BackendVideosDefinitionName;
                    break;
                default:
                    throw new NotImplementedException(String.Format("Unsupported type {0}", itemType));
            }
            return name;
        }

        /// <summary>
        /// Registers the field selector.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="E">Type of the e.</typeparam>
        /// <param name="fieldName">Name of the field.</param>
        /// <example>
        /// ContentHelper.RegisterFieldSelector&lt;Document, CustomFieldElement>("CustomField1");
        /// </example>
        public static void RegisterFieldSelector<T, E>(string fieldName)
            where T : IContent
            where E : FieldControlDefinitionElement
        {
            // Check if the field is not already present for this content type
            string itemType = typeof(T).FullName;
            var fieldExists = GetMetaFieldsForType(itemType)
                .Where(f => f.FieldName == fieldName).SingleOrDefault() != null;

            if (!fieldExists)
            {
                // Ddd the metafield that will hold the data
                App.WorkWith()
                   .DynamicData()
                   .Type(typeof(T))
                   .Field()
                   .TryCreateNew(fieldName, typeof(Guid[]))
                   .SaveChanges(true);

                // Get Backend views(e.g. Edit, Create) configuration
                var section = GetModuleConfigSection(itemType);
                string definitionName = GetModuleBackendDefinitionName(itemType);
                var backendSection = section.ContentViewControls[definitionName];
                var views = backendSection.ViewsConfig.Values.Where(v => v.ViewType == typeof(DetailFormView));

                foreach (DetailFormViewElement view in views)
                {
                    // If there are no custom fields added before, the new field will be placed int he CustomFieldsSection
                    var sectionToInsert = CustomFieldsContext.GetSection(view, CustomFieldsContext.customFieldsSectionName, itemType);

                    var fieldConfigElementType = TypeResolutionService.ResolveType(typeof(E).FullName);

                    // Create a new instance of our field configuration in the current view configuration
                    E newElement = Activator.CreateInstance(fieldConfigElementType, new object[] { sectionToInsert.Fields }) as E;

                    if (newElement != null)
                    {
                        // Populate custom field values
                        newElement.DataFieldName = fieldName;
                        newElement.FieldName = fieldName;
                        newElement.Title = fieldName;
                        newElement.DisplayMode = FieldDisplayMode.Write;

                        sectionToInsert.Fields.Add(newElement);
                    }
                }

                var manager = ConfigManager.GetManager();
                using (new ElevatedModeRegion(manager))
                {
                    manager.SaveSection(section);
                }
                
                SystemHelper.RestartApplication();
            }
        }

        /// <summary>
        /// Retrieves all Sitefinity custom fields and their values for the given
        /// <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="propertyDescriptorTypeName">Name of the PropertyDescriptor type.</param>
        /// <param name="includeRelatedData">(Optional) true to include, false to exclude the related data.</param>
        /// <returns>
        /// The custom field values.
        /// </returns>
        public static IDictionary<string, object> GetCustomFieldValues(object item, string propertyDescriptorTypeName, bool includeRelatedData = true)
        {
            var propertyDescriptorCollection = TypeDescriptor.GetProperties(item);
            var propertyDescriptorType = Type.GetType(propertyDescriptorTypeName);

            return propertyDescriptorCollection.Cast<PropertyDescriptor>()
                .Where(d => d.GetType() == propertyDescriptorType && (includeRelatedData || !DescriptorIsRelatedData(d)))
                .ToDictionary(d => d.Name, d => d.GetValue(item));
        }

        /// <summary>
        /// Descriptor is related data.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        private static bool DescriptorIsRelatedData(MemberDescriptor descriptor)
        {
            var metaFieldAttributeAttribute = (MetaFieldAttributeAttribute)descriptor.Attributes[typeof(MetaFieldAttributeAttribute)];
            return metaFieldAttributeAttribute != null && metaFieldAttributeAttribute.Attributes.Any(x => x.Key == "UserFriendlyDataType" && x.Value == "RelatedData");
        }
    }
}
