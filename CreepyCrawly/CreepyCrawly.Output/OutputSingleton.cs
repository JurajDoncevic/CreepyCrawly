using System;
using System.Collections.Generic;
using System.Linq;

namespace CreepyCrawly.Output
{
    public class OutputSingleton
    {
        private static List<IOutputter> _Outputters = new List<IOutputter>();
        public static void AssignEventHandlerToStringOutputters(EventHandler<NewOutputAppearedEventArgs> handler)
        {
            _Outputters.Where(_ => _ is StringOutputter).Select(_ => (StringOutputter)_).ToList().ForEach(_ => _.NewOutputAppeared += handler);
        }
        public static void CreateFileTextOutputter(string filePath)
        {
            _Outputters.Add(new FileTextOutputter(filePath));
        }
        public static void CreateStringOutputter()
        {
            _Outputters.Add(new StringOutputter());
        }
        public static void CreateConsoleTextOutputter()
        {
            _Outputters.Add(new ConsoleOutputter());
        }
        public static void CreateImageFileOutputter(string baseDirPath)
        {
            _Outputters.Add(new ImageFileOutputter(baseDirPath));
        }
        public static void WriteToTextOutputters(object output)
        {
            _Outputters.Where(_ => _ is ITextOutputter)
                       .ToList()
                       .ForEach(_ => _.WriteOutput(output));
        }

        public static void WriteToImageOutputters(object output)
        {
            _Outputters.Where(_ => _ is ImageFileOutputter)
                       .ToList()
                       .ForEach(_ => _.WriteOutput(output));
        }
        public static void WriteToConsoleOutputters(object output)
        {
            _Outputters.Where(_ => _ is ConsoleOutputter)
                       .ToList()
                       .ForEach(_ => _.WriteOutput(output));
        }
    }
}
