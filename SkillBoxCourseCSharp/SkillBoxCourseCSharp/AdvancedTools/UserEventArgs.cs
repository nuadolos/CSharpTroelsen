using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.AdvancedTools
{
    internal class UserEventArgs : EventArgs
    {
        public string message { get; set; }

        public UserEventArgs(string msg)
        {
            message = msg;
        }
    }
}
