using CognitionWPF.CustomCommand;
using CognitionWPF.CustomCommand.RelayCommands;
using CognitionWPF.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CognitionWPF.MVVM.ViewModel
{
    /// <summary>
    /// Вся логика визуального отображения по паттерну "MVVM"
    /// должна храниться в модели представления
    /// </summary>
    public class UserDataViewModel
    {
        public IList<User> Users { get; } = new ObservableCollection<User>();

        #region Команды для привязки к кнопкам

        private RelayCommand<User> _addUserCommand = null;
        public RelayCommand<User> AddUserCmd => _addUserCommand ?? new RelayCommand<User>(AddUser, CanAddUser);
        private RelayCommand<User> _deleteUserCommand = null;
        public RelayCommand<User> DeleteUserCmd => _deleteUserCommand ?? new RelayCommand<User>(DeleteUser, CanDeleteUser);

        private ICommand _randomPasswordCommand = null;
        public ICommand RndPasswordCmd => _randomPasswordCommand ?? new RandomPasswordCommand();

        #endregion

        public UserDataViewModel()
        {
            Users.Add(new User { Id = 1, Login = "giJB29", Password = "Ijgbffsa21f", Fullname = "Голиков Анатолий", IsChanged = false });
            Users.Add(new User { Id = 2, Login = "kgi20a", Password = "85nfsafaew", Fullname = "Олова Марина", IsChanged = false });
        }

        #region Методы для команд

        private bool CanAddUser(User user) => true;

        private void AddUser(User user)
        {
            User newUser = new User()
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Fullname = user.Fullname
            };

            Users.Add(newUser);
        }

        private bool CanDeleteUser(User user) => true;

        private void DeleteUser(User user) => Users.Remove(user);

        #endregion
    }
}
