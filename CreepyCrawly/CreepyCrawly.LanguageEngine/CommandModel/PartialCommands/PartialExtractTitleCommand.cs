﻿using CreepyCrawly.Output;
using CreepyCrawly.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.LanguageEngine.CommandModel.PartialCommands
{
    public class PartialExtractTitleCommand : IPartialCommand
    {
        public string Name { get; private set; }
        public Func<object> Execution { get; private set; }

        public PartialExtractTitleCommand(Func<object> execution)
        {
            Name = "EXTRACT_TITLE";
            Execution = execution;
        }

        public object Execute()
        {
            try
            {
                object result = Execution.Invoke();
                //OutputSingleton.WriteToTextOutputters(result);
                return result;
            }
            catch (Exception e)
            {
                ErrorHandler.ReportCommandExecutionFailed(e, Name);
                return null;
            }
        }

        public ExpectedReturnType TryExecute<ExpectedReturnType>()
        {
            return (ExpectedReturnType)Execute();
        }
    }
}
