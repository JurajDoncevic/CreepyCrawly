using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

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

        public void WriteOutput(object base64Image)
        {
            _FileCount++;
            using (var stream = new MemoryStream(Convert.FromBase64String(base64Image.ToString())))
            {
                using (var bitmap = new Bitmap(stream))
                {
                    var filepath = Path.Combine(BaseDirPath, _FileCount.ToString() + ".png");
                    bitmap.Save(filepath, ImageFormat.Png);
                }
            }
        }

        public void WriteOutputWithName(string base64Image, string fileName)
        {
            using (var stream = new MemoryStream(Convert.FromBase64String(base64Image)))
            {
                using (var bitmap = new Bitmap(stream))
                {
                    var filepath = Path.Combine(BaseDirPath, fileName);
                    bitmap.Save(filepath, ImageFormat.Png);
                }
            }
        }
    }
}
