using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CreepyCrawlyWPF.Commands
{
    public class NewFileCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _Execute { get; set; }
        public NewFileCommand(Action execute)
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
