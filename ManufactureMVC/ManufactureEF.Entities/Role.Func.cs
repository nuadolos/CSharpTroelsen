using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF.Entities
{
    public partial class Role
    {
        public override string ToString() => $"Наименование: {Name}";
    }
}
