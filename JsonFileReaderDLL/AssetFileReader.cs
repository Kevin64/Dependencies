using ConstantsDLL;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
    ///<summary>Template class for 'Asset'</summary>
    public class AssetFile
    {
        public string AssetNumber { get; set; }
        public string Building { get; set; }
        public string RoomNumber { get; set; }
        public string Standard { get; set; }
        public string AdRegistered { get; set; }
        public string InUse { get; set; }
        public string SealNumber { get; set; }
        public string Tag { get; set; }
        public string HwType { get; set; }
        public string Discarded { get; set; }
        public string ServiceDate { get; set; }
    }

    ///<summary>Class for handling a 'Asset' json file</summary>
    public static class AssetFileReader
    {
        private static string jsonFile, sha256, aux;
        private static WebClient wc;
        private static StreamReader fileAsset;

        ///<summary>
        ///Checks if the server is answering any requests, through a json file verification (creates a separate thread)
        ///</summary>
        ///<param name="ipAddress">Server IP address</param>
        ///<param name="port">Server port</param>
        ///<param name="assetNumber">Asset number</param>
        ///<returns>If server is reachable and sends a json file, returns true. If not, returns false.</returns>
        public static Task<bool> CheckHostMT(string ipAddress, string port, string assetNumber)
        {
            return Task.Run(() =>
            {
                try
                {
                    wc = new WebClient();
                    _ = wc.DownloadString(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.SUPPLY_ASSET_DATA + ConstantsDLL.Properties.Resources.PHP_ASSET_NUMBER + assetNumber);
                    System.Threading.Thread.Sleep(300);
                    wc.DownloadFile(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.JSON_SERVER_PATH + ConstantsDLL.Properties.Resources.FILE_ASSET, StringsAndConstants.ASSET_FILE_PATH);
                    System.Threading.Thread.Sleep(300);
                    sha256 = wc.DownloadString(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.JSON_SERVER_PATH + ConstantsDLL.Properties.Resources.FILE_SHA_ASSET);
                    System.Threading.Thread.Sleep(300);
                    sha256 = sha256.ToUpper();
                    aux = StringsAndConstants.ASSET_FILE_PATH;
                }
                catch
                {
                    return false;
                }
                return true;
            });
        }

        ///<summary>
        ///Checks if the server is answering any requests, through a json file verification (single threaded)
        ///</summary>
        ///<param name="ipAddress">Server IP address</param>
        ///<param name="port">Server port</param>
        ///<param name="assetNumber">Asset number</param>
        ///<returns>If server is reachable and sends a json file, returns true. If not, returns false.</returns>
        public static bool CheckHostST(string ipAddress, string port, string assetNumber)
        {
            try
            {
                wc = new WebClient();
                _ = wc.DownloadString(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.SUPPLY_ASSET_DATA + ConstantsDLL.Properties.Resources.PHP_ASSET_NUMBER + assetNumber);
                System.Threading.Thread.Sleep(300);
                wc.DownloadFile(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.JSON_SERVER_PATH + ConstantsDLL.Properties.Resources.FILE_ASSET, StringsAndConstants.ASSET_FILE_PATH);
                System.Threading.Thread.Sleep(300);
                sha256 = wc.DownloadString(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.JSON_SERVER_PATH + ConstantsDLL.Properties.Resources.FILE_SHA_ASSET);
                System.Threading.Thread.Sleep(300);
                sha256 = sha256.ToUpper();
                aux = StringsAndConstants.ASSET_FILE_PATH;
            }
            catch
            {
                return false;
            }
            return true;
        }

        ///<summary>
        ///Reads a json file retrieved from the server and parses username and encoded password, returning them (creates a separate thread)
        ///</summary>
        ///<param name="assetNumber">Asset number</param>
        ///<param name="ipAddress">Server IP address</param>
        ///<param name="port">Server port</param>
        ///<returns>If user exists on the fetched json file, returns a string array with the username and respective id. If not, returns a single position array with a "false" string.</returns>
        public static Task<string[]> FetchInfoMT(string assetNumber, string ipAddress, string port)
        {
            return Task.Run(async () =>
            {
                if (!await CheckHostMT(ipAddress, port, assetNumber))
                {
                    return null;
                }

                string[] arr;
                fileAsset = new StreamReader(StringsAndConstants.ASSET_FILE_PATH);
                if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
                {
                    jsonFile = fileAsset.ReadToEnd();
                    AssetFile[] jsonParse = JsonConvert.DeserializeObject<AssetFile[]>(@jsonFile);

                    for (int i = 0; i < jsonParse.Length; i++)
                    {
                        if (assetNumber.Equals(jsonParse[i].AssetNumber))
                        {
                            arr = new string[] { jsonParse[i].AssetNumber, jsonParse[i].Building, jsonParse[i].RoomNumber, jsonParse[i].Standard, jsonParse[i].AdRegistered, jsonParse[i].InUse, jsonParse[i].SealNumber, jsonParse[i].Tag, jsonParse[i].HwType, jsonParse[i].Discarded, jsonParse[i].ServiceDate };
                            fileAsset.Close();
                            return arr;
                        }
                    }
                }
                arr = new string[] { ConstantsDLL.Properties.Resources.FALSE }; ;
                fileAsset.Close();
                return arr;
            });
        }

        ///<summary>
        ///Reads a json file retrieved from the server and parses username and encoded password, returning them  (single threaded)
        ///</summary>
        ///<param name="assetNumber">Asset number</param>
        ///<param name="ipAddress">Server IP address</param>
        ///<param name="port">Server port</param>
        ///<returns>If user exists on the fetched json file, returns a string array with the username and respective id. If not, returns a single position array with a "false" string.</returns>
        public static string[] FetchInfoST(string assetNumber, string ipAddress, string port)
        {
            if (!CheckHostST(ipAddress, port, assetNumber))
            {
                return null;
            }

            string[] arr;
            fileAsset = new StreamReader(StringsAndConstants.ASSET_FILE_PATH);
            if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
            {
                jsonFile = fileAsset.ReadToEnd();
                AssetFile[] jsonParse = JsonConvert.DeserializeObject<AssetFile[]>(@jsonFile);

                for (int i = 0; i < jsonParse.Length; i++)
                {
                    if (assetNumber.Equals(jsonParse[i].AssetNumber))
                    {
                        arr = new string[] { jsonParse[i].AssetNumber, jsonParse[i].Building, jsonParse[i].RoomNumber, jsonParse[i].Standard, jsonParse[i].AdRegistered, jsonParse[i].InUse, jsonParse[i].SealNumber, jsonParse[i].Tag, jsonParse[i].HwType, jsonParse[i].Discarded, jsonParse[i].ServiceDate };
                        fileAsset.Close();
                        return arr;
                    }
                }
            }
            arr = new string[] { ConstantsDLL.Properties.Resources.FALSE }; ;
            fileAsset.Close();
            return arr;
        }
    }
}
