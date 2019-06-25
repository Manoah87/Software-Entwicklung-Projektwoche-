using System;
using System.Windows;
using System.Windows.Input;

namespace hfupilot.app.CustomFramework.mvvm
{
    class RelayCommand : ICommand
    {
        private Predicate<object> _canExecute;
        private Action<object> _method;
        readonly WeakEventManager _weakEventManager = new WeakEventManager();


        public RelayCommand(Action<object> method)
            : this(method, null)
        {
        }

        public RelayCommand(Action<object> method, Predicate<object> canExecute)
        {
            _method = method;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _method.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            // https://joshsmithonwpf.wordpress.com/2008/06/17/allowing-commandmanager-to-query-your-icommand-objects/
            // http://stackoverflow.com/questions/6634777/what-is-the-actual-task-of-canexecutechanged-and-commandmanager-requerysuggested

            //add { CommandManager.RequerySuggested += value; }
            //remove { CommandManager.RequerySuggested -= value; }

            add { _weakEventManager.AddEventHandler(value); }
            remove { _weakEventManager.RemoveEventHandler(value); }
        }
    }
}
