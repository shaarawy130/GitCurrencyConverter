using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CurrencyConverter
{
    class DelegationCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Func<object, bool> _canExecute;
        private Func<object, bool> p;

        public DelegationCommand(Action action)
            : this((o) => action())
        {
        }
        public DelegationCommand(Action<object> action)
            : this(action, (o) => true)
        {
        }

        public DelegationCommand(Action<object> action, Func<object, bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this._action(parameter);
        }
    }
}
