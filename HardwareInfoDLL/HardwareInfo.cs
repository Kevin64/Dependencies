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
            IDE_RAID,
            AHCI,
            NVMe
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
            UNKNOWN = 0,
            DDR2 = 22,
            DDR3 = 24,
            DDR4 = 26
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
                       + " " + GenericResources.FREQUENCY + " (" + queryObj.Properties["NumberOfCores"].Value.ToString() + "C/" + logical + "T)";
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

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
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

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
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

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
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

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
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

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
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

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
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
                                ? Convert.ToString(Math.Round(gpuram / 1024, 1)) + " " + GenericResources.GB
                                : gpuram + " " + GenericResources.MB;
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
        /// Creates a list of the computer's video card IDs
        /// </summary>
        /// <returns>List with the computer's video card IDs, or an exception message otherwise</returns>
        public static List<string> GetVideoCardIdList()
        {
            string gpuId;
            int gpuIdAdj;
            List<string> list = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (!queryObj["Caption"].ToString().Equals("Microsoft Remote Display Adapter"))
                    {
                        gpuId = queryObj["DeviceId"].ToString();
                        gpuIdAdj = Convert.ToInt32(gpuId.Substring(gpuId.Length - 1)) - 1;
                        list.Add(gpuIdAdj.ToString());
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

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
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

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
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
                if (GetWinVersion().Equals(GenericResources.WINDOWS_10) || GetWinVersion().Equals(GenericResources.WINDOWS_8_1) || GetWinVersion().Equals(GenericResources.WINDOWS_8))
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
                                    ? Convert.ToString(Math.Round(dresult / 1000, 1)) + " " + GenericResources.TB
                                    : dresult + " " + GenericResources.GB;

                                switch (Convert.ToInt16(queryObj["MediaType"]))
                                {
                                    case 3:
                                        type[i] = GenericResources.HDD;
                                        bytesHDD[i] = dresultStr;
                                        i++;
                                        break;
                                    case 4:
                                        type[i] = GenericResources.SSD;
                                        bytesSSD[i] = dresultStr;
                                        i++;
                                        break;
                                    case 0:
                                        type[i] = GenericResources.HDD;
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
                                ? Convert.ToString(Math.Round(dresult / 1000, 1)) + " " + GenericResources.TB
                                : dresult + " " + GenericResources.GB;
                            type[i] = GenericResources.HDD;
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
        /// Fetches the computer's storage drive IDs
        /// </summary>
        /// <returns>String with the computer's storage drive IDs, or 'Unknown' otherwise</returns>
        public static List<string> GetStorageIdsList()
        {
            string msftName = "Msft Virtual Disk";
            List<string> list = new List<string>();
            try
            {
                if (GetWinVersion().Equals(GenericResources.WINDOWS_10) || GetWinVersion().Equals(GenericResources.WINDOWS_8_1) || GetWinVersion().Equals(GenericResources.WINDOWS_8))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get())
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

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
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
            try
            {
                if (GetWinVersion().Equals(GenericResources.WINDOWS_10) || GetWinVersion().Equals(GenericResources.WINDOWS_8_1) || GetWinVersion().Equals(GenericResources.WINDOWS_8))
                {
                    int size = 10, i = 0;
                    string[] type = new string[size];
                    string msftName = "Msft Virtual Disk";

                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (!Convert.ToString(queryObj["FriendlyName"]).Equals(msftName))
                        {
                            if ((Convert.ToInt16(queryObj["MediaType"]).Equals(3) || Convert.ToInt16(queryObj["MediaType"]).Equals(4) || Convert.ToInt16(queryObj["MediaType"]).Equals(0)) && !Convert.ToInt16(queryObj["BusType"]).Equals(7))
                            {
                                switch (Convert.ToInt16(queryObj["MediaType"]))
                                {
                                    case 3:
                                        type[i] = ((int)StorageTypes.HDD).ToString();
                                        i++;
                                        break;
                                    case 4:
                                        type[i] = ((int)StorageTypes.SSD).ToString();
                                        i++;
                                        break;
                                    case 0:
                                        type[i] = ((int)StorageTypes.HDD).ToString();
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
                    int size = 10;
                    int i = 0;
                    string[] type = new string[size];

                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                        {
                            type[i] = ((int)StorageTypes.HDD).ToString();
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
                if (GetWinVersion().Equals(GenericResources.WINDOWS_10) || GetWinVersion().Equals(GenericResources.WINDOWS_8_1) || GetWinVersion().Equals(GenericResources.WINDOWS_8))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get())
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

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
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
                if (GetWinVersion().Equals(GenericResources.WINDOWS_10) || GetWinVersion().Equals(GenericResources.WINDOWS_8_1) || GetWinVersion().Equals(GenericResources.WINDOWS_8))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        if (!Convert.ToString(queryObj["FriendlyName"]).Equals(msftName))
                        {
                            if ((Convert.ToInt16(queryObj["MediaType"]).Equals(3) || Convert.ToInt16(queryObj["MediaType"]).Equals(4) || Convert.ToInt16(queryObj["MediaType"]).Equals(0)) && !Convert.ToInt16(queryObj["BusType"]).Equals(7))
                            {
                                if (queryObj["BusType"].ToString() == GenericResources.WMI_SATA)
                                    list.Add(((int)StorageConnectionTypes.SATA).ToString());
                                else if (queryObj["BusType"].ToString() == GenericResources.WMI_PCIE)
                                    list.Add(((int)StorageConnectionTypes.PCI_E).ToString());
                                else
                                    list.Add(((int)StorageConnectionTypes.IDE).ToString());
                            }
                        }
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
                            if (queryObj["InterfaceType"].ToString().Equals("IDE"))
                                list.Add(((int)StorageConnectionTypes.SATA).ToString());
                            else if (queryObj["InterfaceType"].ToString().Equals("SCSI"))
                                list.Add(((int)StorageConnectionTypes.PCI_E).ToString());
                            else
                                list.Add(((int)StorageConnectionTypes.IDE).ToString());
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
                if (GetWinVersion().Equals(GenericResources.WINDOWS_10) || GetWinVersion().Equals(GenericResources.WINDOWS_8_1) || GetWinVersion().Equals(GenericResources.WINDOWS_8))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get())
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

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
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
                if (GetWinVersion().Equals(GenericResources.WINDOWS_10) || GetWinVersion().Equals(GenericResources.WINDOWS_8_1) || GetWinVersion().Equals(GenericResources.WINDOWS_8))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get())
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

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
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
                if (GetWinVersion().Equals(GenericResources.WINDOWS_10) || GetWinVersion().Equals(GenericResources.WINDOWS_8_1) || GetWinVersion().Equals(GenericResources.WINDOWS_8))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\wmi", "SELECT * FROM MSStorageDriver_FailurePredictStatus");

                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        if (queryObj.GetPropertyValue("PredictFailure").ToString() == "False")
                            list.Add(GenericResources.OK);
                        else
                            list.Add(GenericResources.PRED_FAIL);
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
                                list.Add(GenericResources.OK);
                            else
                                list.Add(GenericResources.PRED_FAIL);
                        }
                    }
                    return list;
                }
            }
            catch (ManagementException)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE };
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
                if (GetWinVersion().Equals(GenericResources.WINDOWS_10) || GetWinVersion().Equals(GenericResources.WINDOWS_8_1) || GetWinVersion().Equals(GenericResources.WINDOWS_8))
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
                    dresultStr = Convert.ToString(Math.Round(dresult / 1000, 1)) + " " + GenericResources.TB;
                    return dresultStr;
                }
                else
                {
                    dresultStr = dresult + " " + GenericResources.GB;
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
                    return str != string.Empty ? str : GenericResources.NOT_AVAILABLE;
                }
                return GenericResources.NOT_AVAILABLE;
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
                return GenericResources.NOT_AVAILABLE;
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
                        if (GetWinVersion().Equals(GenericResources.WINDOWS_10))
                        {
                            if (queryObj["SMBIOSMemoryType"].ToString().Equals(GenericResources.DDR4_SMBIOS))
                            {
                                mType = GenericResources.DDR4;
                                mSpeed = " " + queryObj["Speed"].ToString() + " " + GenericResources.MHZ;
                            }
                            else if (queryObj["SMBIOSMemoryType"].ToString().Equals(GenericResources.DDR3_SMBIOS))
                            {
                                mType = GenericResources.DDR3;
                                mSpeed = " " + queryObj["Speed"].ToString() + " " + GenericResources.MHZ;
                            }
                            else if (queryObj["SMBIOSMemoryType"].ToString().Equals("3"))
                            {
                                mType = string.Empty;
                                mSpeed = string.Empty;
                            }
                            else
                            {
                                mType = GenericResources.DDR2;
                                try
                                {
                                    mSpeed = " " + queryObj["Speed"].ToString() + " " + GenericResources.MHZ;
                                }
                                catch
                                {
                                    mSpeed = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            if (queryObj["MemoryType"].ToString().Equals(GenericResources.DDR3_MEMORY_TYPE))
                            {
                                mType = GenericResources.DDR3;
                                mSpeed = " " + queryObj["Speed"].ToString() + " " + GenericResources.MHZ;
                            }
                            else if (queryObj["MemoryType"].ToString().Equals("2") || queryObj["MemoryType"].ToString().Equals("0"))
                            {
                                mType = string.Empty;
                                mSpeed = string.Empty;
                            }
                            else
                            {
                                mType = GenericResources.DDR2;
                                try
                                {
                                    mSpeed = " " + queryObj["Speed"].ToString() + " " + GenericResources.MHZ;
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
                return MemSize.ToString() + " " + GenericResources.GB + " " + mType + mSpeed;
            }
            catch (ManagementException e)
            {
                return e.Message;
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
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
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
                    list.Add(UIStrings.FREE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE };
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
                    list.Add(UIStrings.FREE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE };
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
                    if (GetWinVersion().Equals(GenericResources.WINDOWS_10))
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
                    list.Add(UIStrings.FREE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE };
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

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    ramFrequency = queryObj["Speed"].ToString();
                    count++;
                    list.Add(ramFrequency);
                }

                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemoryArray");
                foreach (ManagementObject queryObj in searcher2.Get().Cast<ManagementObject>())
                    numRamFreeSlots = Convert.ToInt32(queryObj["MemoryDevices"]);

                for (int i = 0; i < numRamFreeSlots - count; i++)
                    list.Add(UIStrings.FREE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE };
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

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    ramSerialNumber = queryObj["SerialNumber"].ToString();
                    count++;
                    list.Add(ramSerialNumber);
                }

                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemoryArray");
                foreach (ManagementObject queryObj in searcher2.Get().Cast<ManagementObject>())
                    numRamFreeSlots = Convert.ToInt32(queryObj["MemoryDevices"]);

                for (int i = 0; i < numRamFreeSlots - count; i++)
                    list.Add(UIStrings.FREE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE };
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

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    ramPartNumber = queryObj["PartNumber"].ToString();
                    count++;
                    list.Add(ramPartNumber);
                }

                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemoryArray");
                foreach (ManagementObject queryObj in searcher2.Get().Cast<ManagementObject>())
                    numRamFreeSlots = Convert.ToInt32(queryObj["MemoryDevices"]);

                for (int i = 0; i < numRamFreeSlots - count; i++)
                    list.Add(UIStrings.FREE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE };
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

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    ramManufacturer = queryObj["Manufacturer"].ToString();
                    count++;
                    list.Add(ramManufacturer);
                }

                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemoryArray");
                foreach (ManagementObject queryObj in searcher2.Get().Cast<ManagementObject>())
                    numRamFreeSlots = Convert.ToInt32(queryObj["MemoryDevices"]);

                for (int i = 0; i < numRamFreeSlots - count; i++)
                    list.Add(UIStrings.FREE);
                return list;
            }
            catch (ManagementException e)
            {
                return new List<string>() { e.Message };
            }
            catch (Exception)
            {
                return new List<string>() { GenericResources.NOT_AVAILABLE };
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
                        ? GenericResources.X64
                        : GenericResources.X86;
                }
                return GenericResources.NOT_AVAILABLE;
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
                                operatingSystem = GenericResources.WINDOWS_7;
                            else if (vs.Minor == 2)
                                operatingSystem = GenericResources.WINDOWS_8;
                            else
                                operatingSystem = GenericResources.WINDOWS_8_1;
                            break;
                        case 10:
                            operatingSystem = GenericResources.WINDOWS_10;
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
                    if (GetWinVersion().Equals(GenericResources.WINDOWS_10))
                    {
                        if (Convert.ToInt32(releaseId) <= 2004)
                            return (queryObj["Caption"].ToString() + ", v" + releaseId + ", " + GenericResources.BUILD + " " + queryObj["Version"].ToString() + "." + updateBuildRevision + " (" + GetOSArchAlt() + ")").Replace("Microsoft", string.Empty).Trim();
                        else
                            return (queryObj["Caption"].ToString() + ", v" + displayVersion + ", " + GenericResources.BUILD + " " + queryObj["Version"].ToString() + "." + updateBuildRevision + " (" + GetOSArchAlt() + ")").Replace("Microsoft", string.Empty).Trim();
                    }
                    else if (GetWinVersion().Equals(GenericResources.WINDOWS_8_1) || GetWinVersion().Equals(GenericResources.WINDOWS_8))
                    {
                        return (queryObj["Caption"].ToString() + ", " + GenericResources.BUILD + " " + queryObj["Version"].ToString() + "." + updateBuildRevision + " (" + GetOSArchAlt() + ")").Replace("Microsoft", string.Empty).Trim();
                    }
                    else
                    {
                        return (queryObj["Caption"].ToString() + " " + queryObj["CSDVersion"].ToString() + ", " + GenericResources.BUILD + " " + queryObj["Version"].ToString() + "." + updateBuildRevision + " (" + GetOSArchAlt() + ")").Replace("Microsoft", string.Empty).Trim();
                    }
                }
                return GenericResources.NOT_AVAILABLE;
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
                    if (GetWinVersion().Equals(GenericResources.WINDOWS_10))
                    {
                        if (Convert.ToInt32(releaseId) <= 2004)
                            return releaseId;
                        else
                            return displayVersion;
                    }
                    else if (GetWinVersion().Equals(GenericResources.WINDOWS_8_1) || GetWinVersion().Equals(GenericResources.WINDOWS_8))
                    {
                        return null;
                    }
                    else
                    {
                        return queryObj["CSDVersion"].ToString();
                    }
                }
                return GenericResources.NOT_AVAILABLE;
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
                return GenericResources.NOT_AVAILABLE;
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
                return GenericResources.NOT_AVAILABLE;
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
        public static string GetFwType7()
        {
            try
            {
                _ = GetFirmwareType(string.Empty, "{00000000-0000-0000-0000-000000000000}", IntPtr.Zero, 0);
                return Marshal.GetLastWin32Error() == ERROR_INVALID_FUNCTION ? ((int)FirmwareTypes.BIOS).ToString() : ((int)FirmwareTypes.UEFI).ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary> 
        /// Fetches the firmware type on Windows 8 and later
        /// </summary>
        /// <returns>String with the firmware type code. '1' for UEFI, '0' for BIOS, 'Not determined' for not determined</returns>
        [DllImport("kernel32.dll")]
        private static extern bool GetFirmwareType(ref uint FirmwareType);
        public static string GetFwType()
        {
            try
            {
                if (GetWinVersion().Equals(GenericResources.WINDOWS_10) || GetWinVersion().Equals(GenericResources.WINDOWS_8_1) || GetWinVersion().Equals(GenericResources.WINDOWS_8))
                {
                    uint firmwaretype = 0;
                    if (GetFirmwareType(ref firmwaretype))
                    {
                        if (firmwaretype == 1)
                            return ((int)FirmwareTypes.BIOS).ToString();
                        else if (firmwaretype == 2)
                            return ((int)FirmwareTypes.UEFI).ToString();
                    }

                    return UIStrings.NOT_DETERMINED;
                }
                else
                {
                    return GetFwType7();
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
                    return ((int)SecureBootStates.ENABLED).ToString();
                return ((int)SecureBootStates.DISABLED).ToString();
            }
            catch
            {
                return ((int)SecureBootStates.NOT_SUPPORTED).ToString();
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
                return secBoot.Equals("0") ? ((int)SecureBootStates.DISABLED).ToString() : ((int)SecureBootStates.ENABLED).ToString();
            }
            catch
            {
                return ((int)SecureBootStates.NOT_SUPPORTED).ToString();
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
                if (!GetWinVersion().Equals(GenericResources.WINDOWS_7))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (queryObj["VirtualizationFirmwareEnabled"].ToString().Equals("True"))
                            flag = (int)VirtualizationTechnologyStates.ENABLED;
                        else if (bool.Parse(GetHyperVStatus()))
                            flag = (int)VirtualizationTechnologyStates.ENABLED;
                    }
                    if (flag != (int)VirtualizationTechnologyStates.ENABLED)
                        flag = GetFwType() == ((int)FirmwareTypes.UEFI).ToString() ? (int)VirtualizationTechnologyStates.DISABLED : (int)VirtualizationTechnologyStates.NOT_SUPPORTED;
                }
                if (flag == (int)VirtualizationTechnologyStates.ENABLED)
                    return ((int)SecureBootStates.ENABLED).ToString();
                else if (flag == (int)VirtualizationTechnologyStates.DISABLED)
                    return ((int)SecureBootStates.DISABLED).ToString();
                else
                    return ((int)SecureBootStates.NOT_SUPPORTED).ToString();
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
                        str = ((int)TpmTypes.v1_2).ToString();
                    else if (specVersion.Substring(0, 3).Equals(GenericResources.TPM_2_0_NAME))
                        str = ((int)TpmTypes.v2_0).ToString();
                    return str;
                }
                else
                {
                    return ((int)TpmTypes.N_A).ToString();
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
                if (GetWinVersion().Equals(GenericResources.WINDOWS_10) || GetWinVersion().Equals(GenericResources.WINDOWS_8_1) || GetWinVersion().Equals(GenericResources.WINDOWS_8))
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage", "SELECT * FROM MSFT_PhysicalDisk");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (queryObj["DeviceId"].ToString().Equals("0"))
                        {
                            if (queryObj["BusType"].ToString().Equals(GenericResources.WMI_PCIE))
                                return ((int)MediaOperationTypes.NVMe).ToString();
                            else if (queryObj["BusType"].ToString().Equals(GenericResources.WMI_SATA))
                                return ((int)MediaOperationTypes.AHCI).ToString();
                        }
                    }
                    return ((int)MediaOperationTypes.IDE_RAID).ToString();
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
                                    return ((int)MediaOperationTypes.AHCI).ToString();
                                else if (queryObj["InterfaceType"].ToString().Equals("SCSI"))
                                    return ((int)MediaOperationTypes.NVMe).ToString();
                            }
                        }
                    }
                    return ((int)MediaOperationTypes.IDE_RAID).ToString();
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
                            if (group.Key == GenericResources.HDD)
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
                                if (group.Key == GenericResources.HDD)
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