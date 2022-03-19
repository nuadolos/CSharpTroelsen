using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitionWPF.MVVM.Model.Base
{
    public class EntityBase : INotifyDataErrorInfo
    {
        protected readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => _errors.Count != 0;

        /// <summary>
        /// Инициирует событие ErrorsChanged
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Возвращает ошибки указанного свойства. 
        /// В случае если поле пустое, то возвращаются ошибки всех свойств.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return _errors.Values;

            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        #region Добавление и очищение сообщений об ошибке

        /// <summary>
        /// Добавление ошибки в список
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="error"></param>
        protected void AddError(string propertyName, string error)
        {
            AddErrors(propertyName, new List<string> { error });
        }

        /// <summary>
        /// Добавление списка ошибок
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="errors"></param>
        protected bool AddErrors(string propertyName, IList<string> errors)
        {
            if (errors == null)
                return false;

            bool changed = false;
            if (!_errors.ContainsKey(propertyName))
            {
                _errors.Add(propertyName, new List<string>());
                changed = true;
            }

            foreach (string err in errors)
            {
                if (_errors[propertyName].Contains(err))
                    continue;

                _errors[propertyName].Add(err);
                changed = true;
            }

            if (changed)
                OnErrorsChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Очистка всех свойств или указанного свойства
        /// </summary>
        /// <param name="propertyName"></param>
        protected void ClearErrors(string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName))
                _errors.Clear();
            else
                _errors.Remove(propertyName);

            OnErrorsChanged(propertyName);
        }

        #endregion

        /// <summary>
        /// Проверка значения указанного свойства
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string[] GetErrorsFromAnnotations<T>(string propertyName, T value)
        {
            //Список, хранящий результаты выполненных проверок достоверности
            var results = new List<ValidationResult>();

            //Передача контексту имя свойства для его проверки
            var vc = new ValidationContext(this, null, null) { MemberName = propertyName };

            //Проверка значения данного свойства
            var isValid = Validator.TryValidateProperty(value, vc, results);

            //При отсутствии ошибок возвращается null, иначе массив ошибок
            return (isValid) ? null : Array.ConvertAll(results.ToArray(), o => o.ErrorMessage);
        }
    }
}
