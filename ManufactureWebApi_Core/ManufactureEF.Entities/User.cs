using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF.Entities
{
    [Table("User")]
    public partial class User : Base.EntityBase
    {
        [Required]
        [StringLength(50)]
        public string? Login { get; set; }

        [Required]
        [StringLength(50)]
        public string? Password { get; set; }

        [Required]
        [StringLength(50)]
        public string? Fullname { get; set; }

        public int? RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }
    }
}
