using ConstantsDLL;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
    /// <summary> 
    /// Template class for 'Parameters'
    /// </summary>
    public class ConfigFile
    {
        public Parameters Parameters { get; set; }
    }

    /// <summary> 
    /// Template class for 'Parameters'
    /// </summary>
    public class Parameters
    {
        public string[] Buildings { get; set; }
        public string[] HardwareTypes { get; set; }
        public string[] FirmwareTypes { get; set; }
        public string[] TpmTypes { get; set; }
        public string[] MediaOperationTypes { get; set; }
        public string[] SecureBootStates { get; set; }
        public string[] VirtualizationTechnologyStates { get; set; }
    }

    /// <summary> 
    /// Class for handling a 'Config' json file
    /// </summary>
    public static class ConfigFileReader
    {
        private static string jsonFile;
        private static WebClient wc;
        private static StreamReader fileC;

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
                wc.DownloadFile(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + ConstantsDLL.Properties.Resources.FILE_CONFIG_PATH + ConstantsDLL.Properties.Resources.FILE_CONFIG, StringsAndConstants.CONFIG_FILE_PATH);
                System.Threading.Thread.Sleep(300);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary> 
        /// 
        ///Reads a json file retrieved from the server and parses standard building list, hardware types, firmware types, TPM types, Media operation types, secure boot states and virtualization technology states, returning them (creates a separate thread)
        ///
        /// </summary>
        /// <param name="ipAddress">Server IP address</param>
        /// <param name="port">Server port</param>
        /// <returns>An array with all data fetched.</returns>
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
                ConfigFile jsonParse = JsonConvert.DeserializeObject<ConfigFile>(@jsonFile);

                arr = new List<string[]>() { jsonParse.Parameters.Buildings, jsonParse.Parameters.HardwareTypes, jsonParse.Parameters.FirmwareTypes, jsonParse.Parameters.TpmTypes, jsonParse.Parameters.MediaOperationTypes, jsonParse.Parameters.SecureBootStates, jsonParse.Parameters.VirtualizationTechnologyStates };

                fileC.Close();
                return arr;
            });
        }

        /// <summary> 
        /// 
        ///Reads a json file retrieved from the server and parses standard building list, hardware types, firmware types, TPM types, Media operation types, secure boot states and virtualization technology states, returning them (single threaded)
        ///
        /// </summary>
        /// <param name="ipAddress">Server IP address</param>
        /// <param name="port">Server port</param>
        /// <returns>An array with all data fetched.</returns>
        public static List<string[]> FetchInfoST(string ipAddress, string port)
        {
            if (!CheckHostST(ipAddress, port))
            {
                return null;
            }

            List<string[]> arr;
            fileC = new StreamReader(StringsAndConstants.CONFIG_FILE_PATH);

            jsonFile = fileC.ReadToEnd();
            ConfigFile jsonParse = JsonConvert.DeserializeObject<ConfigFile>(@jsonFile);

            arr = new List<string[]>() { jsonParse.Parameters.Buildings, jsonParse.Parameters.HardwareTypes, jsonParse.Parameters.FirmwareTypes, jsonParse.Parameters.TpmTypes, jsonParse.Parameters.MediaOperationTypes, jsonParse.Parameters.SecureBootStates, jsonParse.Parameters.VirtualizationTechnologyStates };

            fileC.Close();
            return arr;
        }

        /// <summary> 
        /// 
        ///Reads a json file retrieved from the application folder and parses standard building list, hardware types, firmware types, TPM types, Media operation types, secure boot states and virtualization technology states, returning them (single threaded)
        ///
        /// </summary>
        /// <returns>An array with all data fetched.</returns>
        public static List<string[]> GetOfflineModeConfigFile()
        {
            List<string[]> arr;
            fileC = new StreamReader(ConstantsDLL.Properties.Resources.OFFLINE_MODE_CONFIG);

            jsonFile = fileC.ReadToEnd();
            ConfigFile jsonParse = JsonConvert.DeserializeObject<ConfigFile>(@jsonFile);

            arr = new List<string[]>() { jsonParse.Parameters.Buildings, jsonParse.Parameters.HardwareTypes, jsonParse.Parameters.FirmwareTypes, jsonParse.Parameters.TpmTypes, jsonParse.Parameters.MediaOperationTypes, jsonParse.Parameters.SecureBootStates, jsonParse.Parameters.VirtualizationTechnologyStates };

            fileC.Close();
            return arr;
        }
    }
}
