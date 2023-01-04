using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JsonFileReaderDLL
{
    public static class MiscMethods
    {

		//Get SHA1 Hash from file input
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
