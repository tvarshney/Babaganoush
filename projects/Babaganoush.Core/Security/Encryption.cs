using Babaganoush.Core.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Babaganoush.Core.Security
{
    /// <summary>
    /// Encrypt and decrypt based on app setting keys.
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// The default encryption key.
        /// </summary>
        private const string DEFAULT_ENCRYPTION_KEY = "%#@falafel#^!!fsaw!W";

        /// <summary>
        /// The iv.
        /// </summary>
        private readonly byte[] _iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        /// <summary>
        /// The encryption in bytes.
        /// </summary>
        private byte[] _encryptionBytes = { };

        /// <summary>
        /// The encryption key.
        /// </summary>
        private string _encryptionKey = string.Empty;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Encryption()
        {
            _encryptionKey = AppSettings.Get(Constants.KEY_SECURITY_ENCRYPTION, DEFAULT_ENCRYPTION_KEY);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="encryptionKey">The encryption key.</param>
        public Encryption(string encryptionKey)
        {
            //GET ENCRYPTION KEY FROM NEW INSTANCE PARAM OR USE DEFAULT
            _encryptionKey = !string.IsNullOrWhiteSpace(encryptionKey)
                ? encryptionKey.PadRight(8, ' ')
                : AppSettings.Get(Constants.KEY_SECURITY_ENCRYPTION, DEFAULT_ENCRYPTION_KEY);
        }

        /// <summary>
        /// Decrypts.
        /// </summary>
        ///
        /// <param name="stringToDecrypt">The string to decrypt.</param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public string Decrypt(string stringToDecrypt)
        {

            stringToDecrypt = HttpUtility.UrlDecode(stringToDecrypt);

            int mod4 = stringToDecrypt.Length % 4;
            if (mod4 > 0)
            {
                stringToDecrypt += new string('=', 4 - mod4);
            }

            var inputByteArray = new byte[stringToDecrypt.Length + 1];
            try
            {
                _encryptionBytes = Encoding.UTF8.GetBytes(_encryptionKey.Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt.Replace(" ", "+"));
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(_encryptionBytes, _iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                var encoding = Encoding.UTF8;

                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                //TODO: LOG ERROR
                return e.Message;
            }
        }

        /// <summary>
        /// Encrypts.
        /// </summary>
        ///
        /// <param name="stringToEncrypt">The string to encrypt.</param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public string Encrypt(string stringToEncrypt)
        {
            try
            {
                _encryptionBytes = Encoding.UTF8.GetBytes(_encryptionKey.Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(_encryptionBytes, _iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return HttpUtility.UrlEncode(Convert.ToBase64String(ms.ToArray()));
            }
            catch (Exception e)
            {
                //TODO: LOG ERROR
                return e.Message;
            }
        }
    }
}
