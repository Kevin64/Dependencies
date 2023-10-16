using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
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
        public firmware firmware { get; set; }
        public hardware hardware { get; set; }
        public location location { get; set; }
        public List<maintenances> maintenances { get; set; }
        public network network { get; set; }
        public operatingSystem operatingSystem { get; set; }
    }

    public class firmware
    {
        public string mediaOperationMode { get; set; }
        public string secureBoot { get; set; }
        public string tpmVersion { get; set; }
        public string type { get; set; }
        public string version { get; set; }
        public string virtualizationTechnology { get; set; }
    }

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
    }

    public class processor
    {
        public string processorId { get; set; }
        public string name { get; set; }
        public string frequency { get; set; }
        public string numberOfCores { get; set; }
        public string numberOfThreads { get; set; }
        public string cache { get; set; }
    }

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

    public class storage
    {
        public string connection { get; set; }
        public string model { get; set; }
        public string serialNumber { get; set; }
        public string size { get; set; }
        public string smartStatus { get; set; }
        public string storageId { get; set; }
        public string type { get; set; }
    }

    public class videoCard
    {
        public string gpuId { get; set; }
        public string name { get; set; }
        public string vRam { get; set; }
    }

    public class location
    {
        public string building { get; set; }
        public string deliveredToRegistrationNumber { get; set; }
        public string lastDeliveryDate { get; set; }
        public string lastDeliveryMadeBy { get; set; }
        public string roomNumber { get; set; }
    }

    public class maintenances
    {
        public string agentId { get; set; }
        public string batteryChange { get; set; }
        public string serviceDate { get; set; }
        public string serviceType { get; set; }
        public string ticketNumber { get; set; }
    }

    public class network
    {
        public string hostname { get; set; }
        public string ipAddress { get; set; }
        public string macAddress { get; set; }
    }

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
        public static async Task<Asset> GetAssetAsync(HttpClient client, string path)
        {
            Asset a = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
                a = await response.Content.ReadAsAsync<Asset>();
            return a;
        }

        public static async Task<Uri> SetAssetAsync(HttpClient client, string path, Asset a)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(path, a);
            _ = response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }
    }
}
