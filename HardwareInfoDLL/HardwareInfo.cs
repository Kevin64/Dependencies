using ConstantsDLL;
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
    public static class HardwareInfo
    {
        //Fetches the CPU information, including the number of cores/threads
        public static string GetProcessorCores()
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
                       + " " + ConstantsDLL.Properties.Resources.frequency + " (" + queryObj.Properties["NumberOfCores"].Value.ToString() + "C/" + logical + "T)";
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

        //Fetches the GPU information
        public static string GetGPUInfo()
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
                                ? Convert.ToString(Math.Round(gpuram / 1024, 1)) + " " + ConstantsDLL.Properties.Resources.gb
                                : gpuram + " " + ConstantsDLL.Properties.Resources.mb;
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

        //Fetches the operation mode that the storage is running (IDE/AHCI/NVMe)
        public static string GetStorageOperation()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_SCSIController");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (queryObj["Name"].ToString().Contains("NVM"))
                    {
                        return ConstantsDLL.Properties.Resources.nvme;
                    }
                }

                searcher = new ManagementObjectSearcher("select * from Win32_IDEController");

                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if ((queryObj["Name"].ToString().Contains(ConstantsDLL.Properties.Resources.ahci) || queryObj["Name"].ToString().Contains(ConstantsDLL.Properties.Resources.sata)) && !queryObj["Name"].ToString().Contains(ConstantsDLL.Properties.Resources.raid))
                    {
                        return ConstantsDLL.Properties.Resources.ahci;
                    }
                }

                return ConstantsDLL.Properties.Resources.ide;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Fetches the type of drive the system has (SSD or HDD), and the quantity of each
        public static string GetStorageType()
        {
            double dresult;
            string dresultStr;

            try
            {
                if (GetOSInfoAux().Equals(ConstantsDLL.Properties.Resources.windows10) || GetOSInfoAux().Equals(ConstantsDLL.Properties.Resources.windows8_1) || GetOSInfoAux().Equals(ConstantsDLL.Properties.Resources.windows8))
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
                                    ? Convert.ToString(Math.Round(dresult / 1000, 1)) + " " + ConstantsDLL.Properties.Resources.tb
                                    : dresult + " " + ConstantsDLL.Properties.Resources.gb;

                                switch (Convert.ToInt16(queryObj["MediaType"]))
                                {
                                    case 3:
                                        type[i] = ConstantsDLL.Properties.Resources.hdd;
                                        bytesHDD[i] = dresultStr;
                                        i++;
                                        break;
                                    case 4:
                                        type[i] = ConstantsDLL.Properties.Resources.ssd;
                                        bytesSSD[i] = dresultStr;
                                        i++;
                                        break;
                                    case 0:
                                        type[i] = ConstantsDLL.Properties.Resources.hdd;
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
                                ? Convert.ToString(Math.Round(dresult / 1000, 1)) + " " + ConstantsDLL.Properties.Resources.tb
                                : dresult + " " + ConstantsDLL.Properties.Resources.gb;
                            type[i] = ConstantsDLL.Properties.Resources.hdd;
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

        //Auxiliary method for GetStorageType method, that groups the same objects in a list and counts them
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
                            if (group.Key == ConstantsDLL.Properties.Resources.hdd)
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
                                if (group.Key == ConstantsDLL.Properties.Resources.hdd)
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

        //Fetches the SSD/HDD total size (sums all drives sizes)
        public static string GetHDSize()
        {
            int i = 0;
            double dresult = 0;
            string dresultStr;

            try
            {
                if (GetOSInfoAux().Equals(ConstantsDLL.Properties.Resources.windows10) || GetOSInfoAux().Equals(ConstantsDLL.Properties.Resources.windows8_1) || GetOSInfoAux().Equals(ConstantsDLL.Properties.Resources.windows8))
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
                    dresultStr = Convert.ToString(Math.Round(dresult / 1000, 1)) + " " + ConstantsDLL.Properties.Resources.tb;
                    return dresultStr;
                }
                else
                {
                    dresultStr = dresult + " " + ConstantsDLL.Properties.Resources.gb;
                    return dresultStr;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Fetches the primary MAC Address
        public static string GetMACAddress()
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

        //Fetches the primary IP address
        public static string GetIPAddress()
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

        //Fetches the computer's manufacturer
        public static string GetBoardMaker()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_ComputerSystem");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj.GetPropertyValue("Manufacturer").ToString();
                }

                return StringsAndConstants.unknown;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Fetches the computer's manufacturer (alternative method)
        public static string GetBoardMakerAlt()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_BaseBoard");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj.GetPropertyValue("Manufacturer").ToString();
                }

                return StringsAndConstants.unknown;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Fetches the computer's model
        public static string GetModel()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_ComputerSystem");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj.GetPropertyValue("Model").ToString();
                }

                return StringsAndConstants.unknown;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Fetches the computer's model (alternative method)
        public static string GetModelAlt()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_BaseBoard");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj.GetPropertyValue("Product").ToString();
                }

                return StringsAndConstants.unknown;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Fetches the motherboard serial number
        public static string GetBoardProductId()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_BaseBoard");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj.GetPropertyValue("SerialNumber").ToString();
                }

                return StringsAndConstants.unknown;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Fetches the amount of RAM of the system
        public static string GetPhysicalMemory()
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
                    if (!Convert.ToString(queryObj["DeviceLocator"]).Contains(ConstantsDLL.Properties.Resources.systemRom))
                    {
                        mCap = Convert.ToInt64(queryObj["Capacity"]);
                        MemSize += mCap;
                        if (GetOSInfoAux().Equals(ConstantsDLL.Properties.Resources.windows10))
                        {
                            if (queryObj["SMBIOSMemoryType"].ToString().Equals(ConstantsDLL.Properties.Resources.ddr4smbios))
                            {
                                mType = ConstantsDLL.Properties.Resources.ddr4;
                                mSpeed = " " + queryObj["Speed"].ToString() + ConstantsDLL.Properties.Resources.frequency;
                            }
                            else if (queryObj["SMBIOSMemoryType"].ToString().Equals(ConstantsDLL.Properties.Resources.ddr3smbios))
                            {
                                mType = ConstantsDLL.Properties.Resources.ddr3;
                                mSpeed = " " + queryObj["Speed"].ToString() + ConstantsDLL.Properties.Resources.frequency;
                            }
                            else if (queryObj["SMBIOSMemoryType"].ToString().Equals("3"))
                            {
                                mType = string.Empty;
                                mSpeed = string.Empty;
                            }
                            else
                            {
                                mType = ConstantsDLL.Properties.Resources.ddr2;
                                try
                                {
                                    mSpeed = " " + queryObj["Speed"].ToString() + ConstantsDLL.Properties.Resources.frequency;
                                }
                                catch
                                {
                                    mSpeed = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            if (queryObj["MemoryType"].ToString().Equals(ConstantsDLL.Properties.Resources.ddr3memorytype))
                            {
                                mType = ConstantsDLL.Properties.Resources.ddr3;
                                mSpeed = " " + queryObj["Speed"].ToString() + ConstantsDLL.Properties.Resources.frequency;
                            }
                            else if (queryObj["MemoryType"].ToString().Equals("2") || queryObj["MemoryType"].ToString().Equals("0"))
                            {
                                mType = string.Empty;
                                mSpeed = string.Empty;
                            }
                            else
                            {
                                mType = ConstantsDLL.Properties.Resources.ddr2;
                                try
                                {
                                    mSpeed = " " + queryObj["Speed"].ToString() + ConstantsDLL.Properties.Resources.frequency;
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
                return MemSize.ToString() + " " + ConstantsDLL.Properties.Resources.gb + " " + mType + mSpeed;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Fetches the amount of RAM of the system (alternative method)
        public static string GetPhysicalMemoryAlt()
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
                    if (!Convert.ToString(queryObj["DeviceLocator"]).Contains(ConstantsDLL.Properties.Resources.systemRom))
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

        //Fetches the number of RAM slots on the system
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

        //Fetches the number of free RAM slots on the system
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
                    if (!Convert.ToString(queryObj["DeviceLocator"]).Contains(ConstantsDLL.Properties.Resources.systemRom))
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

        //Fetches the default gateway of the NIC
        public static string GetDefaultIPGateway()
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

        //Fetches the OS architecture
        public static string GetOSArch()
        {
            bool is64bit = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
            return is64bit ? ConstantsDLL.Properties.Resources.arch64 : ConstantsDLL.Properties.Resources.arch32;
        }

        //Fetches the OS architecture (alternative method)
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
                return StringsAndConstants.unknown;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Fetches the NT version
        public static string GetOSInfoAux()
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
                            {
                                operatingSystem = ConstantsDLL.Properties.Resources.windows7;
                            }
                            else
                            {
                                operatingSystem = vs.Minor == 2 ? ConstantsDLL.Properties.Resources.windows8 : ConstantsDLL.Properties.Resources.windows8_1;
                            }

                            break;
                        case 10:
                            operatingSystem = ConstantsDLL.Properties.Resources.windows10;
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

        //Fetches the operating system information
        public static string GetOSInformation()
        {
            string displayVersion = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DisplayVersion", string.Empty).ToString();
            string releaseId = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "releaseId", string.Empty).ToString();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    if (GetOSInfoAux().Equals(ConstantsDLL.Properties.Resources.windows10))
                    {
                        return Convert.ToInt32(releaseId) <= 2004
                            ? (((string)queryObj["Caption"]).Trim() + ", v" + releaseId + ", " + ConstantsDLL.Properties.Resources.build + " " + (string)queryObj["Version"] + ", " + (string)queryObj["OSArchitecture"]).Substring(10)
                            : (((string)queryObj["Caption"]).Trim() + ", v" + displayVersion + ", " + ConstantsDLL.Properties.Resources.build + " " + (string)queryObj["Version"] + ", " + (string)queryObj["OSArchitecture"]).Substring(10);
                    }
                    else
                    {
                        return (((string)queryObj["Caption"]).Trim() + " " + (string)queryObj["CSDVersion"] + ", " + ConstantsDLL.Properties.Resources.build + " " + (string)queryObj["Version"] + ", " + (string)queryObj["OSArchitecture"]).Substring(10);
                    }
                }
                return StringsAndConstants.unknown;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        //Fetches the OS build number
        public static string GetOSVersion()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_OperatingSystem");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    return queryObj.GetPropertyValue("Version").ToString();
                }

                return StringsAndConstants.unknown;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        //Fetches the computer's hostname
        public static string GetComputerName()
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

        //Fetches the BIOS version
        public static string GetComputerBIOS()
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

        //Fetches the BIOS type (BIOS or UEFI) on Windows 7
        public const int ERROR_INVALID_FUNCTION = 1;
        [DllImport("kernel32.dll",
            EntryPoint = "GetFirmwareEnvironmentVariableW",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFirmwareType(string lpName, string lpGUID, IntPtr pBuffer, uint size);
        public static string GetBIOSType7()
        {
            try
            {
                _ = GetFirmwareType(string.Empty, "{00000000-0000-0000-0000-000000000000}", IntPtr.Zero, 0);

                return Marshal.GetLastWin32Error() == ERROR_INVALID_FUNCTION ? ConstantsDLL.Properties.Resources.bios : ConstantsDLL.Properties.Resources.uefi;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Fetches the BIOS type (BIOS or UEFI) on Windows 8 and later
        [DllImport("kernel32.dll")]
        private static extern bool GetFirmwareType(ref uint FirmwareType);
        public static string GetBIOSType()
        {
            try
            {
                if (GetOSInfoAux().Equals(ConstantsDLL.Properties.Resources.windows10) || GetOSInfoAux().Equals(ConstantsDLL.Properties.Resources.windows8_1) || GetOSInfoAux().Equals(ConstantsDLL.Properties.Resources.windows8))
                {
                    uint firmwaretype = 0;
                    if (GetFirmwareType(ref firmwaretype))
                    {
                        if (firmwaretype == 1)
                        {
                            return ConstantsDLL.Properties.Resources.bios;
                        }
                        else if (firmwaretype == 2)
                        {
                            return ConstantsDLL.Properties.Resources.uefi;
                        }
                    }
                    return ConstantsDLL.Properties.Strings.notDetermined;
                }
                else
                {
                    return GetBIOSType7();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        //Fetches the Secure Boot status (alternative method)
        public static string GetSecureBootAlt()
        {
            try
            {
                PowerShell PowerShellInst = PowerShell.Create();
                _ = PowerShellInst.AddScript("Confirm-SecureBootUEFI");
                Collection<PSObject> PSOutput = PowerShellInst.Invoke();

                foreach (PSObject queryObj in PSOutput)
                {
                    return ConstantsDLL.Properties.Strings.activated;
                }

                return ConstantsDLL.Properties.Strings.deactivated;
            }
            catch
            {
                return ConstantsDLL.Properties.Strings.notSupported;
            }
        }

        //Fetches the Secure Boot status
        public static string GetSecureBoot()
        {
            try
            {
                string secBoot = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecureBoot\State", "UEFISecureBootEnabled", 0).ToString();
                return secBoot.Equals("0") ? ConstantsDLL.Properties.Strings.deactivated : ConstantsDLL.Properties.Strings.activated;
            }
            catch
            {
                return ConstantsDLL.Properties.Strings.notSupported;
            }
        }

        //Fetches the Virtualization Technology status
        public static string GetVirtualizationTechnology()
        {
            int flag = 0;

            try
            {
                if (!GetOSInfoAux().Equals(ConstantsDLL.Properties.Resources.windows7))
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
                        flag = GetBIOSType() == ConstantsDLL.Properties.Resources.uefi ? 1 : 0;
                    }
                }

                if (flag == 2)
                {
                    return ConstantsDLL.Properties.Strings.activated;
                }
                else
                {
                    return flag == 1 ? ConstantsDLL.Properties.Strings.deactivated : ConstantsDLL.Properties.Strings.notSupported;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        //Fetches the Hyper-V installation status
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
                        return "true";
                    }
                }
                return "false";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Fetches the S.M.A.R.T. status
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
                    if (statusValue == ConstantsDLL.Properties.Resources.predFail)
                    {
                        return statusCaption;
                    }
                }
                return ConstantsDLL.Properties.Resources.ok;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Fetches the TPM version
        public static string GetTPMStatus()
        {
            string isActivated;
            string isEnabled;
            string specVersion = string.Empty;
            ManagementScope scope = new ManagementScope(@"\\.\root\cimv2\Security\MicrosoftTPM");
            ObjectQuery query = new ObjectQuery("select * from Win32_Tpm");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            try
            {
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    isActivated = queryObj.Properties["IsActivated_InitialValue"].Value.ToString();
                    isEnabled = queryObj.Properties["IsEnabled_InitialValue"].Value.ToString();
                    specVersion = queryObj.Properties["SpecVersion"].Value.ToString();
                }
                specVersion = specVersion != string.Empty ? specVersion.Substring(0, 3) : ConstantsDLL.Properties.Strings.notExistant;
                return specVersion;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}