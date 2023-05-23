﻿using ConstantsDLL;
using System;
using System.IO;

namespace LogGeneratorDLL
{
    ///<summary>Class for LogGenerator</summary>
    public class LogGenerator
    {
        private readonly string path, fileNameStr;

        ///<summary>LogGenerator constructor</summary>
        ///<param name="softwareName">Name of the software</param>
        ///<param name="path">Path where the log file will be written</param>
        ///<param name="fileName">Name of the log file</param>
        ///<param name="consoleOut">Toggle for CLI output</param>
        public LogGenerator(string softwareName, string path, string fileName, bool consoleOut)
        {
            this.path = path;
            using (StreamWriter w = File.AppendText(this.path + "\\" + fileName))
            {
                fileNameStr = fileName;
                LogInit(w, softwareName, consoleOut);
            }
        }

        ///<summary>Initiates a log file</summary>
        ///<param name="txtWriter">StreamWriter for writing characters to a stream in a particular encoding</param>
        ///<param name="softwareName">Name of the software</param>
        ///<param name="consoleOut">Toggle for CLI output</param>
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

        ///<summary>Method that appends a log line into the file</summary>
        ///<param name="logType">Severity of the event</param>
        ///<param name="logMessage1">Log message for first field - Key/Title</param>
        ///<param name="logMessage2">Log message for second field - Value/Explanation</param>
        ///<param name="consoleOut">Toggle for CLI output</param>
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

        ///<summary>Method that define how log entries will be written</summary>
        ///<param name="logType">Severity of the event</param>
        ///<param name="logMessage1">Log message for first field - Key/Title</param>
        ///<param name="logMessage2">Log message for second field - Value/Explanation</param>
        ///<param name="txtWriter">StreamWriter for writing characters to a stream in a particular encoding</param>
        ///<param name="consoleOut">Toggle for CLI output</param>
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

        ///<summary>Ends a log file</summary>
        ///<param name="txtWriter">StreamWriter for writing characters to a stream in a particular encoding</param>
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