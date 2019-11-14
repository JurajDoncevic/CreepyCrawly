using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public class WaitLoadCommand : ISimpleCommand
    {
        public string Name { get; private set; }
        public int WaitAmount { get; set; }
        public string Selector { get; private set; }
        public Func<string, int, object> Execution { get; private set; }

        public WaitLoadCommand(string selector, int waitAmount, Func<string, int, object> execution)
        {
            Name = "WAIT_LOAD";
            Selector = selector;
            Execution = execution;
            WaitAmount = waitAmount;
        }

        public object Execute()
        {
            return Execution.Invoke(Selector, WaitAmount);
        }

        public ExpectedReturnType TryExecute<ExpectedReturnType>()
        {
            return (ExpectedReturnType)Execute();
        }
    }
}
