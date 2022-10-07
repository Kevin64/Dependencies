﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using ConstantsDLL;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
	public class lFile
	{
		public string id { get; set; }
		public string usuario { get; set; }
		public string senha { get; set; }
		public string nivel { get; set; }
		public string status { get; set; }
	}
	public static class LoginFileReader
	{		
		private static string jsonFile, sha1, aux;
		private static WebClient wc;
		private static StreamReader fileL;

        //Checks if the server is answering any requests, through a json file verification (creates a separate thread)
        public static Task<bool> checkHost(string ip, string port)
        {
			return Task.Run(() =>
			{
				try
				{
					wc = new WebClient();
					wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.supplyLoginData);
					System.Threading.Thread.Sleep(300);
					wc.DownloadFile("http://" + ip + ":" + port + "/" + StringsAndConstants.fileLogin, StringsAndConstants.loginPath);
					System.Threading.Thread.Sleep(300);
					sha1 = wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.fileShaLogin);
					System.Threading.Thread.Sleep(300);
					sha1 = sha1.ToUpper();
					fileL = new StreamReader(StringsAndConstants.loginPath);
					aux = StringsAndConstants.loginPath;
					fileL.Close();
				}
				catch
				{
					return false;
				}
				return true;
			});
		}

        //Checks if the server is answering any requests, through a json file verification (single threaded)
        public static bool checkHostST(string ip, string port)
        {
            try
            {
                wc = new WebClient();
                wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.supplyLoginData);
                System.Threading.Thread.Sleep(300);
                wc.DownloadFile("http://" + ip + ":" + port + "/" + StringsAndConstants.fileLogin, StringsAndConstants.loginPath);
                System.Threading.Thread.Sleep(300);
                sha1 = wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.fileShaLogin);
                System.Threading.Thread.Sleep(300);
                sha1 = sha1.ToUpper();
                fileL = new StreamReader(StringsAndConstants.loginPath);
                aux = StringsAndConstants.loginPath;
                fileL.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them (creates a separate thread)
		public static Task<string[]> fetchInfo(string nome, string senha, string ip, string port)
		{
			return Task.Run(async () =>
			{
				if (!await checkHost(ip, port))
					return null;

				string[] arr;
				fileL = new StreamReader(StringsAndConstants.loginPath);
				if (MiscMethods.GetSha1Hash(aux).Equals(sha1))
				{
					jsonFile = fileL.ReadToEnd();
					lFile[] jsonParse = JsonConvert.DeserializeObject<lFile[]>(@jsonFile);

					for (int i = 0; i < jsonParse.Length; i++)
					{
						if (nome.Equals(jsonParse[i].usuario) && MiscMethods.HashMd5Generator(senha).Equals(jsonParse[i].senha))
						{
							arr = new string[] { "true", jsonParse[i].usuario };
							fileL.Close();
							return arr;
						}
					}
				}
				arr = new string[] { "false" }; ;
				fileL.Close();
				return arr;
			});
		}

        //Reads a json file retrieved from the server and parses username and encoded password, returning them  (single threaded)
        public static string[] fetchInfoST(string nome, string senha, string ip, string port)
        {
            if (!checkHostST(ip, port))
                return null;

            string[] arr;
            fileL = new StreamReader(StringsAndConstants.loginPath);
            if (MiscMethods.GetSha1Hash(aux).Equals(sha1))
            {
                jsonFile = fileL.ReadToEnd();
                lFile[] jsonParse = JsonConvert.DeserializeObject<lFile[]>(@jsonFile);

                for (int i = 0; i < jsonParse.Length; i++)
                {
                    if (nome.Equals(jsonParse[i].usuario) && MiscMethods.HashMd5Generator(senha).Equals(jsonParse[i].senha))
                    {
                        arr = new string[] { "true", jsonParse[i].usuario };
                        fileL.Close();
                        return arr;
                    }
                }
            }
            arr = new string[] { "false" }; ;
            fileL.Close();
            return arr;
        }
    }
}
