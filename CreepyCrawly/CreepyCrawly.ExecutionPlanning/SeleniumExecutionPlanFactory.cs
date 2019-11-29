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
            return new ExecutionPlan(commands, rootUrl);
        }

        private static IComplexCommand GetComplexCommand(Complex_commandContext ctx, SeleniumExecutionEngine.SeleniumExecutionEngine executionEngine)
        {
            List<ICommand> commands = new List<ICommand>();
            if (ctx.GetText().StartsWith("FOREACH_CLICK"))
            {
                Foreach_click_commandContext foreach_click = ctx.foreach_click_command();
                var commandContexts = foreach_click.command_block().children;
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
                ForEachClickCommand forEachClickCommand = new ForEachClickCommand(commands,
                                                                   foreach_click.selector().GetText().Trim('\''),
                                                                   executionEngine.ForEachClick_Head,
                                                                   executionEngine.ForEachClick_IterationBegin,
                                                                   executionEngine.ForEachClick_IterationEnd,
                                                                   executionEngine.ForEachClick_Tail
                                                                   );
                return forEachClickCommand;
            }
            else if (ctx.GetText().StartsWith("GOTO_CLICK"))
            {
                Goto_click_commandContext goto_click = ctx.goto_click_command();
                var commandContexts = goto_click.command_block().children;
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
                GotoClickCommand gotoClickCommand = new GotoClickCommand(commands,
                                                                   goto_click.selector().GetText().Trim('\''),
                                                                   executionEngine.GotoClick_Head,
                                                                   executionEngine.GotoClick_Tail
                                                                   );
                return gotoClickCommand;
            }
            else if (ctx.GetText().StartsWith("WHILE_CLICK"))
            {
                While_click_commandContext while_click = ctx.while_click_command();
                var commandContexts = while_click.command_block().children;
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
                WhileClickCommand whileClickCommand = new WhileClickCommand(commands,
                                                                   while_click.selector().GetText().Trim('\''),
                                                                   executionEngine.WhileClick_Head,
                                                                   executionEngine.WhileClick_IterationBegin,
                                                                   executionEngine.WhileClick_IterationEnd,
                                                                   executionEngine.GotoClick_Tail
                                                                   );
                return whileClickCommand;
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
            else if (ctx.GetText().StartsWith("WAIT_FOR"))
            {
                Wait_for_commandContext waitLoad = ctx.wait_for_command();
                WaitForCommand waitLoadCommand = new WaitForCommand(waitLoad.selector().GetText().Trim('\''),
                                                                      Convert.ToInt32(waitLoad.wait_amount().POSITIVE_INTEGER().GetText()),
                                                                      executionEngine.WaitLoad
                                                                      );
                return waitLoadCommand;
            }
            else if (ctx.GetText().StartsWith("WAIT_MS"))
            {
                Wait_ms_commandContext wait = ctx.wait_ms_command();
                WaitMsCommand waitCommand = new WaitMsCommand(Convert.ToInt32(wait.wait_amount().POSITIVE_INTEGER().GetText()),
                                                          executionEngine.WaitMs
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
            else if (ctx.GetText().StartsWith("EXTRACT_TEXT"))
            {
                Extract_text_commandContext extract = ctx.extract_text_command();
                ExtractTextCommand extractCommand = new ExtractTextCommand(extract.selector().GetText().Trim('\''),
                                                                   executionEngine.ExtractText
                                                                  );
                return extractCommand;
            }
            else if (ctx.GetText().StartsWith("EXTRACT_IMAGE"))
            {
                Extract_image_commandContext extract = ctx.extract_image_command();
                ExtractImageCommand extractCommand = new ExtractImageCommand(extract.selector().GetText().Trim('\''),
                                                                             executionEngine.ExtractImage
                                                                             );
                return extractCommand;
            }
            else if (ctx.GetText().StartsWith("EXTRACT_ALL_IMAGES"))
            {
                Extract_all_images_commandContext extract = ctx.extract_all_images_command();
                ExtractAllImagesCommand extractCommand = new ExtractAllImagesCommand(extract.selector().GetText().Trim('\''),
                                                                                     executionEngine.ExtractAllImages
                                                                                     );
                return extractCommand;
            }
            else if (ctx.GetText().StartsWith("EXTRACT_TITLE"))
            {
                Extract_image_commandContext extract = ctx.extract_image_command();
                ExtractTitleCommand extractCommand = new ExtractTitleCommand(executionEngine.ExtractTitle);
                return extractCommand;
            }
            else
            {
                return null;
            }
        }
    }
}
