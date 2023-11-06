using ConstantsDLL.Properties;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace RestApiDLL
{
    /// <summary> 
    /// Class for miscelaneous methods
    /// </summary>
    public static class MiscMethods
    {
        /// <summary>
        /// Sets the HTTP client used throughout the application 
        /// </summary>
        /// <param name="ip">Server IP</param>
        /// <param name="port">Server Port</param>
        /// <param name="mediaType">Content-Type HTTP header</param>
        /// <returns></returns>
        public static HttpClient SetHttpClient(string ip, string port, string mediaType, string username, string password)
        {

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(GenericResources.HTTP + ip + ":" + port)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password)));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            return client;
        }
    }
}
