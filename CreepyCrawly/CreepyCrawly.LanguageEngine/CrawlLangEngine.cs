using Antlr4.Runtime;
using CreepyCrawly.LanguageDefinition;
using System;
using System.Collections.Generic;
using static CreepyCrawly.LanguageDefinition.CrawlLangParser;

namespace CreepyCrawly.LanguageEngine
{
    public class CrawlLangEngine
    {
        public string ScriptText { get; private set; }
        public ProgContext StartingContext { get; set; }
        public List<string> Errors { get; private set; }
        public bool HasErrors { get => Errors.Count != 0; }
        public CrawlLangEngine(string scriptText)
        {
            ScriptText = scriptText;
            Errors = new List<string>();
            CreateParseTree();
        }

        private void CreateParseTree()
        {
            AntlrInputStream inputStream = new AntlrInputStream(ScriptText);
            CrawlLangLexer lexer = new CrawlLangLexer(inputStream);

            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            CrawlLangParser parser = new CrawlLangParser(commonTokenStream);

            VerboseErrorListener errorListener = new VerboseErrorListener();
            parser.RemoveErrorListeners();
            parser.AddErrorListener(errorListener);

            StartingContext = parser.prog();
            Errors = errorListener.Errors;
        }
    }
}
