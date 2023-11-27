using ConstantsDLL.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestApiDLL
{
    /// <summary> 
    /// Template class for 'Asset'
    /// </summary>
    public class Asset
    {
        public string assetNumber { get; set; }
        public string discarded { get; set; }
        public string inUse { get; set; }
        public string note { get; set; }
        public string sealNumber { get; set; }
        public string standard { get; set; }
        public string tag { get; set; }
        public string adRegistered { get; set; }
        public string assetHash { get; set; }
        public string hwHash { get; set; }
        public firmware firmware { get; set; }
        public hardware hardware { get; set; }
        public location location { get; set; }
        public List<maintenances> maintenances { get; set; }
        public network network { get; set; }
        public operatingSystem operatingSystem { get; set; }

        public Asset ShallowCopy()
        {
            return (Asset)MemberwiseClone();
        }
    }

    /// <summary> 
    /// Template class for 'firmware'
    /// </summary>
    public class firmware
    {
        public string mediaOperationMode { get; set; }
        public string secureBoot { get; set; }
        public string tpmVersion { get; set; }
        public string type { get; set; }
        public string version { get; set; }
        public string virtualizationTechnology { get; set; }
    }

    /// <summary> 
    /// Template class for 'hardware'
    /// </summary>
    public class hardware
    {
        public string brand { get; set; }
        public string model { get; set; }
        public string serialNumber { get; set; }
        public string type { get; set; }
        public List<processor> processor { get; set; }
        public List<ram> ram { get; set; }
        public List<storage> storage { get; set; }
        public List<videoCard> videoCard { get; set; }
        public hardware ShallowCopy()
        {
            return (hardware)MemberwiseClone();
        }
    }

    /// <summary> 
    /// Template class for 'processor'
    /// </summary>
    public class processor
    {
        public string processorId { get; set; }
        public string name { get; set; }
        public string frequency { get; set; }
        public string numberOfCores { get; set; }
        public string numberOfThreads { get; set; }
        public string cache { get; set; }
    }

    /// <summary> 
    /// Template class for 'ram'
    /// </summary>
    public class ram
    {
        public string slot { get; set; }
        public string amount { get; set; }
        public string type { get; set; }
        public string frequency { get; set; }
        public string serialNumber { get; set; }
        public string partNumber { get; set; }
        public string manufacturer { get; set; }
    }

    /// <summary> 
    /// Template class for 'storage'
    /// </summary>
    public class storage
    {
        public string storageId { get; set; }
        public string connection { get; set; }
        public string model { get; set; }
        public string serialNumber { get; set; }
        public string size { get; set; }
        public string smartStatus { get; set; }
        public string type { get; set; }
    }

    /// <summary> 
    /// Template class for 'videoCard'
    /// </summary>
    public class videoCard
    {
        public string videoCardId { get; set; }
        public string name { get; set; }
        public string vRam { get; set; }
    }

    /// <summary> 
    /// Template class for 'location'
    /// </summary>
    public class location
    {
        public string building { get; set; }
        public string deliveredToRegistrationNumber { get; set; }
        public string lastDeliveryDate { get; set; }
        public string lastDeliveryMadeBy { get; set; }
        public string roomNumber { get; set; }
    }

    /// <summary> 
    /// Template class for 'maintenances'
    /// </summary>
    public class maintenances
    {
        public string agentId { get; set; }
        public string batteryChange { get; set; }
        public string serviceDate { get; set; }
        public string serviceType { get; set; }
        public string ticketNumber { get; set; }
    }

    /// <summary> 
    /// Template class for 'network'
    /// </summary>
    public class network
    {
        public string hostname { get; set; }
        public string ipAddress { get; set; }
        public string macAddress { get; set; }
    }

    /// <summary> 
    /// Template class for 'operatingSystem'
    /// </summary>
    public class operatingSystem
    {
        public string arch { get; set; }
        public string build { get; set; }
        public string name { get; set; }
        public string version { get; set; }
    }

    /// <summary> 
    /// Class for handling an asset through a REST API
    /// </summary>
    public static class AssetHandler
    {
        /// <summary>
        /// Gets Asset data via REST
        /// </summary>
        /// <param name="client">HTTP client object</param>
        /// <param name="path">Uri path</param>
        /// <returns>An Asset object</returns>
        /// <exception cref="UnregisteredAssetException">Asset not found</exception>
        /// <exception cref="InvalidAgentException">Unauthorized agent</exception>
        /// <exception cref="InvalidRestApiCallException">Rest call unsuccessful</exception>
        /// <exception cref="HttpRequestException">Server not found</exception>
        public static async Task<Asset> GetAssetAsync(HttpClient client, string path)
        {
            try
            {
                Asset a = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (Convert.ToInt32(response.StatusCode).Equals(200))
                    a = await response.Content.ReadAsAsync<Asset>();
                else if (Convert.ToInt32(response.StatusCode).Equals(204) || Convert.ToInt32(response.StatusCode).Equals(400))
                    throw new UnregisteredAssetException();
                else if (Convert.ToInt32(response.StatusCode).Equals(401))
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

        /// <summary>
        /// Sends asset info to server via REST
        /// </summary>
        /// <param name="client">HTTP client object</param>
        /// <param name="path">Uri path</param>
        /// <param name="a">Asset object</param>
        /// <returns>The resulting HTTP status code</returns>
        /// <exception cref="HttpRequestException">Server not found</exception>
        public static async Task<HttpStatusCode> SetAssetAsync(HttpClient client, string path, Asset a)
        {
            try
            {
                HttpResponseMessage response;
                StringContent content = new StringContent(JsonConvert.SerializeObject(a), Encoding.UTF8, GenericResources.HTTP_CONTENT_TYPE_JSON);
                response = await client.PostAsync(path, content);
                _ = response.EnsureSuccessStatusCode();
                return response.StatusCode;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }
        }
    }
}
