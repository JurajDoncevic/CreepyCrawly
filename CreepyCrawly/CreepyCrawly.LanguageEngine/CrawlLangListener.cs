using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using CreepyCrawly.LanguageDefinition;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.LanguageEngine
{
    public abstract class CrawlLangListener : CrawlLangBaseListener
    {
        public abstract override void EnterClick_command([NotNull] CrawlLangParser.Click_commandContext context);
        public abstract override void EnterCommand_block([NotNull] CrawlLangParser.Command_blockContext context);
        public abstract override void EnterComplex_command([NotNull] CrawlLangParser.Complex_commandContext context);
        public abstract override void EnterDo_while_click_command([NotNull] CrawlLangParser.Do_while_click_commandContext context);
        public abstract override void EnterEveryRule([NotNull] ParserRuleContext ctx);
        public abstract override void EnterExtract_all_images_command([NotNull] CrawlLangParser.Extract_all_images_commandContext context);
        public abstract override void EnterExtract_href_command([NotNull] CrawlLangParser.Extract_href_commandContext context);
        public abstract override void EnterExtract_image_command([NotNull] CrawlLangParser.Extract_image_commandContext context);
        public abstract override void EnterExtract_script_command([NotNull] CrawlLangParser.Extract_script_commandContext context);
        public abstract override void EnterExtract_text_command([NotNull] CrawlLangParser.Extract_text_commandContext context);
        public abstract override void EnterExtract_title_command([NotNull] CrawlLangParser.Extract_title_commandContext context);
        public abstract override void EnterExtract_to_csv_command([NotNull] CrawlLangParser.Extract_to_csv_commandContext context);
        public abstract override void EnterForeach_click_command([NotNull] CrawlLangParser.Foreach_click_commandContext context);
        public abstract override void EnterForeach_href_command([NotNull] CrawlLangParser.Foreach_href_commandContext context);
        public abstract override void EnterGoto_click_command([NotNull] CrawlLangParser.Goto_click_commandContext context);
        public abstract override void EnterGoto_src_command([NotNull] CrawlLangParser.Goto_src_commandContext context);
        public abstract override void EnterInput_command([NotNull] CrawlLangParser.Input_commandContext context);
        public abstract override void EnterOn_root_command([NotNull] CrawlLangParser.On_root_commandContext context);
        public abstract override void EnterProg([NotNull] CrawlLangParser.ProgContext context);
        public abstract override void EnterSelect_command([NotNull] CrawlLangParser.Select_commandContext context);
        public abstract override void EnterSimple_command([NotNull] CrawlLangParser.Simple_commandContext context);
        public abstract override void EnterSubmit_command([NotNull] CrawlLangParser.Submit_commandContext context);
        public abstract override void EnterWait_for_command([NotNull] CrawlLangParser.Wait_for_commandContext context);
        public abstract override void EnterWait_ms_command([NotNull] CrawlLangParser.Wait_ms_commandContext context);
        public abstract override void EnterWhile_click_command([NotNull] CrawlLangParser.While_click_commandContext context);
        public abstract override void ExitClick_command([NotNull] CrawlLangParser.Click_commandContext context);
        public abstract override void ExitCommand_block([NotNull] CrawlLangParser.Command_blockContext context);
        public abstract override void ExitComplex_command([NotNull] CrawlLangParser.Complex_commandContext context);
        public abstract override void ExitDo_while_click_command([NotNull] CrawlLangParser.Do_while_click_commandContext context);
        public abstract override void ExitEveryRule([NotNull] ParserRuleContext ctx);
        public abstract override void ExitExtract_all_images_command([NotNull] CrawlLangParser.Extract_all_images_commandContext context);
        public abstract override void ExitExtract_href_command([NotNull] CrawlLangParser.Extract_href_commandContext context);
        public abstract override void ExitExtract_image_command([NotNull] CrawlLangParser.Extract_image_commandContext context);
        public abstract override void ExitExtract_script_command([NotNull] CrawlLangParser.Extract_script_commandContext context);
        public abstract override void ExitExtract_text_command([NotNull] CrawlLangParser.Extract_text_commandContext context);
        public abstract override void ExitExtract_title_command([NotNull] CrawlLangParser.Extract_title_commandContext context);
        public abstract override void ExitExtract_to_csv_command([NotNull] CrawlLangParser.Extract_to_csv_commandContext context);
        public abstract override void ExitForeach_click_command([NotNull] CrawlLangParser.Foreach_click_commandContext context);
        public abstract override void ExitForeach_href_command([NotNull] CrawlLangParser.Foreach_href_commandContext context);
        public abstract override void ExitGoto_click_command([NotNull] CrawlLangParser.Goto_click_commandContext context);
        public abstract override void ExitGoto_src_command([NotNull] CrawlLangParser.Goto_src_commandContext context);
        public abstract override void ExitInput_command([NotNull] CrawlLangParser.Input_commandContext context);
        public abstract override void ExitOn_root_command([NotNull] CrawlLangParser.On_root_commandContext context);
        public abstract override void ExitProg([NotNull] CrawlLangParser.ProgContext context);
        public abstract override void ExitSelect_command([NotNull] CrawlLangParser.Select_commandContext context);
        public abstract override void ExitSimple_command([NotNull] CrawlLangParser.Simple_commandContext context);
        public abstract override void ExitSubmit_command([NotNull] CrawlLangParser.Submit_commandContext context);
        public abstract override void ExitWait_for_command([NotNull] CrawlLangParser.Wait_for_commandContext context);
        public abstract override void ExitWait_ms_command([NotNull] CrawlLangParser.Wait_ms_commandContext context);
        public abstract override void ExitWhile_click_command([NotNull] CrawlLangParser.While_click_commandContext context);
    }
}
