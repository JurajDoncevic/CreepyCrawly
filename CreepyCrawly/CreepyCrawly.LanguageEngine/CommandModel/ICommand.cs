using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.LanguageEngine.CommandModel
{
    public interface ICommand
    {
        object Execute();
        ExpectedReturnType TryExecute<ExpectedReturnType>();
    }
}
