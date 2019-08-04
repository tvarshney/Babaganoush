// file:	Content\Managers\Abstracts\DynamicModuleManager.cs
//
// summary:	Implements the dynamic module manager class
using Babaganoush.Sitefinity.Content.Managers.Interfaces;
using Babaganoush.Sitefinity.Extensions;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Babaganoush.Sitefinity.Content.Managers.Abstracts
{
    /// <summary>
    /// Manager for dynamic modules.
    /// </summary>
    /// <typeparam name="TBaseDynamicModuleManager">Type of the base dynamic module manager.</typeparam>
    /// <typeparam name="TDynamicModel">Type of the dynamic model.</typeparam>
    public abstract class DynamicModuleManager<TBaseDynamicModuleManager, TDynamicModel> : BaseFieldManager<DynamicModuleManager, DynamicContent, TBaseDynamicModuleManager, TDynamicModel>, IDynamicManager<TDynamicModel>
        where TBaseDynamicModuleManager : BaseManager<DynamicModuleManager>, new()
        where TDynamicModel : DynamicModel, new()
    {
        /// <summary>
        /// Type of the resolved dynamic.
        /// </summary>
        private Type _resolvedDynamicType;

        /// <summary>
        /// Gets the type of the dynamic.
        /// </summary>
        /// <returns>
        /// The dynamic type.
        /// </returns>
        public override Type GetDynamicType()
        {
            return _resolvedDynamicType ??
                (_resolvedDynamicType = TypeResolutionService.ResolveType(new TDynamicModel().MappedType));
        }

        /// <summary>
        /// Determines whether or not the dynamic type <typeparamref name="TDynamicModel"/> exists.
        /// </summary>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public virtual bool DoesTypeExist()
        {
            try
            {
                GetDynamicType();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all instances of <typeparamref name="TDynamicModel"/>.
        /// </summary>
        /// <param name="providerName">(Optional) the provider name to get.</param>
        /// <returns>
        /// An IQueryable&lt;DynamicContent&gt;
        /// </returns>
        protected override IQueryable<DynamicContent> Get(string providerName = null)
        {
            return GetManager(providerName).GetDataItems(GetDynamicType())
                .Where(c => c.Status == ContentLifecycleStatus.Live
                    && c.Visible);
        }

        /// <summary>
        /// Gets the specified content by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// A DynamicContent.
        /// </returns>
        protected override DynamicContent Get(Guid id, string providerName = null)
        {
            return Get(id, false, providerName);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="checkout">if set to <c>true</c> [checkout].</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// A DynamicContent.
        /// </returns>
        protected virtual DynamicContent Get(Guid id, bool checkout, string providerName = null)
        {
            //VALIDATE INPUT
            if (id == Guid.Empty)
                return null;

            //GET LIVE ITEM FROM STORAGE
            var content = GetManager(providerName).GetDataItem(GetDynamicType(), id);

            //CHECK OUT ITEM FOR UPDATE IF APPLICABLE
            if (checkout)
            {
                //VALIDATE INPUT
                if (content == null)
                    return null;

                //EDIT MODE ON CONTENT AND RETURNED CHECKED OUT ITEM
                var master = GetManager(providerName).Lifecycle.Edit(content) as DynamicContent;
                return GetManager(providerName).Lifecycle.CheckOut(master) as DynamicContent;
            }

            return content;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if
        /// you want to override the default constructor.</param>
        /// <returns>
        /// all.
        /// </returns>
        public override IEnumerable<TDynamicModel> GetAll(
            string providerName = null,
            Expression<Func<DynamicContent, bool>> filter = null, 
            int take = 0, 
            int skip = 0,
            Expression<Func<DynamicContent, TDynamicModel>> convert = null)
        {
            var sfItems = Get(providerName);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            // Return default instantiated or one given in parameter
            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by name.
        /// </returns>
        public virtual TDynamicModel GetByName(string value,
            string providerName = null,
            Func<DynamicContent, TDynamicModel> convert = null)
        {
            var sfItem = Get(providerName).FirstOrDefault(i => i.UrlName == value.ToLower());
            return sfItem != null ? (convert != null ? convert(sfItem) : CreateInstance(sfItem)) : null;
        }

        /// <summary>
        /// Gets the most recent item(s).
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) The take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The recent.
        /// </returns>
        public virtual IEnumerable<TDynamicModel> GetRecent(
            string providerName = null, 
            Expression<Func<DynamicContent, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<DynamicContent, TDynamicModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(s => s.Status == ContentLifecycleStatus.Live
                    && s.Visible);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE SORTING
            sfItems = sfItems
                .OrderByDescending(i => i.PublicationDate);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        /// <summary>
        /// Gets the by title.
        /// </summary>
        /// <param name="value">The title.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if you want to override the default constructor.</param>
        /// <returns>
        /// The by title.
        /// </returns>
        public virtual TDynamicModel GetByTitle(string value,
            string providerName = null,
            Func<DynamicContent, TDynamicModel> convert = null)
        {
            var sfItems = GetByField("Title", value, providerName, convert: i => convert(i)).ToList();
            return sfItems.Any() ? sfItems.First() : null;
        }

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// A TDynamicModel.
        /// </returns>
        public virtual TDynamicModel Create(TDynamicModel value, string providerName = null)
        {
            using (new ElevatedModeRegion(GetManager(providerName)))
            {
                try
                {
                    //CONVERT CURRENT MODEL TO SITEFINITY MODEL
                    var sfContent = value.ToSitefinityModel();

                    //HANDLE PARENT IF APPLICABLE
                    DynamicModel parent = null;
                    if (value is IHierarchy)
                    {
                        parent = (value as IHierarchy).Parent;
                    }

                    //PUBLISH AND PERSIST TO SITEFINITY STORAGE
                    if (GetManager(providerName).PublishAndSave(sfContent, parent))
                    {
                        //UPDATE ANY GENERATED PROPERTIES
                        value.Id = sfContent.Id;

                        //RETURN ITEM
                        return value;
                    }
                    return null;
                }
                catch (Exception)
                {
                    //TODO: LOG ERROR
                    return null;
                }
            }
        }

        /// <summary>
        /// Updates the specified <typeparamref name="TDynamicModel"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public virtual bool Update(TDynamicModel value, string providerName = null)
        {
            DynamicModuleManager dynamicModuleManager = GetManager(providerName);
            using (new ElevatedModeRegion(dynamicModuleManager))
            {
                try
                {
                    //CONVERT CURRENT MODEL TO SITEFINITY MODEL
                    var sfContent = value.ToSitefinityModel();

                    //PUBLISH AND PERSIST TO SITEFINITY STORAGE
                    if (sfContent != null)
                    {
                        return dynamicModuleManager.PublishAndSave(sfContent);
                    }
                }
                catch (Exception)
                {
                    //TODO: LOG ERROR
                }
                return false;
            }
        }
    }
}
