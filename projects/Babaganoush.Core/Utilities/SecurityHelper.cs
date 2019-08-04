using Babaganoush.Core.Configuration;
using Babaganoush.Core.Security;
using System.Security.Cryptography;
using System.Text;

namespace Babaganoush.Core.Utilities
{
    /// <summary>
    /// A security helper.
    /// </summary>
    public static class SecurityHelper
    {
        /// <summary>
        /// Encrypts the specified value.
        /// </summary>
        ///
        /// <param name="value">The value.</param>
        /// <param name="encryptionKey">(Optional) The encryption key.</param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public static string Encrypt(string value, string encryptionKey = null)
        {
            var security = new Encryption(encryptionKey);
            return security.Encrypt(value);
        }

        /// <summary>
        /// Decrypts the specified value.
        /// </summary>
        ///
        /// <param name="value">The value.</param>
        /// <param name="encryptionKey">(Optional) The encryption key.</param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public static string Decrypt(string value, string encryptionKey = null)
        {
            var security = new Encryption(encryptionKey);
            return security.Decrypt(value);
        }

        /// <summary>
        /// Return a code for a unique id, such as a Phone number or Email address. The code will always
        /// be the same for the same id.
        /// </summary>
        ///
        /// <param name="id">Phone Number or Email Address (or any identifying string for a user)</param>
        ///
        /// <returns>
        /// Code to use for validating user email or phone.
        /// </returns>
        public static string GetCodeForString(string id)
        {
            // random key for HMAC function - must remain same for codes to match
            string _hashkey = AppSettings.Get(Constants.KEY_SECURITY_HASH, "B5BbSFLD9RkpysoeR5E3SJd6G7Uqpowh");
            // lenth of passcode
            int _codelength = 6;
            // define characters allowed in passcode.  set length so divisible into 256
            char[] _validChars = 
			    {
				    '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 
				    'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
			    }; // len=32
            byte[] hash;

            using (var sha1 = new HMACSHA1(Encoding.ASCII.GetBytes(_hashkey)))
            {
                hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(id));
            }

            int startpos = hash[hash.Length - 1] % (hash.Length - _codelength);
            var passbuilder = new StringBuilder();
            for (int i = startpos; i < startpos + _codelength; i++)
            {
                passbuilder.Append(_validChars[hash[i] % _validChars.Length]);
            }

            return passbuilder.ToString();
        }
    }
}
