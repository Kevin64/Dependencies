using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApiDLL
{
    public class ServerParam
    {
        public Parameters Parameters { get; set; }
    }

    /// <summary> 
    /// Template class for 'Parameters'
    /// </summary>
    public class Parameters
    {
        public List<string> Buildings { get; set; }
        public List<string> HardwareTypes { get; set; }
        public List<string> FirmwareTypes { get; set; }
        public List<string> TpmTypes { get; set; }
        public List<string> MediaOperationTypes { get; set; }
        public List<string> RamTypes { get; set; }
        public List<string> SecureBootStates { get; set; }
        public List<string> VirtualizationTechnologyStates { get; set; }
    }

    /// <summary> 
    /// Class for handling a 'Config' json file
    /// </summary>
    public static class ParameterHandler
    {
        public static async Task<ServerParam> GetParameterAsync(HttpClient client, string path)
        {
            ServerParam sp = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
                sp = await response.Content.ReadAsAsync<ServerParam>();
            return sp;
        }

        /// <summary> 
        /// 
        ///Reads a json file retrieved from the application folder and parses standard building list, hardware types, firmware types, TPM types, Media operation types, secure boot states and virtualization technology states, returning them (single threaded)
        ///
        /// </summary>
        /// <returns>An array with all data fetched.</returns>
        //public static List<string[]> GetOfflineModeConfigFile()
        //{
        //    List<string[]> arr;
        //    fileC = new StreamReader(ConstantsDLL.Properties.Resources.OFFLINE_MODE_CONFIG);

        //    jsonFile = fileC.ReadToEnd();
        //    ConfigFile jsonParse = JsonConvert.DeserializeObject<ConfigFile>(@jsonFile);

        //    arr = new List<string[]>() { jsonParse.Parameters.Buildings, jsonParse.Parameters.HardwareTypes, jsonParse.Parameters.FirmwareTypes, jsonParse.Parameters.TpmTypes, jsonParse.Parameters.MediaOperationTypes, jsonParse.Parameters.SecureBootStates, jsonParse.Parameters.VirtualizationTechnologyStates };

        //    fileC.Close();
        //    return arr;
        //}
    }
}
