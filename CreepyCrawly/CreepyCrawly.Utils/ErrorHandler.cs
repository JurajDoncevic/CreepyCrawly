using CreepyCrawly.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.Utils
{
    public class ErrorHandler
    {

        public static void ReportCommandExecutionNonFatalFailed(Exception e, string commandName)
        {
            string failMessage = "WARN: Command " + commandName + " failed: " + e.Message.Replace("\n", " ");
            if (InstanceOptions.Options.VerboseErrors)
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
