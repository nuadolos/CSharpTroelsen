using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.EntityFramework.Entities
{
    public partial class Role
    {
        public override string ToString() => $"Наименование: {Name}";
    }
}
