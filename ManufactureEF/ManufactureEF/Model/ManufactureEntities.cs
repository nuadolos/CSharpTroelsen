using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using ManufactureEF.Entities;
using ManufactureEF.Interceptor;
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

        /// <summary>
        /// Перехватчик класса DatabaseLogger
        /// </summary>
        static readonly DatabaseLogger databaseLogger = new DatabaseLogger("sqllog.txt", true);
        public ManufactureEntities()
            : base("name=ManufactureConnection")
        {
            //Регистрация перехватчика (Желательга регистрации в конфигурационном файле)
            //DbInterception.Add(new ConsoleWriterInterceptor());

            //Регистрация перехватчика DatabaseLogger
            databaseLogger.StartLogging();
            DbInterception.Add(databaseLogger);

            EventsSignature();
        }

        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }

        /// <summary>
        /// Установка отношений между таблицами
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //В качестве примера
            //modelBuilder.Entity<Role>()
            //    .HasMany(e => e.User)
            //    .WithRequired(p => p.Role)
            //    .WillCascadeOnDelete(true);
        }

        protected override void Dispose(bool disposing)
        {
            //Прекращение регистрации в журнале
            //и удаление объекта перехватчика
            DbInterception.Remove(databaseLogger);
            databaseLogger.StopLogging();
            base.Dispose(disposing);
        }

        #region События класса DbContext

        private void EventsSignature()
        {
            //Код перехватчика
            var context = (this as IObjectContextAdapter).ObjectContext;
            //Используется перед возвращением объекта вызывающему коду
            context.ObjectMaterialized += (sender, e) =>
            {
                //var model = (e.Entity as EntityBase);
                //model?.IsChanged = false;
            };

            //Событие, возникающее после вызова SaveChadges(), но перед обновлением базы данных 
            context.SavingChanges += (sender, e) =>
            {
                //Можно получать текущие и исходные значенния,
                //а также отменять/модифицировать операцию сохранения любым желаемым образом
                var saveContext = sender as ObjectContext;

                if (saveContext != null)
                {
                    foreach (ObjectStateEntry item in saveContext.ObjectStateManager
                        .GetObjectStateEntries(EntityState.Modified | EntityState.Added))
                    {
                        if (item.Entity is User us)
                        {
                            //Вызов метода RejectPropertyChanges,
                            //отменяющий любые изменения, внесенные в свойство Login
                            if (us.Login == "ggwpcs")
                            {
                                item.RejectPropertyChanges(nameof(us.Login));
                                Console.WriteLine("Изменение не было применено!");
                            }
                        }
                    }
                }
            };
        }

        #endregion
    }
}
