using CreepyCrawly.ExecutionPlanning.Model;
using System;
using System.Collections.Generic;
using static CreepyCrawly.LanguageDefinition.CrawlLangParser;
using CreepyCrawly.LanguageDefinition;
using Antlr4.Runtime.Tree;
using CreepyCrawly.Core;

namespace CreepyCrawly.ExecutionPlanning
{
    public class ExecutionPlanFactory
    {
        public string ScriptText { get; private set; }
        private ProgContext _StartingContext { get; set; }
        public List<string> Errors { get; private set; }
        public bool HasErrors { get => Errors.Count != 0; }

        public ExecutionPlanFactory(string scriptText)
        {
            ScriptText = scriptText;
        }

        public ExecutionPlan GenerateExecutionPlan(string scriptText, IExecutionEngine executionEngine, ExecutionPlanListener executionPlanListener)
        {

        }
    }
}
