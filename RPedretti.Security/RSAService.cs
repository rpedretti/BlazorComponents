using System;
using System.Security.Cryptography;
using System.Text;
using RPedretti.Security.Extensions;

namespace RPedretti.Security
{
    /// <summary>
    /// Class responsible for handlig RSA requests
    /// </summary>
    public class RSAService
    {
        /// <summary>
        /// Generates the key pair.
        /// </summary>
        /// <returns>A tuple with a public/private key pair</returns>
        public Tuple<string, string> GenerateKeyPair()
        {
            using (var rsa = RSA.Create())
            {
                Console.WriteLine($"rsa created");
                var publicKey = rsa.CustomToXmlString();
                var publicPrivate = rsa.CustomToXmlString(true);
                Console.WriteLine($"rsa parsed");

                return Tuple.Create(publicKey, publicPrivate);
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
    }
}
