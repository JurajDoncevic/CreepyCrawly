using CreepyCrawly.Output;
using CreepyCrawly.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public class InputCommand : ISimpleCommand
    {
        public string Name { get; private set; }
        public string Selector { get; private set; }
        public string InputValue { get; private set; }
        public Func<string, string, object> Execution { get; private set; }

        public InputCommand(string selector, string inputValue, Func<string, string, object> execution)
        {
            Name = "INPUT";
            Selector = selector;
            InputValue = inputValue;
            Execution = execution;
        }

        public object Execute()
        {
            try
            {
                return Execution.Invoke(Selector, InputValue);
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
