using System;

namespace CreepyCrawly.LanguageEngine.CommandModel
{
    public interface ISimpleCommand : ICommand
    { 
        string Name { get; }
    }
}