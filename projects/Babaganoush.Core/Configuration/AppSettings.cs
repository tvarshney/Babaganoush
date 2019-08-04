using Babaganoush.Core.Extensions;
using System.Configuration;

namespace Babaganoush.Core.Configuration
{
    /// <summary>
    /// An application settings.
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// Gets the app setting.
        /// </summary>
        ///
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">(Optional) The default value to use if the setting has no value.</param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public static string Get(string key, string defaultValue = null)
        {
            //GET VALUE FROM APP SETTINGS CONFIG
            string value = ConfigurationManager.AppSettings[key];

            //RETURN VALUE OR DEFAULT
            return value ?? defaultValue ?? string.Empty;
        }

        /// <summary>
        /// Gets the strongly typed app setting.
        /// </summary>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">(Optional) The default value.</param>
        ///
        /// <returns>
        /// A T.
        /// </returns>
        ///
        /// ### <typeparam name="T">.</typeparam>
        public static T Get<T>(string key, T defaultValue = default(T))
        {
            //GET VALUE FROM APP SETTINGS CONFIG
            string value = ConfigurationManager.AppSettings[key];

            //USE DEFAULT FOR NON-EXISTANT KEYS
            if (value == null)
                return defaultValue;

            //RETURN VALUE OR DEFAULT
            return value.ChangeTypeTo<T>();
        }
    }
}
