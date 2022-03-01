using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using ManufactureEFExample.Entities;
using ManufactureEFExample.Interceptor;

namespace ManufactureEFExample.Model
{
    public partial class ManufactureEntities : DbContext
    {
        static readonly DatabaseLogger databaseLogger = new DatabaseLogger("sqllog.txt", true);
        public ManufactureEntities()
            : base("name=ManufactureConnection")
        {
            //DbInterception.Add(new ConsoleWriterInterceptor());
            databaseLogger.StartLogging();
            DbInterception.Add(databaseLogger);

            //��� ������������
            var context = (this as IObjectContextAdapter).ObjectContext;
            context.ObjectMaterialized += (sender, e) =>
            {
                //���������� ����� �������� ����� �������� WPF
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

        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        protected override void Dispose(bool disposing)
        {
            DbInterception.Remove(databaseLogger);
            databaseLogger.StopLogging();
            base.Dispose(disposing);
        }
    }
}
