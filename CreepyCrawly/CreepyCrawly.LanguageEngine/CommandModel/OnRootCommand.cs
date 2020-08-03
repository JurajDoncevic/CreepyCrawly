using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.LanguageEngine.CommandModel
{
    public class OnRootCommand : ISimpleCommand
    {
        public string Name { get; private set; }
        public string WwwUrl { get; private set; }
        public Func<string, object> Execution { get; private set; }

        public OnRootCommand(string wwwUrl, Func<string, object> execution)
        {
            Name = "ON ROOT";
            WwwUrl = wwwUrl;
            Execution = execution;
        }

        public object Execute()
        {
            return Execution.Invoke(WwwUrl);
        }

        public ExpectedReturnType TryExecute<ExpectedReturnType>()
        {
            return (ExpectedReturnType)Execute();
        }
    }
}
