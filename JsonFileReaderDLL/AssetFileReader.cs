using ConstantsDLL;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
    public class AFile
    {
        public string AssetNumber { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }
        public string Standard { get; set; }
        public string AdRegistered { get; set; }
        public string InUse { get; set; }
        public string SealNumber { get; set; }
        public string Tag { get; set; }
        public string HwType { get; set; }
        public string Discarded { get; set; }
        public string ServiceDate { get; set; }
    }
    public static class AssetFileReader
    {
        private static string jsonFile, sha256, aux;
        private static WebClient wc;
        private static StreamReader filePC;

        //Checks if the server is answering any requests, through a json file verification (creates a separate thread)
        public static Task<bool> CheckHostMT(string ipAddress, string port, string assetNumber)
        {
            return Task.Run(() =>
            {
                try
                {
                    wc = new WebClient();
                    _ = wc.DownloadString("http://" + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.supplyPCData + ConstantsDLL.Properties.Resources.phpAssetNumber + assetNumber);
                    System.Threading.Thread.Sleep(300);
                    wc.DownloadFile("http://" + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.jsonServerPath + ConstantsDLL.Properties.Resources.filePC, StringsAndConstants.assetFilePath);
                    System.Threading.Thread.Sleep(300);
                    sha256 = wc.DownloadString("http://" + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.jsonServerPath + ConstantsDLL.Properties.Resources.fileShaPC);
                    System.Threading.Thread.Sleep(300);
                    sha256 = sha256.ToUpper();
                    aux = StringsAndConstants.assetFilePath;
                }
                catch
                {
                    return false;
                }
                return true;
            });
        }

        //Checks if the server is answering any requests, through a json file verification (single threaded)
        public static bool CheckHostST(string ipAddress, string port, string assetNumber)
        {
            try
            {
                wc = new WebClient();
                _ = wc.DownloadString("http://" + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.supplyPCData + ConstantsDLL.Properties.Resources.phpAssetNumber + assetNumber);
                System.Threading.Thread.Sleep(300);
                wc.DownloadFile("http://" + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.jsonServerPath + ConstantsDLL.Properties.Resources.filePC, StringsAndConstants.assetFilePath);
                System.Threading.Thread.Sleep(300);
                sha256 = wc.DownloadString("http://" + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.jsonServerPath + ConstantsDLL.Properties.Resources.fileShaPC);
                System.Threading.Thread.Sleep(300);
                sha256 = sha256.ToUpper();
                aux = StringsAndConstants.assetFilePath;
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them (creates a separate thread)
        public static Task<string[]> FetchInfoMT(string assetNumber, string ipAddress, string port)
        {
            return Task.Run(async () =>
            {
                if (!await CheckHostMT(ipAddress, port, assetNumber))
                {
                    return null;
                }

                string[] arr;
                filePC = new StreamReader(StringsAndConstants.assetFilePath);
                if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
                {
                    jsonFile = filePC.ReadToEnd();
                    AFile[] jsonParse = JsonConvert.DeserializeObject<AFile[]>(@jsonFile);

                    for (int i = 0; i < jsonParse.Length; i++)
                    {
                        if (assetNumber.Equals(jsonParse[i].AssetNumber))
                        {
                            arr = new string[] { jsonParse[i].AssetNumber, jsonParse[i].Building, jsonParse[i].Room, jsonParse[i].Standard, jsonParse[i].AdRegistered, jsonParse[i].InUse, jsonParse[i].SealNumber, jsonParse[i].Tag, jsonParse[i].HwType, jsonParse[i].Discarded, jsonParse[i].ServiceDate };
                            filePC.Close();
                            return arr;
                        }
                    }
                }
                arr = new string[] { "false" }; ;
                filePC.Close();
                return arr;
            });
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them  (single threaded)
        public static string[] FetchInfoST(string assetNumber, string ipAddress, string port)
        {
            if (!CheckHostST(ipAddress, port, assetNumber))
            {
                return null;
            }

            string[] arr;
            filePC = new StreamReader(StringsAndConstants.assetFilePath);
            if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
            {
                jsonFile = filePC.ReadToEnd();
                AFile[] jsonParse = JsonConvert.DeserializeObject<AFile[]>(@jsonFile);

                for (int i = 0; i < jsonParse.Length; i++)
                {
                    if (assetNumber.Equals(jsonParse[i].AssetNumber))
                    {
                        arr = new string[] { jsonParse[i].AssetNumber, jsonParse[i].Building, jsonParse[i].Room, jsonParse[i].Standard, jsonParse[i].AdRegistered, jsonParse[i].InUse, jsonParse[i].SealNumber, jsonParse[i].Tag, jsonParse[i].HwType, jsonParse[i].Discarded, jsonParse[i].ServiceDate };
                        filePC.Close();
                        return arr;
                    }
                }
            }
            arr = new string[] { "false" }; ;
            filePC.Close();
            return arr;
        }
    }
}
