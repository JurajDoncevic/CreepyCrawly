using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CreepyCrawlyWPF.Commands
{
    public class OpenFileCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _Execute;

        public OpenFileCommand(Action execute)
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
