using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.AdvancedTools
{
    interface IBalance
    {
        int Subscription { get; set; }
        int Balance { get; set; }

        void Payment(int commission);
        void CancelSubscription();
    }
}
