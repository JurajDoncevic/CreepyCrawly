using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.LanguageEngine.CommandModel
{
    public class ExecutionPlan
    {
        public List<ICommand> Commands { get; private set; }
        public string OnRootUrl { get; private set; }

        public ExecutionPlan(List<ICommand> commands, string onRootUrl)
        {
            Commands = commands;
            OnRootUrl = onRootUrl;
        }
    }
}
