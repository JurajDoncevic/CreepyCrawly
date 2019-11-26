using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CreepyCrawly.Output
{
    class FileOutputter : IOutputter
    {
        public string FilePath { get; private set; }

        public FileOutputter(string filePath)
        {
            FilePath = filePath;
        }

        public void WriteOutput(object output)
        {
            if (File.Exists(FilePath) && output != null)
            {
                File.AppendAllText(FilePath, output.ToString());
            }
        }
    }
}
