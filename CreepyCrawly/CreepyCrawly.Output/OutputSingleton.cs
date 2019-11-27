using System;
using System.Collections.Generic;
using System.Linq;

namespace CreepyCrawly.Output
{
    public class OutputSingleton
    {
        private static List<IOutputter> _Outputters = new List<IOutputter>();

        public static void CreateFileTextOutputter(string filePath)
        {
            _Outputters.Add(new FileTextOutputter(filePath));
        }
        public static void CreateConsoleTextOutputter()
        {
            _Outputters.Add(new ConsoleOutputter());
        }

        public static void WriteToTextOutputters(object output)
        {
            _Outputters.Where(_=>_ is ITextOutputter)
                       .ToList()
                       .ForEach(_ => _.WriteOutput(output));
        }

        public static void CreateImageFileOutputter()
        {

        }
    }
}
