using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillBoxCourseCSharp.Aggregation;

namespace SkillBoxCourseCSharp.Cast
{
    internal class SalesPerson : Employee
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public override string FullName { get => base.FullName; set => base.FullName = value; }

        public override void SetBPName(string name)
        {
            base.SetBPName(name);
        }
    }
}
