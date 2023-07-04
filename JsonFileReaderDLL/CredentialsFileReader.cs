using ConstantsDLL;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
    public enum PrivilegeLevel
    {
        ADMINISTRATOR,
        STANDARD,
        LIMITED
    }
    /// <summary> 
    /// Template class for 'Credentials'
    /// </summary>
    public class CredentialsFile
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PrivilegeLevel { get; set; }
        public string LastLoginDate { get; set; }
    }

    /// <summary> 
    /// Class for handling a 'Credentials' json file
    /// </summary>
    public static class CredentialsFileReader
    {
        private static string jsonFile, sha256, aux;
        private static WebClient wc;
        private static StreamReader fileL;

        /// <summary> 
        /// 
        ///Checks if the server is answering any requests, through a json file verification (creates a separate thread)
        ///
        /// </summary>
        /// <param name="ipAddress">Server IP address</param>
        /// <param name="port">Server port</param>
        /// <returns>If server is reachable and sends a json file, returns true. If not, returns false.</returns>
        public static Task<bool> CheckHostMT(string ipAddress, string port)
        {
            return Task.Run(() =>
            {
                try
                {
                    wc = new WebClient();
                    _ = wc.DownloadString(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.SUPPLY_CREDENTIALS_DATA);
                    System.Threading.Thread.Sleep(300);
                    wc.DownloadFile(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.JSON_SERVER_PATH + ConstantsDLL.Properties.Resources.FILE_CREDENTIALS, StringsAndConstants.CREDENTIALS_FILE_PATH);
                    System.Threading.Thread.Sleep(300);
                    sha256 = wc.DownloadString(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.JSON_SERVER_PATH + ConstantsDLL.Properties.Resources.FILE_SHA_CREDENTIALS);
                    System.Threading.Thread.Sleep(300);
                    sha256 = sha256.ToUpper();
                    aux = StringsAndConstants.CREDENTIALS_FILE_PATH;
                }
                catch
                {
                    return false;
                }
                return true;
            });
        }

        /// <summary> 
        /// 
        ///Checks if the server is answering any requests, through a json file verification (single threaded)
        ///
        /// </summary>
        /// <param name="ipAddress">Server IP address</param>
        /// <param name="port">Server port</param>
        /// <returns>If server is reachable and sends a json file, returns true. If not, returns false.</returns>
        public static bool CheckHostST(string ipAddress, string port)
        {
            try
            {
                wc = new WebClient();
                _ = wc.DownloadString(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.SUPPLY_CREDENTIALS_DATA);
                System.Threading.Thread.Sleep(300);
                wc.DownloadFile(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.JSON_SERVER_PATH + ConstantsDLL.Properties.Resources.FILE_CREDENTIALS, StringsAndConstants.CREDENTIALS_FILE_PATH);
                System.Threading.Thread.Sleep(300);
                sha256 = wc.DownloadString(ConstantsDLL.Properties.Resources.HTTP + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.JSON_SERVER_PATH + ConstantsDLL.Properties.Resources.FILE_SHA_CREDENTIALS);
                System.Threading.Thread.Sleep(300);
                sha256 = sha256.ToUpper();
                aux = StringsAndConstants.CREDENTIALS_FILE_PATH;
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary> 
        /// 
        ///Reads a json file retrieved from the server and parses username and encoded password, returning them (creates a separate thread)
        ///
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">User's password</param>
        /// <param name="ipAddress">Server IP address</param>
        /// <param name="port">Server port</param>
        /// <returns>If user exists on the fetched json file, returns a string array with the username and respective id. If not, returns a single position array with a "false" string.</returns>
        public static Task<string[]> FetchInfoMT(string username, string password, string ipAddress, string port)
        {
            return Task.Run(async () =>
            {
                if (!await CheckHostMT(ipAddress, port))
                {
                    return null;
                }

                string[] arr;
                fileL = new StreamReader(StringsAndConstants.CREDENTIALS_FILE_PATH);
                if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
                {
                    jsonFile = fileL.ReadToEnd();
                    CredentialsFile[] jsonParse = JsonConvert.DeserializeObject<CredentialsFile[]>(@jsonFile);

                    for (int i = 0; i < jsonParse.Length; i++)
                    {
                        if (username.Equals(jsonParse[i].Username) && !jsonParse[i].PrivilegeLevel.Equals(Convert.ToInt32(PrivilegeLevel.LIMITED).ToString()) && BCrypt.Net.BCrypt.Verify(password, jsonParse[i].Password))
                        {
                            arr = new string[] { jsonParse[i].Id, jsonParse[i].Username };
                            fileL.Close();
                            return arr;
                        }
                    }
                }
                arr = new string[] { ConstantsDLL.Properties.Resources.FALSE }; ;
                fileL.Close();
                return arr;
            });
        }

        /// <summary> 
        /// 
        ///Reads a json file retrieved from the server and parses username and encoded password, returning them  (single threaded)
        ///
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">User's password</param>
        /// <param name="ipAddress">Server IP address</param>
        /// <param name="port">Server port</param>
        /// <returns>If user exists on the fetched json file, returns a string array with the username and respective id. If not, returns a single position array with a "false" string.</returns>
        public static string[] FetchInfoST(string username, string password, string ipAddress, string port)
        {
            if (!CheckHostST(ipAddress, port))
            {
                return null;
            }

            string[] arr;
            fileL = new StreamReader(StringsAndConstants.CREDENTIALS_FILE_PATH);
            if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
            {
                jsonFile = fileL.ReadToEnd();
                CredentialsFile[] jsonParse = JsonConvert.DeserializeObject<CredentialsFile[]>(@jsonFile);

                for (int i = 0; i < jsonParse.Length; i++)
                {
                    if (username.Equals(jsonParse[i].Username) && !jsonParse[i].PrivilegeLevel.Equals(Convert.ToInt32(PrivilegeLevel.LIMITED).ToString()) && BCrypt.Net.BCrypt.Verify(password, jsonParse[i].Password))
                    {
                        arr = new string[] { jsonParse[i].Id, jsonParse[i].Username };
                        fileL.Close();
                        return arr;
                    }
                }
            }
            arr = new string[] { ConstantsDLL.Properties.Resources.FALSE }; ;
            fileL.Close();
            return arr;
        }
    }
}
