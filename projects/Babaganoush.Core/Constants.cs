
namespace Babaganoush.Core
{
    /// <summary>
    /// Constants and keys needed throughout the site.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The value alphanumeric regular expression.
        /// </summary>
        public const string VALUE_ALPHANUMERIC_REGEX = @"^[a-zA-Z]+[_ a-zA-Z0-9]*$";

        /// <summary>
        /// The value numeric regular expression.
        /// </summary>
        public const string VALUE_NUMERIC_REGEX = @"^\d+$";

        /// <summary>
        /// The key security encryption.
        /// </summary>
        public const string KEY_SECURITY_ENCRYPTION = "Babaganoush.Core.Security.EncryptionKey";

        /// <summary>
        /// The key security hash.
        /// </summary>
        public const string KEY_SECURITY_HASH = "Babaganoush.Core.Security.Hash";
    }
}
