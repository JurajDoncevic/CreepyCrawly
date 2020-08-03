using CreepyCrawly.LanguageEngine.CommandModel.PartialCommands;
using CreepyCrawly.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreepyCrawly.LanguageEngine.CommandModel
{
    public class CompositeExtractCommand : ISimpleCommand
    {
        public List<IPartialCommand> Commands { get; private set; }

        public string Name { get; private set; }

        public CompositeExtractCommand(List<IPartialCommand> commands)
        {
            Commands = commands;
            Name = "EXTRACT";
        }

        public object Execute()
        {
            string result = string.Join(",", Commands.Select(_ => _.Execute()));
            OutputSingleton.WriteToTextOutputters(result);
            return result;
        }

        public ExpectedReturnType TryExecute<ExpectedReturnType>()
        {
            throw new NotImplementedException();
        }
    }
}
