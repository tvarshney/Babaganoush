// file:	Extensions\TaxonomyExtensions.cs
//
// summary:	Implements the taxonomy extensions class
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using Telerik.OpenAccess;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Babaganoush.Sitefinity.Extensions
{
    /// <summary>
    /// Extensions for taxonomy.
    /// </summary>
    public static class TaxonomyExtensions
    {
        /// <summary>
        /// Gets the Root of a HierarchicalTaxon
        /// ** Sitefinitysteve.com Extension **.
        /// </summary>
        /// <param name="currentTaxon">This Taxon.</param>
        /// <returns>
        /// Root Taxon.
        /// </returns>
        public static HierarchicalTaxon GetRootTaxon(this HierarchicalTaxon currentTaxon)
        {
            if (currentTaxon.Parent == null)
            {
                return currentTaxon;
            }
            
            HierarchicalTaxon parent = currentTaxon.Parent;
            while (parent.HasParent())
            {
                parent = parent.Parent;
            }
            return parent;
        }

        /// <summary>
        /// Gets a list of the parent Taxon items
        /// ** Sitefinitysteve.com Extension **.
        /// </summary>
        /// <param name="currentTaxon">Current Taxon.</param>
        /// <returns>
        /// List of HierarchicalTaxons linked to this item.  0 index is the closest parent.
        /// </returns>
        public static List<HierarchicalTaxon> GetParentTaxa(this HierarchicalTaxon currentTaxon)
        {
            var taxa = new List<HierarchicalTaxon>();

            if (currentTaxon.Parent != null)
            {
                var parent = currentTaxon.Parent;
                taxa.Add(parent);

                while (parent.HasParent())
                {
                    parent = parent.Parent;
                    taxa.Add(parent);
                }
            }

            return taxa;
        }

        /// <summary>
        /// Searches from Parent to parent to match the taxon name you need, returns the first match
        /// ** Sitefinitysteve.com Extension **.
        /// </summary>
        /// <param name="currentTaxon">Current Node.</param>
        /// <param name="textToFind">Text to locate.</param>
        /// <param name="type">Compare type.</param>
        /// <param name="isCaseSensitive">Case Sensitive check, doesn't apply to Contains.</param>
        /// <returns>
        /// Null if nothing found.
        /// </returns>
        public static HierarchicalTaxon GetFirstParentTaxon(this HierarchicalTaxon currentTaxon, string textToFind, HierarchicalTaxonCompareType type, bool isCaseSensitive)
        {
            StringComparison compareType = (isCaseSensitive) ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;

            while (currentTaxon != null)
            {
                if (TextInTaxonTitle(currentTaxon.Title, textToFind, type, compareType))
                {
                    return currentTaxon;
                }
                currentTaxon = currentTaxon.Parent;
            }

            return null;
        }

        /// <summary>
        /// Text in taxon title.
        /// </summary>
        /// <param name="taxonTitle">The taxon title.</param>
        /// <param name="textToFind">Text to locate.</param>
        /// <param name="type">Compare type.</param>
        /// <param name="compareType">Type of the compare.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        private static bool TextInTaxonTitle(string taxonTitle, string textToFind, HierarchicalTaxonCompareType type, StringComparison compareType)
        {
            switch (type)
            {
                case HierarchicalTaxonCompareType.StartsWith:
                    return taxonTitle.StartsWith(textToFind, compareType);
                case HierarchicalTaxonCompareType.EndsWith:
                    return taxonTitle.EndsWith(textToFind, compareType);
                case HierarchicalTaxonCompareType.Contains:
                    return taxonTitle.Contains(textToFind);
                case HierarchicalTaxonCompareType.Equals:
                    return taxonTitle.Equals(textToFind, compareType);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Flattens out a taxon tree to a list
        /// ** Sitefinitysteve.com Extension **.
        /// </summary>
        /// <param name="parent">The parent to act on.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process flatten hierarchy in this collection.
        /// </returns>
        public static IEnumerable<HierarchicalTaxon> FlattenHierarchy(this HierarchicalTaxon parent)
        {
            if (parent != null)
            {
                foreach (HierarchicalTaxon control in parent.Subtaxa)
                {
                    yield return control;
                    foreach (HierarchicalTaxon descendant in control.FlattenHierarchy())
                    {
                        yield return descendant;
                    }
                }
            }
        }

        /// <summary>
        /// Lets us know if a Hierarchical taxon object has a parent object
        /// ** Sitefinitysteve.com Extension **.
        /// </summary>
        /// <param name="currentTaxon">The taxon to check.</param>
        /// <returns>
        /// true if parent exists, false if not.
        /// </returns>
        private static bool HasParent(this HierarchicalTaxon currentTaxon)
        {
            return currentTaxon.Parent != null;
        }

        /// <summary>
        /// Flattens out the taxons to a delimited string
        /// ** Sitefinitysteve.com Extension **.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="seperator">(Optional) The seperator.</param>
        /// <param name="appendSpace">(Optional) if set to <c>true</c> [append space].</param>
        /// <returns>
        /// A string.
        /// </returns>
        public static string FlattenToString(this IEnumerable<HierarchicalTaxon> items, char seperator = ',', bool appendSpace = true)
        {
            string result = String.Empty;
            foreach (HierarchicalTaxon t in items)
            {
                result += String.Format("{0}{1}{2}", t.Title, seperator, (appendSpace) ? " " : "");
            }

            return result.Trim().TrimEnd(seperator);
        }

        /// <summary>
        /// Gets a taxon attribute value
        /// ** Sitefinitysteve.com Extension **.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="currentTaxon">This taxon.</param>
        /// <param name="key">Dictionary key the data is stored as.</param>
        /// <param name="createIfDoesntExist">(Optional) If the item isnt in the attribute collection,
        /// create it.</param>
        /// <param name="isJson">(Optional) If the data is a complex object stored as JSON make this true
        /// to return the deserialized object.</param>
        /// <returns>
        /// The value.
        /// </returns>
        public static T GetValue<T>(this Taxon currentTaxon, string key, bool createIfDoesntExist = false, bool isJson = false)
        {
            bool keyExists = currentTaxon.Attributes.ContainsKey(key);

            if (!keyExists)
            {
                if (createIfDoesntExist)
                {
                    var value = (default(T) == null) ? "null" : default(T).ToString();

                    currentTaxon.Attributes.Add(key, value);
                }
            }
            else
            {
                if (isJson)
                {
                    return JsonConvert.DeserializeObject<T>(currentTaxon.Attributes[key]);
                }
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(currentTaxon.Attributes[key]);
            }

            return default(T);
        }

        /// <summary>
        /// Saves an attribute to a taxon.  Do not forget to call SaveChanges on your TaxonomyManager.
        /// ** Sitefinitysteve.com Extension **.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when one or more arguments are outside the
        /// required range.</exception>
        /// <param name="currentTaxon">This taxon.</param>
        /// <param name="key">Dictionary Key to store the value as.</param>
        /// <param name="value">Data to store.</param>
        /// <param name="isJson">(Optional) If it's a complex object, convert to JSON.</param>
        public static void SetValue(this Taxon currentTaxon, string key, object value, bool isJson = false)
        {
            string data = (isJson) ? JsonConvert.SerializeObject(value) : value.ToString();

            if (data.Length >= 255)
            {
                throw new ArgumentOutOfRangeException("value", "Data length exceeded, Max 255");
            }

            if (!currentTaxon.Attributes.ContainsKey(key))
            {
                currentTaxon.Attributes.Add(key, data);
            }
            else
            {
                currentTaxon.Attributes[key] = data;
            }
        }

        /// <summary>
        /// Checks for a key
        /// ** Sitefinitysteve.com Extension **.
        /// </summary>
        /// <param name="currentTaxon">This taxon.</param>
        /// <param name="key">Key to find.</param>
        /// <returns>
        /// true if attribute contains the given <paramref name="key"/>, false if it doesn't, or if the
        /// given taxon or its attributes are null.
        /// </returns>
        public static bool HasAttribute(this Taxon currentTaxon, string key)
        {
            return currentTaxon != null && currentTaxon.Attributes != null && currentTaxon.Attributes.ContainsKey(key);
        }

        /// <summary>
        /// Get the linked Categories.
        /// </summary>
        /// <param name="item">The item to act on.</param>
        /// <returns>
        /// The categories.
        /// </returns>
        public static List<HierarchicalTaxon> GetCategories(this DynamicContent item)
        {
            var categories = item.GetValue<TrackedList<Guid>>("Category");

            TaxonomyManager manager = TaxonomyManager.GetManager();

            var taxonomyParent = manager.GetTaxonomy<HierarchicalTaxonomy>(TaxonomyManager.CategoriesTaxonomyId);

            var taxons = taxonomyParent.Taxa.Where(x => categories.Contains(x.Id)).Select(x => (HierarchicalTaxon)x);

            return taxons.ToList();
        }

        /// <summary>
        /// Get the linked Tags.
        /// </summary>
        /// <param name="item">The item to act on.</param>
        /// <returns>
        /// The tags.
        /// </returns>
        public static List<Taxon> GetTags(this DynamicContent item)
        {
            var tags = item.GetValue<TrackedList<Guid>>("Tags");

            TaxonomyManager manager = TaxonomyManager.GetManager();

            var taxonomyParent = manager.GetTaxonomy<Taxonomy>(TaxonomyManager.TagsTaxonomyId);
            var taxons = taxonomyParent.Taxa.Where(x => tags.Contains(x.Id)).ToList();

            return taxons;
        }

        /// <summary>
        /// Flattens the titles.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="separator">(Optional) The seperator.</param>
        /// <param name="appendSpace">(Optional) if set to <c>true</c> [append space].</param>
        /// <returns>
        /// A string.
        /// </returns>
        public static string FlattenTitles(this IEnumerable<TaxonModel> items, char separator = ',', bool appendSpace = true)
        {
            return FlattenField(items, separator, appendSpace, i => i.Title);
        }

        /// <summary>
        /// Flattens the URL names.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="separator">(Optional) The seperator.</param>
        /// <param name="appendSpace">(Optional) if set to <c>true</c> [append space].</param>
        /// <returns>
        /// A string.
        /// </returns>
        public static string FlattenUrlNames(this IEnumerable<TaxonModel> items, char separator = ',', bool appendSpace = false)
        {
            return FlattenField(items, separator, appendSpace, i => i.Slug);
        }

        /// <summary>
        /// Flatten field.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="separator">The seperator.</param>
        /// <param name="appendSpace">if set to <c>true</c> [append space].</param>
        /// <param name="selector">The selector.</param>
        /// <returns>
        /// A string.
        /// </returns>
        private static string FlattenField(IEnumerable<TaxonModel> items, char separator, bool appendSpace, Func<TaxonModel, string> selector)
        {
            return string.Join((appendSpace ? " " : string.Empty) + separator, items.Select(selector)).TrimStart();
        }

        /// <summary>
        /// Gets the taxon items.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="taxon">The taxon.</param>
        /// <param name="skip">(Optional) The skip.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process the taxon items in this collection.
        /// </returns>
        public static IEnumerable<T> GetItems<T>(this ITaxon taxon, int skip = 0, int take = 999)
        {
            // get the manager for the items, e.g. NewsManager
            var manager = ManagerBase.GetMappedManager(typeof(T));

            // get the base content database provider
            var contentProvider = manager.Provider as ContentDataProviderBase;

            // get a taxonomy property descriptor for this item type and taxon
            var prop = TaxonomyManager.GetPropertyDescriptor(typeof(T), taxon);

            int? totalCount = 0;
            // use the GetItemsByTaxon() method to return IEnumerable of items. 
            var items = contentProvider.GetItemsByTaxon(taxon.Id,
                prop.MetaField.IsSingleTaxon,
                prop.Name,
                typeof(T),
                string.Empty, // filter
                string.Empty, // order by
                skip, // skip
                take, // take
                ref totalCount);

            return items as IEnumerable<T>;
        }
    }
}
