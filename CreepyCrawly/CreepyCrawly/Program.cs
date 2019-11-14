using CreepyCrawly.ExecutionPlanning;
using CreepyCrawly.LanguageEngine;
using System;

namespace CreepyCrawly
{
    class Program
    {
        static void Main(string[] args)
        {
            string scriptText = System.IO.File.ReadAllText("D:/CreepyCrawly/test1.cl");
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
                    ExecutionPlanning.Model.ExecutionPlan plan = ExecutionPlanFactory.GenerateSeleniumExecutionPlan(crawlLangEngine.StartingContext);
                    SeleniumExecutionEngine.SeleniumExecutionEngine.StartDriver(plan.OnRootUrl);
                    plan.SimpleCommands.ForEach(cmd => cmd.Execute());
                    
                }
                catch(Exception e)
                {
                    Console.WriteLine("An error occurred during script execution. See the following stacktrace:");
                    Console.WriteLine(e.StackTrace);
                }
                SeleniumExecutionEngine.SeleniumExecutionEngine.StopDriver();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

        }
    }
}
