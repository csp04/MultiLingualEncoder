using System;
using System.Windows.Input;

namespace mle_app.Common
{
    public class DelegateCommand : ICommand
    {
        private readonly Delegate _command;
        private readonly Func<object, bool> _canExecute;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action command, Func<object, bool> canExecute = null)
        {
            _canExecute = canExecute;
            _command = command ?? throw new ArgumentNullException();
        }

        public DelegateCommand(Action<object> command, Func<object, bool> canExecute = null)
        {
            _canExecute = canExecute;
            _command = command ?? throw new ArgumentNullException();
        }

        public void Execute(object parameter = null)
        {
            _command.DynamicInvoke(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

    }
}