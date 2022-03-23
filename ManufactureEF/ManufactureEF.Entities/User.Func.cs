using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF.Entities
{
    public partial class User
    {
        public override string ToString() => $"Id = {Id}, Login: {Login}, Password: {Password}, Fullname: {Fullname}, Role: {Role.Name}";
    }
}
