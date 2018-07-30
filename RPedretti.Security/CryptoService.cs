using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RPedretti.Security
{
    /// <summary>
    /// Class responsible for cryptographic operations
    /// </summary>
    /// <seealso cref="RPedretti.Security.ICryptoService" />
    public sealed class CryptoService : ICryptoService
    {
        #region Fields

        private static Dictionary<string, byte[]> _mergedTripleDesKeys = new Dictionary<string, byte[]>();
        private RSAService _rsaService = new RSAService();
        private TripleDESService _tripleDESService = new TripleDESService();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Decrypts a value with RSA asynchronous.
        /// </summary>
        /// <param name="value">The value to be decrypted</param>
        /// <param name="key">The key to be used at the decryption</param>
        /// <returns></returns>
        public async Task<string> DecryptRSAAsync(byte[] value, string key)
        {
            return await Task.Run(() => { return _rsaService.Decrypt(value, key); });
        }

        /// <summary>
        /// Decrypts a value with 3DES asynchronous.
        /// </summary>
        /// <param name="value">The value to be decrypted</param>
        /// <param name="key">The key to be used at the decryption</param>
        /// <returns></returns>
        public async Task<string> DecryptTripleDESAsync(byte[] value, byte[] key)
        {
            return await Task.Run(() => { return _tripleDESService.Decrypt(value, key); });
        }

        /// <summary>
        /// Encrypts a value with RSA asynchronous.
        /// </summary>
        /// <param name="value">The value to be encrypted</param>
        /// <param name="key">The key to be used at the encryption</param>
        /// <returns>
        /// A array of bytes of the encrypted value
        /// </returns>
        public async Task<byte[]> EncryptRSAAsync(string value, string key)
        {
            return await Task.Run(() => { return _rsaService.Encrypt(value, key); });
        }

        /// <summary>
        /// Encrypts a value with 3DES asynchronous.
        /// </summary>
        /// <param name="value">The value to be encrypted</param>
        /// <param name="key">The key to be used at the encryption</param>
        /// <returns>
        /// A array of bytes of the encrypted value
        /// </returns>
        public async Task<byte[]> EncryptTripleDESAsync(string value, byte[] key)
        {
            return await Task.Run(() => { return _tripleDESService.Encrypt(value, key); });
        }

        /// <summary>
        /// Generates the combined triple DES key.
        /// </summary>
        /// <param name="key1">The first key</param>
        /// <param name="key2">The second key2</param>
        /// <returns></returns>
        public byte[] GenerateCombinedTripleDesKey(byte[] key1, byte[] key2)
        {
            byte[] mergedKey = new byte[key1.Length];

            for (int i = 0; i < mergedKey.Length; i++)
            {
                mergedKey[i] = (byte)(key1[i] & key2[i]);
            }

            return mergedKey;
        }

        /// <summary>
        /// Generates the RSA key pair asynchronous.
        /// </summary>
        /// <returns>
        /// A tuple of public/private key
        /// </returns>
        public async Task<Tuple<string, string>> GenerateRSAKeyPairAsync()
        {
            var keys = await Task.FromResult(_rsaService.GenerateKeyPair());
            return keys;
        }

        /// <summary>
        /// Generates the triple DES key asynchronous.
        /// </summary>
        /// <returns>
        /// the key as bytes
        /// </returns>
        public async Task<byte[]> GenerateTripleDESKeyAsync()
        {
            return await Task.Run(() => { return _tripleDESService.GenerateKey(); });
        }

        /// <summary>
        /// Hashes the with sha256.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public string HashWithSha256(string data)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                var sb = new StringBuilder();
                for (var i = 0; i < hashed.Length; i++)
                {
                    sb.Append(String.Format("{0:X2}", hashed[i]));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Registers the merged key.
        /// </summary>
        /// <param name="id">The key identifier</param>
        /// <param name="key">The key itself</param>
        public void RegisterMergedKey(string id, byte[] key)
        {
            _mergedTripleDesKeys[id] = key;
        }

        /// <summary>
        /// Removes the merged key for the given id.
        /// </summary>
        /// <param name="id">The key identifier</param>
        /// <returns>
        /// a boolean indicating weather the key was removed
        /// </returns>
        public bool RemoveMergedKey(string id)
        {
            return _mergedTripleDesKeys.Remove(id);
        }

        /// <summary>
        /// Retrieves the merged key for the given id.
        /// </summary>
        /// <param name="id">The key identifier</param>
        /// <returns>
        /// the key as bytes
        /// </returns>
        public byte[] RetrieveMergedKey(string id)
        {
            _mergedTripleDesKeys.TryGetValue(id, out byte[] key);
            return key;
        }

        #endregion Methods
    }
}
