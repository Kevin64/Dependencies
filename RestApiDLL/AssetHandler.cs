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
        public string fwMediaOperationMode { get; set; }
        public string fwSecureBoot { get; set; }
        public string fwTpmVersion { get; set; }
        public string fwType { get; set; }
        public string fwVersion { get; set; }
        public string fwVirtualizationTechnology { get; set; }
    }

    /// <summary> 
    /// Template class for 'hardware'
    /// </summary>
    public class hardware
    {
        public string hwBrand { get; set; }
        public string hwModel { get; set; }
        public string hwSerialNumber { get; set; }
        public string hwType { get; set; }
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
        public string procId { get; set; }
        public string procName { get; set; }
        public string procFrequency { get; set; }
        public string procNumberOfCores { get; set; }
        public string procNumberOfThreads { get; set; }
        public string procCache { get; set; }
    }

    /// <summary> 
    /// Template class for 'ram'
    /// </summary>
    public class ram
    {
        public string ramSlot { get; set; }
        public string ramAmount { get; set; }
        public string ramType { get; set; }
        public string ramFrequency { get; set; }
        public string ramSerialNumber { get; set; }
        public string ramPartNumber { get; set; }
        public string ramManufacturer { get; set; }
    }

    /// <summary> 
    /// Template class for 'storage'
    /// </summary>
    public class storage
    {
        public string storId { get; set; }
        public string storConnection { get; set; }
        public string storModel { get; set; }
        public string storSerialNumber { get; set; }
        public string storSize { get; set; }
        public string storSmartStatus { get; set; }
        public string storType { get; set; }
    }

    /// <summary> 
    /// Template class for 'videoCard'
    /// </summary>
    public class videoCard
    {
        public string vcId { get; set; }
        public string vcName { get; set; }
        public string vcRam { get; set; }
    }

    /// <summary> 
    /// Template class for 'location'
    /// </summary>
    public class location
    {
        public string locBuilding { get; set; }
        public string locDeliveredToRegistrationNumber { get; set; }
        public string locLastDeliveryDate { get; set; }
        public string locLastDeliveryMadeBy { get; set; }
        public string locRoomNumber { get; set; }
    }

    /// <summary> 
    /// Template class for 'maintenances'
    /// </summary>
    public class maintenances
    {
        public string mainAgentId { get; set; }
        public string mainBatteryChange { get; set; }
        public string mainServiceDate { get; set; }
        public string mainServiceType { get; set; }
        public string mainTicketNumber { get; set; }
    }

    /// <summary> 
    /// Template class for 'network'
    /// </summary>
    public class network
    {
        public string netHostname { get; set; }
        public string netIpAddress { get; set; }
        public string netMacAddress { get; set; }
    }

    /// <summary> 
    /// Template class for 'operatingSystem'
    /// </summary>
    public class operatingSystem
    {
        public string osArch { get; set; }
        public string osBuild { get; set; }
        public string osName { get; set; }
        public string osVersion { get; set; }
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
