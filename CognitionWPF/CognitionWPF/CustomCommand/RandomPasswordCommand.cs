using CognitionWPF.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CognitionWPF.CustomCommand
{
    /// <summary>
    /// Класс, содержащий команду генерации нового пароля пользователя
    /// </summary>
    public class RandomPasswordCommand : CommandBase
    {
        /// <summary>
        /// Проверка входного параметра
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return parameter is User;
        }

        /// <summary>
        /// Если параметр прошел проверку, то выполняется данный метод
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            if (parameter is User us)
            {
                Random random = new Random();

                us.Password = random.Next(10_000_000, 99_999_999).ToString();
            }
        }
    }
}
