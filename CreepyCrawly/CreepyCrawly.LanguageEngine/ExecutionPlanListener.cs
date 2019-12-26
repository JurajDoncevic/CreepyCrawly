using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using CreepyCrawly.Core;
using CreepyCrawly.LanguageEngine.CommandModel;
using CreepyCrawly.LanguageDefinition;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.LanguageDefinition
{
    public class ExecutionPlanListener: CreepyCrawly.LanguageEngine.CrawlLangListener
    {
        private Guid _CurrentBlockId
        {
            get
            {
                return _BlockIds.Peek();
            }
        }
        private List<ICommand> _CurrentBlock
        {
            get
            {
                return _CommandBlocks[_CurrentBlockId];
            }
        }
        private Stack<Guid> _BlockIds;
        private Dictionary<Guid, List<ICommand>> _CommandBlocks;
        private string _RootURL;
        private IExecutionEngine _ExecutionEngine;
        public ExecutionPlan ExecutionPlan { get; private set; }
        public ExecutionPlanListener(IExecutionEngine executionEngine)
        {
            _CommandBlocks = new Dictionary<Guid, List<ICommand>>();
            _BlockIds = new Stack<Guid>();
            _RootURL = "";
            _ExecutionEngine = executionEngine;
        }

        public override void EnterClick_command([NotNull] CrawlLangParser.Click_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            ClickCommand clickCommand = new ClickCommand(selector, _ExecutionEngine.Click);
            _CurrentBlock.Add(clickCommand);
        }

        public override void EnterCommand_block([NotNull] CrawlLangParser.Command_blockContext context)
        {
            
        }

        public override void EnterComplex_command([NotNull] CrawlLangParser.Complex_commandContext context)
        {
            
        }

        public override void EnterDo_while_click_command([NotNull] CrawlLangParser.Do_while_click_commandContext context)
        {
            _BlockIds.Push(Guid.NewGuid());
            _CommandBlocks.Add(_CurrentBlockId, new List<ICommand>());            
        }

        public override void EnterEveryRule([NotNull] ParserRuleContext ctx)
        {
            
        }

        public override void EnterExtract_all_images_command([NotNull] CrawlLangParser.Extract_all_images_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            ExtractAllImagesCommand extractAllImagesCommand = new ExtractAllImagesCommand(selector, _ExecutionEngine.ExtractAllImages);
            _CurrentBlock.Add(extractAllImagesCommand);
        }

        public override void EnterExtract_href_command([NotNull] CrawlLangParser.Extract_href_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            ExtractHrefCommand extractHrefCommand = new ExtractHrefCommand(selector, _ExecutionEngine.ExtractHref);
            _CurrentBlock.Add(extractHrefCommand);
        }

        public override void EnterExtract_image_command([NotNull] CrawlLangParser.Extract_image_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            ExtractImageCommand extractImageCommand = new ExtractImageCommand(selector, _ExecutionEngine.ExtractImage);
            _CurrentBlock.Add(extractImageCommand);
        }

        public override void EnterExtract_script_command([NotNull] CrawlLangParser.Extract_script_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            ExtractScriptCommand extractScriptCommand = new ExtractScriptCommand(selector, _ExecutionEngine.ExtractScript);
            _CurrentBlock.Add(extractScriptCommand);
        }

        public override void EnterExtract_text_command([NotNull] CrawlLangParser.Extract_text_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            ExtractTextCommand extractTextCommand = new ExtractTextCommand(selector, _ExecutionEngine.ExtractText);
            _CurrentBlock.Add(extractTextCommand);
        }

        public override void EnterExtract_title_command([NotNull] CrawlLangParser.Extract_title_commandContext context)
        {
            ExtractTitleCommand extractTitleCommand = new ExtractTitleCommand(_ExecutionEngine.ExtractTitle);
            _CurrentBlock.Add(extractTitleCommand);
        }

        public override void EnterExtract_to_csv_command([NotNull] CrawlLangParser.Extract_to_csv_commandContext context)
        {
            List<string> selectors = new List<string>();
            foreach(var selectorCtx in context.selector())
            {
                selectors.Add(selectorCtx.GetText().Trim('\''));
            }
            ExtractToCsvCommand extractToCsvCommand = new ExtractToCsvCommand(selectors, _ExecutionEngine.ExtractToCsv);
        }

        public override void EnterForeach_click_command([NotNull] CrawlLangParser.Foreach_click_commandContext context)
        {
            _BlockIds.Push(Guid.NewGuid());
            _CommandBlocks.Add(_CurrentBlockId, new List<ICommand>());
        }

        public override void EnterForeach_href_command([NotNull] CrawlLangParser.Foreach_href_commandContext context)
        {
            _BlockIds.Push(Guid.NewGuid());
            _CommandBlocks.Add(_CurrentBlockId, new List<ICommand>());
        }

        public override void EnterGoto_click_command([NotNull] CrawlLangParser.Goto_click_commandContext context)
        {
            _BlockIds.Push(Guid.NewGuid());
            _CommandBlocks.Add(_CurrentBlockId, new List<ICommand>());
        }

        public override void EnterGoto_src_command([NotNull] CrawlLangParser.Goto_src_commandContext context)
        {
            _BlockIds.Push(Guid.NewGuid());
            _CommandBlocks.Add(_CurrentBlockId, new List<ICommand>());
        }

        public override void EnterInput_command([NotNull] CrawlLangParser.Input_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            string value = context.text_value().TEXT().GetText().Trim('\'');

            InputCommand inputCommand = new InputCommand(selector, value, _ExecutionEngine.Input);
            _CurrentBlock.Add(inputCommand);
        }

        public override void EnterOn_root_command([NotNull] CrawlLangParser.On_root_commandContext context)
        {
            _BlockIds.Push(Guid.NewGuid());
            _CommandBlocks.Add(_CurrentBlockId, new List<ICommand>());
            _RootURL = context.www_url().GetText().Trim('\'');

            OnRootCommand onRootCommand = new OnRootCommand(_RootURL, _ExecutionEngine.OnRoot);

            _CurrentBlock.Add(onRootCommand);
        }

        public override void EnterProg([NotNull] CrawlLangParser.ProgContext context)
        {
            
        }

        public override void EnterSelect_command([NotNull] CrawlLangParser.Select_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            int index = Convert.ToInt32(context.select_index().GetText());
            SelectCommand selectCommand = new SelectCommand(selector, index, _ExecutionEngine.Select);
        }

        public override void EnterSelect_index([NotNull] CrawlLangParser.Select_indexContext context)
        {
            
        }

        public override void EnterSimple_command([NotNull] CrawlLangParser.Simple_commandContext context)
        {
            
        }

        public override void EnterSubmit_command([NotNull] CrawlLangParser.Submit_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            SubmitCommand submitCommand = new SubmitCommand(selector, _ExecutionEngine.Submit);
            _CurrentBlock.Add(submitCommand);
        }

        public override void EnterWait_for_command([NotNull] CrawlLangParser.Wait_for_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            int amount = Convert.ToInt32(context.wait_amount().GetText());
            WaitForCommand waitForCommand = new WaitForCommand(selector, amount, _ExecutionEngine.WaitFor);
            _CurrentBlock.Add(waitForCommand);
        }

        public override void EnterWait_ms_command([NotNull] CrawlLangParser.Wait_ms_commandContext context)
        {
            int amount = Convert.ToInt32(context.wait_amount().GetText());
            WaitMsCommand waitMsCommand = new WaitMsCommand(amount, _ExecutionEngine.WaitMs);
            _CurrentBlock.Add(waitMsCommand);
        }

        public override void EnterWhile_click_command([NotNull] CrawlLangParser.While_click_commandContext context)
        {
            _BlockIds.Push(Guid.NewGuid());
            _CommandBlocks.Add(_CurrentBlockId, new List<ICommand>());
        }

        public override void ExitClick_command([NotNull] CrawlLangParser.Click_commandContext context)
        {
            
        }

        public override void ExitCommand_block([NotNull] CrawlLangParser.Command_blockContext context)
        {
            
        }

        public override void ExitComplex_command([NotNull] CrawlLangParser.Complex_commandContext context)
        {
            
        }

        public override void ExitDo_while_click_command([NotNull] CrawlLangParser.Do_while_click_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            
            DoWhileClickCommand doWhileClickCommand = new DoWhileClickCommand(_CurrentBlock, selector,
                                                                             _ExecutionEngine.DoWhileClick_Head,
                                                                             _ExecutionEngine.DoWhileClick_IterationBegin,
                                                                             _ExecutionEngine.DoWhileClick_IterationEnd,
                                                                             _ExecutionEngine.DoWhileClick_Tail);
            _BlockIds.Pop();
            _CurrentBlock.Add(doWhileClickCommand);
        }

        public override void ExitEveryRule([NotNull] ParserRuleContext ctx)
        {
            
        }

        public override void ExitExtract_all_images_command([NotNull] CrawlLangParser.Extract_all_images_commandContext context)
        {
            
        }

        public override void ExitExtract_href_command([NotNull] CrawlLangParser.Extract_href_commandContext context)
        {
            
        }

        public override void ExitExtract_image_command([NotNull] CrawlLangParser.Extract_image_commandContext context)
        {
            
        }

        public override void ExitExtract_script_command([NotNull] CrawlLangParser.Extract_script_commandContext context)
        {
            
        }

        public override void ExitExtract_text_command([NotNull] CrawlLangParser.Extract_text_commandContext context)
        {
            
        }

        public override void ExitExtract_title_command([NotNull] CrawlLangParser.Extract_title_commandContext context)
        {
            
        }

        public override void ExitExtract_to_csv_command([NotNull] CrawlLangParser.Extract_to_csv_commandContext context)
        {
            
        }

        public override void ExitForeach_click_command([NotNull] CrawlLangParser.Foreach_click_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');

            ForEachClickCommand forEachClickCommand = new ForEachClickCommand(_CurrentBlock, selector,
                                                                              _ExecutionEngine.ForEachClick_Head,
                                                                              _ExecutionEngine.ForEachClick_IterationBegin,
                                                                              _ExecutionEngine.ForEachClick_IterationEnd,
                                                                              _ExecutionEngine.ForEachClick_Tail);
            _BlockIds.Pop();
            _CurrentBlock.Add(forEachClickCommand);
        }


        public override void ExitForeach_href_command([NotNull] CrawlLangParser.Foreach_href_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            ForEachHrefCommand forEachHrefCommand = new ForEachHrefCommand(_CurrentBlock, selector,
                                                                           _ExecutionEngine.ForEachHref_Head,
                                                                           _ExecutionEngine.ForEachHref_IterationBegin,
                                                                           _ExecutionEngine.ForEachHref_IterationEnd,
                                                                           _ExecutionEngine.ForEachHref_Tail);

            _BlockIds.Pop();
            _CurrentBlock.Add(forEachHrefCommand);
        }

        public override void ExitGoto_click_command([NotNull] CrawlLangParser.Goto_click_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            GotoClickCommand gotoClickCommand = new GotoClickCommand(_CurrentBlock, selector,
                                                                     _ExecutionEngine.GotoClick_Head,
                                                                     _ExecutionEngine.GotoClick_Tail);

            _BlockIds.Pop();
            _CurrentBlock.Add(gotoClickCommand);
        }

        public override void ExitGoto_src_command([NotNull] CrawlLangParser.Goto_src_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            /*
             *             GotoSrcCommand gotoSrcCommand = new GotoSrcCommand(_CurrentBlock, selector,
                                                                     _ExecutionEngine.GotoClick_Head,
                                                                     _ExecutionEngine.GotoClick_Tail);
             */
            _BlockIds.Pop();
            //_CurrentBlock.Add(gotoSrcCommand);
            throw new NotImplementedException("This command has not been implemented yet");

        }

        public override void ExitInput_command([NotNull] CrawlLangParser.Input_commandContext context)
        {
            
        }

        public override void ExitOn_root_command([NotNull] CrawlLangParser.On_root_commandContext context)
        {
            List<ICommand> commands = _CurrentBlock;
            _BlockIds.Pop();

            ExecutionPlan = new ExecutionPlan(commands, _RootURL);
        }

        public override void ExitProg([NotNull] CrawlLangParser.ProgContext context)
        {
            
        }

        public override void ExitSelect_command([NotNull] CrawlLangParser.Select_commandContext context)
        {
            
        }

        public override void ExitSimple_command([NotNull] CrawlLangParser.Simple_commandContext context)
        {
            
        }

        public override void ExitSubmit_command([NotNull] CrawlLangParser.Submit_commandContext context)
        {
            
        }

        public override void ExitWait_amount([NotNull] CrawlLangParser.Wait_amountContext context)
        {
            
        }

        public override void ExitWait_for_command([NotNull] CrawlLangParser.Wait_for_commandContext context)
        {
            
        }

        public override void ExitWait_ms_command([NotNull] CrawlLangParser.Wait_ms_commandContext context)
        {
            
        }

        public override void ExitWhile_click_command([NotNull] CrawlLangParser.While_click_commandContext context)
        {
            string selector = context.selector().GetText().Trim('\'');
            WhileClickCommand whileClickCommand = new WhileClickCommand(_CurrentBlock, selector,
                                                                        _ExecutionEngine.WhileClick_Head,
                                                                        _ExecutionEngine.WhileClick_IterationBegin,
                                                                        _ExecutionEngine.WhileClick_IterationEnd,
                                                                        _ExecutionEngine.WhileClick_Tail);

            _BlockIds.Pop();
            _CurrentBlock.Add(whileClickCommand);

        }
    }
}
