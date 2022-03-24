using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using ManufactureEF.Entities;
using ManufactureEF.Entities.Base;

namespace ManufactureEF.Model
{
    public partial class ManufactureEntities : DbContext
    {
        /* Про миграции:
         *    Все команды прописываются в "Консоле диспетчера пакетов"
         * 
         * 1) Как все сущности приготовлены к воссозданию в базе данных
         *      прописываем enable-migrations
         * 2) Появляется папка Migrations с содержимым классом Configuration.
         *      Чтобы создать миграцию - add-migration Initial
         * 3) В папке Migrations появляется класс InitialCreate,
         *      а в названии присутствует полные дата и время создания миграции.
         *      Чтобы воссоздать базу данных - update-database
         *      
         *  Примечание:
         *      Если миграции уже существуют и вам необходимо лишь воссоздать БД,
         *      то просто вводим в "Консоль диспетчера пакетов" update-database
         */

        public ManufactureEntities()
            : base("name=ManufactureConnection")
        { }

        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
