using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillBoxCourseCSharp.EntityFramework.Entities;
using SkillBoxCourseCSharp.EntityFramework.Model;

namespace SkillBoxCourseCSharp.EntityFramework
{
    public class EntityFrameworkExample
    {
        public static void ExampleClassFuncEF()
        {
            FuncEF.AddUser(new User() { Login = "fsaf412", Password = "sadas2", Fullname = "Коучук Николай", RoleId = 4 });

            User userElena = new User() { Login = "fgsg", Password = "532gds", Fullname = "Наумова Елена", RoleId = 4 };
            FuncEF.AddUser(userElena);
            FuncEF.PrintAllUsers();

            Console.ReadKey();
            Console.Clear();

            List<User> users = new List<User>()
            {
                new User() { Login = "Logds2", Password = "gasd521", Fullname = "Базиков Даниил", RoleId = 4 },
                new User() { Login = "Lo421g", Password = "6342gdfs", Fullname = "Ломсанова Екатерина", RoleId = 4 },
                new User() { Login = "hdsa99", Password = "fgas51", Fullname = "Огромов Алексей", RoleId = 4 },
            };
            FuncEF.AddRangeUsers(users);
            FuncEF.PrintAllUsers();

            Console.ReadKey();
            Console.Clear();

            FuncEF.RemoveUser(10);
            FuncEF.RemoveUser(ManufactureEntities.Context.User.FirstOrDefault(u => u.Fullname == "Наумова Елена"));
            FuncEF.PrintAllUsers();

            Console.ReadKey();
            Console.Clear();

            FuncEF.UpdateLoginUser(ManufactureEntities.Context.User.FirstOrDefault(u => u.RoleId == 3));
            FuncEF.PrintAllUsers();

            Console.ReadKey();
            Console.Clear();

            FuncEF.PrintAllUserRoles();

            Console.ReadKey();
            Console.Clear();

            FuncEF.RemoveUsers(ManufactureEntities.Context.User.Where(u => u.RoleId == 4 && u.Fullname != "Рговский Михаил"));
            FuncEF.PrintAllUsers();
        }

        public static void Example()
        {

        }
    }
}
