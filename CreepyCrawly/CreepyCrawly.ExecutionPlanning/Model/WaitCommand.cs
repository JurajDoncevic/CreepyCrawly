using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public class WaitCommand : ISimpleCommand
    {
        public string Name { get; private set; }
        public Func<int, object> Execution { get; private set; }
        public int WaitAmount { get; private set; }

        public WaitCommand(Func<int, object> execution, int waitAmount)
        {
            Name = "Wait";
            Execution = execution;
            WaitAmount = waitAmount;
        }

        public object Execute()
        {
            return Execution.Invoke(WaitAmount);
        }

        public ExpectedReturnType TryExecute<ExpectedReturnType>()
        {
            return (ExpectedReturnType)Execute();
        }
    }
}
