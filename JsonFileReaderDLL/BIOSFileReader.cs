using ConstantsDLL;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JsonFileReaderDLL
{
    public class BFile
    {
        public string Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Versao { get; set; }
        public string Tipo { get; set; }
        public string Tpm { get; set; }
        public string MediaOp { get; set; }
    }
    public static class BIOSFileReader
    {
        private static string jsonFile, sha256, aux;
        private static WebClient wc;
        private static StreamReader fileB;

        //Checks if the server is answering any requests, through a json file verification (creates a separate thread)
        public static Task<bool> CheckHostMT(string ip, string port)
        {
            return Task.Run(() =>
            {
                try
                {
                    wc = new WebClient();
                    wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.supplyBiosData);
                    System.Threading.Thread.Sleep(300);
                    wc.DownloadFile("http://" + ip + ":" + port + "/" + StringsAndConstants.jsonServerPath + StringsAndConstants.fileBios, StringsAndConstants.biosPath);
                    System.Threading.Thread.Sleep(300);
                    sha256 = wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.jsonServerPath + StringsAndConstants.fileShaBios);
                    System.Threading.Thread.Sleep(300);
                    sha256 = sha256.ToUpper();
                    fileB = new StreamReader(StringsAndConstants.biosPath);
                    aux = StringsAndConstants.biosPath;
                    fileB.Close();
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
                wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.supplyBiosData);
                System.Threading.Thread.Sleep(300);
                wc.DownloadFile("http://" + ip + ":" + port + "/" + StringsAndConstants.jsonServerPath + StringsAndConstants.fileBios, StringsAndConstants.biosPath);
                System.Threading.Thread.Sleep(300);
                sha256 = wc.DownloadString("http://" + ip + ":" + port + "/" + StringsAndConstants.jsonServerPath + StringsAndConstants.fileShaBios);
                System.Threading.Thread.Sleep(300);
                sha256 = sha256.ToUpper();
                fileB = new StreamReader(StringsAndConstants.biosPath);
                aux = StringsAndConstants.biosPath;
                fileB.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Reads a json file retrieved from the server and parses brand, model, BIOS versions, operatin mode and TPM version, returning them (creates a separate thread)
        public static Task<string[]> FetchInfoMT(string brd, string mod, string type, string tpm, string mediaOp, string ip, string port)
        {
            return Task.Run(async () =>
            {
                if (!await CheckHostMT(ip, port))
                    return null;

                string[] arr;
                string typeRet = "true", tpmRet = "true", mediaOpRet = "true";
                fileB = new StreamReader(StringsAndConstants.biosPath);
                if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
                {
                    jsonFile = fileB.ReadToEnd();
                    BFile[] jsonParse = JsonConvert.DeserializeObject<BFile[]>(@jsonFile);

                    for (int i = 0; i < jsonParse.Length; i++)
                    {
                        if (mod.Contains(jsonParse[i].Modelo) && brd.Contains(jsonParse[i].Marca))
                        {
                            if (!type.Equals(jsonParse[i].Tipo))
                                typeRet = "false";
                            if (!tpm.Equals(jsonParse[i].Tpm))
                                tpmRet = "false";
                            if (!mediaOp.Equals(jsonParse[i].MediaOp))
                                mediaOpRet = "false";
                            arr = new String[] { jsonParse[i].Versao, typeRet, tpmRet, mediaOpRet };
                            fileB.Close();
                            return arr;
                        }
                    }
                }
                arr = new String[] { "-1", "-1", "-1", "-1" };
                fileB.Close();
                return arr;
            });
        }

        //Reads a json file retrieved from the server and parses brand, model, BIOS versions, operatin mode and TPM version, returning them (single threaded)
        public static string[] FetchInfoST(string brd, string mod, string type, string tpm, string mediaOp, string ip, string port)
        {
            if (!CheckHostST(ip, port))
                return null;

            string[] arr;
            string typeRet = "true", tpmRet = "true", mediaOpRet = "true";
            fileB = new StreamReader(StringsAndConstants.biosPath);
            if (MiscMethods.GetSha256Hash(aux).Equals(sha256))
            {
                jsonFile = fileB.ReadToEnd();
                BFile[] jsonParse = JsonConvert.DeserializeObject<BFile[]>(@jsonFile);

                for (int i = 0; i < jsonParse.Length; i++)
                {
                    if (mod.Contains(jsonParse[i].Modelo) && brd.Contains(jsonParse[i].Marca))
                    {
                        if (!type.Equals(jsonParse[i].Tipo))
                            typeRet = "false";
                        if (!tpm.Equals(jsonParse[i].Tpm))
                            tpmRet = "false";
                        if (!mediaOp.Equals(jsonParse[i].MediaOp))
                            mediaOpRet = "false";
                        arr = new String[] { jsonParse[i].Versao, typeRet, tpmRet, mediaOpRet };
                        fileB.Close();
                        return arr;
                    }
                }
            }
            arr = new String[] { "-1", "-1", "-1", "-1" };
            fileB.Close();
            return arr;
        }
    }
}
