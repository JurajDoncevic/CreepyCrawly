using Antlr4.Runtime;
using CreepyCrawly.LanguageDefinition;
using CreepyCrawly.LanguageEngine.CommandModel;
using System;
using System.Collections.Generic;
using static CreepyCrawly.LanguageDefinition.CrawlLangParser;
using CreepyCrawly.Core;
using Antlr4.Runtime.Tree;

namespace CreepyCrawly.LanguageEngine
{
    public class CrawlLangEngine
    {
        public string ScriptText { get; private set; }
        public List<string> Errors { get; private set; }
        public bool HasErrors { get => Errors.Count != 0; }

        private IExecutionEngine _ExecutionEngine;
        private ProgContext _ProgContext;
        public CrawlLangEngine(string scriptText, IExecutionEngine executionEngine)
        {
            ScriptText = scriptText;
            _ExecutionEngine = executionEngine;
            Errors = new List<string>();

        }

        public void ParseScript()
        {
            AntlrInputStream inputStream = new AntlrInputStream(ScriptText);
            CrawlLangLexer lexer = new CrawlLangLexer(inputStream);

            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            CrawlLangParser parser = new CrawlLangParser(commonTokenStream);

            VerboseErrorListener errorListener = new VerboseErrorListener();
            parser.RemoveErrorListeners();
            parser.AddErrorListener(errorListener);
            Errors = errorListener.Errors;

            _ProgContext = parser.prog();
        }

        public ExecutionPlan GenerateExecutionPlan()
        {
            ExecutionPlan executionPlan = null;
            if (!HasErrors)
            {
                ExecutionPlanListener executionPlanListener = new ExecutionPlanListener(_ExecutionEngine);
                ParseTreeWalker.Default.Walk(executionPlanListener, _ProgContext);
                executionPlan = executionPlanListener.ExecutionPlan;
            }

            return executionPlan;
        }
    }
}
