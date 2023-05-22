using ConstantsDLL;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
    public class MFile
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string FwVersion { get; set; }
        public string FwType { get; set; }
        public string TpmVersion { get; set; }
        public string MediaOperationMode { get; set; }
    }
    public static class ModelFileReader
    {
        private static string jsonFile, sha256, aux;
        private static WebClient wc;
        private static StreamReader fileB;

        //Checks if the server is answering any requests, through a json file verification (creates a separate thread)
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

        //Checks if the server is answering any requests, through a json file verification (single threaded)
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

        //Reads a json file retrieved from the server and parses brand, model, BIOS versions, operatin mode and TPM version, returning them (creates a separate thread)
        public static Task<string[]> FetchInfoMT(string brand, string model, string fwType, string tpmVersion, string mediaOperationMode, string ipAddress, string port)
        {
            return Task.Run(async () =>
            {
                if (!await CheckHostMT(ipAddress, port))
                {
                    return null;
                }

                string[] arr;
                string typeRet = ConstantsDLL.Properties.Resources.TRUE, tpmRet = ConstantsDLL.Properties.Resources.TRUE, mediaOpRet = ConstantsDLL.Properties.Resources.TRUE;
                fileB = new StreamReader(StringsAndConstants.MODEL_FILE_PATH);
                if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
                {
                    jsonFile = fileB.ReadToEnd();
                    MFile[] jsonParse = JsonConvert.DeserializeObject<MFile[]>(@jsonFile);

                    for (int i = 0; i < jsonParse.Length; i++)
                    {
                        if (model.Contains(jsonParse[i].Model) && brand.Contains(jsonParse[i].Brand))
                        {
                            if (!fwType.Equals(jsonParse[i].FwType))
                            {
                                typeRet = ConstantsDLL.Properties.Resources.FALSE;
                            }

                            if (!tpmVersion.Equals(jsonParse[i].TpmVersion))
                            {
                                tpmRet = ConstantsDLL.Properties.Resources.FALSE;
                            }

                            if (!mediaOperationMode.Equals(jsonParse[i].MediaOperationMode))
                            {
                                mediaOpRet = ConstantsDLL.Properties.Resources.FALSE;
                            }

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

        //Reads a json file retrieved from the server and parses brand, model, BIOS versions, operatin mode and TPM version, returning them (single threaded)
        public static string[] FetchInfoST(string brand, string model, string fwType, string tpmVersion, string mediaOperationMode, string ipAddress, string port)
        {
            if (!CheckHostST(ipAddress, port))
            {
                return null;
            }

            string[] arr;
            string typeRet = ConstantsDLL.Properties.Resources.TRUE, tpmRet = ConstantsDLL.Properties.Resources.TRUE, mediaOpRet = ConstantsDLL.Properties.Resources.TRUE;
            fileB = new StreamReader(StringsAndConstants.MODEL_FILE_PATH);
            if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
            {
                jsonFile = fileB.ReadToEnd();
                MFile[] jsonParse = JsonConvert.DeserializeObject<MFile[]>(@jsonFile);

                for (int i = 0; i < jsonParse.Length; i++)
                {
                    if (model.Contains(jsonParse[i].Model) && brand.Contains(jsonParse[i].Brand))
                    {
                        if (!fwType.Equals(jsonParse[i].FwType))
                        {
                            typeRet = ConstantsDLL.Properties.Resources.FALSE;
                        }

                        if (!tpmVersion.Equals(jsonParse[i].TpmVersion))
                        {
                            tpmRet = ConstantsDLL.Properties.Resources.FALSE;
                        }

                        if (!mediaOperationMode.Equals(jsonParse[i].MediaOperationMode))
                        {
                            mediaOpRet = ConstantsDLL.Properties.Resources.FALSE;
                        }

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
