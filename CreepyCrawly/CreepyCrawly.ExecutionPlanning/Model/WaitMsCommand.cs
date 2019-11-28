using CreepyCrawly.Output;
using CreepyCrawly.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public class WaitMsCommand : ISimpleCommand
    {
        public string Name { get; private set; }
        public Func<int, object> Execution { get; private set; }
        public int WaitAmount { get; private set; }

        public WaitMsCommand(int waitAmount, Func<int, object> execution)
        {
            Name = "WAIT_MS";
            Execution = execution;
            WaitAmount = waitAmount;
        }

        public object Execute()
        {
            try
            {
                return Execution.Invoke(WaitAmount);
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
