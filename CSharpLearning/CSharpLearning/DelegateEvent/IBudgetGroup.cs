using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.DelegateEvent
{
    interface IBudgetGroup
    {
        bool BudgetGroup { get; set; }
        int Payment { get; }
        int TotalYearsOfStudy { get; set; }

        int TotalPayment();
    }
}
