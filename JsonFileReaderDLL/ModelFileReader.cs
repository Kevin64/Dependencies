using ConstantsDLL;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
    /// <summary> 
    /// Template class for 'Model'
    /// </summary>
    public class ModelFile
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string FwVersion { get; set; }
        public string FwType { get; set; }
        public string TpmVersion { get; set; }
        public string MediaOperationMode { get; set; }
    }

    /// <summary> 
    /// Class for handling a 'Model' json file
    /// </summary>
    public static class ModelFileReader
    {
        private static string jsonFile, sha256, aux;
        private static WebClient wc;
        private static StreamReader fileB;

        /// <summary> 
        /// 
        ///Checks if the server is answering any requests, through a json file verification (creates a separate thread)
        ///
        /// </summary>
        /// <param name="ipAddress">Server IP address</param>
        /// <param name="port">Server port</param>
        /// <returns>If server is reachable and sends a json file, returns true. If not, returns false.</returns>
        public static Task<bool> CheckHostMT(string ipAddress, string port)
        {
            return Task.Run(() =>
            {
                try
                {
                    wc = new WebClient();
                    _ = wc.DownloadString(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.SUPPLY_MODEL_DATA);
                    System.Threading.Thread.Sleep(300);
                    wc.DownloadFile(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.JSON_SERVER_PATH + ConstantsDLL.Properties.Resources.FILE_MODEL, StringsAndConstants.MODEL_FILE_PATH);
                    System.Threading.Thread.Sleep(300);
                    sha256 = wc.DownloadString(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.JSON_SERVER_PATH + ConstantsDLL.Properties.Resources.FILE_SHA_MODEL);
                    System.Threading.Thread.Sleep(300);
                    sha256 = sha256.ToUpper();
                    aux = StringsAndConstants.MODEL_FILE_PATH;
                }
                catch
                {
                    return false;
                }
                return true;
            });
        }

        /// <summary> 
        /// 
        ///Checks if the server is answering any requests, through a json file verification (single threaded)
        ///
        /// </summary>
        /// <param name="ipAddress">Server IP address</param>
        /// <param name="port">Server port</param>
        /// <returns>If server is reachable and sends a json file, returns true. If not, returns false.</returns>
        public static bool CheckHostST(string ipAddress, string port)
        {
            try
            {
                wc = new WebClient();
                _ = wc.DownloadString(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.SUPPLY_MODEL_DATA);
                System.Threading.Thread.Sleep(300);
                wc.DownloadFile(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.JSON_SERVER_PATH + ConstantsDLL.Properties.Resources.FILE_MODEL, StringsAndConstants.MODEL_FILE_PATH);
                System.Threading.Thread.Sleep(300);
                sha256 = wc.DownloadString(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.JSON_SERVER_PATH + ConstantsDLL.Properties.Resources.FILE_SHA_MODEL);
                System.Threading.Thread.Sleep(300);
                sha256 = sha256.ToUpper();
                aux = StringsAndConstants.MODEL_FILE_PATH;
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary> 
        /// 
        ///Reads a json file retrieved from the server and parses brand, model, firmware version, operation mode and TPM version, returning them (creates a separate thread)
        ///
        /// </summary>
        /// <param name="brand">Asset brand</param>
        /// <param name="model">Asset model</param>
        /// <param name="fwType">Firmware type</param>
        /// <param name="tpmVersion">TPM version</param>
        /// <param name="mediaOperationMode">Media operation mode</param>
        /// <param name="ipAddress">Server IP address</param>
        /// <param name="port">Server port</param>
        /// <returns>If user exists on the fetched json file, returns a string array with the username and respective id. If not, returns a single position array with a "false" string.</returns>
        public static Task<string[]> FetchInfoMT(string brand, string model, string fwType, string tpmVersion, string mediaOperationMode, string ipAddress, string port)
        {
            return Task.Run(async () =>
            {
                if (!await CheckHostMT(ipAddress, port))
                    return null;

                string[] arr;
                string typeRet = ConstantsDLL.Properties.Resources.TRUE, tpmRet = ConstantsDLL.Properties.Resources.TRUE, mediaOpRet = ConstantsDLL.Properties.Resources.TRUE;
                fileB = new StreamReader(StringsAndConstants.MODEL_FILE_PATH);
                if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
                {
                    jsonFile = fileB.ReadToEnd();
                    ModelFile[] jsonParse = JsonConvert.DeserializeObject<ModelFile[]>(@jsonFile);

                    for (int i = 0; i < jsonParse.Length; i++)
                    {
                        if (model.Contains(jsonParse[i].Model) && brand.Contains(jsonParse[i].Brand))
                        {
                            if (!fwType.Equals(jsonParse[i].FwType))
                                typeRet = ConstantsDLL.Properties.Resources.FALSE;
                            if (!tpmVersion.Equals(jsonParse[i].TpmVersion))
                                tpmRet = ConstantsDLL.Properties.Resources.FALSE;
                            if (!mediaOperationMode.Equals(jsonParse[i].MediaOperationMode))
                                mediaOpRet = ConstantsDLL.Properties.Resources.FALSE;
                            arr = new string[] { jsonParse[i].FwVersion, typeRet, tpmRet, mediaOpRet };
                            fileB.Close();
                            return arr;
                        }
                    }
                }
                arr = new string[] { "-1", "-1", "-1", "-1" };
                fileB.Close();
                return arr;
            });
        }

        /// <summary> 
        /// 
        ///Reads a json file retrieved from the server and parses brand, model, firmware version, operation mode and TPM version, returning them (single threaded)
        ///
        /// </summary>
        /// <param name="brand">Asset brand</param>
        /// <param name="model">Asset model</param>
        /// <param name="fwType">Firmware type</param>
        /// <param name="tpmVersion">TPM version</param>
        /// <param name="mediaOperationMode">Media operation mode</param>
        /// <param name="ipAddress">Server IP address</param>
        /// <param name="port">Server port</param>
        /// <returns>If user exists on the fetched json file, returns a string array with the username and respective id. If not, returns a single position array with a "false" string.</returns>
        public static string[] FetchInfoST(string brand, string model, string fwType, string tpmVersion, string mediaOperationMode, string ipAddress, string port)
        {
            if (!CheckHostST(ipAddress, port))
                return null;

            string[] arr;
            string typeRet = ConstantsDLL.Properties.Resources.TRUE, tpmRet = ConstantsDLL.Properties.Resources.TRUE, mediaOpRet = ConstantsDLL.Properties.Resources.TRUE;
            fileB = new StreamReader(StringsAndConstants.MODEL_FILE_PATH);
            if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
            {
                jsonFile = fileB.ReadToEnd();
                ModelFile[] jsonParse = JsonConvert.DeserializeObject<ModelFile[]>(@jsonFile);

                for (int i = 0; i < jsonParse.Length; i++)
                {
                    if (model.Contains(jsonParse[i].Model) && brand.Contains(jsonParse[i].Brand))
                    {
                        if (!fwType.Equals(jsonParse[i].FwType))
                            typeRet = ConstantsDLL.Properties.Resources.FALSE;
                        if (!tpmVersion.Equals(jsonParse[i].TpmVersion))
                            tpmRet = ConstantsDLL.Properties.Resources.FALSE;
                        if (!mediaOperationMode.Equals(jsonParse[i].MediaOperationMode))
                            mediaOpRet = ConstantsDLL.Properties.Resources.FALSE;
                        arr = new string[] { jsonParse[i].FwVersion, typeRet, tpmRet, mediaOpRet };
                        fileB.Close();
                        return arr;
                    }
                }
            }
            arr = new string[] { "-1", "-1", "-1", "-1" };
            fileB.Close();
            return arr;
        }
    }
}
