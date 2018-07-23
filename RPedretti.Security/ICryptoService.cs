using System;
using System.Threading.Tasks;

namespace RPedretti.Security
{
    /// <summary>
    /// Handles all cryptograpy requests
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        /// Generates the RSA key pair asynchronous.
        /// </summary>
        /// <returns>A tuple of public/private key</returns>
        Task<Tuple<string, string>> GenerateRSAKeyPairAsync();

        /// <summary>
        /// Generates the triple DES key asynchronous.
        /// </summary>
        /// <returns>the key as bytes</returns>
        Task<byte[]> GenerateTripleDESKeyAsync();

        /// <summary>
        /// Registers the merged key.
        /// </summary>
        /// <param name="id">The key identifier</param>
        /// <param name="key">The key itself</param>
        void RegisterMergedKey(string id, byte[] key);

        /// <summary>
        /// Generates the combined triple DES key.
        /// </summary>
        /// <param name="key1">The first key</param>
        /// <param name="key2">The second key2</param>
        /// <returns></returns>
        byte[] GenerateCombinedTripleDesKey(byte[] key1, byte[] key2);

        /// <summary>
        /// Retrieves the merged key for the given id.
        /// </summary>
        /// <param name="id">The key identifier</param>
        /// <returns>the key as bytes</returns>
        byte[] RetrieveMergedKey(string id);

        /// <summary>
        /// Removes the merged key for the given id.
        /// </summary>
        /// <param name="id">The key identifier</param>
        /// <returns>a boolean indicating weather the key was removed</returns>
        bool RemoveMergedKey(string id);

        /// <summary>
        /// Decrypts a value with RSA asynchronous.
        /// </summary>
        /// <param name="value">The value to be decrypted</param>
        /// <param name="key">The key to be used at the decryption</param>
        /// <returns></returns>
        Task<string> DecryptRSAAsync(byte[] value, string key);

        /// <summary>
        /// Encrypts a value with RSA asynchronous.
        /// </summary>
        /// <param name="value">The value to be encrypted</param>
        /// <param name="key">The key to be used at the encryption</param>
        /// <returns>A array of bytes of the encrypted value</returns>
        Task<byte[]> EncryptRSAAsync(string value, string key);

        /// <summary>
        /// Decrypts a value with 3DES asynchronous.
        /// </summary>
        /// <param name="value">The value to be decrypted</param>
        /// <param name="key">The key to be used at the decryption</param>
        /// <returns></returns>
        Task<string> DecryptTripleDESAsync(byte[] value, byte[] key);

        /// <summary>
        /// Encrypts a value with 3DES asynchronous.
        /// </summary>
        /// <param name="value">The value to be encrypted</param>
        /// <param name="key">The key to be used at the encryption</param>
        /// <returns>A array of bytes of the encrypted value</returns>
        Task<byte[]> EncryptTripleDESAsync(string value, byte[] key);


        /// <summary>
        /// Hashes a value with sha256.
        /// </summary>
        /// <param name="v">The value to be hashed.</param>
        /// <returns></returns>
        string HashWithSha256(string v);
    }
}