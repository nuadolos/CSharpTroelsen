using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CognitionWPF.CustomCommand
{
    /// <summary>
    /// Базовый класс для создания новых команд
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// Присоединение команды к диспетчеру команд
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }
}
