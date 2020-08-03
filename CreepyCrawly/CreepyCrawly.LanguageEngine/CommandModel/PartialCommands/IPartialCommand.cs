using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.LanguageEngine.CommandModel.PartialCommands
{
    public interface IPartialCommand : ICommand
    {
        string Name { get; }
    }
}
