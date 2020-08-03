using CreepyCrawly.Output;
using CreepyCrawly.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.LanguageEngine.CommandModel.PartialCommands
{
    public class PartialExtractImageCommand : IPartialCommand
    {
        public string Name { get; private set; }
        public string Selector { get; private set; }
        public Func<string, object> Execution { get; private set; }

        public PartialExtractImageCommand(string selector, Func<string, object> execution)
        {
            Name = "EXTRACT_IMAGE";
            Selector = selector;
            Execution = execution;
        }

        public object Execute()
        {
            try
            {
                object result = Execution.Invoke(Selector);
                string fileName = Guid.NewGuid().ToString();
                OutputSingleton.WriteToImageOutputtersWithFileName(result, fileName);
                return fileName;
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
