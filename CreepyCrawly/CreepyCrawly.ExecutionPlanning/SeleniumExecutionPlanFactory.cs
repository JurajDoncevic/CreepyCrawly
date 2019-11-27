using CreepyCrawly.ExecutionPlanning.Model;
using CreepyCrawly.SeleniumExecutionEngine;
using System;
using System.Collections.Generic;
using static CreepyCrawly.LanguageDefinition.CrawlLangParser;
using CreepyCrawly.LanguageDefinition;
using Antlr4.Runtime.Tree;

namespace CreepyCrawly.ExecutionPlanning
{
    public class SeleniumExecutionPlanFactory
    {
        public static ExecutionPlan GenerateExecutionPlan(ProgContext context, SeleniumExecutionEngine.SeleniumExecutionEngine executionEngine)
        {
            List<ICommand> commands = new List<ICommand>();
            string rootUrl = context.on_root_command().www_url().TEXT().GetText().Trim('\'');

            ICommand rootCommand = new OnRootCommand(rootUrl, executionEngine.OnRoot);
            commands.Add(rootCommand);
            var commandContexts = context.on_root_command().command_block().children;

            foreach (var commandCtx in commandContexts)
            {
                if(commandCtx.GetType() == typeof(Simple_commandContext))
                {
                    ISimpleCommand simpleCommand = GetSimpleCommand((Simple_commandContext)commandCtx, executionEngine);
                    if(simpleCommand != null)
                    {
                        commands.Add(simpleCommand);
                    }
                }
                else if(commandCtx.GetType() == typeof(Complex_commandContext))
                {
                    IComplexCommand complexCommand = GetComplexCommand((Complex_commandContext)commandCtx, executionEngine);
                    if (complexCommand != null)
                    {
                        commands.Add(complexCommand);
                    }
                }
            }
            return new ExecutionPlan(commands, rootUrl);
        }

        private static IComplexCommand GetComplexCommand(Complex_commandContext ctx, SeleniumExecutionEngine.SeleniumExecutionEngine executionEngine)
        {
            List<ICommand> commands = new List<ICommand>();
            if (ctx.GetText().StartsWith("FOREACH"))
            {
                Foreach_commandContext foreach_ = ctx.foreach_command();
                var commandContexts = foreach_.command_block().children;
                foreach (var commandCtx in commandContexts)
                {
                    if (commandCtx.GetType() == typeof(Simple_commandContext))
                    {
                        ISimpleCommand simpleCommand = GetSimpleCommand((Simple_commandContext)commandCtx, executionEngine);
                        if (simpleCommand != null)
                        {
                            commands.Add(simpleCommand);
                        }
                    }
                    else if (commandCtx.GetType() == typeof(Complex_commandContext))
                    {
                        IComplexCommand complexCommand = GetComplexCommand((Complex_commandContext)commandCtx, executionEngine);
                        if (complexCommand != null)
                        {
                            commands.Add(complexCommand);
                        }
                    }
                }
                ForEachCommand forEachCommand = new ForEachCommand(commands,
                                                                   foreach_.selector().GetText().Trim('\''),
                                                                   executionEngine.ForEachHead,
                                                                   executionEngine.ForEachIterationBegin,
                                                                   executionEngine.ForEachIterationEnd,
                                                                   executionEngine.ForEachTail
                                                                   );
                return forEachCommand;
            }
            else
            {
                return null;
            }
        }

        private static ISimpleCommand GetSimpleCommand(Simple_commandContext ctx, SeleniumExecutionEngine.SeleniumExecutionEngine executionEngine)
        {
            if (ctx.GetText().StartsWith("INPUT"))
            {
                Input_commandContext input = ctx.input_command();
                InputCommand inputCommand = new InputCommand(input.selector().TEXT().GetText().Trim('\''),
                                                             input.text_value().TEXT().GetText().Trim('\''),
                                                             executionEngine.Input
                                                            );
                return inputCommand;
            }
            else if (ctx.GetText().StartsWith("CLICK"))
            {
                Click_commandContext click = ctx.click_command();
                ClickCommand clickCommand = new ClickCommand(click.selector().TEXT().GetText().Trim('\''),
                                                             executionEngine.Click
                                                             );
                return clickCommand;
            }
            else if (ctx.GetText().StartsWith("WAIT_LOAD"))
            {
                Wait_load_commandContext waitLoad = ctx.wait_load_command();
                WaitLoadCommand waitLoadCommand = new WaitLoadCommand(waitLoad.selector().GetText().Trim('\''),
                                                                      Convert.ToInt32(waitLoad.wait_amount().POSITIVE_INTEGER().GetText()),
                                                                      executionEngine.WaitLoad
                                                                      );
                return waitLoadCommand;
            }
            else if (ctx.GetText().StartsWith("WAIT"))//WAIT and WAIT_LOAD should not be moved as one is the others' string prefix - I should fix this
            {
                Wait_commandContext wait = ctx.wait_command();
                WaitCommand waitCommand = new WaitCommand(Convert.ToInt32(wait.wait_amount().POSITIVE_INTEGER().GetText()),
                                                          executionEngine.Wait
                                                          );
                return waitCommand;
            }
            else if (ctx.GetText().StartsWith("SUBMIT"))
            {
                Submit_commandContext submit = ctx.submit_command();
                SubmitCommand submitCommand = new SubmitCommand(submit.selector().GetText().Trim('\''),
                                                                executionEngine.Submit
                                                                );
                return submitCommand;
            }
            else if (ctx.GetText().StartsWith("SELECT"))
            {
                Select_commandContext select = ctx.select_command();
                SelectCommand selectCommand = new SelectCommand(select.selector().GetText().Trim('\''),
                                                                Convert.ToInt32(select.select_index().GetText()),
                                                                executionEngine.Select
                                                                );
                return selectCommand;
            }
            else if (ctx.GetText().StartsWith("EXTRACT"))
            {
                Extract_commandContext extract = ctx.extract_command();
                ExtractCommand extractCommand = new ExtractCommand(extract.selector().GetText().Trim('\''),
                                                                   executionEngine.Extract
                                                                  );
                return extractCommand;
            }
            else
            {
                return null;
            }
        }
    }
}
