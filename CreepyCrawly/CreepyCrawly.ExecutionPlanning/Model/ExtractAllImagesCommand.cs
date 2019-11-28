using CreepyCrawly.Output;
using CreepyCrawly.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public class ExtractAllImagesCommand : ISimpleCommand
    {
        public string Name { get; private set; }
        public string Selector { get; private set; }
        public Func<string, object> Execution { get; private set; }

        public ExtractAllImagesCommand(string selector, Func<string, object> execution)
        {
            Name = "EXTRACT_ALL_IMAGES";
            Selector = selector;
            Execution = execution;
        }

        public object Execute()
        {
            try
            {
                string[] results = (string[])Execution.Invoke(Selector);
                foreach (var result in results)
                {
                    OutputSingleton.WriteToTextOutputters(result);
                }
                
                return 1;
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
