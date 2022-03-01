using SkillBoxCourseCSharp.EntityFramework.Entities;
using SkillBoxCourseCSharp.EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.EntityFramework
{
    public static class FuncEF
    {
        #region Добавление записи(-ей) в таблицу User

        public static void AddUser(User user)
        {
            ManufactureEntities.Context.User.Add(user);

            try
            {
                Console.WriteLine($"Состояние экземпляра с Id = {user.Id}" +
                    $" перед сохранение: {ManufactureEntities.Context.Entry(user).State}");
                ManufactureEntities.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void AddRangeUsers(IEnumerable<User> listUsers)
        {
            ManufactureEntities.Context.User.AddRange(listUsers);

            try
            {
                ManufactureEntities.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Выборка записей из доступных таблиц

        private class UserName
        {
            public string Fullname { get; set; }
            public override string ToString() => $"Существует пользователь {Fullname}";
        }

        public static void PrintAllUsers(bool sqlQuery = false, bool linqQuery = false)
        {
            foreach (User user in ManufactureEntities.Context.User)
            {
                Console.WriteLine(user);
            }

            if (sqlQuery)
            {
                Console.ReadKey();
                Console.Clear();

                //SQL-запрос на получение записей, где Id = 2
                foreach (User user in ManufactureEntities.Context.User
                    .SqlQuery("Select Id,Login,Password,FullName,RoleId from [User] where Id=@p0", "2"))
                {
                    Console.WriteLine(user);
                }

                Console.WriteLine();

                //SQL-запрос через свойство Database дает возможность использовать данные из любой таблицы в любом произодном классе
                foreach (UserName userName in ManufactureEntities.Context
                    .Database.SqlQuery(typeof(UserName), "Select FullName from [User]"))
                {
                    Console.WriteLine(userName);
                }
            }

            if (linqQuery)
            {
                Console.ReadKey();
                Console.Clear();

                //Запрос выборки не выполняется 
                DbSet<User> allData = ManufactureEntities.Context.User;

                //Применение LINQ
                //Запрос выборки не выполняется 
                var linqUser = from user in allData
                               where user.RoleId == 2
                               select new { user.Fullname, user.Role.Name };

                //Происходит выполнение запроса выборки из-за проходу по результирующей коллекции
                foreach (var userAnonym in linqUser)
                {
                    Console.WriteLine(userAnonym);
                }
            }
        }

        public static void PrintAllUserRoles(bool lazyOff = false)
        {
            //Пример ленивой загрузки, так как используется два запроса выборки
            foreach (Role role in ManufactureEntities.Context.Role)
            {
                Console.WriteLine($"\nПользователи с ролью {role.Name}\n");

                foreach (User us in role.User)
                {
                    Console.WriteLine($"{us.Fullname}");
                }

                Console.WriteLine("********************");
            }

            if (lazyOff)
            {
                ManufactureEntities.Context.Configuration.LazyLoadingEnabled = false;

                //Пример энергичной загрузки с помощью метода Include(), информирующий EF
                //о необходимости создания оператора SQL, который соединяет таблицы вместе
                foreach (Role role in ManufactureEntities.Context.Role.Include(r => r.User))
                {
                    Console.WriteLine($"\nПользователи с ролью {role.Name}\n");

                    foreach (User us in role.User)
                    {
                        Console.WriteLine($"{us.Fullname}");
                    }
                }

                //Пример явной загрузки с помощью метода Entry() и Load(), позволяющие
                //явным образом загрузить коллекцию или класс в конец навигационного свойства
                foreach (Role role in ManufactureEntities.Context.Role)
                {
                    Console.WriteLine($"\nПользователи с ролью {role.Name}\n");

                    //Метод Collection применяется для свойств с возвращающим значением ICollection
                    ManufactureEntities.Context.Entry(role).Collection(r => r.User).Load();

                    //Метод Reference применяется для свойств с возвращающим значением сущностей
                    //User test = ManufactureEntities.Context.User.Find(2);
                    //ManufactureEntities.Context.Entry(test).Reference(t => t.Role).Load();

                    foreach (User us in role.User)
                    {
                        Console.WriteLine($"{us.Fullname}");
                    }
                }

                ManufactureEntities.Context.Configuration.LazyLoadingEnabled = true;
            }
        }

        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            foreach (User user in ManufactureEntities.Context.User)
            {
                users.Add(user);
            }

            return users;
        }

        #endregion

        #region Удаление данных из таблицы User

        public static void RemoveUser(int userIdToDelete)
        {
            User user = ManufactureEntities.Context.User.Find(userIdToDelete);

            if (user != null)
            {
                ManufactureEntities.Context.User.Remove(user);

                Console.WriteLine($"\nСостояние объекта, подверженный удалению: {ManufactureEntities.Context.Entry(user).State}\n");

                try
                {
                    ManufactureEntities.Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void RemoveUser(User userToDelete)
        {
            if (userToDelete != null)
            {
                //Удаление записи из базы данных с использованием EntityState
                ManufactureEntities.Context.Entry(userToDelete).State = EntityState.Deleted;

                try
                {
                    ManufactureEntities.Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void RemoveUsers(IEnumerable<User> listUser)
        {
            if (listUser != null)
            {
                ManufactureEntities.Context.User.RemoveRange(listUser);

                try
                {
                    ManufactureEntities.Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        #endregion

        #region Обновление столбца "Login" в таблице User

        public static void UpdateLoginUser(User userToUpdate)
        {
            if (userToUpdate != null)
            {
                Console.WriteLine($"\nСостояние объекта до изменения: {ManufactureEntities.Context.Entry(userToUpdate).State}");

                userToUpdate.Login = Console.ReadLine();

                Console.WriteLine($"Состояние объекта после изменения: {ManufactureEntities.Context.Entry(userToUpdate).State}\n");

                try
                {
                    ManufactureEntities.Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        #endregion
    }
}
