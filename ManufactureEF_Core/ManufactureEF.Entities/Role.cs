using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF.Entities
{
    [Table("Role")]
    public partial class Role : Base.EntityBase
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public List<User> User { get; set; } = new List<User>();
    }
}
