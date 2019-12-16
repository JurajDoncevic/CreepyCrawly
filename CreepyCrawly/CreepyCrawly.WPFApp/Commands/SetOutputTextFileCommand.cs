using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CreepyCrawly.WPFApp.Commands
{
    public class SetOutputTextFileCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _Execute;

        public SetOutputTextFileCommand(Action execute)
        {
            _Execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _Execute.Invoke();
        }
    }
}
