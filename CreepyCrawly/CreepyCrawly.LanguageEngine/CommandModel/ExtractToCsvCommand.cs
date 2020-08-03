using CreepyCrawly.Output;
using CreepyCrawly.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.LanguageEngine.CommandModel
{
    public class ExtractToCsvCommand : ISimpleCommand
    {
        public string Name { get; private set; }
        public List<string> Selectors { get; private set; }
        public Func<ICollection<string>, object> Execution { get; private set; }

        public ExtractToCsvCommand(ICollection<string> selectors, Func<ICollection<string>, object> execution)
        {
            Name = "EXTRACT_TO_CSV";
            Selectors = new List<string>(selectors);
            Execution = execution;
        }

        public object Execute()
        {
            try
            {
                object result = Execution.Invoke(Selectors);
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
