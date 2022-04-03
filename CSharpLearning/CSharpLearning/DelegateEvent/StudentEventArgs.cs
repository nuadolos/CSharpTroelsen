using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.DelegateEvent
{
    public class StudentEventArgs : EventArgs
    {
        public readonly string message;

        public StudentEventArgs(string msg)
        {
            message = msg;
        }
    }
}
