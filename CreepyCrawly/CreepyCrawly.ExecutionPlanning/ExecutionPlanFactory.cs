using CreepyCrawly.ExecutionPlanning.Model;
using CreepyCrawly.SeleniumExecutionEngine;
using System;
using System.Collections.Generic;
using static CreepyCrawly.LanguageDefinition.CrawlLangParser;
using CreepyCrawly.LanguageDefinition;

namespace CreepyCrawly.ExecutionPlanning
{
    public class ExecutionPlanFactory
    {
        public static ExecutionPlan GenerateSeleniumExecutionPlan(ProgContext context)
        {
            List<ISimpleCommand> commands = new List<ISimpleCommand>();
            string rootUrl = context.on_root_command().www_url().TEXT().GetText().Trim('\'');
            var ctx_commands = context.on_root_command().command_block().children;
            foreach (var ctx_command in ctx_commands)
            {
                if (ctx_command.GetText().StartsWith("INPUT"))
                {
                    Input_commandContext input = ((Simple_commandContext)ctx_command.Payload).input_command();
                    InputCommand inputCommand = new InputCommand(input.selector().TEXT().GetText().Trim('\''),
                                                                 input.text_value().TEXT().GetText().Trim('\''),
                                                                 SeleniumExecutionMethods.Input
                                                                );
                    commands.Add(inputCommand);
                }
                else if (ctx_command.GetText().StartsWith("CLICK"))
                {
                    Click_commandContext click = ((Simple_commandContext)ctx_command.Payload).click_command();
                    ClickCommand clickCommand = new ClickCommand(click.selector().TEXT().GetText().Trim('\''),
                                                                SeleniumExecutionMethods.Click
                                                                );
                    commands.Add(clickCommand);
                }
                else if (ctx_command.GetText().StartsWith("WAIT_LOAD"))
                {
                    Wait_load_commandContext waitLoad = ((Simple_commandContext)ctx_command.Payload).wait_load_command();
                    WaitLoadCommand waitLoadCommand = new WaitLoadCommand(waitLoad.selector().GetText().Trim('\''),
                                                                          Convert.ToInt32(waitLoad.wait_amount().POSITIVE_INTEGER().GetText()),
                                                                          SeleniumExecutionMethods.WaitLoad
                                                                         );
                    commands.Add(waitLoadCommand);
                }
                else if (ctx_command.GetText().StartsWith("WAIT"))//WAIT and WAIT_LOAD should not be moved as one is the others' string prefix - I should fix this
                {
                    Wait_commandContext wait = ((Simple_commandContext)ctx_command.Payload).wait_command();
                    WaitCommand waitCommand = new WaitCommand(Convert.ToInt32(wait.wait_amount().POSITIVE_INTEGER().GetText()),
                                                              SeleniumExecutionMethods.Wait
                                                             );
                    commands.Add(waitCommand);
                }
                else if (ctx_command.GetText().StartsWith("SUBMIT"))
                {
                    Submit_commandContext submit = ((Simple_commandContext)ctx_command.Payload).submit_command();
                    SubmitCommand submitCommand = new SubmitCommand(submit.selector().GetText().Trim('\''),
                                                              SeleniumExecutionMethods.Submit
                                                             );
                    commands.Add(submitCommand);
                }
                else if (ctx_command.GetText().StartsWith("SELECT"))
                {
                    Select_commandContext select = ((Simple_commandContext)ctx_command.Payload).select_command();
                    SelectCommand selectCommand = new SelectCommand(select.selector().GetText().Trim('\''),
                                                                    Convert.ToInt32(select.select_index().GetText()),
                                                                    SeleniumExecutionMethods.Select
                                                                   );
                    commands.Add(selectCommand);
                }
            }
            return new ExecutionPlan(commands, rootUrl);
        }
    }
}
