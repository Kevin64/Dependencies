using ConstantsDLL.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApiDLL
{
    /// <summary>
    /// Class for ServerParam
    /// </summary>
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
        public string HostnamePattern { get; set; }
        public int AssetNumberDigitLimit { get; set; }
        public int SealNumberDigitLimit { get; set; }
        public int RoomNumberDigitLimit { get; set; }
        public int TicketNumberDigitLimit { get; set; }
    }

    /// <summary> 
    /// Class for handling parameters through a REST API
    /// </summary>
    public static class ParameterHandler
    {
        private static string jsonOfflineFile;
        private static StreamReader fileC;

        /// <summary>
        /// Gets server parameters via REST
        /// </summary>
        /// <param name="client">HTTP client object</param>
        /// <param name="path">Uri path</param>
        /// <returns>An object containing all server parameters</returns>
        /// <exception cref="HttpRequestException">Server not found</exception>
        /// <exception cref="InvalidAgentException">Unauthorized agent</exception>
        /// <exception cref="InvalidRestApiCallException">Rest call unsuccessful</exception>
        public static async Task<ServerParam> GetParameterAsync(HttpClient client, string path)
        {
            try
            {
                ServerParam sp = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                    sp = await response.Content.ReadAsAsync<ServerParam>();
                else if (Convert.ToInt32(response.StatusCode).Equals(401) || Convert.ToInt32(response.StatusCode).Equals(400))
                    throw new InvalidAgentException();
                else if (Convert.ToInt32(response.StatusCode).Equals(404))
                    throw new InvalidRestApiCallException();
                return sp;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }
        }

        /// <summary> 
        /// Reads a json file retrieved from the application folder and parses standard building list, hardware types, firmware types, TPM types, Media operation types, secure boot states and virtualization technology states, returning them (single threaded)
        /// </summary>
        /// <returns>An array with all data fetched.</returns>
        public static ServerParam GetOfflineModeConfigFile()
        {
            fileC = new StreamReader(GenericResources.OFFLINE_MODE_PARAMETER_FILE);
            jsonOfflineFile = fileC.ReadToEnd();
            ServerParam jsonParse = JsonConvert.DeserializeObject<ServerParam>(@jsonOfflineFile);

            fileC.Close();
            return jsonParse;
        }
    }
}
