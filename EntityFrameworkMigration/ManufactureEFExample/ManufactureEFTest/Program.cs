using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManufactureEFExample.DataInitializer;
using ManufactureEFExample.Entities;
using ManufactureEFExample.Model;
using ManufactureEFExample.Repos;

namespace ManufactureEFTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Создание уровня доступа к ManufactureDB

            //Database.SetInitializer(new Initializer());

            //foreach (User user in ManufactureEntities.Context.User)
            //{
            //    Console.WriteLine(user);
            //}

            #endregion

            //ReposExample();

            //LoginChangeExample();

            Console.ReadKey();
        }

        #region Хранилища для многократного использования

        public static void ReposExample()
        {
            Random random = new Random();

            User newUser = new User() { Login = $"24fad", Password = $"JFusT", Fullname = "Изуимова Ира", RoleId = random.Next(1, 5) };

            using (UserRepo ur = new UserRepo())
            {
                //ur.Add(newUser);

                User u1 = ur.First(u => u.Fullname == "Изуимова Ира");
                User u2 = ur.First(u => u.Fullname == "Изуимова Ира");


                //u1.Fullname = "Рубцова Ира";
                //ur.Save(u1);

                u2.Fullname = Console.ReadLine();

                try
                {
                    while (true)
                        //Ошибка не возникнет, так как используется единственный статический контекст
                        //Чтобы вызвать ошибку, необходимо открыть приложение через .exe в папке bin\Debug
                        //А после еще одно приложение через F5 (Отладка), тогда можно поймать ошибку
                        ur.Save(u2);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var currentValues = entry.CurrentValues;
                    var originalValues = entry.OriginalValues;
                    var dbValues = entry.GetDatabaseValues();

                    Console.WriteLine("\n\tСовпадение\n");
                    Console.WriteLine($"Type:\tFullname");
                    Console.WriteLine($"Current:\t{currentValues[nameof(User.Fullname)]}");
                    Console.WriteLine($"Original:\t{originalValues[nameof(User.Fullname)]}");
                    Console.WriteLine($"DataBase:\t{dbValues[nameof(User.Fullname)]}");
                }

                ur.Delete(ur.First(u => u.Fullname == u2.Fullname));
            }
        }

        #endregion

        #region Событие SavingChanges

        public static void LoginChangeExample()
        {
            UserRepo ur = new UserRepo();

            User logChange = ur.First(u => u.RoleId == 4);
            logChange.Login = "ggwpcs";
            ur.Save(logChange);
        }

        #endregion
    }
}
