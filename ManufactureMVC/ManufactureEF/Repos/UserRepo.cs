using ManufactureEF.Entities;
using ManufactureEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF.Repos
{
    public class UserRepo : BaseRepo<User>
    {
        public UserRepo() : base()
        { }

        public override List<User> GetAll()
            => Context.User.OrderBy(u => u.Fullname).ToList();
    }
}
