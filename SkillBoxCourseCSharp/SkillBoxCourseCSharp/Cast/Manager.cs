using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillBoxCourseCSharp.Aggregation;

namespace SkillBoxCourseCSharp.Cast
{
    internal class Manager : Employee
    {
        public string CreditCard { get; set; }

        public override void SetBPName(string name)
        {
            base.SetBPName(name);
        }
    }
}
