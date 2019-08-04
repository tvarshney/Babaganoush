// file:	Extensions\DynamicModuleManagerExtensions.cs
//
// summary:	Implements the dynamic module manager extensions class
using Babaganoush.Sitefinity.Models;
using System;
using System.Linq;
using Telerik.Sitefinity;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Babaganoush.Sitefinity.Extensions
{
    /// <summary>
    /// Extensions for the Sitefinity DynamicModuleManager.
    /// </summary>
    public static class DynamicModuleManagerExtensions
    {
        /// <summary>
        /// Publishes the specified data item.
        /// </summary>
        /// <param name="manager">The manager to publish with.</param>
        /// <param name="dataItem">The data item.</param>
        /// <param name="parentItem">(Optional) The parent of the data item to publish and save.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public static bool PublishAndSave(this DynamicModuleManager manager, DynamicContent dataItem, DynamicModel parentItem = null)
        {
            //CODE TAKEN FROM DEFAULT CODE REFERENCE OF MODULE BUILDER
            try
            {
                //UPDATE PUBLICATION DATE
                dataItem.PublicationDate = DateTime.UtcNow;

                //HANDLE NEW ITEM IF APPLICABLE
                if (dataItem.OriginalContentId == Guid.Empty)
                {
                    // Set item parent if applicable
                    if (parentItem != null)
                    {
                        var parentMaster = manager.GetDataItems(TypeResolutionService.ResolveType(parentItem.MappedType))
                            .First(i => i.UrlName == parentItem.Slug && i.Status == ContentLifecycleStatus.Master);

                        dataItem.SetParent(parentMaster.Id, parentItem.MappedType);
                    }

                    //You need to set appropriate workflow status
                    dataItem.SetWorkflowStatus(manager.Provider.ApplicationName, "Published");

                    // We can now call the following to publish the item
                    manager.Lifecycle.Publish(dataItem);
                }
                else //HANDLE UPDATES ON EXISTING ITEM
                {
                    // Now we need to check in, so the changes apply
                    var master = manager.Lifecycle.CheckIn(dataItem);

                    // We can now call the following to publish the item
                    manager.Lifecycle.Publish(master);
                }

                // You need to call SaveChanges() in order for the items to be actually persisted to data store
                manager.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //TODO: LOG ERROR
                return false;
            }
        }
    }
}
