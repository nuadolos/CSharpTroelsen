using ManufactureEF.Entities;
using ManufactureEF_Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF_Core.Repos
{
    public class RoleRepo : BaseRepo<Role>, IRoleRepo
    {
        internal RoleRepo() : base()
        { }

        public RoleRepo(ManufactureContext context) : base(context)
        { }
    }
}
