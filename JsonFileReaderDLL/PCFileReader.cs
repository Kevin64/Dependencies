using Newtonsoft.Json;
using System.IO;
using System.Net;
using ConstantsDLL;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
    public class pcFile
    {
        public string patrimonio { get; set; }
        public string predio { get; set; }
        public string sala { get; set; }
        public string padrao { get; set; }
        public string ad { get; set; }
        public string emUso { get; set; }
        public string lacre { get; set; }
        public string etiqueta { get; set; }
        public string tipo { get; set; }
        public string descarte { get; set; }
        public string dataFormatacao { get; set; }
    }
    public static class PCFileReader
    {
        private static string jsonFile, sha256, aux;
        private static WebClient wc;
        private static StreamReader filePC;

        //Checks if the server is answering any requests, through a json file verification (creates a separate thread)
        public static Task<bool> checkHostMT(string ip, string port, string patr)
        {
            return Task.Run(() =>
            {
                try
                {
                    wc = new WebClient();
                    wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.supplyPCData + "?patrimonio=" + patr);
                    System.Threading.Thread.Sleep(300);
                    wc.DownloadFile("http://" + ip + ":" + port + "/" + StringsAndConstants.filePC, StringsAndConstants.pcPath);
                    System.Threading.Thread.Sleep(300);
                    sha256 = wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.fileShaPC);
                    System.Threading.Thread.Sleep(300);
                    sha256 = sha256.ToUpper();
                    filePC = new StreamReader(StringsAndConstants.pcPath);
                    aux = StringsAndConstants.pcPath;
                    filePC.Close();
                }
                catch
                {
                    return false;
                }
                return true;
            });
        }

        //Checks if the server is answering any requests, through a json file verification (single threaded)
        public static bool checkHostST(string ip, string port, string patr)
        {
            try
            {
                wc = new WebClient();
                wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.supplyPCData + "?patrimonio=" + patr);
                System.Threading.Thread.Sleep(300);
                wc.DownloadFile("http://" + ip + ":" + port + "/" + StringsAndConstants.filePC, StringsAndConstants.pcPath);
                System.Threading.Thread.Sleep(300);
                sha256 = wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.fileShaPC);
                System.Threading.Thread.Sleep(300);
                sha256 = sha256.ToUpper();
                filePC = new StreamReader(StringsAndConstants.pcPath);
                aux = StringsAndConstants.pcPath;
                filePC.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them (creates a separate thread)
        public static Task<string[]> fetchInfoMT(string patrimonio, string ip, string port)
        {
            return Task.Run(async () =>
            {
                if (!await checkHostMT(ip, port, patrimonio))
                    return null;

                string[] arr;
                filePC = new StreamReader(StringsAndConstants.pcPath);
                if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
                {
                    jsonFile = filePC.ReadToEnd();
                    pcFile[] jsonParse = JsonConvert.DeserializeObject<pcFile[]>(@jsonFile);

                    for (int i = 0; i < jsonParse.Length; i++)
                    {
                        if (patrimonio.Equals(jsonParse[i].patrimonio))
                        {
                            arr = new string[] { jsonParse[i].patrimonio, jsonParse[i].predio, jsonParse[i].sala, jsonParse[i].padrao, jsonParse[i].ad, jsonParse[i].emUso, jsonParse[i].lacre, jsonParse[i].etiqueta, jsonParse[i].tipo, jsonParse[i].descarte, jsonParse[i].dataFormatacao };
                            filePC.Close();
                            return arr;
                        }
                    }
                }
                arr = new string[] { "false" }; ;
                filePC.Close();
                return arr;
            });
        }

        //Reads a json file retrieved from the server and parses username and encoded password, returning them  (single threaded)
        public static string[] fetchInfoST(string patrimonio, string ip, string port)
        {
            if (!checkHostST(ip, port, patrimonio))
                return null;

            string[] arr;   
            filePC = new StreamReader(StringsAndConstants.pcPath);
            if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
            {
                jsonFile = filePC.ReadToEnd();
                pcFile[] jsonParse = JsonConvert.DeserializeObject<pcFile[]>(@jsonFile);

                for (int i = 0; i < jsonParse.Length; i++)
                {
                    if (patrimonio.Equals(jsonParse[i].patrimonio))
                    {
                        arr = new string[] { jsonParse[i].patrimonio, jsonParse[i].predio, jsonParse[i].sala, jsonParse[i].padrao, jsonParse[i].ad, jsonParse[i].emUso, jsonParse[i].lacre, jsonParse[i].etiqueta, jsonParse[i].tipo, jsonParse[i].descarte, jsonParse[i].dataFormatacao };
                        filePC.Close();
                        return arr;
                    }
                }
            }
            arr = new string[] { "false" }; ;
            filePC.Close();
            return arr;
        }
    }
}
