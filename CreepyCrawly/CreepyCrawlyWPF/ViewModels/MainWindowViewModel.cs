using CreepyCrawlyWPF.Commands;
using CreepyCrawlyWPF.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace CreepyCrawlyWPF.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public NewFileCommand NewFileCommand { get; set; }
        public OpenFileCommand OpenFileCommand { get; set; }
        public SaveFileCommand SaveFileCommand { get; set; }
        public RunScriptCommand RunScriptCommand { get; set; }
        public ObservableCollection<ScriptTab> OpenedTabs { get; set; }
        public ScriptTab SelectedTab { get; set; }

        public MainWindowViewModel()
        {
            OpenedTabs = new ObservableCollection<ScriptTab>();
            NewFileCommand = new NewFileCommand(OpenNewFileTab);
            OpenFileCommand = new OpenFileCommand(OpenFile);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OpenNewFileTab()
        {
            var scriptTab = new ScriptTab("New file", new ScriptPage(""), CloseTab);
            OpenedTabs.Add(scriptTab);
            SelectedTab = OpenedTabs[OpenedTabs.Count - 1];
        }
        public void OpenNewFileTab(string filePath, string fileName)
        {
            var scriptTab = new ScriptTab(fileName, new ScriptPage(filePath), CloseTab);
            OpenedTabs.Add(scriptTab);
            SelectedTab = OpenedTabs[OpenedTabs.Count - 1];
        }
        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CrawlLang files (*.cl) |*.cl|All files (*.*)|*.*",
                Title = "Open a CrawlLang script"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                var fileName = openFileDialog.SafeFileName;
                OpenNewFileTab(filePath, fileName);
            }
        }

        public void CloseTab(ScriptTab tabToClose)
        {
            if(SelectedTab == tabToClose)
                if (OpenedTabs.Count - 1 > 0)
                    SelectedTab = OpenedTabs[OpenedTabs.Count - 1];
            OpenedTabs.Remove(tabToClose);

        }

        public void RunScriptOnSelectedTab()
        {
            if(SelectedTab != null)
            {

            }
        }
    }
}
