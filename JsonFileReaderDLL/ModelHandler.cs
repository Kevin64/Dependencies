using System.Net.Http;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
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
        public static bool CheckHost(HttpClient client, string path)
        {
            return true;
        }

        public static async Task<Model> GetModelAsync(HttpClient client, string path)
        {
            Model m = null;
            path = path.Replace(" ", "-");
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
                m = await response.Content.ReadAsAsync<Model>();
            return m;
        }
    }
}
