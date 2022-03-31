using ManufactureEF.Entities;
using ManufactureEF_Core.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF_Core.DataInitializer
{
    public static class Initializer
    {
        /// <summary>
        /// Заполняет таблицы БД начальными данными
        /// </summary>
        /// <param name="context"></param>
        public static void InitializeData(ManufactureContext context)
        {
            List<Role> roles = new List<Role>()
            {
                new Role() { Name = "Директор" },
                new Role() { Name = "Администратор" },
                new Role() { Name = "Бухгалтер" },
                new Role() { Name = "Заказчик" }
            };

            roles.ForEach(r => context.Role.Add(r));
            context.SaveChanges();

            List<User> users = new List<User>()
            {
                new User() { Login = "hSFo42", Password = "igsdOI2", Fullname = "Пригров Павел", RoleId = 4 },
                new User() { Login = "fasGJAL0", Password = "gadsju4", Fullname = "Андрюгова Кристина", RoleId = 4 },
                new User() { Login = "FASYfs26", Password = "gds6321", Fullname = "Ловина Анна", RoleId = 4 },
                new User() { Login = "gdsJFGS4", Password = "jgfn46", Fullname = "Губернива Полина", RoleId = 4 },
                new User() { Login = "sadyFS4", Password = "jfgdu6", Fullname = "Бофова Елизавета", RoleId = 4 }
            };

            context.AddRange(users);
            context.SaveChanges();
        }

        /// <summary>
        /// Воссоздает базу данных
        /// </summary>
        /// <param name="context"></param>
        public static void RecreateDatabase(ManufactureContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        /// <summary>
        /// Очищает данные всех таблиц
        /// </summary>
        /// <param name="context"></param>
        public static void ClearData(ManufactureContext context)
        {
            var executeDeleteSql = delegate (ManufactureContext ctx, string tableName)
            {
                var rawSqlString = $"Delete from dbo.{tableName}";
                ctx.Database.ExecuteSqlRaw(rawSqlString);
            };

            var resetIdentity = (ManufactureContext ctx) =>
            {
                var tables = new[] { "[User]", context.GetTableName(typeof(Role)) };

                foreach (var table in tables)
                {
                    var rawSqlString = $"DBCC CHECKIDENT (\"dbo.{table}\", RESEED, 0);";
                    context.Database.ExecuteSqlRaw(rawSqlString);
                }
            };

            executeDeleteSql(context, "[User]");
            executeDeleteSql(context, context.GetTableName(typeof(Role)));

            resetIdentity(context);
        }
    }
}
