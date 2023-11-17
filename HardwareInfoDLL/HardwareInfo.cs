using ConstantsDLL.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Runtime.InteropServices;

namespace HardwareInfoDLL
{
    /// <summary> 
    /// Class for handling various hardware and setting detection tasks
    /// </summary>
    public static class HardwareInfo
    {
        public enum FirmwareTypes
        {
            BIOS,
            UEFI
        }

        public enum TpmTypes
        {
            N_A,
            v1_2,
            v2_0
        }

        public enum MediaOperationTypes
        {
            IDE,
            AHCI,
            NVMe,
            RAID
        }

        public enum StorageTypes
        {
            HDD,
            SSD
        }

        public enum StorageConnectionTypes
        {
            IDE,
            SATA,
            PCI_E
        }

        public enum RamTypes
        {
            Free = -2,
            Unknown = 0,
            Invalid = 2,
            DDR2 = 22,
            DDR3 = 24,
            DDR4 = 26
        }

        public enum SmartStates
        {
            N_A = -1,
            OK = 0,
            Pred_Fail = 1
        }

        public enum SecureBootStates
        {
            NOT_SUPPORTED,
            DISABLED,
            ENABLED
        }

        public enum VirtualizationTechnologyStates
        {
            NOT_SUPPORTED,
            DISABLED,
            ENABLED
        }

