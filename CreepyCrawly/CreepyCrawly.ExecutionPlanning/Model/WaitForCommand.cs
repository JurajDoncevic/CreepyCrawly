using CreepyCrawly.Output;
using CreepyCrawly.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public class WaitForCommand : ISimpleCommand
    {
        public string Name { get; private set; }
        public int WaitAmount { get; set; }
        public string Selector { get; private set; }
        public Func<string, int, object> Execution { get; private set; }

        public WaitForCommand(string selector, int waitAmount, Func<string, int, object> execution)
        {
            Name = "WAIT_FOR";
            Selector = selector;
            Execution = execution;
            WaitAmount = waitAmount;
        }

        public object Execute()
        {
            try
            {
                return Execution.Invoke(Selector, WaitAmount);
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
