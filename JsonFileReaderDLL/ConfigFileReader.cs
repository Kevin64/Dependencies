using ConstantsDLL;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
    public class CFile
    {
        public Definitions Definitions { get; set; }
        public OrgData Orgdata { get; set; }
        public DbSettings Dbsettings { get; set; }
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
        private static string jsonFile, sha256;
        private static WebClient wc;
        private static StreamReader fileC;

        //Checks if the server is answering any requests, through a json file verification (creates a separate thread)
        public static Task<bool> CheckHostMT(string ip, string port)
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
        public static bool CheckHostST(string ip, string port)
        {
            try
            {
                wc = new WebClient();
                wc.DownloadFile("http://" + ip + ":" + port + StringsAndConstants.fileConfigPath + StringsAndConstants.fileConfig, StringsAndConstants.configPath);
                System.Threading.Thread.Sleep(300);
                fileC = new StreamReader(StringsAndConstants.configPath);
                fileC.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them (creates a separate thread)
        public static Task<List<string[]>> FetchInfoMT(string ip, string port)
        {
            return Task.Run(async () =>
            {
                if (!await CheckHostMT(ip, port))
                    return null;

                List<string[]> arr;
                fileC = new StreamReader(StringsAndConstants.configPath);

                jsonFile = fileC.ReadToEnd();
                CFile jsonParse = JsonConvert.DeserializeObject<CFile>(@jsonFile);

                arr = new List<string[]>() { jsonParse.Definitions.Buildings, jsonParse.Definitions.HWTypes };

                fileC.Close();
                return arr;
            });
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them  (single threaded)
        public static List<string[]> FetchInfoST(string ip, string port)
        {
            if (!CheckHostST(ip, port))
                return null;

            List<string[]> arr;
            fileC = new StreamReader(StringsAndConstants.configPath);

            jsonFile = fileC.ReadToEnd();
            CFile jsonParse = JsonConvert.DeserializeObject<CFile>(@jsonFile);

            arr = new List<string[]>() { jsonParse.Definitions.Buildings, jsonParse.Definitions.HWTypes };

            fileC.Close();
            return arr;
        }
    }
}
