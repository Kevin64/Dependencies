﻿using Newtonsoft.Json;
using System.IO;
using System.Net;
using ConstantsDLL;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JsonFileReaderDLL
{
    public class cFile
    {
        public Definitions definitions { get; set; }
        public OrgData orgdata { get; set; }
        public DbSettings dbsettings { get; set; }
    }

    public class Definitions
    {
        public string LogLocation { get; set; }
        public string ServerIP { get; set; }
        public string ServerPort { get; set; }
        public string[] Buildings { get; set; }
        public string[] HWTypes { get; set; }
        public string ThemeUI { get; set; }
    }

    public class OrgData
    {
        public string OrganizationFullName { get; set; }
        public string OrganizationAcronym { get; set; }
        public string DepartamentFullName { get; set; }
        public string DepartamentAcronym { get; set; }
        public string SubDepartamentFullName { get; set; }
        public string SubDepartamentAcronym { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class DbSettings
    {
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string DbName { get; set; }
        public string DbIP { get; set; }
        public string DbPort { get; set; }
    }

    public static class ConfigFileReader
    {
        private static string jsonFile, sha256, aux;
        private static WebClient wc;
        private static StreamReader fileC;

        //Checks if the server is answering any requests, through a json file verification (creates a separate thread)
        public static Task<bool> checkHostMT(string ip, string port)
        {
            return Task.Run(() =>
            {
                try
                {
                    wc = new WebClient();
                    wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.supplyConfigData);
                    System.Threading.Thread.Sleep(300);
                    wc.DownloadFile("http://" + ip + ":" + port + "/" + StringsAndConstants.fileConfig, StringsAndConstants.configPath);
                    System.Threading.Thread.Sleep(300);
                    sha256 = wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.fileShaConfig);
                    System.Threading.Thread.Sleep(300);
                    sha256 = sha256.ToUpper();
                    fileC = new StreamReader(StringsAndConstants.configPath);
                    aux = StringsAndConstants.configPath;
                    fileC.Close();
                }
                catch
                {
                    return false;
                }
                return true;
            });
        }

        //Checks if the server is answering any requests, through a json file verification (single threaded)
        public static bool checkHostST(string ip, string port)
        {
            try
            {
                wc = new WebClient();
                wc.DownloadFile("http://" + ip + ":" + port + StringsAndConstants.fileConfigPath + StringsAndConstants.fileConfig, StringsAndConstants.configPath);
                System.Threading.Thread.Sleep(300);
                fileC = new StreamReader(StringsAndConstants.configPath);
                aux = StringsAndConstants.configPath;
                fileC.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them (creates a separate thread)
        public static Task<List<string[]>> fetchInfoMT(string ip, string port)
        {
            return Task.Run(async () =>
            {
                if (!await checkHostMT(ip, port))
                    return null;

                List<string[]> arr;
                fileC = new StreamReader(StringsAndConstants.configPath);

                jsonFile = fileC.ReadToEnd();
                cFile jsonParse = JsonConvert.DeserializeObject<cFile>(@jsonFile);

                arr = new List<string[]>() { jsonParse.definitions.Buildings, jsonParse.definitions.HWTypes };

                fileC.Close();
                return arr;
            });
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them  (single threaded)
        public static List<string[]> fetchInfoST(string ip, string port)
        {
            if (!checkHostST(ip, port))
                return null;

            List<string[]> arr;
            fileC = new StreamReader(StringsAndConstants.configPath);
            
            jsonFile = fileC.ReadToEnd();
            cFile jsonParse = JsonConvert.DeserializeObject<cFile>(@jsonFile);

            arr = new List<string[]>() { jsonParse.definitions.Buildings, jsonParse.definitions.HWTypes };

            fileC.Close();
            return arr;
        }
    }
}
