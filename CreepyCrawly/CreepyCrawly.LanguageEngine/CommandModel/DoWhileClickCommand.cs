﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.LanguageEngine.CommandModel
{
    public class DoWhileClickCommand : IComplexCommand
    {
        public string Name { get; private set; }
        public List<ICommand> Commands { get; private set; }
        public string Selector { get; private set; }
        public Func<object> ExecutionHead { get; set; }
        public Func<object> ExecutionTail { get; set; }
        public Func<object> ExecutionIterationBegin { get; set; }
        public Func<string, object> ExecutionIterationEnd { get; set; }
        public DoWhileClickCommand(List<ICommand> commands, string selector, Func<object> executionHead, Func<object> executionIterationBegin, Func<string, object> executionIterationEnd, Func<object> executionTail)
        {
            Name = "DO_WHILE_CLICK";
            Commands = commands;
            Selector = selector;
            ExecutionHead = executionHead;
            ExecutionTail = executionTail;
            ExecutionIterationBegin = executionIterationBegin;
            ExecutionIterationEnd = executionIterationEnd;
        }

        public object Execute()
        {
            List<object> results = new List<object>();
            ExecuteHead();
            do
            {
                ExecutionIterationBegin.Invoke();
                results.Add(ExecuteBlock());

            } while (ExecutionIterationEnd.Invoke(Selector) != null);

            ExecutionTail.Invoke();

            return results;
        }

        public ExpectedReturnType TryExecute<ExpectedReturnType>()
        {
            throw new NotImplementedException();
        }

        public object ExecuteHead()
        {
            return ExecutionHead.Invoke();
        }

        public ExpectedReturnType TryExecuteHead<ExpectedReturnType>()
        {
            return (ExpectedReturnType)ExecutionHead.Invoke();
        }

        public object ExecuteTail()
        {
            return ExecutionTail.Invoke();
        }

        public ExpectedReturnType TryExecuteTail<ExpectedReturnType>()
        {
            return (ExpectedReturnType)ExecutionTail.Invoke();
        }
        public List<object> ExecuteBlock()
        {
            List<object> results = new List<object>();
            Commands.ForEach(_ => results.Add(_.Execute()));
            return results;
        }

        public List<ExpectedReturnType> TryExecuteBlock<ExpectedReturnType>()
        {
            List<ExpectedReturnType> results = new List<ExpectedReturnType>();
            Commands.ForEach(_ => results.Add((ExpectedReturnType)_.Execute()));
            return results;
        }
    }
}
