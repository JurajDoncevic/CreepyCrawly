using CommandLine;
using CommandLine.Text;
using CreepyCrawly.ExecutionPlanning;
using CreepyCrawly.LanguageEngine;
using CreepyCrawly.SeleniumExecutionEngine;
using CreepyCrawly.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CreepyCrawly
{
    class Program
    {

        static void Main(string[] args)
        {

            Options options = GenerateOptionsFromArgs(args);

            if (options != null)
                Run(options);
            else
                CloseApp();

        }
        private static void Run(Options options)
        {
            string scriptText = "";
            try
            {
                scriptText = System.IO.File.ReadAllText(options.InputFilePath);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("An error occurred during opening of file:\n{0}\nSee the following stacktrace:\n{1}", e.Message, e.StackTrace);
                CloseApp();
            }

            CrawlLangEngine crawlLangEngine = new CrawlLangEngine(scriptText);
            if (crawlLangEngine.HasErrors)
            {
                Console.Error.WriteLine("Found {0} error(s)!", crawlLangEngine.Errors.Count.ToString());
                crawlLangEngine.Errors.ForEach(err => Console.Error.WriteLine(err));
            }
            else
            {
                Console.WriteLine("Script seems OK, starting execution!");
                try
                {
                    List<object> outputs = new List<object>();
                    using (SeleniumExecutionEngine.SeleniumExecutionEngine executionEngine = new SeleniumExecutionEngine.SeleniumExecutionEngine())
                    {
                        executionEngine.StartEngine();
                        ExecutionPlanning.Model.ExecutionPlan plan = SeleniumExecutionPlanFactory.GenerateExecutionPlan(crawlLangEngine.StartingContext, executionEngine);
                        if (options.WriteToStdout)
                        {
                            CreepyCrawly.Output.OutputSingleton.CreateConsoleTextOutputter();
                        }
                        if (Uri.IsWellFormedUriString(options.ResultFilePath, UriKind.RelativeOrAbsolute))
                        {
                            CreepyCrawly.Output.OutputSingleton.CreateFileTextOutputter(options.ResultFilePath);
                        }
                        if (Uri.IsWellFormedUriString(options.ResultFilePath, UriKind.RelativeOrAbsolute))
                        {
                            CreepyCrawly.Output.OutputSingleton.CreateImageFileOutputter(options.ImageDirectoryPath);
                        }
                        CreepyCrawly.Output.OutputSingleton.CreateStringOutputter();
                        CreepyCrawly.Output.OutputSingleton.AssignEventHandlerToStringOutputters(__NewOutputAppeared);
                        if (executionEngine.IsEngineOk)
                        {
                            plan.Commands.ForEach(cmd =>
                            {
                                cmd.Execute();
                            });
                        }
                        else
                        {
                            Console.Error.WriteLine("Engine wasn't started :(\nMaybe you are missing an appropriate chromedriver in the app root directory.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("An error occurred during script execution with message:\n{0}\nSee the following stacktrace:\n{1}", e.Message, e.StackTrace);
                }


            }
            CloseApp();
        }

        private static void __NewOutputAppeared(object sender, Output.NewOutputAppearedEventArgs e)
        {
            Console.WriteLine("STRING WRITER SAYS = " + e.Output + "AT:" +e.TimeAppeared.ToString());
        }

        public static void CloseApp()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        public static Options GenerateOptionsFromArgs(string[] args)
        {
            Options options = null;
            var parser = new Parser(with => { with.EnableDashDash = true; with.HelpWriter = Console.Out; });
            var result = parser.ParseArguments<Options>(args);
            result.WithParsed<Options>(_ => options = _);

            return options;
        }
        public static void SetErrorOptions(Options options)
        {
            ErrorHandler.DisplayVerboseErrors = options.VerboseErrors;
        }
        public static SeleniumExecutionEngineOptions CreateEngineOptions(Options options)
        {
            return new SeleniumExecutionEngineOptions()
            {
                RunHeadlessBrowser = options.RunHeadlessDriver,
                DisableWebSecurity = options.DisableWebSecurity
            };
        }
    }
}

