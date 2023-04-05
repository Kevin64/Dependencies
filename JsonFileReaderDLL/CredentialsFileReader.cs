using ConstantsDLL;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
    public class CredFile
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PrivilegeLevel { get; set; }
        public string LastLoginDate { get; set; }
    }
    public static class CredentialsFileReader
    {
        private static string jsonFile, sha256, aux;
        private static WebClient wc;
        private static StreamReader fileL;

        //Checks if the server is answering any requests, through a json file verification (creates a separate thread)
        public static Task<bool> CheckHostMT(string ipAddress, string port)
        {
            return Task.Run(() =>
            {
                try
                {
                    wc = new WebClient();
                    _ = wc.DownloadString("http://" + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.supplyLoginData);
                    System.Threading.Thread.Sleep(300);
                    wc.DownloadFile("http://" + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.jsonServerPath + ConstantsDLL.Properties.Resources.fileLogin, ConstantsDLL.Properties.Resources.loginPath);
                    System.Threading.Thread.Sleep(300);
                    sha256 = wc.DownloadString("http://" + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.jsonServerPath + ConstantsDLL.Properties.Resources.fileShaLogin);
                    System.Threading.Thread.Sleep(300);
                    sha256 = sha256.ToUpper();
                    fileL = new StreamReader(ConstantsDLL.Properties.Resources.loginPath);
                    aux = ConstantsDLL.Properties.Resources.loginPath;
                    fileL.Close();
                }
                catch
                {
                    return false;
                }
                return true;
            });
        }

        //Checks if the server is answering any requests, through a json file verification (single threaded)
        public static bool CheckHostST(string ipAddress, string port)
        {
            try
            {
                wc = new WebClient();
                _ = wc.DownloadString("http://" + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.supplyLoginData);
                System.Threading.Thread.Sleep(300);
                wc.DownloadFile("http://" + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.jsonServerPath + ConstantsDLL.Properties.Resources.fileLogin, ConstantsDLL.Properties.Resources.loginPath);
                System.Threading.Thread.Sleep(300);
                sha256 = wc.DownloadString("http://" + ipAddress + ":" + port + "/" + ConstantsDLL.Properties.Resources.jsonServerPath + ConstantsDLL.Properties.Resources.fileShaLogin);
                System.Threading.Thread.Sleep(300);
                sha256 = sha256.ToUpper();
                fileL = new StreamReader(ConstantsDLL.Properties.Resources.loginPath);
                aux = ConstantsDLL.Properties.Resources.loginPath;
                fileL.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them (creates a separate thread)
        public static Task<string[]> FetchInfoMT(string username, string password, string ipAddress, string port)
        {
            return Task.Run(async () =>
            {
                if (!await CheckHostMT(ipAddress, port))
                {
                    return null;
                }

                string[] arr;
                fileL = new StreamReader(ConstantsDLL.Properties.Resources.loginPath);
                if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
                {
                    jsonFile = fileL.ReadToEnd();
                    CredFile[] jsonParse = JsonConvert.DeserializeObject<CredFile[]>(@jsonFile);

                    for (int i = 0; i < jsonParse.Length; i++)
                    {
                        if (username.Equals(jsonParse[i].Username) && !jsonParse[i].PrivilegeLevel.Equals(ConstantsDLL.Properties.Resources.limitedUserType) && BCrypt.Net.BCrypt.Verify(password, jsonParse[i].Password))
                        {
                            arr = new string[] { jsonParse[i].Id, jsonParse[i].Username };
                            fileL.Close();
                            return arr;
                        }
                    }
                }
                arr = new string[] { "false" }; ;
                fileL.Close();
                return arr;
            });
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them  (single threaded)
        public static string[] FetchInfoST(string username, string password, string ipAddress, string port)
        {
            if (!CheckHostST(ipAddress, port))
            {
                return null;
            }

            string[] arr;
            fileL = new StreamReader(ConstantsDLL.Properties.Resources.loginPath);
            if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
            {
                jsonFile = fileL.ReadToEnd();
                CredFile[] jsonParse = JsonConvert.DeserializeObject<CredFile[]>(@jsonFile);

                for (int i = 0; i < jsonParse.Length; i++)
                {
                    if (username.Equals(jsonParse[i].Username) && !jsonParse[i].PrivilegeLevel.Equals(ConstantsDLL.Properties.Resources.limitedUserType) && BCrypt.Net.BCrypt.Verify(password, jsonParse[i].Password))
                    {
                        arr = new string[] { jsonParse[i].Id, jsonParse[i].Username };
                        fileL.Close();
                        return arr;
                    }
                }
            }
            arr = new string[] { "false" }; ;
            fileL.Close();
            return arr;
        }
    }
}
