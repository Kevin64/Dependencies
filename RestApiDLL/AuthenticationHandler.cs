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
        public static async Task<Agent> GetAgentAsync(HttpClient client, string path)
        {
            Agent a = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                    a = await response.Content.ReadAsAsync<Agent>();
                if (a == null)
                {
                    a = new Agent
                    {
                        username = string.Empty
                    };
                }
            }
            catch (HttpRequestException)
            {
                a = null;
            }
            return a;
        }
    }
}
