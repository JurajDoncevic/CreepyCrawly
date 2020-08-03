using CreepyCrawly.Output;
using CreepyCrawly.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.LanguageEngine.CommandModel
{
    public class ExtractAllHrefsCommand : ISimpleCommand
    {
        public string Name { get; private set; }
        public string Selector { get; private set; }
        public Func<string, object> Execution { get; private set; }

        public ExtractAllHrefsCommand(string selector, Func<string, object> execution)
        {
            Name = "EXTRACT_ALL_HREFS";
            Execution = execution;
            Selector = selector;
        }

        public object Execute()
        {
            try
            {
                object result = Execution.Invoke(Selector);
                OutputSingleton.WriteToTextOutputters(result);
                return result;
            }
            catch (Exception e)
            {
                ErrorHandler.ReportCommandExecutionFailed(e, Name);
                return null;
            }
        }

        public ExpectedReturnType TryExecute<ExpectedReturnType>()
        {
            return (ExpectedReturnType)Execute();
        }
    }
}