        public enum SpecBinaryStates
        {
            DISABLED,
            ENABLED
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        // Processor functions
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// Fetches the CPU information, including the number of cores/threads
        /// </summary>
        /// <returns>String with the CPU information</returns>
        public static string GetProcessorSummary()
        {
            string Id = string.Empty;
            string logical = string.Empty;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    logical = queryObj["NumberOfLogicalProcessors"].ToString();

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    Id = queryObj.Properties["name"].Value.ToString() + " " + queryObj.Properties["MaxClockSpeed"].Value.ToString()
                       + " " + GenericResources.FREQUENCY_MHZ + " (" + queryObj.Properties["NumberOfCores"].Value.ToString() + "C/" + logical + "T)";
                    break;
                }
                Id = Id.Replace("(R)", string.Empty);
                Id = Id.Replace("(TM)", string.Empty);
                Id = Id.Replace("(tm)", string.Empty);
                return Id;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Fetches all processor details from all sockets
        /// </summary>
        /// <returns>A list of processors containing a list of properties</returns>
        public static List<List<string>> GetProcessorDetails()
        {
            int size = 10, cpuIdAdj;
            string[] type = new string[size];
            string cpuId, cpuName, cpuFreq, cpuCores, cpuThreads, cpuCache;
            List<List<string>> list = new List<List<string>>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                {
                    List<string> device = new List<string>();

                    try
                    {
                        //Grabs processor ID (DeviceId property)
                        cpuId = queryObj["DeviceId"].ToString();
                        cpuIdAdj = Convert.ToInt32(cpuId.Substring(cpuId.Length - 1));
                    }
                    catch (Exception)
                    {
                        cpuIdAdj = Convert.ToInt32(GenericResources.NOT_AVAILABLE_CODE);
                    }
                    device.Add(cpuIdAdj.ToString());

                    try
                    {
                        //Grabs processor name (Name property)
                        cpuName = queryObj["Name"].ToString();
                    }
                    catch (Exception)
                    {
                        cpuName = GenericResources.NOT_AVAILABLE_CODE;
                    }
                    device.Add(cpuName);

                    try
                    {
                        //Grabs processor maximum clock speed (MaxClockSpeed property)
                        cpuFreq = queryObj["MaxClockSpeed"].ToString();
                    }
                    catch (Exception)
                    {
                        cpuFreq = GenericResources.NOT_AVAILABLE_CODE;
                    }
                    device.Add(cpuFreq);

                    try
                    {
                        //Grabs processor number of cores (NumberOfCores property)
                        cpuCores = queryObj["NumberOfCores"].ToString();
                    }
                    catch (Exception)
                    {
                        cpuCores = GenericResources.NOT_AVAILABLE_CODE;
                    }
                    device.Add(cpuCores);

                    try
                    {
                        //Grabs processor number of threads (NumberOfThreads property)
                        cpuThreads = queryObj["NumberOfLogicalProcessors"].ToString();
                    }
                    catch (Exception)
                    {
                        cpuThreads = GenericResources.NOT_AVAILABLE_CODE;
                    }
                    device.Add(cpuThreads);

                    try
                    {
                        //Grabs processor cache memory (L2CacheSize or L3CacheSize property)
                        if (queryObj["L3CacheSize"].ToString().Equals("0"))
                            cpuCache = (Convert.ToInt64(queryObj["L2CacheSize"]) * 1024).ToString();
                        else
                            cpuCache = (Convert.ToInt64(queryObj["L3CacheSize"]) * 1024).ToString();
                    }
                    catch (Exception)
                    {
                        cpuCache = GenericResources.NOT_AVAILABLE_CODE;
                    }
                    device.Add(cpuCache);

                    list.Add(device);
                }
                return list;
            }
            catch (ManagementException e)
            {
                return new List<List<string>>() { new List<string> { e.Message } };
            }
            catch (Exception)
            {
                return new List<List<string>>() { new List<string> { GenericResources.NOT_AVAILABLE_CODE } };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's processor IDs
        /// </summary>
        /// <returns>List with the computer's processor IDs, or an exception message otherwise</returns>
        public static List<string> GetProcessorIdList()
        {
            string cpuId;
            int cpuIdAdj;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                {
                    cpuId = queryObj["DeviceId"].ToString();
                    cpuIdAdj = Convert.ToInt32(cpuId.Substring(cpuId.Length - 1));
                    list.Add(cpuIdAdj.ToString());
                }
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's processor names
        /// </summary>
        /// <returns>List with the computer's processor names, or an exception message otherwise</returns>
        public static List<string> GetProcessorNameList()
        {
            string cpuName;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                {
                    cpuName = queryObj["Name"].ToString();
                    list.Add(cpuName);
                }
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's processor frequencies
        /// </summary>
        /// <returns>List with the computer's processor frequencies, or an exception message otherwise</returns>
        public static List<string> GetProcessorFrequencyList()
        {
            string cpuFreq;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                {
                    cpuFreq = queryObj["MaxClockSpeed"].ToString();
                    list.Add(cpuFreq);
                }
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's processor number of cores
        /// </summary>
        /// <returns>List with the computer's processor number of cores, or an exception message otherwise</returns>
        public static List<string> GetProcessorCoresList()
        {
            string cpuCores;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                {
                    cpuCores = queryObj["NumberOfCores"].ToString();
                    list.Add(cpuCores);
                }
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's processor number of threads
        /// </summary>
        /// <returns>List with the computer's processor number of threads, or an exception message otherwise</returns>
        public static List<string> GetProcessorThreadsList()
        {
            string cpuThreads;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                {
                    cpuThreads = queryObj["NumberOfLogicalProcessors"].ToString();
                    list.Add(cpuThreads);
                }
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's processor cache
        /// </summary>
        /// <returns>List with the computer's processor cache, or an exception message otherwise</returns>
        public static List<string> GetProcessorCacheList()
        {
            string cpuCache;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                {
                    if (queryObj["L3CacheSize"].ToString().Equals("0"))
                        cpuCache = (Convert.ToInt64(queryObj["L2CacheSize"]) * 1024).ToString();
                    else
                        cpuCache = (Convert.ToInt64(queryObj["L3CacheSize"]) * 1024).ToString();
                    list.Add(cpuCache);
                }
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        // Video Card functions
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// Fetches the primary Video Card information
        /// </summary>
        /// <returns>String with the primary Video Card information</returns>
        public static string GetVideoCardSummary()
        {
            string gpuname = string.Empty;
            string gpuramStr;
            double gpuram;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (!queryObj["Caption"].ToString().Equals("Microsoft Remote Display Adapter"))
                    {
                        if (queryObj["MaxRefreshRate"] != null)
                        {
                            gpuram = Convert.ToInt64(queryObj["AdapterRAM"]);
                            gpuram = Math.Round(gpuram / 1024 / 1024, 0);
                            gpuramStr = Math.Ceiling(Math.Log10(gpuram)) > 3
                                ? Convert.ToString(Math.Round(gpuram / 1024, 1)) + " " + GenericResources.SIZE_GB
                                : gpuram + " " + GenericResources.SIZE_MB;
                            gpuname = queryObj["Caption"].ToString() + " (" + gpuramStr + ")";
                        }
                    }
                }
                gpuname = gpuname.Replace("(R)", string.Empty);
                gpuname = gpuname.Replace("(TM)", string.Empty);
                gpuname = gpuname.Replace("(tm)", string.Empty);
                return gpuname;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Fetches all video card details from all cards
        /// </summary>
        /// <returns>A list of video cards containing a list of properties</returns>
        public static List<List<string>> GetVideoCardDetails()
        {
            int size = 10, videoCardIdAdj;
            string[] type = new string[size];
            string videoCardId, gpuName, gpuRamStr;
            double gpuRam;
            List<List<string>> list = new List<List<string>>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                {
                    List<string> device = new List<string>();

                    if (!queryObj["Caption"].ToString().Equals("Microsoft Remote Display Adapter"))
                    {
                        try
                        {
                            //Grabs video card ID (DeviceId property)
                            videoCardId = queryObj["DeviceId"].ToString();
                            videoCardIdAdj = Convert.ToInt32(videoCardId.Substring(videoCardId.Length - 1)) - 1;
                        }
                        catch (Exception)
                        {
                            videoCardIdAdj = Convert.ToInt32(GenericResources.NOT_AVAILABLE_CODE);
                        }
                        device.Add(videoCardIdAdj.ToString());

                        try
                        {
                            //Grabs video card name (Name property)
                            gpuName = queryObj["Name"].ToString();
                        }
                        catch (Exception)
                        {
                            gpuName = GenericResources.NOT_AVAILABLE_CODE;
                        }
                        device.Add(gpuName);

                        try
                        {
                            //Grabs video card vRAM (AdapterRAM property)
                            gpuRam = Convert.ToInt64(queryObj["AdapterRAM"]);
                            gpuRamStr = gpuRam.ToString();
                        }
                        catch (Exception)
                        {
                            gpuRamStr = GenericResources.NOT_AVAILABLE_CODE;
                        }
                        device.Add(gpuRamStr);

                        list.Add(device);
                    }
                }
                return list;
            }
            catch (ManagementException e)
            {
                return new List<List<string>>() { new List<string> { e.Message } };
            }
            catch (Exception)
            {
                return new List<List<string>>() { new List<string> { GenericResources.NOT_AVAILABLE_CODE } };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's video card IDs
        /// </summary>
        /// <returns>List with the computer's video card IDs, or an exception message otherwise</returns>
        public static List<string> GetVideoCardIdList()
        {
            string videoCardId;
            int videoCardIdAdj;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                {
                    if (!queryObj["Caption"].ToString().Equals("Microsoft Remote Display Adapter"))
                    {
                        videoCardId = queryObj["DeviceId"].ToString();
                        videoCardIdAdj = Convert.ToInt32(videoCardId.Substring(videoCardId.Length - 1)) - 1;
                        list.Add(videoCardIdAdj.ToString());
                    }
                }
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's video card names
        /// </summary>
        /// <returns>List with the computer's video card names, or an exception message otherwise</returns>
        public static List<string> GetVideoCardNameList()
        {
            string gpuName;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                {
                    if (!queryObj["Caption"].ToString().Equals("Microsoft Remote Display Adapter"))
                    {
                        gpuName = queryObj["Name"].ToString();
                        list.Add(gpuName);
                    }
                }
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's video card vRams
        /// </summary>
        /// <returns>List with the computer's video card vRams, or an exception message otherwise</returns>
        public static List<string> GetVideoCardRamList()
        {
            double gpuRam;
            string gpuRamStr;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                {
                    if (!queryObj["Caption"].ToString().Equals("Microsoft Remote Display Adapter"))
                    {
                        gpuRam = Convert.ToInt64(queryObj["AdapterRAM"]);
                        gpuRamStr = gpuRam.ToString();
                        list.Add(gpuRamStr);
                    }
                }
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        // Storage functions
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// Fetches the type of drive the system has (SSD or HDD), and the quantity of each
        /// </summary>
        /// <returns>String with the SSD/HDD amount</returns>
        public static string GetStorageSummary()
        {
            double dresult;
            string dresultStr;

            try
            {
                if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                {
                    int size = 10, i = 0;
                    string[] type = new string[size];
                    string[] bytesHDD = new string[size];
                    string[] bytesSSD = new string[size];
                    string concat, msftName = "Msft Virtual Disk";

                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (!Convert.ToString(queryObj["FriendlyName"]).Equals(msftName))
                        {
                            if ((Convert.ToInt16(queryObj["MediaType"]).Equals(3) || Convert.ToInt16(queryObj["MediaType"]).Equals(4) || Convert.ToInt16(queryObj["MediaType"]).Equals(0)) && !Convert.ToInt16(queryObj["BusType"]).Equals(7))
                            {
                                dresult = Convert.ToInt64(queryObj.Properties["Size"].Value.ToString());
                                dresult = Math.Round(dresult / 1000 / 1000 / 1000, 0);

                                dresultStr = Math.Log10(dresult) > 2.9999
                                    ? Convert.ToString(Math.Round(dresult / 1000, 1)) + " " + GenericResources.SIZE_TB
                                    : dresult + " " + GenericResources.SIZE_GB;

                                switch (Convert.ToInt16(queryObj["MediaType"]))
                                {
                                    case 3:
                                        type[i] = GenericResources.STORAGE_TYPE_HDD_NAME;
                                        bytesHDD[i] = dresultStr;
                                        i++;
                                        break;
                                    case 4:
                                        type[i] = GenericResources.STORAGE_TYPE_SSD_NAME;
                                        bytesSSD[i] = dresultStr;
                                        i++;
                                        break;
                                    case 0:
                                        type[i] = GenericResources.STORAGE_TYPE_HDD_NAME;
                                        bytesHDD[i] = dresultStr;
                                        i++;
                                        break;
                                }
                            }
                        }
                    }

                    IEnumerable<string> typeSliced = type.Take(i);
                    IEnumerable<string> typeSlicedHDD = bytesHDD.Take(i);
                    IEnumerable<string> typeSlicedSSD = bytesSSD.Take(i);
                    searcher.Dispose();
                    concat = CountDistinct(typeSliced.ToArray(), typeSlicedHDD.ToArray(), typeSlicedSSD.ToArray());
                    return concat;
                }
                else
                {
                    int size = 10;
                    int i = 0;
                    string[] type = new string[size];
                    string[] bytesHDD = new string[size];
                    string concat;

                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                        {
                            dresult = Convert.ToInt64(queryObj.Properties["Size"].Value.ToString());
                            dresult = Math.Round(dresult / 1000000000, 0);

                            dresultStr = Math.Log10(dresult) > 2.9999
                                ? Convert.ToString(Math.Round(dresult / 1000, 1)) + " " + GenericResources.SIZE_TB
                                : dresult + " " + GenericResources.SIZE_GB;
                            type[i] = GenericResources.STORAGE_TYPE_HDD_NAME;
                            bytesHDD[i] = dresultStr;
                            i++;
                        }
                    }
                    IEnumerable<string> typeSliced = type.Take(i);
                    IEnumerable<string> typeSlicedHDD = bytesHDD.Take(i);
                    searcher.Dispose();
                    concat = CountDistinct(typeSliced.ToArray(), typeSlicedHDD.ToArray(), typeSlicedHDD.ToArray());
                    return concat;
                }
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Fetches all storage drive details from all drives
        /// </summary>
        /// <returns>A list of drives containing a list of properties</returns>
        public static List<List<string>> GetStorageDeviceDetails()
        {
            string msftName = "Msft Virtual Disk", dresultStr;
            string storageId, storageType, storageConnection, storageModel, storageSerialNumber;
            int size = 10;
            string[] type = new string[size];
            double dresult;
            List<List<string>> list = new List<List<string>>();
            try
            {
                if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                    ManagementObjectSearcher searcher3 = new ManagementObjectSearcher("root\\wmi", "SELECT * FROM MSStorageDriver_FailurePredictStatus");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                    {
                        if (!Convert.ToString(queryObj["FriendlyName"]).Equals(msftName))
                        {
                            if ((Convert.ToInt16(queryObj["MediaType"]).Equals(3) || Convert.ToInt16(queryObj["MediaType"]).Equals(4) || Convert.ToInt16(queryObj["MediaType"]).Equals(0)) && !Convert.ToInt16(queryObj["BusType"]).Equals(7))
                            {
                                List<string> device = new List<string>();
                                bool smart;

                                try
                                {
                                    //Grabs storage ID (DeviceId property)
                                    storageId = queryObj["DeviceId"].ToString();
                                }
                                catch (Exception)
                                {
                                    storageId = GenericResources.NOT_AVAILABLE_CODE;
                                }
                                device.Add(storageId);

                                try
                                {
                                    //Grabs storage type (MediaType property)
                                    storageType = queryObj["MediaType"].ToString();
                                }
                                catch (Exception)
                                {
                                    storageType = GenericResources.NOT_AVAILABLE_CODE;
                                }
                                switch (storageType)
                                {
                                    case "3":
                                        device.Add(Convert.ToInt32(StorageTypes.HDD).ToString());
                                        break;
                                    case "4":
                                        device.Add(Convert.ToInt32(StorageTypes.SSD).ToString());
                                        break;
                                    case "0":
                                        device.Add(Convert.ToInt32(StorageTypes.HDD).ToString());
                                        break;
                                }

                                try
                                {
                                    //Grabs storage total size (Size property)
                                    dresult = Convert.ToInt64(queryObj.GetPropertyValue("Size"));
                                    dresultStr = dresult.ToString();
                                }
                                catch (Exception)
                                {
                                    dresultStr = GenericResources.NOT_AVAILABLE_CODE;
                                }
                                device.Add(dresultStr);

                                try
                                {
                                    //Grabs connection type (BusType property)
                                    storageConnection = queryObj["BusType"].ToString();

                                    if (storageConnection == GenericResources.WMI_SATA_CODE)
                                    {
                                        smart = true;
                                        device.Add(Convert.ToInt32(StorageConnectionTypes.SATA).ToString());
                                    }
                                    else if (storageConnection == GenericResources.WMI_PCIE_CODE)
                                    {
                                        smart = false;
                                        device.Add(Convert.ToInt32(StorageConnectionTypes.PCI_E).ToString());
                                    }
                                    else
                                    {
                                        smart = true;
                                        device.Add(Convert.ToInt32(StorageConnectionTypes.IDE).ToString());
                                    }
                                }
                                catch (Exception)
                                {
                                    smart = false;
                                    storageConnection = GenericResources.NOT_AVAILABLE_CODE;
                                    device.Add(storageConnection);
                                }

                                try
                                {
                                    //Grabs storage model (Model property)
                                    storageModel = queryObj["Model"].ToString();
                                }
                                catch (Exception)
                                {
                                    storageModel = GenericResources.NOT_AVAILABLE_CODE;
                                }
                                device.Add(storageModel);

                                try
                                {
                                    //Grabs storage serial number (SerialNumber property)
                                    storageSerialNumber = queryObj["SerialNumber"].ToString();
                                }
                                catch (Exception)
                                {
                                    storageSerialNumber = GenericResources.NOT_AVAILABLE_CODE;
                                }
                                device.Add(storageSerialNumber);

                                if (smart)
                                {
                                    //Grabs storage S.M.A.R.T. status (PredictFailure property)
                                    foreach (ManagementObject queryObj2 in searcher2.Get().Cast<ManagementObject>())
                                    {
                                        if (queryObj["Model"].ToString() == queryObj2["Model"].ToString())
                                        {
                                            foreach (ManagementObject queryObj3 in searcher3.Get().Cast<ManagementObject>())
                                            {
                                                if (queryObj3["InstanceName"].ToString().ToUpper().Contains(queryObj2["PNPDeviceID"].ToString().ToUpper()))
                                                {
                                                    if (queryObj3["PredictFailure"].ToString() == "False")
                                                    {
                                                        device.Add(GenericResources.OK_CODE);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        device.Add(GenericResources.PRED_FAIL_CODE);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    device.Add(GenericResources.NOT_AVAILABLE_CODE);
                                }
                                list.Add(device);
                            }
                        }
                    }
                    return list;
                }
                else
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["Index"]))
                    {
                        List<string> device = new List<string>();
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                        {
                            try
                            {
                                //Grabs storage ID (Index property)
                                storageId = queryObj.Properties["Index"].Value.ToString();
                            }
                            catch (Exception)
                            {
                                storageId = GenericResources.NOT_AVAILABLE_CODE;
                            }
                            device.Add(storageId);

                            try
                            {
                                //Grabs storage type
                                device.Add(Convert.ToInt32(StorageTypes.HDD).ToString());
                            }
                            catch (Exception)
                            {
                                device.Add(GenericResources.NOT_AVAILABLE_CODE);
                            }

                            try
                            {
                                //Grabs storage total size (Size property)
                                dresult = Convert.ToInt64(queryObj.Properties["Size"].Value.ToString());
                                dresultStr = dresult.ToString();
                            }
                            catch (Exception)
                            {
                                dresultStr = GenericResources.NOT_AVAILABLE_CODE;
                            }
                            device.Add(dresultStr);

                            try
                            {
                                //Grabs connection type (InterfaceType property)
                                storageConnection = queryObj["InterfaceType"].ToString();

                                if (storageConnection.Equals(StorageConnectionTypes.IDE.ToString()))
                                    device.Add(Convert.ToInt32(StorageConnectionTypes.SATA).ToString());
                                else if (storageConnection.Equals("SCSI"))
                                    device.Add(Convert.ToInt32(StorageConnectionTypes.PCI_E).ToString());
                                else
                                    device.Add(Convert.ToInt32(StorageConnectionTypes.IDE).ToString());
                            }
                            catch (Exception)
                            {
                                storageConnection = GenericResources.NOT_AVAILABLE_CODE;
                                device.Add(storageConnection);
                            }

                            try
                            {
                                //Grabs storage model (Model property)
                                storageModel = queryObj.Properties["Model"].Value.ToString();
                            }
                            catch (Exception)
                            {
                                storageModel = GenericResources.NOT_AVAILABLE_CODE;
                            }
                            device.Add(storageModel);

                            try
                            {
                                //Grabs storage serial number (SerialNumber property)
                                storageSerialNumber = queryObj.Properties["SerialNumber"].Value.ToString();
                            }
                            catch (Exception)
                            {
                                storageSerialNumber = GenericResources.NOT_AVAILABLE_CODE;
                            }
                            device.Add(storageSerialNumber);

                            try
                            {
                                //Grabs storage S.M.A.R.T. status (Status property)
                                if (queryObj.GetPropertyValue("Status").ToString() == "OK")
                                    device.Add(Convert.ToInt32(SmartStates.OK).ToString());
                                else
                                    device.Add(Convert.ToInt32(SmartStates.Pred_Fail).ToString());
                            }
                            catch (Exception)
                            {
                                device.Add(GenericResources.NOT_AVAILABLE_CODE);
                            }
                            list.Add(device);
                        }
                    }
                    return list;
                }
            }
            catch (ManagementException e)
            {
                return new List<List<string>>() { new List<string> { e.Message } };
            }
            catch (Exception)
            {
                return new List<List<string>>() { new List<string> { GenericResources.NOT_AVAILABLE_CODE } };
            }
        }

        /// <summary> 
        /// Fetches the computer's storage drive IDs
        /// </summary>
        /// <returns>String with the computer's storage drive IDs, or 'Unknown' otherwise</returns>
        public static List<string> GetStorageIdsList()
        {
            string msftName = "Msft Virtual Disk";
            List<string> list = new List<string>();
            try
            {
                if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                    {
                        if (!Convert.ToString(queryObj["FriendlyName"]).Equals(msftName))
                        {
                            if ((Convert.ToInt16(queryObj["MediaType"]).Equals(3) || Convert.ToInt16(queryObj["MediaType"]).Equals(4) || Convert.ToInt16(queryObj["MediaType"]).Equals(0)) && !Convert.ToInt16(queryObj["BusType"]).Equals(7))
                            {
                                string s = queryObj.GetPropertyValue("DeviceId").ToString();
                                list.Add(s);
                            }
                        }
                    }
                    return list;
                }
                else
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["Index"]))
                    {
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                        {
                            string s = queryObj.Properties["Index"].Value.ToString();
                            list.Add(s);
                        }
                    }
                    return list;
                }

            }
            catch (ManagementException e)
            {
                return new List<string>() { e.ToString() };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's storage drives types
        /// </summary>
        /// <returns>List with the computer's storage drives types, or an exception message otherwise</returns>
        public static List<string> GetStorageTypeList()
        {
            IEnumerable<string> typeSliced;
            int size = 10, i = 0;
            string[] type = new string[size];
            string msftName = "Msft Virtual Disk";
            try
            {
                if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                    {
                        if (!Convert.ToString(queryObj["FriendlyName"]).Equals(msftName))
                        {
                            if ((Convert.ToInt16(queryObj["MediaType"]).Equals(3) || Convert.ToInt16(queryObj["MediaType"]).Equals(4) || Convert.ToInt16(queryObj["MediaType"]).Equals(0)) && !Convert.ToInt16(queryObj["BusType"]).Equals(7))
                            {
                                switch (Convert.ToInt16(queryObj["MediaType"]))
                                {
                                    case 3:
                                        type[i] = Convert.ToInt32(StorageTypes.HDD).ToString();
                                        i++;
                                        break;
                                    case 4:
                                        type[i] = Convert.ToInt32(StorageTypes.SSD).ToString();
                                        i++;
                                        break;
                                    case 0:
                                        type[i] = Convert.ToInt32(StorageTypes.HDD).ToString();
                                        i++;
                                        break;
                                }
                            }
                        }
                    }

                    typeSliced = type.Take(i);
                    searcher.Dispose();
                }
                else
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["Index"]))
                    {
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                        {
                            type[i] = Convert.ToInt32(StorageTypes.HDD).ToString();
                            i++;
                        }
                    }
                    typeSliced = type.Take(i);
                    searcher.Dispose();
                }
                return typeSliced.ToList();
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.ToString() };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's storage drives sizes
        /// </summary>
        /// <returns>List with the computer's storage drives sizes, or an exception message otherwise</returns>
        public static List<string> GetStorageSizeList()
        {
            string msftName = "Msft Virtual Disk", dresultStr;
            double dresult;
            List<string> list = new List<string>();
            try
            {
                if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                    {
                        if (!Convert.ToString(queryObj["FriendlyName"]).Equals(msftName))
                        {
                            if ((Convert.ToInt16(queryObj["MediaType"]).Equals(3) || Convert.ToInt16(queryObj["MediaType"]).Equals(4) || Convert.ToInt16(queryObj["MediaType"]).Equals(0)) && !Convert.ToInt16(queryObj["BusType"]).Equals(7))
                            {
                                dresult = Convert.ToInt64(queryObj.GetPropertyValue("Size"));
                                dresultStr = dresult.ToString();
                                list.Add(dresultStr);
                            }
                        }
                    }
                    return list;
                }
                else
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["Index"]))
                    {
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                        {
                            dresult = Convert.ToInt64(queryObj.Properties["Size"].Value.ToString());
                            dresultStr = dresult.ToString();
                            list.Add(dresultStr);
                        }
                    }
                    return list;
                }
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.ToString() };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's storage drives sizes
        /// </summary>
        /// <returns>List with the computer's storage drives sizes, or an exception message otherwise</returns>
        public static List<string> GetStorageConnectionList()
        {
            string msftName = "Msft Virtual Disk";
            List<string> list = new List<string>();
            try
            {
                if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                    {
                        if (!Convert.ToString(queryObj["FriendlyName"]).Equals(msftName))
                        {
                            if ((Convert.ToInt16(queryObj["MediaType"]).Equals(3) || Convert.ToInt16(queryObj["MediaType"]).Equals(4) || Convert.ToInt16(queryObj["MediaType"]).Equals(0)) && !Convert.ToInt16(queryObj["BusType"]).Equals(7))
                            {
                                if (queryObj["BusType"].ToString() == GenericResources.WMI_SATA_CODE)
                                    list.Add(Convert.ToInt32(StorageConnectionTypes.SATA).ToString());
                                else if (queryObj["BusType"].ToString() == GenericResources.WMI_PCIE_CODE)
                                    list.Add(Convert.ToInt32(StorageConnectionTypes.PCI_E).ToString());
                                else
                                    list.Add(Convert.ToInt32(StorageConnectionTypes.IDE).ToString());
                            }
                        }
                    }
                    return list;
                }
                else
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["Index"]))
                    {
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                        {
                            if (queryObj["InterfaceType"].ToString().Equals("IDE"))
                                list.Add(Convert.ToInt32(StorageConnectionTypes.SATA).ToString());
                            else if (queryObj["InterfaceType"].ToString().Equals("SCSI"))
                                list.Add(Convert.ToInt32(StorageConnectionTypes.PCI_E).ToString());
                            else
                                list.Add(Convert.ToInt32(StorageConnectionTypes.IDE).ToString());
                        }
                    }
                    return list;
                }
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.ToString() };
            }
        }

