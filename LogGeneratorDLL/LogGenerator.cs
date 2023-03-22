using ConstantsDLL;
using System;
using System.IO;

namespace LogGeneratorDLL
{
    public class LogGenerator
    {
        private readonly string path, fileNameStr;

        public LogGenerator(string softwareName, string path, string fileName, bool consoleOut)
        {
            this.path = path;
            using (StreamWriter w = File.AppendText(this.path + "\\" + fileName))
            {
                fileNameStr = fileName;
                LogInit(w, softwareName, consoleOut);
            }
        }

        public void LogInit(TextWriter txtWriter, string softwareName, bool consoleOut)
        {
            try
            {
                txtWriter.WriteLine(ConstantsDLL.Properties.Resources.LOG_SEPARATOR);
                txtWriter.WriteLine();
                txtWriter.WriteLine(softwareName);
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                txtWriter.WriteLine();
                txtWriter.WriteLine(ConstantsDLL.Properties.Strings.LOG_HEADER);
                if (consoleOut)
                {
                    Console.ForegroundColor = StringsAndConstants.MISC_CONSOLE_COLOR;
                    Console.WriteLine(ConstantsDLL.Properties.Resources.LOG_SEPARATOR);
                    Console.WriteLine();
                    Console.WriteLine(softwareName);
                    Console.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    Console.WriteLine();
                    Console.WriteLine(ConstantsDLL.Properties.Strings.LOG_HEADER);
                    Console.ResetColor();
                }
            }
            catch (Exception e)
            {
                txtWriter.WriteLine(e);
            }
        }

        public void LogWrite(int logType, string logMessage1, string logMessage2, bool consoleOut)
        {
            try
            {
                using (StreamWriter w = File.AppendText(path + "\\" + fileNameStr))
                {
                    Log(logType, logMessage1, logMessage2, w, consoleOut);
                }
            }
            catch
            {
            }
        }

        public void Log(int logType, string logMessage1, string logMessage2, TextWriter txtWriter, bool consoleOut)
        {
            string logTypeAttr;

            try
            {
                if (logType == 2)
                {
                    logTypeAttr = ConstantsDLL.Properties.Strings.LOG_ERROR_ATTR;
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (logType == 1)
                {
                    logTypeAttr = ConstantsDLL.Properties.Strings.LOG_WARNING_ATTR;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (logType == 0)
                {
                    logTypeAttr = ConstantsDLL.Properties.Strings.LOG_INFO_ATTR;
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    logTypeAttr = string.Empty;
                    Console.ForegroundColor = StringsAndConstants.MISC_CONSOLE_COLOR;
                }

                if (!logMessage2.Equals(string.Empty))
                {
                    txtWriter.WriteLine("[{0}] : {1}: {2} - {3}", DateTime.Now.ToString(ConstantsDLL.Properties.Resources.LOG_TIMESTAMP), logTypeAttr, logMessage1, logMessage2);
                    if (consoleOut)
                    {
                        Console.WriteLine("[{0}] : {1}: {2} - {3}", DateTime.Now.ToString(ConstantsDLL.Properties.Resources.LOG_TIMESTAMP), logTypeAttr, logMessage1, logMessage2);
                    }
                }
                else
                {
                    txtWriter.WriteLine("[{0}] : {1}: {2}", DateTime.Now.ToString(ConstantsDLL.Properties.Resources.LOG_TIMESTAMP), logTypeAttr, logMessage1);
                    if (consoleOut)
                    {
                        Console.WriteLine("[{0}] : {1}: {2}", DateTime.Now.ToString(ConstantsDLL.Properties.Resources.LOG_TIMESTAMP), logTypeAttr, logMessage1);
                    }
                }
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                txtWriter.WriteLine("[{0}] : {1}: {2}", DateTime.Now.ToString(ConstantsDLL.Properties.Resources.LOG_TIMESTAMP), ConstantsDLL.Properties.Strings.LOG_ERROR_ATTR, e.Message);
                if (consoleOut)
                {
                    Console.WriteLine("[{0}] : {1}: {2}", DateTime.Now.ToString(ConstantsDLL.Properties.Resources.LOG_TIMESTAMP), ConstantsDLL.Properties.Strings.LOG_ERROR_ATTR, e.Message);
                }

                Console.ResetColor();
            }
        }

        public void LogEnd(TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine();
                txtWriter.WriteLine(ConstantsDLL.Properties.Resources.LOG_SEPARATOR);
            }
            catch (Exception e)
            {
                txtWriter.WriteLine(e);
            }
        }
    }
}