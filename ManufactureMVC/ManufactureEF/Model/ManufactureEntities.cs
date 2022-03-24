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
