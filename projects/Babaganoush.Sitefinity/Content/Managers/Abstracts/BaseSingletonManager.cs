// file:	Content\Managers\Abstracts\BaseSingletonManager.cs
//
// summary:	Implements the base singleton manager class
using Telerik.Sitefinity.Data;

namespace Babaganoush.Sitefinity.Content.Managers.Abstracts
{
    /// <summary>
    /// Manager that creates singleton for easier static use.
    /// </summary>
    /// <typeparam name="TManager">Type of the manager.</typeparam>
    /// <typeparam name="TBaseManager">Type of the base manager.</typeparam>
    public abstract class BaseSingletonManager<TManager, TBaseManager> : BaseManager<TManager>
        where TManager : class, IManager
        where TBaseManager: BaseManager<TManager>, new()
    {
        /// <summary>
        /// CREATE UNIQUE INSTANCE.
        /// </summary>
        private static readonly TBaseManager _instance = new TBaseManager();

        /// <summary>
        /// Expose unique instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static TBaseManager Instance
        {
            get { return _instance; }
        }
    }
}
