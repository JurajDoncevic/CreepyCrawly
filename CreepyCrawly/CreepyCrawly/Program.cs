﻿using CommandLine;
using CommandLine.Text;
using CreepyCrawly.ExecutionPlanning;
using CreepyCrawly.LanguageEngine;
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
            Options options = null;
            var parser = new Parser(with => { with.EnableDashDash = true; with.HelpWriter = Console.Out; });
            var result = parser.ParseArguments<Options>(args);
            //result.WithNotParsed(errs => { DisplayHelp(result, errs); CloseApp(); });
            result.WithParsed<Options>(_ => options = _);

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
                    ExecutionPlanning.Model.ExecutionPlan plan = SeleniumExecutionPlanFactory.GenerateExecutionPlan(crawlLangEngine.StartingContext);
                    SeleniumExecutionEngine.SeleniumExecutionEngine.StartDriver(plan.OnRootUrl);
                    if (SeleniumExecutionEngine.SeleniumExecutionEngine.DriverRunning)
                    {
                        plan.Commands.ForEach(cmd =>
                        {
                            var output = cmd.Execute();
                            if(output != null)
                            {
                                outputs.Add(output);
                                if (options.WriteToStdout)
                                {
                                    Console.WriteLine(output);
                                }
                                if(Uri.IsWellFormedUriString(options.ResultFilePath, UriKind.RelativeOrAbsolute))
                                {
                                    System.IO.File.AppendAllText(options.ResultFilePath, output + "\n");
                                }
                            }

                        });



                        SeleniumExecutionEngine.SeleniumExecutionEngine.StopDriver();
                    }
                    else
                    {
                        Console.Error.WriteLine("Could not start the execution engine :(\nMaybe you are missing an appropriate chromedriver in the app root directory.");
                    }

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("An error occurred during script execution with message:\n{0}\nSee the following stacktrace:\n{1}", e.Message, e.StackTrace);
                }


            }
            CloseApp();
        }


        public static void CloseApp()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Environment.Exit(0);
        }
        public static void WriteResultsToFile(List<object> results)
        {

        }
        public static void WriteResultsToConsole(List<object> results)
        {

        }
    }
}
