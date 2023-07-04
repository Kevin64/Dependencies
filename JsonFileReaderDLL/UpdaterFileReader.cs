using Newtonsoft.Json;
using System.IO;

namespace JsonFileReaderDLL
{
    /// <summary> 
    /// Template class for 'Updater'
    /// </summary>
    public class UpdaterFile
    {
        public string ETag { get; set; }
        public string TagName { get; set; }
        public string Body { get; set; }
        public string HtmlUrl { get; set; }
    }

    /// <summary> 
    /// Class for handling a 'Updater' json file
    /// </summary>
    public static class UpdaterFileReader
    {
        private static string jsonFile;
        private static StreamReader fileU;

        /// <summary> 
        /// 
        ///Reads a json file with update metadata, returning them (single threaded)
        ///
        /// </summary>
        /// <returns>Returns a type with ETag, TagName, Body and HtmlUrl.</returns>
        public static UpdaterFile FetchInfoST(string file)
        {
            fileU = new StreamReader(file);
            jsonFile = fileU.ReadToEnd();
            fileU.Close();
            return JsonConvert.DeserializeObject<UpdaterFile>(@jsonFile);
        }
    }
}
