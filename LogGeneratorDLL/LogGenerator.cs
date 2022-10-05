using System;
using System.IO;
using System.Reflection;
using ConstantsDLL;

namespace LogGeneratorDLL
{
    public class LogGenerator
    {
        private string m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private string fileNameStr;

        public LogGenerator(string softwareName, string fileName)
        {
            using (StreamWriter w = File.AppendText(m_exePath + "\\" + fileName))
            {
                fileNameStr = fileName;
                LogInit(w, softwareName);
            }
        }

        public void LogInit(TextWriter txtWriter, string softwareName)
        {
            try
            {
                txtWriter.WriteLine(StringsAndConstants.LOG_SEPARATOR);
                txtWriter.WriteLine();
                txtWriter.WriteLine(softwareName);
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                txtWriter.WriteLine();
                txtWriter.WriteLine(StringsAndConstants.LOG_HEADER);
            }
            catch (Exception e)
            {
                txtWriter.WriteLine(e);
            }
        }

        public void LogWrite(string logType, string logMessage1, string logMessage2)
        {
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + fileNameStr))
                {
                    Log(logType, logMessage1, logMessage2, w);
                }
            }
            catch (Exception e)
            {
            }
        }

        public void Log(string logType, string logMessage1, string logMessage2, TextWriter txtWriter)
        {
            try
            {
                if(!logMessage2.Equals(string.Empty))
                    txtWriter.WriteLine("[{0}] : {1}: {2} - {3}", DateTime.Now.ToString(StringsAndConstants.LOG_TIMESTAMP), logType, logMessage1, logMessage2);
                else
                    txtWriter.WriteLine("[{0}] : {1}: {2}", DateTime.Now.ToString(StringsAndConstants.LOG_TIMESTAMP), logType, logMessage1);
            }
            catch (Exception e)
            {
                txtWriter.WriteLine(e);
            }
        }

        public void LogEnd(TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine();
                txtWriter.WriteLine(StringsAndConstants.LOG_SEPARATOR);
            }
            catch (Exception e)
            {
                txtWriter.WriteLine(e);
            }
        }
    }
}