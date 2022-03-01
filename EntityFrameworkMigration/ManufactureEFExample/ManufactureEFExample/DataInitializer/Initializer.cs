using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManufactureEFExample.Entities;
using ManufactureEFExample.Model;

namespace ManufactureEFExample.DataInitializer
{
    public class Initializer : DropCreateDatabaseAlways<ManufactureEntities>
    {
        protected override void Seed(ManufactureEntities context)
        {
            List<Role> roles = new List<Role>()
            {
                new Role() { Name = "Директор" },
                new Role() { Name = "Администратор" },
                new Role() { Name = "Бухгалтер" },
                new Role() { Name = "Заказчик" }
            };

            roles.ForEach(r => ManufactureEntities.Context.Role.AddOrUpdate(c => new { c.Name }, r));

            List<User> users = new List<User>()
            {
                new User() { Login = "hSFo42", Password = "igsdOI2", Fullname = "Пригров Павел", RoleId = 4 },
                new User() { Login = "fasGJAL0", Password = "gadsju4", Fullname = "Андрюгова Кристина", RoleId = 4 },
                new User() { Login = "FASYfs26", Password = "gds6321", Fullname = "Ловина Анна", RoleId = 4 },
                new User() { Login = "gdsJFGS4", Password = "jgfn46", Fullname = "Губернива Полина", RoleId = 4 },
                new User() { Login = "sadyFS4", Password = "jfgdu6", Fullname = "Бофова Елизавета", RoleId = 4 }
            };

            users.ForEach(u => ManufactureEntities.Context.User.AddOrUpdate(c => new { c.Fullname }, u));
        }
    }
}
