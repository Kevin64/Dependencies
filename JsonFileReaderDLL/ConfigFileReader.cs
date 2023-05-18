using ConstantsDLL;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
    public class CFile
    {
        public Definitions Definitions { get; set; }
    }

    public class Definitions
    {
        public string[] Buildings { get; set; }
        public string[] HardwareTypes { get; set; }
        public string[] FirmwareTypes { get; set; }
        public string[] TpmTypes { get; set; }
        public string[] MediaOperationTypes { get; set; }
        public string[] SecureBootStates { get; set; }
        public string[] VirtualizationTechnologyStates { get; set; }
    }

    public static class ConfigFileReader
    {
        private static string jsonFile;
        private static WebClient wc;
        private static StreamReader fileC;

        //Checks if the server is answering any requests, through a json file verification (creates a separate thread)
        public static Task<bool> CheckHostMT(string ipAddress, string port)
        {
            return Task.Run(() =>
            {
                try
                {
                    wc = new WebClient();
                    wc.DownloadFile(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.FILE_CONFIG, StringsAndConstants.CONFIG_FILE_PATH);
                    System.Threading.Thread.Sleep(300);
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
                wc.DownloadFile(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + ConstantsDLL.Properties.Resources.FILE_CONFIG_PATH + ConstantsDLL.Properties.Resources.FILE_CONFIG, StringsAndConstants.CONFIG_FILE_PATH);
                System.Threading.Thread.Sleep(300);
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them (creates a separate thread)
        public static Task<List<string[]>> FetchInfoMT(string ipAddress, string port)
        {
            return Task.Run(async () =>
            {
                if (!await CheckHostMT(ipAddress, port))
                {
                    return null;
                }

                List<string[]> arr;
                fileC = new StreamReader(StringsAndConstants.CONFIG_FILE_PATH);

                jsonFile = fileC.ReadToEnd();
                CFile jsonParse = JsonConvert.DeserializeObject<CFile>(@jsonFile);

                arr = new List<string[]>() { jsonParse.Definitions.Buildings, jsonParse.Definitions.HardwareTypes, jsonParse.Definitions.FirmwareTypes, jsonParse.Definitions.TpmTypes, jsonParse.Definitions.MediaOperationTypes, jsonParse.Definitions.SecureBootStates, jsonParse.Definitions.VirtualizationTechnologyStates };

                fileC.Close();
                return arr;
            });
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them  (single threaded)
        public static List<string[]> FetchInfoST(string ipAddress, string port)
        {
            if (!CheckHostST(ipAddress, port))
            {
                return null;
            }

            List<string[]> arr;
            fileC = new StreamReader(StringsAndConstants.CONFIG_FILE_PATH);

            jsonFile = fileC.ReadToEnd();
            CFile jsonParse = JsonConvert.DeserializeObject<CFile>(@jsonFile);

            arr = new List<string[]>() { jsonParse.Definitions.Buildings, jsonParse.Definitions.HardwareTypes, jsonParse.Definitions.FirmwareTypes, jsonParse.Definitions.TpmTypes, jsonParse.Definitions.MediaOperationTypes, jsonParse.Definitions.SecureBootStates, jsonParse.Definitions.VirtualizationTechnologyStates };

            fileC.Close();
            return arr;
        }

        public static List<string[]> GetOfflineModeConfigFile()
        {
            List<string[]> arr;
            fileC = new StreamReader(ConstantsDLL.Properties.Resources.OFFLINE_MODE_CONFIG);

            jsonFile = fileC.ReadToEnd();
            CFile jsonParse = JsonConvert.DeserializeObject<CFile>(@jsonFile);

            arr = new List<string[]>() { jsonParse.Definitions.Buildings, jsonParse.Definitions.HardwareTypes, jsonParse.Definitions.FirmwareTypes, jsonParse.Definitions.TpmTypes, jsonParse.Definitions.MediaOperationTypes, jsonParse.Definitions.SecureBootStates, jsonParse.Definitions.VirtualizationTechnologyStates };

            fileC.Close();
            return arr;
        }
    }
}
