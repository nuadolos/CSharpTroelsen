using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.ADO
{
    internal class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public int RoleId { get; set; }

        public override string ToString() => $"Id = {Id}, Login = {Login}, Password = {Password}, Fullname = {Fullname}, RoleId = {RoleId}";
    }
}
