using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public class ExecutionPlan
    {
        public List<ISimpleCommand> SimpleCommands { get; private set; }
        public string OnRootUrl { get; private set; }

        public ExecutionPlan(List<ISimpleCommand> simpleCommands, string onRootUrl)
        {
            SimpleCommands = simpleCommands;
            OnRootUrl = onRootUrl;
        }
    }
}
