using System.Collections.Generic;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public interface IComplexCommand : ICommand
    {
        List<ICommand> Commands { get; }
        string Name { get; }

        object ExecuteHead();
        ExpectedReturnType TryExecuteHead<ExpectedReturnType>();

        List<object> ExecuteBlock();
        List<ExpectedReturnType> TryExecuteBlock<ExpectedReturnType>();

        object ExecuteTail();
        ExpectedReturnType TryExecuteTail<ExpectedReturnType>();

    }
}