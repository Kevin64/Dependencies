using ConstantsDLL.Properties;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
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
        private static string jsonFile;
        private static StreamReader fileC;

        public static async Task<ServerParam> GetParameterAsync(HttpClient client, string path)
        {
            ServerParam sp = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
                sp = await response.Content.ReadAsAsync<ServerParam>();
            return sp;
        }

        /// <summary> 
        /// Reads a json file retrieved from the application folder and parses standard building list, hardware types, firmware types, TPM types, Media operation types, secure boot states and virtualization technology states, returning them (single threaded)
        /// </summary>
        /// <returns>An array with all data fetched.</returns>
        public static ServerParam GetOfflineModeConfigFile()
        {
            fileC = new StreamReader(Resources.OFFLINE_MODE_PARAMETER_FILE);
            jsonFile = fileC.ReadToEnd();
            ServerParam jsonParse = JsonConvert.DeserializeObject<ServerParam>(@jsonFile);

            fileC.Close();
            return jsonParse;
        }
    }
}
