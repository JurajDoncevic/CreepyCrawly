using System;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public interface ISimpleCommand
    { 
        string Name { get; }

        object Execute();
        ExpectedReturnType TryExecute<ExpectedReturnType>();
    }
}