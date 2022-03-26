using ManufactureEF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF_Core.Repos
{
    public interface IUserRepo : IRepo<User>
    {
        List<User> Search(string searchString);
        List<User> GetDirectors();
        List<User> GetRelatedData();
    }
}
