// file:	Utilities\SystemHelper.cs
//
// summary:	Implements the system helper class
using Telerik.Sitefinity.Services;

namespace Babaganoush.Sitefinity.Utilities
{
    /// <summary>
    /// A system helper.
    /// </summary>
    public static class SystemHelper
    {
        /// <summary>
        /// Restarts the app if the app is not already restarting.
        /// </summary>
        public static void RestartApplication()
        {
            //VALIDATE TO ENSURE NOT ALREADY RESTARTING
            if (SystemManager.Initializing)
            {
                return;
            }

            //RESTART THROUGH SITEFINITY
            SystemManager.RestartApplication(true);
        }
    }
}
