using System;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public interface ISimpleCommand : ICommand
    { 
        string Name { get; }
    }
}