using CreepyCrawly.WPFApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CreepyCrawly.WPFApp.Views
{
    /// <summary>
    /// Interaction logic for ScriptPage.xaml
    /// </summary>
    public partial class ScriptPage : Page
    {
        private ScriptPageViewModel _ViewModel;
        private ScriptPage()
        {
            InitializeComponent();
        }

        public ScriptPage(string filePath) : this()
        {
            _ViewModel = new ScriptPageViewModel(filePath);
            DataContext = _ViewModel;
        }
    }
}
