using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CreepyCrawly.Output
{
    class FileTextOutputter : ITextOutputter
    {
        public string FilePath { get; private set; }

        public FileTextOutputter(string filePath)
        {
            FilePath = filePath;
        }

        public void WriteOutput(object output)
        {
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Close();
            }
            if (File.Exists(FilePath) && output != null)
            {
                File.AppendAllText(FilePath, output.ToString() + "\n");
            }
        }
    }
}
