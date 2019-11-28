using CommandLine;
using System;

namespace CreepyCrawly.Utils
{
    public class InstanceOptions
    {
        public static Options Options { get; private set; }

        public static void GenerateOptionsFromArgs(string[] args)
        {
            var parser = new Parser(with => { with.EnableDashDash = true; with.HelpWriter = Console.Out; });
            var result = parser.ParseArguments<Options>(args);
            result.WithParsed<Options>(_ => Options = _);
        }
    }
}
