using ConstantsDLL;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
    public class LFile
    {
        public string Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Nivel { get; set; }
        public string Status { get; set; }
    }
    public static class LoginFileReader
    {
        private static string jsonFile, sha256, aux;
        private static WebClient wc;
        private static StreamReader fileL;

        //Checks if the server is answering any requests, through a json file verification (creates a separate thread)
        public static Task<bool> CheckHostMT(string ip, string port)
        {
            return Task.Run(() =>
            {
                try
                {
                    wc = new WebClient();
                    wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.supplyLoginData);
                    System.Threading.Thread.Sleep(300);
                    wc.DownloadFile("http://" + ip + ":" + port + "/" + StringsAndConstants.jsonServerPath + StringsAndConstants.fileLogin, StringsAndConstants.loginPath);
                    System.Threading.Thread.Sleep(300);
                    sha256 = wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.jsonServerPath + StringsAndConstants.fileShaLogin);
                    System.Threading.Thread.Sleep(300);
                    sha256 = sha256.ToUpper();
                    fileL = new StreamReader(StringsAndConstants.loginPath);
                    aux = StringsAndConstants.loginPath;
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
        public static bool CheckHostST(string ip, string port)
        {
            try
            {
                wc = new WebClient();
                wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.supplyLoginData);
                System.Threading.Thread.Sleep(300);
                wc.DownloadFile("http://" + ip + ":" + port + "/" + StringsAndConstants.jsonServerPath + StringsAndConstants.fileLogin, StringsAndConstants.loginPath);
                System.Threading.Thread.Sleep(300);
                sha256 = wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.jsonServerPath + StringsAndConstants.fileShaLogin);
                System.Threading.Thread.Sleep(300);
                sha256 = sha256.ToUpper();
                fileL = new StreamReader(StringsAndConstants.loginPath);
                aux = StringsAndConstants.loginPath;
                fileL.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them (creates a separate thread)
        public static Task<string[]> FetchInfoMT(string nome, string senha, string ip, string port)
        {
            return Task.Run(async () =>
            {
                if (!await CheckHostMT(ip, port))
                    return null;

                string[] arr;
                fileL = new StreamReader(StringsAndConstants.loginPath);
                if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
                {
                    jsonFile = fileL.ReadToEnd();
                    LFile[] jsonParse = JsonConvert.DeserializeObject<LFile[]>(@jsonFile);

                    for (int i = 0; i < jsonParse.Length; i++)
                    {
                        if (nome.Equals(jsonParse[i].Usuario) && !jsonParse[i].Nivel.Equals(StringsAndConstants.limitedUserType) && BCrypt.Net.BCrypt.Verify(senha, jsonParse[i].Senha))
                        {
                            arr = new string[] { "true", jsonParse[i].Usuario };
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
        public static string[] FetchInfoST(string nome, string senha, string ip, string port)
        {
            if (!CheckHostST(ip, port))
                return null;

            string[] arr;
            fileL = new StreamReader(StringsAndConstants.loginPath);
            if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
            {
                jsonFile = fileL.ReadToEnd();
                LFile[] jsonParse = JsonConvert.DeserializeObject<LFile[]>(@jsonFile);

                for (int i = 0; i < jsonParse.Length; i++)
                {
                    if (nome.Equals(jsonParse[i].Usuario) && !jsonParse[i].Nivel.Equals(StringsAndConstants.limitedUserType) && BCrypt.Net.BCrypt.Verify(senha, jsonParse[i].Senha))
                    {
                        arr = new string[] { "true", jsonParse[i].Usuario };
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
