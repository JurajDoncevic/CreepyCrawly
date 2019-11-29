using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.ExecutionPlanning.Model
{
    public class GotoClickCommand : IComplexCommand
    {
        public List<ICommand> Commands { get; private set; }

        public string Name { get; private set; }

        public string Selector { get; private set; }

        public Func<string, object> ExecutionHead { get; private set; }
        public Func<object> ExecutionTail { get; private set; }

        public GotoClickCommand(List<ICommand> commands, string selector, Func<string, object> executionHead, Func<object> executionTail)
        {
            Commands = commands;
            Name = "GOTO_CLICK";
            Selector = selector;
            ExecutionHead = executionHead;
            ExecutionTail = executionTail;
        }

        public object Execute()
        {
            List<object> results = new List<object>();
            ExecuteHead();

            results.Add(ExecuteBlock());

            ExecutionTail.Invoke();

            return results;
        }

        public List<object> ExecuteBlock()
        {
            List<object> results = new List<object>();
            Commands.ForEach(_ => results.Add(_.Execute()));
            return results;
        }

        public object ExecuteHead()
        {
            return ExecutionHead.Invoke(Selector);
        }

        public object ExecuteTail()
        {
            return ExecutionTail.Invoke();
        }

        public ExpectedReturnType TryExecute<ExpectedReturnType>()
        {
            throw new NotImplementedException();
        }

        public List<ExpectedReturnType> TryExecuteBlock<ExpectedReturnType>()
        {
            throw new NotImplementedException();
        }

        public ExpectedReturnType TryExecuteHead<ExpectedReturnType>()
        {
            throw new NotImplementedException();
        }

        public ExpectedReturnType TryExecuteTail<ExpectedReturnType>()
        {
            throw new NotImplementedException();
        }
    }
}
