using ManufactureEFExample.Entities;
using ManufactureEFExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEFExample.Repos
{
    public class UserRepo : BaseRepo<User>
    {
        public override List<User> GetAll()
            => ManufactureEntities.Context.User.OrderBy(u => u.Fullname).ToList();
    }
}
