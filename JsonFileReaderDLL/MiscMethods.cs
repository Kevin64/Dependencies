using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JsonFileReaderDLL
{
    public static class MiscMethods
    {

		//Get SHA1 Hash from file input
		public static string GetSha1Hash(string filePath)
		{
			using (FileStream fs = File.OpenRead(filePath))
			{
				SHA1 sha = new SHA1Managed();
				return BitConverter.ToString(sha.ComputeHash(fs)).Replace("-", string.Empty);
			}
		}

		//Generates a MD5 hash from an input
		public static string HashMd5Generator(string input)
		{
			MD5 md5Hash = MD5.Create();
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
			StringBuilder sBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}
			return sBuilder.ToString();
		}
	}
}
