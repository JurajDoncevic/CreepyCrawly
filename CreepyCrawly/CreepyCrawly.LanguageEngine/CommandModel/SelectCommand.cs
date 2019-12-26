using CreepyCrawly.Output;
using CreepyCrawly.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.LanguageEngine.CommandModel
{
    public class SelectCommand : ISimpleCommand
    {
        public string Name { get; private set; }
        public string Selector { get; private set; }
        public int OptionIndex { get; private set; }
        public Func<string, int, object> Execution { get; private set; }

        public SelectCommand(string selector, int optionIndex, Func<string, int, object> execution)
        {
            Name = "SELECT";
            Selector = selector;
            OptionIndex = optionIndex;
            Execution = execution;
        }

        public object Execute()
        {
            try
            {
                return Execution.Invoke(Selector, OptionIndex);
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
