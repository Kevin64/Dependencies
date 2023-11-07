using ConstantsDLL.Properties;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApiDLL
{
    /// <summary> 
    /// Template class for 'Model'
    /// </summary>
    public class Model
    {
        public string brand { get; set; }
        public string model { get; set; }
        public string fwVersion { get; set; }
        public string fwType { get; set; }
        public string tpmVersion { get; set; }
        public string mediaOperationMode { get; set; }
    }

    /// <summary> 
    /// Class for handling a model through a REST API
    /// </summary>
    public static class ModelHandler
    {
        /// <summary>
        /// Checks if host is alive
        /// </summary>
        /// <param name="client">HTTP client object</param>
        /// <param name="path">Uri path</param>
        /// <returns>True if is alive, False otherwise</returns>
        public static async Task<bool> CheckHost(HttpClient client, string path)
        {
            HttpResponseMessage result;
            try
            {
                HttpRequestMessage response = new HttpRequestMessage
                {
                    RequestUri = new Uri(path),
                    Method = HttpMethod.Head
                };
                result = await client.SendAsync(response);
            }
            catch (HttpRequestException)
            {
                return false;
            }
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Gets Model data via REST
        /// </summary>
        /// <param name="client">HTTP client object</param>
        /// <param name="path">Uri path</param>
        /// <returns>A Model object</returns>
        /// <exception cref="HttpRequestException">Server not found</exception>
        /// <exception cref="InvalidModelException">Model not found</exception>
        public static async Task<Model> GetModelAsync(HttpClient client, string path)
        {
            try
            {
                Model m = null;
                path = path.Replace(" ", "-");
                HttpResponseMessage response = await client.GetAsync(path);
                if (Convert.ToInt32(response.StatusCode).Equals(200))
                    m = await response.Content.ReadAsAsync<Model>();
                else if (Convert.ToInt32(response.StatusCode).Equals(204))
                    m = null;
                else if (Convert.ToInt32(response.StatusCode).Equals(401) || Convert.ToInt32(response.StatusCode).Equals(400))
                    throw new InvalidAgentException();
                else if (Convert.ToInt32(response.StatusCode).Equals(404))
                    throw new InvalidRestApiCallException();
                return m;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }
        }
    }
}