        /// <summary> 
        /// Fetches the computer's storage drive model
        /// </summary>
        /// <returns>String with the computer's storage drive model, or 'Unknown' otherwise</returns>
        public static List<string> GetStorageModelList()
        {
            string msftName = "Msft Virtual Disk";
            List<string> list = new List<string>();
            try
            {
                if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                    {
                        if (!Convert.ToString(queryObj["FriendlyName"]).Equals(msftName))
                        {
                            if ((Convert.ToInt16(queryObj["MediaType"]).Equals(3) || Convert.ToInt16(queryObj["MediaType"]).Equals(4) || Convert.ToInt16(queryObj["MediaType"]).Equals(0)) && !Convert.ToInt16(queryObj["BusType"]).Equals(7))
                            {
                                string s = queryObj.GetPropertyValue("Model").ToString();
                                list.Add(s);
                            }
                        }
                    }
                    return list;
                }
                else
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["Index"]))
                    {
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                        {
                            string s = queryObj.Properties["Model"].Value.ToString();
                            list.Add(s);
                        }
                    }
                    return list;
                }

            }
            catch (ManagementException e)
            {
                return new List<string>() { e.ToString() };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's storage drives serial numbers
        /// </summary>
        /// <returns>List with the computer's storage drives serial numbers, or an exception message otherwise</returns>
        public static List<string> GetStorageSerialNumberList()
        {
            string msftName = "Msft Virtual Disk";
            List<string> list = new List<string>();
            try
            {
                if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceId"]))
                    {
                        if (!Convert.ToString(queryObj["FriendlyName"]).Equals(msftName))
                        {
                            if ((Convert.ToInt16(queryObj["MediaType"]).Equals(3) || Convert.ToInt16(queryObj["MediaType"]).Equals(4) || Convert.ToInt16(queryObj["MediaType"]).Equals(0)) && !Convert.ToInt16(queryObj["BusType"]).Equals(7))
                            {
                                string s = queryObj.GetPropertyValue("SerialNumber").ToString();
                                list.Add(s);
                            }
                        }
                    }
                    return list;
                }
                else
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["Index"]))
                    {
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                        {
                            string s = queryObj.Properties["SerialNumber"].Value.ToString();
                            list.Add(s);
                        }
                    }
                    return list;
                }
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.ToString() };
            }
        }

        /// <summary> 
        /// Fetches the S.M.A.R.T. status
        /// </summary>
        /// <returns>String with the S.M.A.R.T. status. 'OK' for ok, everything else for a problem</returns>
        public static List<string> GetStorageSmartList()
        {
            List<string> list = new List<string>();
            try
            {
                if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\wmi", "SELECT * FROM MSStorageDriver_FailurePredictStatus");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (queryObj.GetPropertyValue("PredictFailure").ToString() == "False")
                            list.Add(Convert.ToInt32(SmartStates.OK).ToString());
                        else
                            list.Add(Convert.ToInt32(SmartStates.Pred_Fail).ToString());
                    }
                    return list;
                }
                else
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                        {
                            if (queryObj.GetPropertyValue("Status").ToString() == "OK")
                                list.Add(Convert.ToInt32(SmartStates.OK).ToString());
                            else
                                list.Add(Convert.ToInt32(SmartStates.Pred_Fail).ToString());
                        }
                    }
                    return list;
                }
            }
            catch (ManagementException)
            {
                return new List<string>() { Convert.ToInt32(SmartStates.N_A).ToString() };
            }
        }

        /// <summary> 
        /// Fetches the SSD/HDD total size (sums all drives sizes)
        /// </summary>
        /// <returns>String with the SSD/HDD total size</returns>
        public static string GetStorageTotalSize()
        {
            int i = 0;
            double dresult = 0;
            string dresultStr;

            try
            {
                if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (Convert.ToInt16(queryObj["MediaType"]).Equals(3) || Convert.ToInt16(queryObj["MediaType"]).Equals(4))
                        {
                            dresult += Convert.ToInt64(queryObj.Properties["Size"].Value.ToString());
                            i++;
                        }
                    }
                    if (i == 0)
                    {
                        foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                            dresult += Convert.ToInt64(queryObj.Properties["Size"].Value.ToString());
                    }
                }
                else
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                            dresult += Convert.ToInt64(queryObj.Properties["Size"].Value.ToString());
                    }
                }
                dresult = Math.Round(dresult / 1000000000, 0);
                if (Math.Log10(dresult) > 2.9999)
                {
                    dresultStr = Convert.ToString(Math.Round(dresult / 1000, 1)) + " " + GenericResources.SIZE_TB;
                    return dresultStr;
                }
                else
                {
                    dresultStr = dresult + " " + GenericResources.SIZE_GB;
                    return dresultStr;
                }
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        // Network functions
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// Fetches the primary MAC Address
        /// </summary>
        /// <returns>String with the primary MAC Address, or 'null' otherwise</returns>
        public static string GetMacAddress()
        {
            string MACAddress = string.Empty;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_NetworkAdapterConfiguration");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    string[] gat = (string[])queryObj["DefaultIPGateway"];
                    if (MACAddress == string.Empty)
                    {
                        if ((bool)queryObj["IPEnabled"] == true && gat != null)
                            MACAddress = queryObj["MacAddress"].ToString();
                    }

                    queryObj.Dispose();
                }
                return MACAddress != string.Empty ? MACAddress : null;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the primary IP address
        /// </summary>
        /// <returns>String with the primary IP address, or 'null' otherwise</returns>
        public static string GetIpAddress()
        {
            string[] IPAddress = null;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_NetworkAdapterConfiguration");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    string[] gat = (string[])queryObj["DefaultIPGateway"];
                    if ((bool)queryObj["IPEnabled"] == true && gat != null)
                        IPAddress = (string[])queryObj["IPAddress"];
                    queryObj.Dispose();
                }
                return IPAddress[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary> 
        /// Fetches the computer's hostname
        /// </summary>
        /// <returns>String with the hostname</returns>
        public static string GetHostname()
        {
            string info = string.Empty;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    info = (string)queryObj["Name"];
                return info;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the default gateway of the NIC
        /// </summary>
        /// <returns>String with the NIC's gateway</returns>
        public static string GetDefaultIpGateway()
        {
            string gateway = string.Empty;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_NetworkAdapterConfiguration");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (gateway == string.Empty)
                    {
                        if ((bool)queryObj["IPEnabled"] == true)
                            gateway = queryObj["DefaultIPGateway"].ToString();
                    }

                    queryObj.Dispose();
                }
                gateway = gateway.Replace(":", string.Empty);
                return gateway;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        // Hardware functions
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// Fetches the computer's manufacturer
        /// </summary>
        /// <returns>String with the computer's manufacturer, or 'Unknown' otherwise</returns>
        public static string GetBrand()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    return queryObj.GetPropertyValue("Manufacturer").ToString();
                return GenericResources.NOT_AVAILABLE_NAME;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the computer's manufacturer (alternative method)
        /// </summary>
        /// <returns>String with the computer's manufacturer, or 'Unknown' otherwise</returns>
        public static string GetBrandAlt()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    return queryObj.GetPropertyValue("Manufacturer").ToString();
                return GenericResources.NOT_AVAILABLE_NAME;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the computer's model
        /// </summary>
        /// <returns>String with the computer's model, or 'Unknown' otherwise</returns>
        public static string GetModel()
        {
            string str;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    str = queryObj.GetPropertyValue("Model").ToString();
                    return str != string.Empty ? str : GenericResources.NOT_AVAILABLE_NAME;
                }
                return GenericResources.NOT_AVAILABLE_NAME;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the computer's model (alternative method)
        /// </summary>
        /// <returns>String with the computer's model, or 'Unknown' otherwise</returns>
        public static string GetModelAlt()
        {
            string str;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    str = queryObj.GetPropertyValue("Product").ToString();
                    return str != string.Empty ? str : GenericResources.NOT_AVAILABLE_CODE;
                }
                return GenericResources.NOT_AVAILABLE_CODE;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the motherboard serial number
        /// </summary>
        /// <returns>String with the motherboard serial number, or 'Unknown' otherwise</returns>
        public static string GetSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    return queryObj.GetPropertyValue("SerialNumber").ToString();
                return GenericResources.NOT_AVAILABLE_CODE;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        // Ram functions
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// Fetches the summary of RAM of the system
        /// </summary>
        /// <returns>String with the summary of RAM of the system</returns>
        public static string GetRamSummary()
        {
            long MemSize = 0;
            long mCap;
            string mType = string.Empty;
            string mSpeed = string.Empty;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (!Convert.ToString(queryObj["DeviceLocator"]).Contains(GenericResources.SYSTEM_ROM))
                    {
                        mCap = Convert.ToInt64(queryObj["Capacity"]);
                        MemSize += mCap;
                        if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM))
                        {
                            if (queryObj["SMBIOSMemoryType"].ToString().Equals(GenericResources.DDR4_SMBIOS_CODE))
                            {
                                mType = GenericResources.DDR4_NAME;
                                mSpeed = " " + queryObj["Speed"].ToString() + " " + GenericResources.FREQUENCY_MHZ;
                            }
                            else if (queryObj["SMBIOSMemoryType"].ToString().Equals(GenericResources.DDR3_SMBIOS_CODE))
                            {
                                mType = GenericResources.DDR3_NAME;
                                mSpeed = " " + queryObj["Speed"].ToString() + " " + GenericResources.FREQUENCY_MHZ;
                            }
                            else if (queryObj["SMBIOSMemoryType"].ToString().Equals("3"))
                            {
                                mType = string.Empty;
                                mSpeed = string.Empty;
                            }
                            else
                            {
                                mType = GenericResources.DDR2_NAME;
                                try
                                {
                                    mSpeed = " " + queryObj["Speed"].ToString() + " " + GenericResources.FREQUENCY_MHZ;
                                }
                                catch
                                {
                                    mSpeed = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            if (queryObj["MemoryType"].ToString().Equals(GenericResources.DDR3_MEMORY_TYPE_CODE))
                            {
                                mType = GenericResources.DDR3_NAME;
                                mSpeed = " " + queryObj["Speed"].ToString() + " " + GenericResources.FREQUENCY_MHZ;
                            }
                            else if (queryObj["MemoryType"].ToString().Equals("2") || queryObj["MemoryType"].ToString().Equals("0"))
                            {
                                mType = string.Empty;
                                mSpeed = string.Empty;
                            }
                            else
                            {
                                mType = GenericResources.DDR2_NAME;
                                try
                                {
                                    mSpeed = " " + queryObj["Speed"].ToString() + " " + GenericResources.FREQUENCY_MHZ;
                                }
                                catch
                                {
                                    mSpeed = string.Empty;
                                }
                            }
                        }
                    }
                }
                MemSize = MemSize / 1024 / 1024 / 1024;
                return MemSize.ToString() + " " + GenericResources.SIZE_GB + " " + mType + mSpeed;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Fetches all RAM modules details from all slots
        /// </summary>
        /// <returns>A list of RAM modules containing a list of properties</returns>
        public static List<List<string>> GetRamDetails()
        {
            int count = 0, numRamSlots = 0;
            string ramSlot, ramAmount, ramType, ramFrequency, ramSerialNumber, ramPartNumber, ramManufacturer;
            List<List<string>> list = new List<List<string>>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceLocator"].ToString().Substring(obj["DeviceLocator"].ToString().Length - 1)))
                {
                    List<string> device = new List<string>();

                    try
                    {
                        //Grabs RAM slot (DeviceLocator property)
                        ramSlot = queryObj["DeviceLocator"].ToString();
                        ramSlot = ramSlot.Substring(ramSlot.Length - 1);
                    }
                    catch (Exception)
                    {
                        ramSlot = GenericResources.NOT_AVAILABLE_CODE;
                    }
                    device.Add(ramSlot);

                    try
                    {
                        //Grabs RAM amount (Capacity property)
                        ramAmount = queryObj["Capacity"].ToString();
                    }
                    catch (Exception)
                    {
                        ramAmount = GenericResources.NOT_AVAILABLE_CODE;
                    }
                    device.Add(ramAmount);

                    try
                    {
                        //Grabs RAM type (SMBIOSMemoryType or MemoryType property)
                        if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM))
                            ramType = queryObj["SMBIOSMemoryType"].ToString();
                        else
                            ramType = queryObj["MemoryType"].ToString();
                    }
                    catch (Exception)
                    {
                        ramType = GenericResources.NOT_AVAILABLE_CODE;
                    }
                    device.Add(ramType);

                    try
                    {
                        //Grabs RAM speed (Speed property)
                        ramFrequency = queryObj["Speed"].ToString();
                    }
                    catch (Exception)
                    {
                        ramFrequency = GenericResources.NOT_AVAILABLE_CODE;
                    }
                    device.Add(ramFrequency);

                    try
                    {
                        //Grabs RAM Serial Number (SerialNumber property)
                        ramSerialNumber = queryObj["SerialNumber"].ToString();
                    }
                    catch (Exception)
                    {
                        ramSerialNumber = GenericResources.NOT_AVAILABLE_CODE;
                    }
                    device.Add(ramSerialNumber);

                    try
                    {
                        //Grabs RAM PartNumber (PartNumber property)
                        ramPartNumber = queryObj["PartNumber"].ToString();
                    }
                    catch (Exception)
                    {
                        ramPartNumber = GenericResources.NOT_AVAILABLE_CODE;
                    }
                    device.Add(ramPartNumber);

                    try
                    {
                        //Grabs RAM Manufacturer (Manufacturer property)
                        ramManufacturer = queryObj["Manufacturer"].ToString();
                    }
                    catch (Exception)
                    {
                        ramManufacturer = GenericResources.NOT_AVAILABLE_CODE;
                    }
                    device.Add(ramManufacturer);

                    count++;
                    list.Add(device);
                }

                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemoryArray");
                foreach (ManagementObject queryObj in searcher2.Get().Cast<ManagementObject>())
                    numRamSlots = Convert.ToInt32(queryObj["MemoryDevices"]);

                for (int i = 0; i < numRamSlots - count; i++)
                {
                    List<string> freeDevice = new List<string>
                    {
                        GenericResources.RAM_FREE_CODE,
                        GenericResources.RAM_FREE_CODE,
                        GenericResources.RAM_FREE_CODE,
                        GenericResources.RAM_FREE_CODE,
                        GenericResources.RAM_FREE_CODE,
                        GenericResources.RAM_FREE_CODE,
                        GenericResources.RAM_FREE_CODE
                    };
                    list.Add(freeDevice);
                }

                return list;
            }
            catch (ManagementException e)
            {
                return new List<List<string>>() { new List<string> { e.Message } };
            }
            catch (Exception)
            {
                return new List<List<string>>() { new List<string> { GenericResources.NOT_AVAILABLE_CODE } };
            }
        }

        /// <summary> 
        /// Fetches the amount of RAM of the system (alternative method)
        /// </summary>
        /// <returns>String with the amount of RAM of the system</returns>
        public static string GetRamAlt()
        {
            double MemSize = 0;
            long mCap;
            string MemSizeStr;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (!Convert.ToString(queryObj["DeviceLocator"]).Contains(GenericResources.SYSTEM_ROM))
                    {
                        mCap = Convert.ToInt64(queryObj["Capacity"]);
                        MemSize += mCap;
                    }
                }
                MemSize = MemSize / 1024 / 1024 / 1024;
                MemSizeStr = MemSize.ToString();
                return MemSizeStr;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the number of RAM slots on the system
        /// </summary>
        /// <returns>String with the number of RAM slots</returns>
        public static string GetNumRamSlots()
        {
            int MemSlots = 0;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemoryArray");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (Convert.ToString(queryObj["Tag"]).Equals("Physical Memory Array 0"))
                        MemSlots = Convert.ToInt32(queryObj["MemoryDevices"]);
                }

                return MemSlots.ToString();
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the number of free RAM slots on the system
        /// </summary>
        /// <returns>String with the number of free RAM slots</returns>
        public static string GetNumFreeRamSlots()
        {
            int i = 0;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (!Convert.ToString(queryObj["DeviceLocator"]).Contains(GenericResources.SYSTEM_ROM))
                        i++;
                }

                return i.ToString();
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Creates a list of the computer's ram slots
        /// </summary>
        /// <returns>List with the computer's ram slots, or an exception message otherwise</returns>
        public static List<string> GetRamSlotList()
        {
            int count = 0, numRamFreeSlots = 0;
            string ramSlot;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceLocator"].ToString().Substring(obj["DeviceLocator"].ToString().Length - 1)))
                {
                    ramSlot = queryObj["DeviceLocator"].ToString();
                    ramSlot = ramSlot.Substring(ramSlot.Length - 1);
                    count++;
                    list.Add(ramSlot);
                }

                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemoryArray");
                foreach (ManagementObject queryObj in searcher2.Get().Cast<ManagementObject>())
                    numRamFreeSlots = Convert.ToInt32(queryObj["MemoryDevices"]);

                for (int i = 0; i < numRamFreeSlots - count; i++)
                    list.Add(GenericResources.RAM_FREE_CODE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE_CODE };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's ram amount
        /// </summary>
        /// <returns>List with the computer's ram amount, or an exception message otherwise</returns>
        public static List<string> GetRamAmountList()
        {
            int count = 0, numRamFreeSlots = 0;
            string ramAmount;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    ramAmount = queryObj["Capacity"].ToString();
                    count++;
                    list.Add(ramAmount);
                }

                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemoryArray");
                foreach (ManagementObject queryObj in searcher2.Get().Cast<ManagementObject>())
                    numRamFreeSlots = Convert.ToInt32(queryObj["MemoryDevices"]);

                for (int i = 0; i < numRamFreeSlots - count; i++)
                    list.Add(GenericResources.RAM_FREE_CODE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE_CODE };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's ram types
        /// </summary>
        /// <returns>List with the computer's ram types, or an exception message otherwise</returns>
        public static List<string> GetRamTypeList()
        {
            int count = 0, numRamFreeSlots = 0;
            string ramType;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM))
                        ramType = queryObj["SMBIOSMemoryType"].ToString();
                    else
                        ramType = queryObj["MemoryType"].ToString();
                    count++;
                    list.Add(ramType);
                }

                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemoryArray");
                foreach (ManagementObject queryObj in searcher2.Get().Cast<ManagementObject>())
                    numRamFreeSlots = Convert.ToInt32(queryObj["MemoryDevices"]);

                for (int i = 0; i < numRamFreeSlots - count; i++)
                    list.Add(GenericResources.RAM_FREE_CODE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE_CODE };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's ram frequencies
        /// </summary>
        /// <returns>List with the computer's ram frequencies, or an exception message otherwise</returns>
        public static List<string> GetRamFrequencyList()
        {
            int count = 0, numRamFreeSlots = 0;
            string ramFrequency;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceLocator"].ToString().Substring(obj["DeviceLocator"].ToString().Length - 1)))
                {
                    ramFrequency = queryObj["Speed"].ToString();
                    count++;
                    list.Add(ramFrequency);
                }

                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemoryArray");
                foreach (ManagementObject queryObj in searcher2.Get().Cast<ManagementObject>())
                    numRamFreeSlots = Convert.ToInt32(queryObj["MemoryDevices"]);

                for (int i = 0; i < numRamFreeSlots - count; i++)
                    list.Add(GenericResources.RAM_FREE_CODE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE_CODE };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's ram serial numbers
        /// </summary>
        /// <returns>List with the computer's ram serial numbers, or an exception message otherwise</returns>
        public static List<string> GetRamSerialNumberList()
        {
            int count = 0, numRamFreeSlots = 0;
            string ramSerialNumber;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceLocator"].ToString().Substring(obj["DeviceLocator"].ToString().Length - 1)))
                {
                    ramSerialNumber = queryObj["SerialNumber"].ToString();
                    count++;
                    list.Add(ramSerialNumber);
                }

                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemoryArray");
                foreach (ManagementObject queryObj in searcher2.Get().Cast<ManagementObject>())
                    numRamFreeSlots = Convert.ToInt32(queryObj["MemoryDevices"]);

                for (int i = 0; i < numRamFreeSlots - count; i++)
                    list.Add(GenericResources.RAM_FREE_CODE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE_CODE };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's ram part numbers
        /// </summary>
        /// <returns>List with the computer's ram part numbers, or an exception message otherwise</returns>
        public static List<string> GetRamPartNumberList()
        {
            int count = 0, numRamFreeSlots = 0;
            string ramPartNumber;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceLocator"].ToString().Substring(obj["DeviceLocator"].ToString().Length - 1)))
                {
                    ramPartNumber = queryObj["PartNumber"].ToString();
                    count++;
                    list.Add(ramPartNumber);
                }

                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemoryArray");
                foreach (ManagementObject queryObj in searcher2.Get().Cast<ManagementObject>())
                    numRamFreeSlots = Convert.ToInt32(queryObj["MemoryDevices"]);

                for (int i = 0; i < numRamFreeSlots - count; i++)
                    list.Add(GenericResources.RAM_FREE_CODE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE_CODE };
            }
        }

        /// <summary> 
        /// Creates a list of the computer's ram manufacturers
        /// </summary>
        /// <returns>List with the computer's ram manufacturers, or an exception message otherwise</returns>
        public static List<string> GetRamManufacturerList()
        {
            int count = 0, numRamFreeSlots = 0;
            string ramManufacturer;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get().OfType<ManagementObject>().OrderBy(obj => obj["DeviceLocator"].ToString().Substring(obj["DeviceLocator"].ToString().Length - 1)))
                {
                    ramManufacturer = queryObj["Manufacturer"].ToString();
                    count++;
                    list.Add(ramManufacturer);
                }

                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemoryArray");
                foreach (ManagementObject queryObj in searcher2.Get().Cast<ManagementObject>())
                    numRamFreeSlots = Convert.ToInt32(queryObj["MemoryDevices"]);

                for (int i = 0; i < numRamFreeSlots - count; i++)
                    list.Add(GenericResources.RAM_FREE_CODE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE_CODE };
            }
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        // Operating System functions
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// Fetches the OS architecture in binary form
        /// </summary>
        /// <returns>String with the OS architecture. '1' for x64, '0' for x86</returns>
        public static string GetOSArchBinary()
        {
            bool is64bit = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
            return is64bit ? "1" : "0";
        }

        /// <summary> 
        /// Fetches the OS architecture
        /// </summary>
        /// <returns>String with the OS architecture. '64' for x64, '32' for x86</returns>
        public static string GetOSArch()
        {
            bool is64bit = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
            return is64bit ? GenericResources.ARCH64 : GenericResources.ARCH32;
        }

        /// <summary> 
        /// Fetches the OS architecture (alternative method)
        /// </summary>
        /// <returns>String with the OS architecture. 'x64' for 64-bit, 'x86' for 32-bit</returns>
        public static string GetOSArchAlt()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj.GetPropertyValue("OSArchitecture").ToString().Contains(GenericResources.ARCH64)
                        ? GenericResources.X64_NAME
                        : GenericResources.X86_NAME;
                }
                return GenericResources.NOT_AVAILABLE_CODE;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the NT version
        /// </summary>
        /// <returns>String with the NT version. '7' for Windows 7, '8' for Windows 8, '8.1' for Windows 8.1, '10' for Windows 10</returns>
        public static string GetWinVersion()
        {
            string operatingSystem = string.Empty;

            OperatingSystem os = Environment.OSVersion;
            Version vs = os.Version;

            try
            {
                if (os.Platform == PlatformID.Win32NT)
                {
                    switch (vs.Major)
                    {
                        case 6:
                            if (vs.Minor == 1)
                                operatingSystem = GenericResources.WIN_7_NAMENUM;
                            else if (vs.Minor == 2)
                                operatingSystem = GenericResources.WIN_8_NAMENUM;
                            else
                                operatingSystem = GenericResources.WIN_8_1_NAMENUM;
                            break;
                        case 10:
                            operatingSystem = GenericResources.WIN_10_NAMENUM;
                            break;
                        default:
                            break;
                    }
                }
                return operatingSystem;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the operating system summary
        /// </summary>
        /// <returns>String with the operating system summary, or 'Unknown' otherwise</returns>
        public static string GetOSSummary()
        {
            RegistryKey rk;
            if (Environment.Is64BitOperatingSystem)
                rk = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            else
                rk = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
            rk = rk.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            string displayVersion = rk.GetValue("DisplayVersion", string.Empty).ToString();
            string releaseId = rk.GetValue("releaseId", string.Empty).ToString();
            string updateBuildRevision = rk.GetValue("UBR", string.Empty).ToString();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM))
                    {
                        if (Convert.ToInt32(releaseId) <= 2004)
                            return (queryObj["Caption"].ToString() + ", v" + releaseId + ", " + GenericResources.BUILD + " " + queryObj["Version"].ToString() + "." + updateBuildRevision + " (" + GetOSArchAlt() + ")").Replace("Microsoft", string.Empty).Trim();
                        else
                            return (queryObj["Caption"].ToString() + ", v" + displayVersion + ", " + GenericResources.BUILD + " " + queryObj["Version"].ToString() + "." + updateBuildRevision + " (" + GetOSArchAlt() + ")").Replace("Microsoft", string.Empty).Trim();
                    }
                    else if (GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                    {
                        return (queryObj["Caption"].ToString() + ", " + GenericResources.BUILD + " " + queryObj["Version"].ToString() + "." + updateBuildRevision + " (" + GetOSArchAlt() + ")").Replace("Microsoft", string.Empty).Trim();
                    }
                    else
                    {
                        return (queryObj["Caption"].ToString() + " " + queryObj["CSDVersion"].ToString() + ", " + GenericResources.BUILD + " " + queryObj["Version"].ToString() + "." + updateBuildRevision + " (" + GetOSArchAlt() + ")").Replace("Microsoft", string.Empty).Trim();
                    }
                }
                return GenericResources.NOT_AVAILABLE_CODE;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the operating system summary
        /// </summary>
        /// <returns>String with the operating system summary, or 'Unknown' otherwise</returns>
        public static string GetOSVersion()
        {
            RegistryKey rk;
            if (Environment.Is64BitOperatingSystem)
                rk = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            else
                rk = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
            rk = rk.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            string displayVersion = rk.GetValue("DisplayVersion", string.Empty).ToString();
            string releaseId = rk.GetValue("releaseId", string.Empty).ToString();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM))
                    {
                        if (Convert.ToInt32(releaseId) <= 2004)
                            return releaseId;
                        else
                            return displayVersion;
                    }
                    else if (GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                    {
                        return null;
                    }
                    else
                    {
                        return queryObj["CSDVersion"].ToString();
                    }
                }
                return GenericResources.NOT_AVAILABLE_CODE;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the operating system name
        /// </summary>
        /// <returns>String with the operating system name, or 'Unknown' otherwise</returns>
        public static string GetOSName()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj["Caption"].ToString().Replace("Microsoft", string.Empty).Trim();
                }
                return GenericResources.NOT_AVAILABLE_CODE;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the OS build number
        /// </summary>
        /// <returns>String with the OS build number and revision, or 'Unknown' otherwise</returns>
        public static string GetOSBuildAndRevision()
        {
            RegistryKey rk;
            if (Environment.Is64BitOperatingSystem)
                rk = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            else
                rk = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
            rk = rk.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            string updateBuildRevision = rk.GetValue("UBR", string.Empty).ToString();
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    return queryObj.GetPropertyValue("Version").ToString() + "." + updateBuildRevision;
                return GenericResources.NOT_AVAILABLE_CODE;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }

        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        // Firmware functions
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// Fetches the firmware version
        /// </summary>
        /// <returns>String with the firmware version</returns>
        public static string GetFirmwareVersion()
        {
            string biosVersion = string.Empty;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    biosVersion = (string)queryObj["SMBIOSBIOSVersion"];
                return biosVersion;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the firmware type on Windows 7
        /// </summary>
        /// <returns>String with the firmware type code. '1' for UEFI, '0' for BIOS</returns>
        public const int ERROR_INVALID_FUNCTION = 1;
        [DllImport("kernel32.dll",
            EntryPoint = "GetFirmwareEnvironmentVariableW",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFirmwareType(string lpName, string lpGUID, IntPtr pBuffer, uint size);

        /// <summary> 
        /// Fetches the firmware type on Windows 8 and later
        /// </summary>
        /// <returns>String with the firmware type code. '1' for UEFI, '0' for BIOS, 'Not determined' for not determined</returns>
        [DllImport("kernel32.dll")]
        private static extern bool GetFirmwareType(ref uint FirmwareType);
        public static string GetFirmwareType()
        {
            try
            {
                if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                {
                    uint firmwaretype = 0;
                    if (GetFirmwareType(ref firmwaretype))
                    {
                        if (firmwaretype == 1)
                            return Convert.ToInt32(FirmwareTypes.BIOS).ToString();
                        else if (firmwaretype == 2)
                            return Convert.ToInt32(FirmwareTypes.UEFI).ToString();
                    }

                    return UIStrings.NOT_DETERMINED;
                }
                else
                {
                    try
                    {
                        _ = GetFirmwareType(string.Empty, "{00000000-0000-0000-0000-000000000000}", IntPtr.Zero, 0);
                        return Marshal.GetLastWin32Error() == ERROR_INVALID_FUNCTION ? Convert.ToInt32(FirmwareTypes.BIOS).ToString() : Convert.ToInt32(FirmwareTypes.UEFI).ToString();
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the Secure Boot status (alternative method)
        /// </summary>
        /// <returns>String with the Secure Boot status. '2' for activated, '1' for deactivated, '0' for not supported</returns>
        public static string GetSecureBootAlt()
        {
            try
            {
                PowerShell PowerShellInst = PowerShell.Create();
                _ = PowerShellInst.AddScript("Confirm-SecureBootUEFI");
                Collection<PSObject> PSOutput = PowerShellInst.Invoke();

                foreach (PSObject queryObj in PSOutput)
                    return Convert.ToInt32(SecureBootStates.ENABLED).ToString();
                return Convert.ToInt32(SecureBootStates.DISABLED).ToString();
            }
            catch
            {
                return Convert.ToInt32(SecureBootStates.NOT_SUPPORTED).ToString();
            }
        }

        /// <summary> 
        /// Fetches the Secure Boot status
        /// </summary>
        /// <returns>String with the Secure Boot status. '2' for activated, '1' for deactivated, '0' for not supported</returns>
        public static string GetSecureBoot()
        {
            try
            {
                string secBoot = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecureBoot\State", "UEFISecureBootEnabled", 0).ToString();
                return secBoot.Equals("0") ? Convert.ToInt32(SecureBootStates.DISABLED).ToString() : Convert.ToInt32(SecureBootStates.ENABLED).ToString();
            }
            catch
            {
                return Convert.ToInt32(SecureBootStates.NOT_SUPPORTED).ToString();
            }
        }

        /// <summary> 
        /// Fetches the Virtualization Technology status
        /// </summary>
        /// <returns>String with the Virtualization Technology status. '2' for activated, '1' for deactivated, '0' for not supported</returns>
        public static string GetVirtualizationTechnology()
        {
            int flag = 0;

            try
            {
                if (!GetWinVersion().Equals(GenericResources.WIN_7_NAMENUM))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (queryObj["VirtualizationFirmwareEnabled"].ToString().Equals("True"))
                            flag = Convert.ToInt32(VirtualizationTechnologyStates.ENABLED);
                        else if (bool.Parse(GetHyperVStatus()))
                            flag = Convert.ToInt32(VirtualizationTechnologyStates.ENABLED);
                    }
                    if (flag != Convert.ToInt32(VirtualizationTechnologyStates.ENABLED))
                        flag = GetFirmwareType() == Convert.ToInt32(FirmwareTypes.UEFI).ToString() ? Convert.ToInt32(VirtualizationTechnologyStates.DISABLED) : Convert.ToInt32(VirtualizationTechnologyStates.NOT_SUPPORTED);
                }
                if (flag == Convert.ToInt32(VirtualizationTechnologyStates.ENABLED))
                    return Convert.ToInt32(SecureBootStates.ENABLED).ToString();
                else if (flag == Convert.ToInt32(VirtualizationTechnologyStates.DISABLED))
                    return Convert.ToInt32(SecureBootStates.DISABLED).ToString();
                else
                    return Convert.ToInt32(SecureBootStates.NOT_SUPPORTED).ToString();
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the Hyper-V installation status
        /// </summary>
        /// <returns>String with the Hyper-V status. 'true' for true, 'false' for false</returns>
        public static string GetHyperVStatus()
        {
            string featureName;
            uint featureToggle;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OptionalFeature");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    featureName = (string)queryObj.Properties["Name"].Value;
                    featureToggle = (uint)queryObj.Properties["InstallState"].Value;

                    if ((featureName.Equals("Microsoft-Hyper-V") && featureToggle.Equals(1)) || (featureName.Equals("Microsoft-Hyper-V-Hypervisor") && featureToggle.Equals(1)) || (featureName.Equals("Containers-DisposableClientVM") && featureToggle.Equals(1)))
                        return GenericResources.TRUE;
                }
                return GenericResources.FALSE;
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the TPM version
        /// </summary>
        /// <returns>String with the TPM version code. '0' for none, '1' for 1.2, '2' for 2.0</returns>
        public static string GetTPMStatus()
        {
            string specVersion = string.Empty;
            string str = string.Empty;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2\\Security\\MicrosoftTpm", "SELECT * FROM Win32_Tpm");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    specVersion = queryObj.Properties["SpecVersion"].Value.ToString();

                if (specVersion != string.Empty)
                {
                    if (specVersion.Substring(0, 3).Equals(GenericResources.TPM_1_2_NAME))
                        str = Convert.ToInt32(TpmTypes.v1_2).ToString();
                    else if (specVersion.Substring(0, 3).Equals(GenericResources.TPM_2_0_NAME))
                        str = Convert.ToInt32(TpmTypes.v2_0).ToString();
                    return str;
                }
                else
                {
                    return Convert.ToInt32(TpmTypes.N_A).ToString();
                }
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the operation mode that the storage is running (IDE/AHCI/NVMe)
        /// </summary>
        /// <returns>String with the current media operation mode</returns>
        public static string GetMediaOperationMode()
        {
            try
            {
                if (GetWinVersion().Equals(GenericResources.WIN_10_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_1_NAMENUM) || GetWinVersion().Equals(GenericResources.WIN_8_NAMENUM))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (queryObj["DeviceId"].ToString().Equals("0"))
                        {
                            if (queryObj["BusType"].ToString().Equals(GenericResources.WMI_PCIE_CODE))
                                return Convert.ToInt32(MediaOperationTypes.NVMe).ToString();
                            else if (queryObj["BusType"].ToString().Equals(GenericResources.WMI_SATA_CODE))
                                return Convert.ToInt32(MediaOperationTypes.AHCI).ToString();
                        }
                    }
                    return Convert.ToInt32(MediaOperationTypes.IDE).ToString();
                }
                else
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                        {
                            if (queryObj["Index"].ToString().Equals("0"))
                            {
                                if (queryObj["InterfaceType"].ToString().Equals("IDE"))
                                    return Convert.ToInt32(MediaOperationTypes.AHCI).ToString();
                                else if (queryObj["InterfaceType"].ToString().Equals("SCSI"))
                                    return Convert.ToInt32(MediaOperationTypes.NVMe).ToString();
                            }
                        }
                    }
                    return Convert.ToInt32(MediaOperationTypes.IDE).ToString();
                }
            }
            catch (ManagementException e)
            {
                return e.Message;
            }
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        // Auxiliary functions
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// Auxiliary method for GetStorageType method, that groups the same objects in a list and counts them
        /// </summary>
        /// <returns>String with the SSD/HDD amount</returns>
        public static string CountDistinct(string[] array, string[] array2, string[] array3)
        {
            string result = string.Empty;
            int j = 0;
            List<string> sizesHDD = new List<string>();
            List<string> sizesSSD = new List<string>();
            char[] comma = { ',', ' ' };
            IEnumerable<IGrouping<string, string>> groups = array.GroupBy(z => z);

            try
            {
                foreach (IGrouping<string, string> group in groups)
                {
                    j = 0;
                    result += group.Count() + "x " + group.Key;
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (array[i] == group.Key)
                        {
                            array[i] = string.Empty;
                            if (group.Key == GenericResources.STORAGE_TYPE_HDD_NAME)
                            {
                                sizesHDD.Add(array2[i]);
                                j++;
                            }
                            else
                            {
                                sizesSSD.Add(array3[i]);
                                j++;
                            }
                            if (group.Count() == j)
                            {
                                if (group.Key == GenericResources.STORAGE_TYPE_HDD_NAME)
                                    result += " (" + string.Join(", ", sizesHDD) + ")" + ", ";
                                else
                                    result += " (" + string.Join(", ", sizesSSD) + ")" + ", ";
                                break;
                            }
                        }
                    }
                }
                return result.TrimEnd(comma);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}