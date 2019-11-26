using System;
using System.Collections.Generic;

namespace CreepyCrawly.Output
{
    public class OutputSingleton
    {
        private static List<IOutputter> _Outputters = new List<IOutputter>();

        public static void CreateFileOutputter(string filePath)
        {
            _Outputters.Add(new FileOutputter(filePath));
        }
        public static void CreateConsoleOutputter()
        {
            _Outputters.Add(new ConsoleOutputter());
        }

        public static void WriteOutputToAllOutputters(object output)
        {
            _Outputters.ForEach(_ => _.WriteOutput(output));
        }
    }
}
