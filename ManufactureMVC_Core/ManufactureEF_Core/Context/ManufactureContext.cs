using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF_Core.Context
{
    public class ManufactureContext : DbContext
    {
        /*
         * Для применения команд EF .NET CLI необходимо:
         *      1) Окрыть cmd.exe
         *      2) Установить путь к проекту:
         *          cd C:\Users\nuadolos\source\repos\CSharpTroelsen\ManufactureEF_Core\ManufactureEF_Core
         *      3) Установить средство Microsoft.EntityFrameworkCore.Tools.DotNet:
         *          dotnet tool install --global dotnet-ef
         *          
         *  Для создании миграции необходимо:
         *      dotnet ef migrations add Initial --context ManufactureEF_Core.Context.ManufactureContext -o Migrations
         *      
         *  Для обновления базы данных:
         *      dotnet ef database update
         */

        /// <summary>
        /// Для организации пула в ASP.NET Core данный конструктор не является открытым
        /// </summary>
        internal ManufactureContext()
        { }

        /// <summary>
        /// Конструктор для использовния средства организации пула DbContext из ASP.NET Core
        /// </summary>
        /// <param name="options"></param>
        public ManufactureContext(DbContextOptions options) : base(options)
        { }

        //В EF Core объект DbSet<T>
        //  не сопровождается модификатором virtual (Ленивая загрузка)

        public DbSet<ManufactureEF.Entities.Role> Role { get; set; }
        public DbSet<ManufactureEF.Entities.User> User { get; set; }

        /// <summary>
        /// Возвращает имя таблицы Sql Server для объекта DbSet<T>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetTableName(Type type)
        {
            return Model.FindEntityType(type).GetTableName() ?? "";
        }

        /// <summary>
        /// При применении конструтора без параметров
        /// данный метод настраивает создаваемый контекст
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = @"server=.\sqlexpress;database=ManufactureDB;
                    integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

                optionsBuilder.UseSqlServer(connectionString,
                    options => options.EnableRetryOnFailure());
            }
        }

        /// <summary>
        /// Применение интерфейса Fluent API 
        /// для создания индексов и правила удаления данных
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ManufactureEF.Entities.User>(
                entity => entity.HasIndex(e => new { e.Login, e.Password }).IsUnique());

            modelBuilder.Entity<ManufactureEF.Entities.User>()
                .HasOne(e => e.Role)
                .WithMany(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
