using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.ExecutionPlanning.Model
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
            return Execution.Invoke(Selector, OptionIndex);
        }

        public ExpectedReturnType TryExecute<ExpectedReturnType>()
        {
            return (ExpectedReturnType)Execute();
        }
    }
}
