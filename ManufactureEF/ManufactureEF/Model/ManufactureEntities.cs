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
        /* ��� ��������:
         *    ��� ������� ������������� � "������� ���������� �������"
         * 
         * 1) ��� ��� �������� ������������ � ����������� � ���� ������
         *      ����������� enable-migrations
         * 2) ���������� ����� Migrations � ���������� ������� Configuration.
         *      ����� ������� �������� - add-migration Initial
         * 3) � ����� Migrations ���������� ����� InitialCreate,
         *      � � �������� ������������ ������ ���� � ����� �������� ��������.
         *      ����� ���������� ���� ������ - update-database
         *      
         *  ����������:
         *      ���� �������� ��� ���������� � ��� ���������� ���� ���������� ��,
         *      �� ������ ������ � "������� ���������� �������" update-database
         */

        /// <summary>
        /// ����������� ������ DatabaseLogger
        /// </summary>
        static readonly DatabaseLogger databaseLogger = new DatabaseLogger("sqllog.txt", true);
        public ManufactureEntities()
            : base("name=ManufactureConnection")
        {
            //����������� ������������ (���������� ����������� � ���������������� �����)
            //DbInterception.Add(new ConsoleWriterInterceptor());

            //����������� ������������ DatabaseLogger
            databaseLogger.StartLogging();
            DbInterception.Add(databaseLogger);

            EventsSignature();
        }

        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }

        /// <summary>
        /// ��������� ��������� ����� ���������
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //� �������� �������
            //modelBuilder.Entity<Role>()
            //    .HasMany(e => e.User)
            //    .WithRequired(p => p.Role)
            //    .WillCascadeOnDelete(true);
        }

        protected override void Dispose(bool disposing)
        {
            //����������� ����������� � �������
            //� �������� ������� ������������
            DbInterception.Remove(databaseLogger);
            databaseLogger.StopLogging();
            base.Dispose(disposing);
        }

        #region ������� ������ DbContext

        private void EventsSignature()
        {
            //��� ������������
            var context = (this as IObjectContextAdapter).ObjectContext;
            //������������ ����� ������������ ������� ����������� ����
            context.ObjectMaterialized += (sender, e) =>
            {
                //var model = (e.Entity as EntityBase);
                //model?.IsChanged = false;
            };

            //�������, ����������� ����� ������ SaveChadges(), �� ����� ����������� ���� ������ 
            context.SavingChanges += (sender, e) =>
            {
                //����� �������� ������� � �������� ���������,
                //� ����� ��������/�������������� �������� ���������� ����� �������� �������
                var saveContext = sender as ObjectContext;

                if (saveContext != null)
                {
                    foreach (ObjectStateEntry item in saveContext.ObjectStateManager
                        .GetObjectStateEntries(EntityState.Modified | EntityState.Added))
                    {
                        if (item.Entity is User us)
                        {
                            //����� ������ RejectPropertyChanges,
                            //���������� ����� ���������, ��������� � �������� Login
                            if (us.Login == "ggwpcs")
                            {
                                item.RejectPropertyChanges(nameof(us.Login));
                                Console.WriteLine("��������� �� ���� ���������!");
                            }
                        }
                    }
                }
            };
        }

        #endregion
    }
}
