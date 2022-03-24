namespace ManufactureEF.Entities
{
    using ManufactureEF.Entities.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User : EntityBase
    {
        [Required]
        [StringLength(50)]
        [Index("IDX_User_Authorization", IsUnique = true, Order = 1)]
        public string Login { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Пароль меньше 8 символов")]
        [Index("IDX_User_Authorization", IsUnique = true, Order = 2)]
        //[Column("Password")] - применяется в случае изменения названия свойства в коде
        //чтобы база данных увидела соответствие столбца к переименованному свойству
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Fullname { get; set; }

        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }
    }
}
