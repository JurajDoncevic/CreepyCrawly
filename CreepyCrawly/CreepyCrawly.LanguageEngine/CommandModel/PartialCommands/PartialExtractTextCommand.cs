using CreepyCrawly.Output;
using CreepyCrawly.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.LanguageEngine.CommandModel.PartialCommands
{
    public class PartialExtractTextCommand : IPartialCommand
    {
        public string Name { get; private set; }
        public string Selector { get; private set; }
        public string Regex { get; private set; }
        public Func<string, string, object> Execution { get; private set; }

        public PartialExtractTextCommand(string selector, Func<string, string, object> execution, string regex = null)
        {
            Name = "EXTRACT_TEXT";
            Selector = selector;
            Regex = regex;
            Execution = execution;
        }

        public object Execute()
        {
            try
            {
                object result = Execution.Invoke(Selector, Regex);
                //OutputSingleton.WriteToTextOutputters(result);
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
