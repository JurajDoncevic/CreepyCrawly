using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CreepyCrawlyWPF.Options
{
    public class RunOptions : INotifyPropertyChanged
    {
        public bool DisableWebSecurity { get; set; } = false;
        public bool NoBrowser { get; set; } = false;
        public string OutputFilePath { get; set; }
        public string ImageOutputDirectory { get; set; }
        public string WebDriverPath { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
