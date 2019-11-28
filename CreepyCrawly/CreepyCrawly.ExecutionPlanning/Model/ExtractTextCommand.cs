using CreepyCrawly.Output;
using CreepyCrawly.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public class ExtractTextCommand : ISimpleCommand
    {
        public string Name { get; private set; }
        public string Selector { get; private set; }
        public Func<string, object> Execution { get; private set; }

        public ExtractTextCommand(string selector, Func<string, object> execution)
        {
            Name = "EXTRACT_TEXT";
            Selector = selector;
            Execution = execution;
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
                ErrorHandler.ReportCommandExecutionNonFatalFailed(e, Name);
                return null;
            }
        }

        public ExpectedReturnType TryExecute<ExpectedReturnType>()
        {
            return (ExpectedReturnType)Execute();
        }
    }
}
