// file:	Content\Managers\Abstracts\BaseManager.cs
//
// summary:	Implements the base manager class
using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Data;

namespace Babaganoush.Sitefinity.Content.Managers.Abstracts
{
    /// <summary>
    /// Manager for bases.
    /// </summary>
    /// <typeparam name="TManager">Type of the manager.</typeparam>
    public abstract class BaseManager<TManager> 
        where TManager : class, IManager
    {
        /// <summary>
        /// The default provider name.
        /// </summary>
        private const string DEFAULT_PROVIDER_NAME = "Default";

        /// <summary>
        /// Manager for type.
        /// </summary>
        private Type _managerType = null;

        /// <summary>
        /// The managers.
        /// </summary>
        private IDictionary<string, TManager> _managers = null;

        /// <summary>
        /// Gets a manager.
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// The manager.
        /// </returns>
        public TManager GetManager(string providerName = null)
        {
            //ASSIGN MANAGER NAME TO AVOID REFLECTION AFTER FIRST TIME
            if (_managerType == null)
                _managerType = typeof(TManager);

            //ASSIGN DEFAULT MANAGER IN CASE NEEDED LATER
            if (_managers == null)
            {
                //CACHE DEFAULT MANAGER FOR LATER USE
                _managers = new Dictionary<string, TManager>()
                {
                    { DEFAULT_PROVIDER_NAME, ManagerBase.GetManager(_managerType) as TManager }
                };
            }

            //GET DEFAULT OR PROVIDER-BASED MANAGER
            if (!string.IsNullOrWhiteSpace(providerName))
            {
                //CACHE PROVIDER MANAGER FOR LATER USE
                if (!_managers.ContainsKey(providerName))
                    _managers.Add(providerName, ManagerBase.GetManager(_managerType, providerName) as TManager);

                return _managers[providerName];
            }
            else
            {
                //USE CACHED DEFAULT MANAGER
                return _managers[DEFAULT_PROVIDER_NAME];
            }
        }

        /// <summary>
        /// Dispose the Sitefinity managers collection that BaseManager is holding on to.
        /// </summary>
        public void Dispose()
        {
            _managers = null;
        }
    }
}