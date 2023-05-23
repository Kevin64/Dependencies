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
    ///<summary>Class for handling various hardware and setting detection tasks</summary>
    public static class HardwareInfo
    {
        ///<summary>Fetches the CPU information, including the number of cores/threads</summary>
        ///<returns>String with the CPU information</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetProcessorInfo()
        {
            string Id = string.Empty;
            string logical = string.Empty;

            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_ComputerSystem");

            try
            {
                foreach (ManagementBaseObject item in searcher.Get())
                {
                    logical = item["NumberOfLogicalProcessors"].ToString();
                }

                foreach (ManagementObject queryObj in moc.Cast<ManagementObject>())
                {
                    Id = queryObj.Properties["name"].Value.ToString() + " " + queryObj.Properties["CurrentClockSpeed"].Value.ToString()
                       + " " + ConstantsDLL.Properties.Resources.FREQUENCY + " (" + queryObj.Properties["NumberOfCores"].Value.ToString() + "C/" + logical + "T)";
                    break;
                }
                Id = Id.Replace("(R)", string.Empty);
                Id = Id.Replace("(TM)", string.Empty);
                Id = Id.Replace("(tm)", string.Empty);
                return Id;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the GPU information</summary>
        ///<returns>String with the GPU information</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetVideoCardInfo()
        {
            string gpuname = string.Empty;
            string gpuramStr;
            double gpuram;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_VideoController");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (!queryObj["Caption"].ToString().Equals("Microsoft Remote Display Adapter"))
                    {
                        if (queryObj["MaxRefreshRate"] != null)
                        {
                            gpuram = Convert.ToInt64(queryObj["AdapterRAM"]);
                            gpuram = Math.Round(gpuram / 1048576, 0);
                            gpuramStr = Math.Ceiling(Math.Log10(gpuram)) > 3
                                ? Convert.ToString(Math.Round(gpuram / 1024, 1)) + " " + ConstantsDLL.Properties.Resources.GB
                                : gpuram + " " + ConstantsDLL.Properties.Resources.MB;
                            gpuname = queryObj["Caption"].ToString() + " (" + gpuramStr + ")";
                        }
                    }
                }
                gpuname = gpuname.Replace("(R)", string.Empty);
                gpuname = gpuname.Replace("(TM)", string.Empty);
                gpuname = gpuname.Replace("(tm)", string.Empty);
                return gpuname;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the operation mode that the storage is running (IDE/AHCI/NVMe)</summary>
        ///<returns>String with the current media operation mode</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetMediaOperationMode()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_SCSIController");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (queryObj["Name"].ToString().Contains("NVM"))
                    {
                        return ConstantsDLL.Properties.Resources.NVME;
                    }
                }

                searcher = new ManagementObjectSearcher("select * from Win32_IDEController");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if ((queryObj["Name"].ToString().Contains(ConstantsDLL.Properties.Resources.AHCI) || queryObj["Name"].ToString().Contains(ConstantsDLL.Properties.Resources.SATA)) && !queryObj["Name"].ToString().Contains(ConstantsDLL.Properties.Resources.RAID))
                    {
                        return ConstantsDLL.Properties.Resources.AHCI;
                    }
                }

                return ConstantsDLL.Properties.Resources.IDE;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the type of drive the system has (SSD or HDD), and the quantity of each</summary>
        ///<returns>String with the SSD/HDD amount</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetStorageType()
        {
            double dresult;
            string dresultStr;

            try
            {
                if (GetWinVersion().Equals(ConstantsDLL.Properties.Resources.WINDOWS_10) || GetWinVersion().Equals(ConstantsDLL.Properties.Resources.WINDOWS_8_1) || GetWinVersion().Equals(ConstantsDLL.Properties.Resources.WINDOWS_8))
                {
                    int size = 10, i = 0;
                    string[] type = new string[size];
                    string[] bytesHDD = new string[size];
                    string[] bytesSSD = new string[size];
                    string concat, msftName = "Msft Virtual Disk";

                    ManagementScope scope = new ManagementScope(@"\\.\root\microsoft\windows\storage");
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from MSFT_PhysicalDisk");
                    scope.Connect();
                    searcher.Scope = scope;


                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (!Convert.ToString(queryObj["FriendlyName"]).Equals(msftName))
                        {
                            if ((Convert.ToInt16(queryObj["MediaType"]).Equals(3) || Convert.ToInt16(queryObj["MediaType"]).Equals(4) || Convert.ToInt16(queryObj["MediaType"]).Equals(0)) && !Convert.ToInt16(queryObj["BusType"]).Equals(7))
                            {
                                dresult = Convert.ToInt64(queryObj.Properties["Size"].Value.ToString());
                                dresult = Math.Round(dresult / 1000000000, 0);

                                dresultStr = Math.Log10(dresult) > 2.9999
                                    ? Convert.ToString(Math.Round(dresult / 1000, 1)) + " " + ConstantsDLL.Properties.Resources.TB
                                    : dresult + " " + ConstantsDLL.Properties.Resources.GB;

                                switch (Convert.ToInt16(queryObj["MediaType"]))
                                {
                                    case 3:
                                        type[i] = ConstantsDLL.Properties.Resources.HDD;
                                        bytesHDD[i] = dresultStr;
                                        i++;
                                        break;
                                    case 4:
                                        type[i] = ConstantsDLL.Properties.Resources.SSD;
                                        bytesSSD[i] = dresultStr;
                                        i++;
                                        break;
                                    case 0:
                                        type[i] = ConstantsDLL.Properties.Resources.HDD;
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

                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                        {
                            dresult = Convert.ToInt64(queryObj.Properties["Size"].Value.ToString());
                            dresult = Math.Round(dresult / 1000000000, 0);

                            dresultStr = Math.Log10(dresult) > 2.9999
                                ? Convert.ToString(Math.Round(dresult / 1000, 1)) + " " + ConstantsDLL.Properties.Resources.TB
                                : dresult + " " + ConstantsDLL.Properties.Resources.GB;
                            type[i] = ConstantsDLL.Properties.Resources.HDD;
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
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Auxiliary method for GetStorageType method, that groups the same objects in a list and counts them</summary>
        ///<returns>String with the SSD/HDD amount</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
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
                            if (group.Key == ConstantsDLL.Properties.Resources.HDD)
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
                                if (group.Key == ConstantsDLL.Properties.Resources.HDD)
                                {
                                    result += " (" + string.Join(", ", sizesHDD) + ")" + ", ";
                                }
                                else
                                {
                                    result += " (" + string.Join(", ", sizesSSD) + ")" + ", ";
                                }

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

        ///<summary>Fetches the SSD/HDD total size (sums all drives sizes)</summary>
        ///<returns>String with the SSD/HDD total size</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetStorageSize()
        {
            int i = 0;
            double dresult = 0;
            string dresultStr;

            try
            {
                if (GetWinVersion().Equals(ConstantsDLL.Properties.Resources.WINDOWS_10) || GetWinVersion().Equals(ConstantsDLL.Properties.Resources.WINDOWS_8_1) || GetWinVersion().Equals(ConstantsDLL.Properties.Resources.WINDOWS_8))
                {
                    ManagementScope scope = new ManagementScope(@"\\.\root\microsoft\windows\storage");
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from MSFT_PhysicalDisk");
                    scope.Connect();
                    searcher.Scope = scope;

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
                        {
                            dresult += Convert.ToInt64(queryObj.Properties["Size"].Value.ToString());
                        }
                    }
                }
                else
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_DiskDrive");

                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (!queryObj.Properties["MediaType"].Value.ToString().Equals("External hard disk media"))
                        {
                            dresult += Convert.ToInt64(queryObj.Properties["Size"].Value.ToString());
                        }
                    }
                }
                dresult = Math.Round(dresult / 1000000000, 0);
                if (Math.Log10(dresult) > 2.9999)
                {
                    dresultStr = Convert.ToString(Math.Round(dresult / 1000, 1)) + " " + ConstantsDLL.Properties.Resources.TB;
                    return dresultStr;
                }
                else
                {
                    dresultStr = dresult + " " + ConstantsDLL.Properties.Resources.GB;
                    return dresultStr;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the primary MAC Address</summary>
        ///<returns>String with the primary MAC Address, or 'null' otherwise</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetMacAddress()
        {
            string MACAddress = string.Empty;

            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            try
            {
                foreach (ManagementObject mo in moc.Cast<ManagementObject>())
                {
                    string[] gat = (string[])mo["DefaultIPGateway"];
                    if (MACAddress == string.Empty)
                    {
                        if ((bool)mo["IPEnabled"] == true && gat != null)
                        {
                            MACAddress = mo["MacAddress"].ToString();
                        }
                    }

                    mo.Dispose();
                }
                return MACAddress != string.Empty ? MACAddress : null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the primary IP address</summary>
        ///<returns>String with the primary IP address, or 'null' otherwise</returns>
        public static string GetIpAddress()
        {
            string[] IPAddress = null;

            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            try
            {
                foreach (ManagementObject mo in moc.Cast<ManagementObject>())
                {
                    string[] gat = (string[])mo["DefaultIPGateway"];
                    if ((bool)mo["IPEnabled"] == true && gat != null)
                    {
                        IPAddress = (string[])mo["IPAddress"];
                    }

                    mo.Dispose();
                }
                return IPAddress[0];
            }
            catch
            {
                return null;
            }
        }

        ///<summary>Fetches the computer's manufacturer</summary>
        ///<returns>String with the computer's manufacturer, or 'Unknown' otherwise</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetBrand()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_ComputerSystem");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj.GetPropertyValue("Manufacturer").ToString();
                }

                return ConstantsDLL.Properties.Strings.UNKNOWN;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the computer's manufacturer (alternative method)</summary>
        ///<returns>String with the computer's manufacturer, or 'Unknown' otherwise</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetBrandAlt()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_BaseBoard");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj.GetPropertyValue("Manufacturer").ToString();
                }

                return ConstantsDLL.Properties.Strings.UNKNOWN;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the computer's model</summary>
        ///<returns>String with the computer's model, or 'Unknown' otherwise</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetModel()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_ComputerSystem");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj.GetPropertyValue("Model").ToString();
                }

                return ConstantsDLL.Properties.Strings.UNKNOWN;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the computer's model (alternative method)</summary>
        ///<returns>String with the computer's model, or 'Unknown' otherwise</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetModelAlt()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_BaseBoard");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj.GetPropertyValue("Product").ToString();
                }

                return ConstantsDLL.Properties.Strings.UNKNOWN;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the motherboard serial number</summary>
        ///<returns>String with the motherboard serial number, or 'Unknown' otherwise</returns>
        public static string GetSerialNumber()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_BaseBoard");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj.GetPropertyValue("SerialNumber").ToString();
                }
                return ConstantsDLL.Properties.Strings.UNKNOWN;
            }
            catch
            {
                return ConstantsDLL.Properties.Strings.UNKNOWN;
            }
        }

        ///<summary>Fetches the amount of RAM of the system</summary>
        ///<returns>String with the amount of RAM of the system</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetRam()
        {
            long MemSize = 0;
            long mCap;
            string mType = string.Empty;
            string mSpeed = string.Empty;

            ManagementScope scope = new ManagementScope();
            ObjectQuery objQuery = new ObjectQuery("select * from Win32_PhysicalMemory");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, objQuery);
            ManagementObjectCollection moc = searcher.Get();

            try
            {
                foreach (ManagementObject queryObj in moc.Cast<ManagementObject>())
                {
                    if (!Convert.ToString(queryObj["DeviceLocator"]).Contains(ConstantsDLL.Properties.Resources.SYSTEM_ROM))
                    {
                        mCap = Convert.ToInt64(queryObj["Capacity"]);
                        MemSize += mCap;
                        if (GetWinVersion().Equals(ConstantsDLL.Properties.Resources.WINDOWS_10))
                        {
                            if (queryObj["SMBIOSMemoryType"].ToString().Equals(ConstantsDLL.Properties.Resources.DDR4_SMBIOS))
                            {
                                mType = ConstantsDLL.Properties.Resources.DDR4;
                                mSpeed = " " + queryObj["Speed"].ToString() + ConstantsDLL.Properties.Resources.FREQUENCY;
                            }
                            else if (queryObj["SMBIOSMemoryType"].ToString().Equals(ConstantsDLL.Properties.Resources.DDR3_SMBIOS))
                            {
                                mType = ConstantsDLL.Properties.Resources.DDR3;
                                mSpeed = " " + queryObj["Speed"].ToString() + ConstantsDLL.Properties.Resources.FREQUENCY;
                            }
                            else if (queryObj["SMBIOSMemoryType"].ToString().Equals("3"))
                            {
                                mType = string.Empty;
                                mSpeed = string.Empty;
                            }
                            else
                            {
                                mType = ConstantsDLL.Properties.Resources.DDR2;
                                try
                                {
                                    mSpeed = " " + queryObj["Speed"].ToString() + ConstantsDLL.Properties.Resources.FREQUENCY;
                                }
                                catch
                                {
                                    mSpeed = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            if (queryObj["MemoryType"].ToString().Equals(ConstantsDLL.Properties.Resources.DDR3_MEMORY_TYPE))
                            {
                                mType = ConstantsDLL.Properties.Resources.DDR3;
                                mSpeed = " " + queryObj["Speed"].ToString() + ConstantsDLL.Properties.Resources.FREQUENCY;
                            }
                            else if (queryObj["MemoryType"].ToString().Equals("2") || queryObj["MemoryType"].ToString().Equals("0"))
                            {
                                mType = string.Empty;
                                mSpeed = string.Empty;
                            }
                            else
                            {
                                mType = ConstantsDLL.Properties.Resources.DDR2;
                                try
                                {
                                    mSpeed = " " + queryObj["Speed"].ToString() + ConstantsDLL.Properties.Resources.FREQUENCY;
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
                return MemSize.ToString() + " " + ConstantsDLL.Properties.Resources.GB + " " + mType + mSpeed;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the amount of RAM of the system (alternative method)</summary>
        ///<returns>String with the amount of RAM of the system</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetRamAlt()
        {
            double MemSize = 0;
            long mCap;
            string MemSizeStr;

            ManagementScope scope = new ManagementScope();
            ObjectQuery objQuery = new ObjectQuery("select * from Win32_PhysicalMemory");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, objQuery);
            ManagementObjectCollection moc = searcher.Get();

            try
            {
                foreach (ManagementObject queryObj in moc.Cast<ManagementObject>())
                {
                    if (!Convert.ToString(queryObj["DeviceLocator"]).Contains(ConstantsDLL.Properties.Resources.SYSTEM_ROM))
                    {
                        mCap = Convert.ToInt64(queryObj["Capacity"]);
                        MemSize += mCap;
                    }
                }
                MemSize = MemSize / 1024 / 1024 / 1024;
                MemSizeStr = MemSize.ToString();
                return MemSizeStr;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the number of RAM slots on the system</summary>
        ///<returns>String with the number of RAM slots</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetNumRamSlots()
        {
            int MemSlots = 0;

            ManagementScope scope = new ManagementScope();
            ObjectQuery objQuery = new ObjectQuery("select * from Win32_PhysicalMemoryArray");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, objQuery);
            ManagementObjectCollection moc = searcher.Get();

            try
            {
                foreach (ManagementObject queryObj in moc.Cast<ManagementObject>())
                {
                    if (Convert.ToString(queryObj["Tag"]).Equals("Physical Memory Array 0"))
                    {
                        MemSlots = Convert.ToInt32(queryObj["MemoryDevices"]);
                    }
                }

                return MemSlots.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the number of free RAM slots on the system</summary>
        ///<returns>String with the number of free RAM slots</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetNumFreeRamSlots(int num)
        {
            int i = 0;
            string[] MemSlotsUsed = new string[num];

            ManagementScope scope = new ManagementScope();
            ObjectQuery objQuery = new ObjectQuery("select * from Win32_PhysicalMemory");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, objQuery);
            ManagementObjectCollection moc = searcher.Get();

            try
            {
                foreach (ManagementObject queryObj in moc.Cast<ManagementObject>())
                {
                    if (!Convert.ToString(queryObj["DeviceLocator"]).Contains(ConstantsDLL.Properties.Resources.SYSTEM_ROM))
                    {
                        MemSlotsUsed[i] = Convert.ToString(queryObj["DeviceLocator"]);
                        i++;
                    }
                }
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the default gateway of the NIC</summary>
        ///<returns>String with the NIC's gateway</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetDefaultIpGateway()
        {
            string gateway = string.Empty;

            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            try
            {
                foreach (ManagementObject queryObj in moc.Cast<ManagementObject>())
                {
                    if (gateway == string.Empty)
                    {
                        if ((bool)queryObj["IPEnabled"] == true)
                        {
                            gateway = queryObj["DefaultIPGateway"].ToString();
                        }
                    }

                    queryObj.Dispose();
                }
                gateway = gateway.Replace(":", string.Empty);
                return gateway;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the OS architecture</summary>
        ///<returns>String with the OS architecture. '64' for x64, '32' for x86</returns>
        public static string GetOSArch()
        {
            bool is64bit = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
            return is64bit ? ConstantsDLL.Properties.Resources.ARCH64 : ConstantsDLL.Properties.Resources.ARCH32;
        }

        ///<summary>Fetches the OS architecture (alternative method)</summary>
        ///<returns>String with the OS architecture. '64-bit' for x64, '32-bit' for x86</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetOSArchAlt()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_OperatingSystem");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    try
                    {
                        return queryObj.GetPropertyValue("OSArchitecture").ToString();
                    }
                    catch
                    {
                    }
                }
                return ConstantsDLL.Properties.Strings.UNKNOWN;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the NT version</summary>
        ///<returns>String with the NT version. '7' for Windows 7, '8' for Windows 8, '8.1' for Windows 8.1, '10' for Windows 10</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
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
                            operatingSystem = vs.Minor == 1
                                ? ConstantsDLL.Properties.Resources.WINDOWS_7
                                : vs.Minor == 2 ? ConstantsDLL.Properties.Resources.WINDOWS_8 : ConstantsDLL.Properties.Resources.WINDOWS_8_1;

                            break;
                        case 10:
                            operatingSystem = ConstantsDLL.Properties.Resources.WINDOWS_10;
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

        ///<summary>Fetches the operating system information</summary>
        ///<returns>String with the operating system information, or 'Unknown' otherwise</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetOSString()
        {
            string displayVersion = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DisplayVersion", string.Empty).ToString();
            string releaseId = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "releaseId", string.Empty).ToString();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return GetWinVersion().Equals(ConstantsDLL.Properties.Resources.WINDOWS_10)
                        ? Convert.ToInt32(releaseId) <= 2004
                            ? (((string)queryObj["Caption"]).Trim() + ", v" + releaseId + ", " + ConstantsDLL.Properties.Resources.BUILD + " " + (string)queryObj["Version"] + ", " + (string)queryObj["OSArchitecture"]).Substring(10)
                            : (((string)queryObj["Caption"]).Trim() + ", v" + displayVersion + ", " + ConstantsDLL.Properties.Resources.BUILD + " " + (string)queryObj["Version"] + ", " + (string)queryObj["OSArchitecture"]).Substring(10)
                        : (((string)queryObj["Caption"]).Trim() + " " + (string)queryObj["CSDVersion"] + ", " + ConstantsDLL.Properties.Resources.BUILD + " " + (string)queryObj["Version"] + ", " + (string)queryObj["OSArchitecture"]).Substring(10);
                }
                return ConstantsDLL.Properties.Strings.UNKNOWN;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the OS build number</summary>
        ///<returns>String with the OS build number, or 'Unknown' otherwise</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetOSVersion()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_OperatingSystem");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj.GetPropertyValue("Version").ToString();
                }

                return ConstantsDLL.Properties.Strings.UNKNOWN;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        ///<summary>Fetches the computer's hostname</summary>
        ///<returns>String with the hostname</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetHostname()
        {
            string info = string.Empty;

            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();

            try
            {
                foreach (ManagementObject queryObj in moc.Cast<ManagementObject>())
                {
                    info = (string)queryObj["Name"];
                }

                return info;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the firmware version</summary>
        ///<returns>String with the firmware version</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetFirmwareVersion()
        {
            string biosVersion = string.Empty;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_BIOS");
            ManagementObjectCollection moc = searcher.Get();

            try
            {
                foreach (ManagementObject queryObj in moc.Cast<ManagementObject>())
                {
                    biosVersion = (string)queryObj["SMBIOSBIOSVersion"];
                }

                return biosVersion;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the firmware type on Windows 7</summary>
        ///<returns>String with the firmware type code. '1' for UEFI, '0' for BIOS</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
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

                return Marshal.GetLastWin32Error() == ERROR_INVALID_FUNCTION ? ConstantsDLL.Properties.Resources.BIOS : ConstantsDLL.Properties.Resources.UEFI;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the firmware type on Windows 8 and later</summary>
        ///<returns>String with the firmware type code. '1' for UEFI, '0' for BIOS, 'Not determined' for not determined</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        [DllImport("kernel32.dll")]
        private static extern bool GetFirmwareType(ref uint FirmwareType);
        public static string GetFwType()
        {
            try
            {
                if (GetWinVersion().Equals(ConstantsDLL.Properties.Resources.WINDOWS_10) || GetWinVersion().Equals(ConstantsDLL.Properties.Resources.WINDOWS_8_1) || GetWinVersion().Equals(ConstantsDLL.Properties.Resources.WINDOWS_8))
                {
                    uint firmwaretype = 0;
                    if (GetFirmwareType(ref firmwaretype))
                    {
                        if (firmwaretype == 1)
                        {
                            return ConstantsDLL.Properties.Resources.BIOS;
                        }
                        else if (firmwaretype == 2)
                        {
                            return ConstantsDLL.Properties.Resources.UEFI;
                        }
                    }
                    return ConstantsDLL.Properties.Strings.NOT_DETERMINED;
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

        ///<summary>Fetches the Secure Boot status (alternative method)</summary>
        ///<returns>String with the Secure Boot status. '2' for activated, '1' for deactivated, '0' for not supported</returns>
        public static string GetSecureBootAlt()
        {
            try
            {
                PowerShell PowerShellInst = PowerShell.Create();
                _ = PowerShellInst.AddScript("Confirm-SecureBootUEFI");
                Collection<PSObject> PSOutput = PowerShellInst.Invoke();

                foreach (PSObject queryObj in PSOutput)
                {
                    return ConstantsDLL.Properties.Resources.ACTIVATED;
                }

                return ConstantsDLL.Properties.Resources.DEACTIVATED;
            }
            catch
            {
                return ConstantsDLL.Properties.Resources.NOT_SUPPORTED;
            }
        }

        ///<summary>Fetches the Secure Boot status</summary>
        ///<returns>String with the Secure Boot status. '2' for activated, '1' for deactivated, '0' for not supported</returns>
        public static string GetSecureBoot()
        {
            try
            {
                string secBoot = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecureBoot\State", "UEFISecureBootEnabled", 0).ToString();
                return secBoot.Equals("0") ? ConstantsDLL.Properties.Resources.DEACTIVATED : ConstantsDLL.Properties.Resources.ACTIVATED;
            }
            catch
            {
                return ConstantsDLL.Properties.Resources.NOT_SUPPORTED;
            }
        }

        ///<summary>Fetches the Virtualization Technology status</summary>
        ///<returns>String with the Virtualization Technology status. '2' for activated, '1' for deactivated, '0' for not supported</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetVirtualizationTechnology()
        {
            int flag = 0;

            try
            {
                if (!GetWinVersion().Equals(ConstantsDLL.Properties.Resources.WINDOWS_7))
                {
                    ManagementClass mc = new ManagementClass("win32_processor");
                    ManagementObjectCollection moc = mc.GetInstances();

                    foreach (ManagementObject queryObj in moc.Cast<ManagementObject>())
                    {
                        if (queryObj["VirtualizationFirmwareEnabled"].ToString().Equals("True"))
                        {
                            flag = 2;
                        }
                        else if (bool.Parse(GetHyperVStatus()))
                        {
                            flag = 2;
                        }
                    }
                    if (flag != 2)
                    {
                        flag = GetFwType() == ConstantsDLL.Properties.Resources.UEFI ? 1 : 0;
                    }
                }

                return flag == 2
                    ? ConstantsDLL.Properties.Resources.ACTIVATED
                    : flag == 1 ? ConstantsDLL.Properties.Resources.DEACTIVATED : ConstantsDLL.Properties.Resources.NOT_SUPPORTED;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        ///<summary>Fetches the Hyper-V installation status</summary>
        ///<returns>String with the Hyper-V status. 'true' for true, 'false' for false</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetHyperVStatus()
        {
            string featureName;
            uint featureToggle;

            ManagementClass mc = new ManagementClass("Win32_OptionalFeature");
            ManagementObjectCollection moc = mc.GetInstances();

            try
            {
                foreach (ManagementObject queryObj in moc.Cast<ManagementObject>())
                {
                    featureName = (string)queryObj.Properties["Name"].Value;
                    featureToggle = (uint)queryObj.Properties["InstallState"].Value;

                    if ((featureName.Equals("Microsoft-Hyper-V") && featureToggle.Equals(1)) || (featureName.Equals("Microsoft-Hyper-V-Hypervisor") && featureToggle.Equals(1)) || (featureName.Equals("Containers-DisposableClientVM") && featureToggle.Equals(1)))
                    {
                        return ConstantsDLL.Properties.Resources.TRUE;
                    }
                }
                return ConstantsDLL.Properties.Resources.FALSE;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the S.M.A.R.T. status</summary>
        ///<returns>String with the S.M.A.R.T. status. 'OK' for ok, everything else for a problem</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetSMARTStatus()
        {
            string statusCaption, statusValue;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * from Win32_DiskDrive");
            ManagementObjectCollection moc = searcher.Get();

            try
            {
                foreach (ManagementObject queryObj in moc.Cast<ManagementObject>())
                {
                    statusCaption = (string)queryObj.Properties["Caption"].Value;
                    statusValue = (string)queryObj.Properties["Status"].Value;
                    if (statusValue == ConstantsDLL.Properties.Resources.PRED_FAIL)
                    {
                        return statusCaption;
                    }
                }
                return ConstantsDLL.Properties.Resources.OK;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        ///<summary>Fetches the TPM version</summary>
        ///<returns>String with the TPM version code. '0' for none, '1' for 1.2, '2' for 2.0</returns>
        ///<exception cref="System.Exception">Thrown when there is a problem with the query</exception>
        public static string GetTPMStatus()
        {
            string specVersion = string.Empty;
            string str = string.Empty;
            ManagementScope scope = new ManagementScope(@"\\.\root\cimv2\Security\MicrosoftTPM");
            ObjectQuery query = new ObjectQuery("select * from Win32_Tpm");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    specVersion = queryObj.Properties["SpecVersion"].Value.ToString();
                }

                if (specVersion != string.Empty)
                {
                    if (specVersion.Substring(0, 3).Equals(ConstantsDLL.Properties.Resources.TPM_1_2_NAME))
                    {
                        str = ConstantsDLL.Properties.Resources.TPM_1_2;
                    }
                    else if (specVersion.Substring(0, 3).Equals(ConstantsDLL.Properties.Resources.TPM_2_0_NAME))
                    {
                        str = ConstantsDLL.Properties.Resources.TPM_2_0;
                    }
                    return str;
                }
                else
                {
                    return ConstantsDLL.Properties.Resources.NO_TPM;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}