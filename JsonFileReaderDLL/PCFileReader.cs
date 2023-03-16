using ConstantsDLL;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
    public class PcFile
    {
        public string Patrimonio { get; set; }
        public string Predio { get; set; }
        public string Sala { get; set; }
        public string Padrao { get; set; }
        public string Ad { get; set; }
        public string EmUso { get; set; }
        public string Lacre { get; set; }
        public string Etiqueta { get; set; }
        public string Tipo { get; set; }
        public string Descarte { get; set; }
        public string DataFormatacao { get; set; }
    }
    public static class PCFileReader
    {
        private static string jsonFile, sha256, aux;
        private static WebClient wc;
        private static StreamReader filePC;

        //Checks if the server is answering any requests, through a json file verification (creates a separate thread)
        public static Task<bool> CheckHostMT(string ip, string port, string patr)
        {
            return Task.Run(() =>
            {
                try
                {
                    wc = new WebClient();
                    wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.supplyPCData + "?patrimonio=" + patr);
                    System.Threading.Thread.Sleep(300);
                    wc.DownloadFile("http://" + ip + ":" + port + "/" + StringsAndConstants.jsonServerPath + StringsAndConstants.filePC, StringsAndConstants.pcPath);
                    System.Threading.Thread.Sleep(300);
                    sha256 = wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.jsonServerPath + StringsAndConstants.fileShaPC);
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
        public static bool CheckHostST(string ip, string port, string patr)
        {
            try
            {
                wc = new WebClient();
                wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.supplyPCData + "?patrimonio=" + patr);
                System.Threading.Thread.Sleep(300);
                wc.DownloadFile("http://" + ip + ":" + port + "/" + StringsAndConstants.jsonServerPath + StringsAndConstants.filePC, StringsAndConstants.pcPath);
                System.Threading.Thread.Sleep(300);
                sha256 = wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.jsonServerPath + StringsAndConstants.fileShaPC);
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
        public static Task<string[]> FetchInfoMT(string patrimonio, string ip, string port)
        {
            return Task.Run(async () =>
            {
                if (!await CheckHostMT(ip, port, patrimonio))
                    return null;

                string[] arr;
                filePC = new StreamReader(StringsAndConstants.pcPath);
                if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
                {
                    jsonFile = filePC.ReadToEnd();
                    PcFile[] jsonParse = JsonConvert.DeserializeObject<PcFile[]>(@jsonFile);

                    for (int i = 0; i < jsonParse.Length; i++)
                    {
                        if (patrimonio.Equals(jsonParse[i].Patrimonio))
                        {
                            arr = new string[] { jsonParse[i].Patrimonio, jsonParse[i].Predio, jsonParse[i].Sala, jsonParse[i].Padrao, jsonParse[i].Ad, jsonParse[i].EmUso, jsonParse[i].Lacre, jsonParse[i].Etiqueta, jsonParse[i].Tipo, jsonParse[i].Descarte, jsonParse[i].DataFormatacao };
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
        public static string[] FetchInfoST(string patrimonio, string ip, string port)
        {
            if (!CheckHostST(ip, port, patrimonio))
                return null;

            string[] arr;
            filePC = new StreamReader(StringsAndConstants.pcPath);
            if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
            {
                jsonFile = filePC.ReadToEnd();
                PcFile[] jsonParse = JsonConvert.DeserializeObject<PcFile[]>(@jsonFile);

                for (int i = 0; i < jsonParse.Length; i++)
                {
                    if (patrimonio.Equals(jsonParse[i].Patrimonio))
                    {
                        arr = new string[] { jsonParse[i].Patrimonio, jsonParse[i].Predio, jsonParse[i].Sala, jsonParse[i].Padrao, jsonParse[i].Ad, jsonParse[i].EmUso, jsonParse[i].Lacre, jsonParse[i].Etiqueta, jsonParse[i].Tipo, jsonParse[i].Descarte, jsonParse[i].DataFormatacao };
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
