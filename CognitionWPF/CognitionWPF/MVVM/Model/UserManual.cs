using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CognitionWPF.MVVM.Model
{
    /// <summary>
    /// Наблюдаемый класс User,
    /// явно реализующий событие PropertyChanged
    /// с помощью ручного кода (Используется в качестве примера,
    /// что внутри инструмента PropertyChanged.Fody)
    /// </summary>
    public class UserManual : INotifyPropertyChanged
    {
        private int _id;
        private string _login;
        private string _password;
        private string _fullName;
        private bool _isChanged;

        public int Id 
        {
            get => _id;
            set
            {
                if (_id == value)
                    return;

                //Если в параметре атрибут [CallerMemberName] отсутствует
                //то можно использовать операцию nameof, возвращающая строковое имя элемента
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Login
        {
            get => _login;
            set
            {
                if (_login == value) 
                    return;

                _login = value;
                OnPropertyChanged();
            }
        }
        public string Password 
        {
            get => _password;
            set
            {
                if (_password == value)
                    return;

                _password = value;
                OnPropertyChanged();
            }
        }
        public string Fullname 
        {
            get => _fullName;
            set
            {
                if (_fullName == value)
                    return;

                _fullName = value;
                OnPropertyChanged();
            }
        }
        public bool IsChanged
        {
            get => _isChanged;
            set
            {
                if (_isChanged == value)
                    return;

                _isChanged = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName != nameof(IsChanged))
                IsChanged = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
