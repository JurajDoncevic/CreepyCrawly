using CreepyCrawly.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public class ClickCommand : ISimpleCommand
    {
        public string Name { get; private set; }
        public string Selector { get; private set; }
        public Func<string, object> Execution { get; private set; }

        public ClickCommand(string selector, Func<string, object> execution)
        {
            Name = "CLICK";
            Selector = selector;
            Execution = execution;
        }

        public object Execute()
        {
            object result = Execution.Invoke(Selector);
            OutputSingleton.WriteOutputToAllOutputters(result);
            return result;
        }

        public ExpectedReturnType TryExecute<ExpectedReturnType>()
        {
            return (ExpectedReturnType)Execute();
        }
    }
}
