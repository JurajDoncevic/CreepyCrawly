using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly
{
    public class Options
    {
        [Value(0, MetaName = "<input_file_path>", HelpText = "Input CrawlLang script file (.cl).", Required = true)]
        public string InputFilePath { get; set; }

        [Option("stdout", Default = false, HelpText = "Write results to STDOUT.", Required = false, SetName = "output")]
        public bool WriteToStdout { get; set; }

        [Option('r', "result-file", HelpText = "Write results to a text file at given path.", Required = false, SetName = "output")]
        public string ResultFilePath { get; set; }

        [Option('i', "image-dir", HelpText = "Save extracted images to directory at given path", Required = false, SetName = "output", Default ="./images")]
        public string ImageDirectoryPath { get; set; }

        [Option("engine", Default = "selenium", Required = false)]
        public string UseEngine { get; set; }

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
