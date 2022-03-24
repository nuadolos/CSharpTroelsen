using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManufactureEF.Entities;
using ManufactureEF.Model;

namespace ManufactureEF.DataInitializer
{
    public class Initializer : DropCreateDatabaseAlways<ManufactureEntities>
    {
        /// <summary>
        /// После удаления БД инициализатор воссоздает заново БД
        /// с начальными данными
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ManufactureEntities context)
        {
            List<Role> roles = new List<Role>()
            {
                new Role() { Name = "Директор" },
                new Role() { Name = "Администратор" },
                new Role() { Name = "Бухгалтер" },
                new Role() { Name = "Заказчик" }
            };

            context.Role.AddOrUpdate(r => new { r.Name }, roles.ToArray());

            List<User> users = new List<User>()
            {
                new User() { Login = "hSFo42", Password = "igsdOI263", Fullname = "Пригров Павел", RoleId = 4 },
                new User() { Login = "fasGJAL0", Password = "gads34ju4", Fullname = "Андрюгова Кристина", RoleId = 4 },
                new User() { Login = "FASYfs26", Password = "g2ds62321", Fullname = "Ловина Анна", RoleId = 4 },
                new User() { Login = "gdsJFGS4", Password = "j2gf3n46", Fullname = "Губернива Полина", RoleId = 4 },
                new User() { Login = "sadyFS4", Password = "j4fg5du6", Fullname = "Бофова Елизавета", RoleId = 4 }
            };

            context.User.AddOrUpdate(u => new { u.Fullname }, users.ToArray());
        }
    }
}
