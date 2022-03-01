using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEFExample.Entities
{
    public partial class Role
    {
        public override string ToString() => $"Наименование: {Name}";
    }
}
