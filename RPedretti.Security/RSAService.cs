using RPedretti.Security.Extensions;
using System;
using System.Security.Cryptography;
using System.Text;

namespace RPedretti.Security
{
    /// <summary>
    /// Class responsible for handlig RSA requests
    /// </summary>
    public class RSAService
    {
        #region Methods

        /// <summary>
        /// Decrypts the specified value.
        /// </summary>
        /// <param name="value">The value to be decrypted.</param>
        /// <param name="key">The key to be used at the decryption.</param>
        /// <returns></returns>
        public string Decrypt(byte[] value, string key)
        {
            using (var rsa = RSA.Create())
            {
                rsa.CustomFromXmlString(key);
                return Encoding.UTF8.GetString(rsa.Decrypt(value, RSAEncryptionPadding.Pkcs1));
            }
        }

        /// <summary>
        /// Encrypts the specified value.
        /// </summary>
        /// <param name="value">The value to be encrypted.</param>
        /// <param name="key">The key to be used at the encryption.</param>
        /// <returns></returns>
        public byte[] Encrypt(string value, string key)
        {
            using (var rsa = RSA.Create())
            {
                rsa.CustomFromXmlString(key);
                return rsa.Encrypt(Encoding.UTF8.GetBytes(value), RSAEncryptionPadding.Pkcs1);
            }
        }

        /// <summary>
        /// Generates the key pair.
        /// </summary>
        /// <returns>A tuple with a public/private key pair</returns>
        public Tuple<string, string> GenerateKeyPair()
        {
            using (var rsa = RSA.Create())
            {
                var publicKey = rsa.CustomToXmlString();
                var publicPrivate = rsa.CustomToXmlString(true);

                return Tuple.Create(publicKey, publicPrivate);
            }
        }

        #endregion Methods
    }
}
