using System;
using System.IO;
using System.Security.Cryptography;

namespace RPedretti.Security
{
    /// <summary>
    /// Class responsible for handling TripleDes operations
    /// </summary>
    public class TripleDESService
    {
        /// <summary>
        /// Generates a key.
        /// </summary>
        /// <returns>A byte array of the generated key</returns>
        public byte[] GenerateKey()
        {
            using (var tripleDes = TripleDES.Create())
            {
                tripleDes.GenerateKey();
                return tripleDes.Key;
            }
        }

        /// <summary>
        /// Encrypts the specified text.
        /// </summary>
        /// <param name="text">The text to be encrypted.</param>
        /// <param name="key">The key to be used at the encryption.</param>
        /// <returns>A byte array of the encrypted text</returns>
        public byte[] Encrypt(string text, byte[] key)
        {
            using (var tripleDes = TripleDES.Create())
            using (var encryptor = tripleDes.CreateEncryptor(key, tripleDes.IV))
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(text);
                }

                var iv = tripleDes.IV;

                var decryptedContent = msEncrypt.ToArray();

                var result = new byte[iv.Length + decryptedContent.Length];

                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                return result;
            }
        }

        /// <summary>
        /// Decrypts the specified encrypted text.
        /// </summary>
        /// <param name="encryptedText">The encrypted text to be decrypted.</param>
        /// <param name="key">The key to be used at the decryption.</param>
        /// <returns></returns>
        public string Decrypt(byte[] encryptedText, byte[] key)
        {
            var iv = new byte[8];
            var cipher = new byte[encryptedText.Length - iv.Length];

            Buffer.BlockCopy(encryptedText, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(encryptedText, iv.Length, cipher, 0, cipher.Length);

            using (var tripleDes = TripleDES.Create())
            using (var decryptor = tripleDes.CreateDecryptor(key, iv))
            {
                string result;
                using (var msDecrypt = new MemoryStream(cipher))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    result = srDecrypt.ReadToEnd();
                }

                return result;
            }
        }
    }
}
