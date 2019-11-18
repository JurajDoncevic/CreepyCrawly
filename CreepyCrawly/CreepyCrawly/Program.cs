using CreepyCrawly.ExecutionPlanning;
using CreepyCrawly.LanguageEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace CreepyCrawly
{
    class Program
    {

        static void Main(string[] args)
        {
            ParseArgs(args);
            string scriptText = System.IO.File.ReadAllText(ScriptFilePath);
            CrawlLangEngine crawlLangEngine = new CrawlLangEngine(scriptText);
            if (crawlLangEngine.HasErrors)
            {
                Console.WriteLine("Found {0} error(s)!", crawlLangEngine.Errors.Count.ToString());
                crawlLangEngine.Errors.ForEach(err => Console.WriteLine(err));
            }
            else
            {
                Console.WriteLine("Script seems OK, starting execution!");
                try
                {
                    List<object> output = new List<object>();
                    ExecutionPlanning.Model.ExecutionPlan plan = SeleniumExecutionPlanFactory.GenerateExecutionPlan(crawlLangEngine.StartingContext);
                    SeleniumExecutionEngine.SeleniumExecutionEngine.StartDriver(plan.OnRootUrl);
                    plan.Commands.ForEach(cmd => output.Add(cmd.Execute()));

                    output.Remove(null);

                    //OUTPUT SOMEWHERE
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred during script execution with message:\n{0}\nSee the following stacktrace:\n{1}", e.Message, e.StackTrace);
                }
                SeleniumExecutionEngine.SeleniumExecutionEngine.StopDriver();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

        }


        public static void WriteResultsToFile(List<object> results)
        {

        }
        public static void WriteResultsToConsole(List<object> results)
        {

        }
    }
}
