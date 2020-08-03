using CreepyCrawly.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.Utils
{
    public class ErrorHandler
    {
        public static bool DisplayVerboseErrors { get; set; } = false;
        public static void ReportCommandExecutionFailed(Exception e, string commandName)
        {
            string failMessage = "WARN: Command " + commandName + " failed: " + e.Message.Replace("\n", " ");
            if (DisplayVerboseErrors)
            {
                OutputSingleton.WriteToTextOutputters(failMessage);
            }
            else
            {
                OutputSingleton.WriteToConsoleOutputters(failMessage);
            }
        }
        public static void ReportFatalRunError(Exception e, string message)
        {
            string failMessage = "FATAL ERR: " + message + "\n" + e.Message.Replace("\n", " ") + "\n" + e.StackTrace;
            if (DisplayVerboseErrors)
            {
                OutputSingleton.WriteToTextOutputters(failMessage);
            }
            else
            {
                OutputSingleton.WriteToConsoleOutputters(failMessage);
            }
        }

        public static void ReportCrawlLangError(Exception e, string message)
        {
            string failMessage = "CrawlLang ERR: " + message + "\n" + e.Message.Replace("\n", " ") + "\n" + e.StackTrace;
            if (DisplayVerboseErrors)
            {
                OutputSingleton.WriteToTextOutputters(failMessage);
            }
            else
            {
                OutputSingleton.WriteToConsoleOutputters(failMessage);
            }
        }
    }
}
