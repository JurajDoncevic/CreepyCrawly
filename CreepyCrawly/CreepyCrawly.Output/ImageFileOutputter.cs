using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CreepyCrawly.Output
{
    class ImageFileOutputter : IFileOutputter
    {
        public string BaseDirPath { get; private set; }
        private int _FileCount;
        public ImageFileOutputter(string baseDirPath)
        {
            BaseDirPath = baseDirPath;
            _FileCount = 0;
        }

        public void WriteOutput(object output)
        {
            _FileCount++;
            File.WriteAllBytes(BaseDirPath + "/" + _FileCount + ".png", (byte[])output);
        }

        public void WriteOutputWithName(object output, string fileName)
        {

        }
    }
}
