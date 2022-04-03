using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using SkillBoxCourseCSharp.EntityFramework.Entities;

namespace SkillBoxCourseCSharp.EntityFramework.Model
{
    public partial class ManufactureEntities : DbContext
    {
        public ManufactureEntities()
            : base("name=ManufactureConnection")
        {
        }

        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
