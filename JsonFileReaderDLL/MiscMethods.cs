using System;
using System.IO;
using System.Security.Cryptography;

namespace JsonFileReaderDLL
{
    /// <summary> 
    /// Class for miscelaneous methods
    /// </summary>
    public static class MiscMethods
    {
        /// <summary> 
        /// 
        ///Gets the SHA-256 hash from a specific file
        ///
        /// </summary>
        /// <param name="filePath">Path of the file you want to know the hash</param>
        /// <returns>The SHA-256 hash.</returns>
        public static string GetSha256Hash(string filePath)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                SHA256 sha = new SHA256Managed();
                return BitConverter.ToString(sha.ComputeHash(fs)).Replace("-", string.Empty);
            }
        }
    }
}
