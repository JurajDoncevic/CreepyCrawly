using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public interface ICommand
    {
        object Execute();
        ExpectedReturnType TryExecute<ExpectedReturnType>();
    }
}
