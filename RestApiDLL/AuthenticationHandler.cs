﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApiDLL
{
    public enum PrivilegeLevel
    {
        ADMINISTRATOR,
        STANDARD,
        LIMITED
    }
    /// <summary> 
    /// Template class for 'Credentials'
    /// </summary>
    public class Agent
    {
        public string id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }
        public string privilegeLevel { get; set; }
        public string lastLoginDate { get; set; }
    }

    /// <summary> 
    /// Class for handling authentication through a REST API
    /// </summary>
    public static class AuthenticationHandler
    {
        /// <summary>
        /// Gets Agent data via REST
        /// </summary>
        /// <param name="client">HTTP client object</param>
        /// <param name="path">Uri path</param>
        /// <returns>An Agent object</returns>
        /// <exception cref="HttpRequestException">Server not found</exception>
        /// <exception cref="InvalidAgentException">Unauthorized agent</exception>
        /// <exception cref="InvalidRestApiCallException">Rest call unsuccessful</exception>
        public static async Task<Agent> GetAgentAsync(HttpClient client, string path)
        {
            try
            {
                Agent a = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                    a = await response.Content.ReadAsAsync<Agent>();
                else if (Convert.ToInt32(response.StatusCode).Equals(401) || Convert.ToInt32(response.StatusCode).Equals(400))
                    throw new InvalidAgentException();
                else if (Convert.ToInt32(response.StatusCode).Equals(404))
                    throw new InvalidRestApiCallException();
                return a;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }
        }
    }
}
