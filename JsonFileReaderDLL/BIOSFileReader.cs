using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using ConstantsDLL;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
	public class jFile
	{
		public string id { get; set; }
		public string marca { get; set; }
		public string modelo { get; set; }
		public string versao { get; set; }
		public string tipo { get; set; }
        public string tpm { get; set; }
        public string mediaOp { get; set; }
    }
	public static class BIOSFileReader
	{
		private static string jsonFile, sha1, aux;
		private static WebClient wc;
		private static StreamReader fileB;

		public static Task<bool> checkHost(string ip, string port)
        {
			return Task.Run(() =>
			{
				try
				{
					wc = new WebClient();
					wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.supplyBiosData);
					System.Threading.Thread.Sleep(300);
					wc.DownloadFile("http://" + ip + ":" + port + "/" + StringsAndConstants.fileBios, StringsAndConstants.biosPath);
					System.Threading.Thread.Sleep(300);
					sha1 = wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.fileShaBios);
					System.Threading.Thread.Sleep(300);
					sha1 = sha1.ToUpper();
					fileB = new StreamReader(StringsAndConstants.biosPath);
					aux = StringsAndConstants.biosPath;
					fileB.Close();
				}
				catch
				{
					return false;
				}
				return true;
			});
		}

		//Reads a json file retrieved from the server and parses brand, model and BIOS versions, returning the latter
		[STAThread]
		public static Task<string[]> fetchInfo(string brd, string mod, string type, string tpm, string mediaOp, string ip, string port)
		{
			return Task.Run(async () =>
			{
				if (!await checkHost(ip, port))
					return null;

				string[] arr;
				string typeRet = "true", tpmRet = "true", mediaOpRet = "true";
				fileB = new StreamReader(StringsAndConstants.biosPath);
				if (MiscMethods.GetSha1Hash(aux).Equals(sha1))
				{
					jsonFile = fileB.ReadToEnd();
					jFile[] jsonParse = JsonConvert.DeserializeObject<jFile[]>(@jsonFile);

					for (int i = 0; i < jsonParse.Length; i++)
					{
						if (mod.Contains(jsonParse[i].modelo) && brd.Contains(jsonParse[i].marca))
						{
							if (!type.Equals(jsonParse[i].tipo))
								typeRet = "false";
							if (!tpm.Equals(jsonParse[i].tpm))
								tpmRet = "false";
							if (!mediaOp.Equals(jsonParse[i].mediaOp))
								mediaOpRet = "false";
							arr = new String[] { jsonParse[i].versao, typeRet, tpmRet, mediaOpRet };
							fileB.Close();
							return arr;
						}
					}
				}
				arr = new String[] { "-1", "-1" };
				fileB.Close();
				return arr;
			});
		}
	}
}
