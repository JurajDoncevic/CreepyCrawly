using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.ConsoleApp
{
    public class Options
    {
        [Value(0, MetaName = "<input_file_path>", HelpText = "Input CrawlLang script file (.cl).", Required = true)]
        public string InputFilePath { get; set; }

        [Option("stdout", Default = false, HelpText = "Write results to STDOUT.", Required = false, SetName = "output")]
        public bool WriteToStdout { get; set; }

        [Option('r', "result-file", HelpText = "Write results to a text file at given path.", Required = false, SetName = "output")]
        public string ResultFilePath { get; set; }

        [Option('i', "image-dir", HelpText = "Save extracted images to directory at given path.", Required = false, SetName = "output")]
        public string ImageDirectoryPath { get; set; }

        [Option("engine", HelpText = "Use this engine to run the scraping.", Default = "selenium", Required = false)]
        public string UseEngine { get; set; }

        [Option("verbose-errors", HelpText = "Display all errors and warnings.", Default = false, Required = false)]
        public bool VerboseErrors { get; set; }

        [Option("no-browser", HelpText = "Run the scraping without displaying the browser.", Default = false, Required = false)]
        public bool RunHeadlessDriver { get; set; }

        [Option("disable-web-sec", HelpText = "Disable chromedriver's security option.", Default = false, Required = false)]
        public bool DisableWebSecurity { get; set; }

        [Option("chrome-driver-path", HelpText = "Path to chromedriver.exe", Default = "./chromedriver.exe", Required = false)]
        public string ChromeDriverPath { get; set; }

        [Usage(ApplicationAlias = "CreepyCrawly.exe")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>()
                {
                    new Example("Run a script like this:",
                                new Options()
                                {
                                    InputFilePath = "<input_file_path>",
                                    WriteToStdout = true
                                }),
                    new Example("Run CreepyCrawly with a CrawlLang script and display output in the command line:",
                                new Options()
                                {
                                    InputFilePath = "./script.cl",
                                    WriteToStdout = true
                                })
                };
            }
        }
    }
}
