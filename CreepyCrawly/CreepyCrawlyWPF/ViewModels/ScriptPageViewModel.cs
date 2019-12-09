using CreepyCrawlyWPF.Commands;
using CreepyCrawlyWPF.Options;
using CreepyCrawlyWPF.ScriptRunning;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CreepyCrawlyWPF.ViewModels
{
    public class ScriptPageViewModel : INotifyPropertyChanged
    {
        public string FilePath { get; set; }
        public string ScriptText { get; set; }
        public RunOptions RunOptions { get; set; }
        public string ErrorsDisplay { get; private set; }
        public string OutputDisplay { get; private set; }
        public SaveFileCommand SaveFileCommand { get; set; }
        public RunScriptCommand RunScriptCommand { get; set; }
        public SetOutputTextFileCommand SetOutputTextFileCommand { get; set; }
        public SetOutputImageDirectoryCommand SetOutputImageDirectoryCommand { get; set; }
        public SetWebDriverPathCommand SetWebDriverPathCommand { get; set; }
        public ClearInputStringCommand ClearOutputTextFilePathCommand { get; set; }
        public ClearInputStringCommand ClearOutputImageDirectoryPathCommand { get; set; }
        public ClearInputStringCommand ClearWebDriverPathCommand { get; set; }
        public StopScriptCommand StopScriptCommand { get; set; }
        private Task _ScriptRunTask;
        private CancellationTokenSource _CancellationTokenSource;

        public ScriptPageViewModel(string filePath)
        {
            FilePath = filePath;
            RunOptions = new RunOptions();

            SaveFileCommand = new SaveFileCommand(SaveFile);
            RunScriptCommand = new RunScriptCommand(RunScript);
            SetOutputTextFileCommand = new SetOutputTextFileCommand(SetOutputTextFilePath);
            SetOutputImageDirectoryCommand = new SetOutputImageDirectoryCommand(SetOutputImageDirectoryPath);
            SetWebDriverPathCommand = new SetWebDriverPathCommand(SetWebDriverPath);
            ClearOutputTextFilePathCommand = new ClearInputStringCommand(ClearOutputTextFilePath);
            ClearOutputImageDirectoryPathCommand = new ClearInputStringCommand(ClearOutputImageDirectoryPath);
            ClearWebDriverPathCommand = new ClearInputStringCommand(ClearWebDriverPath);
            StopScriptCommand = new StopScriptCommand(StopScript);
            
            TryOpenSelf();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void TryOpenSelf()
        {
            if (!string.IsNullOrWhiteSpace(FilePath))
                ScriptText = System.IO.File.ReadAllText(FilePath);
            else
                ScriptText = "";
        }

        public void SaveFile()
        {
            if (!string.IsNullOrWhiteSpace(FilePath))
            {
                System.IO.File.WriteAllText(FilePath, ScriptText);
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Title = "Save CrawlLang script file",
                    AddExtension = true,
                    Filter = "CrawlLang script (*.cl)|*.cl",
                    DefaultExt = "cl"
                };
                if(saveFileDialog.ShowDialog() == true)
                {
                    System.IO.File.WriteAllText(saveFileDialog.FileName, ScriptText);
                    FilePath = saveFileDialog.FileName;
                }
            }
        }

        public void SetOutputTextFilePath()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "Designate or create output file",
                AddExtension = true,
                Filter = "Text file (*.txt)|*.txt",
                DefaultExt = "txt"
            };
            if(saveFileDialog.ShowDialog() == true)
            {
                RunOptions.OutputFilePath = saveFileDialog.FileName;
            }
        }
        public void SetWebDriverPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Find a webdriver",
                Filter = "Chromedriver (*.exe)|*.exe",
                DefaultExt = "exe"
            };
            if(openFileDialog.ShowDialog() == true)
            {
                RunOptions.WebDriverPath = openFileDialog.FileName;
            }
        }
        public void SetOutputImageDirectoryPath()
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            if(folderBrowserDialog.ShowDialog() == System.Windows.Forms. DialogResult.OK)
            {
                RunOptions.ImageOutputDirectory = folderBrowserDialog.SelectedPath;
            }
        }

        public void ClearOutputTextFilePath()
        {
            RunOptions.OutputFilePath = "";
        }
        public void ClearOutputImageDirectoryPath()
        {
            RunOptions.ImageOutputDirectory = "";
        }

        public void ClearWebDriverPath()
        {
            RunOptions.WebDriverPath = "";
        }

        public void StopScript()
        {
            if(_ScriptRunTask != null && _ScriptRunTask.Status == TaskStatus.Running)
            {
                _CancellationTokenSource.Cancel();

                SendMessageToOutputDisplay("Run cancelled by user!");
                
            }

        }
        public void SendMessageToOutputDisplay(string message)
        {
            OutputDisplay += message + "\n";
        }
        public void RunScript()
        {
            ErrorsDisplay = "";
            OutputDisplay = "";
            _CancellationTokenSource = new CancellationTokenSource();
            var token = _CancellationTokenSource.Token;

            using (ScriptRunner runner = new ScriptRunner(RunOptions, (o, e) => { SendMessageToOutputDisplay(e.Output); }))
            {
                token.Register(runner.StopScript);
                _ScriptRunTask = Task.Run(() =>
                {
                   runner.StartScript(ScriptText);
                   if (runner.ErrorMessages.Count != 0)
                   {
                       ErrorsDisplay = runner.ErrorMessages.Aggregate((_1, _2) => _1 + "\n" + _2);
                   }
                }, token);
            }
        }
    }
}
