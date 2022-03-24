namespace ManufactureEF.Migrations
{
    using ManufactureEF.Entities;
    using ManufactureEF.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ManufactureEF.Model.ManufactureEntities>
    {
        public Configuration()
        {
            //Чтобы инициализатор Initializer работал, необходимо
            //данное свойство установить в значение true
            AutomaticMigrationsEnabled = true;
            ContextKey = "ManufactureEFExample.Model.ManufactureEntities";
        }

        protected override void Seed(ManufactureEF.Model.ManufactureEntities context)
        {
            List<Role> roles = new List<Role>()
            {
                new Role() { Name = "Директор" },
                new Role() { Name = "Администратор" },
                new Role() { Name = "Бухгалтер" },
                new Role() { Name = "Заказчик" }
            };

            roles.ForEach(r => context.Role.AddOrUpdate(c => new { c.Name }, r));

            List<User> users = new List<User>()
            {
                new User() { Login = "hSFo42", Password = "igsdOI263", Fullname = "Пригров Павел", RoleId = 4 },
                new User() { Login = "fasGJAL0", Password = "gads34ju4", Fullname = "Андрюгова Кристина", RoleId = 4 },
                new User() { Login = "FASYfs26", Password = "g2ds62321", Fullname = "Ловина Анна", RoleId = 4 },
                new User() { Login = "gdsJFGS4", Password = "j2gf3n46", Fullname = "Губернива Полина", RoleId = 4 },
                new User() { Login = "sadyFS4", Password = "j4fg5du6", Fullname = "Бофова Елизавета", RoleId = 4 }
            };

            users.ForEach(u => context.User.AddOrUpdate(c => new { c.Fullname }, u));
        }
    }
}
