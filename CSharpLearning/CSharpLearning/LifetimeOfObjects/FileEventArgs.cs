using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.LifetimeOfObjects
{
    public class FileEventArgs : EventArgs
    {
        public string Message { get; set; }
        public string FunctionUsed { get; set; }

        public FileEventArgs(string message, string funcUsed)
        {
            Message = message;
            FunctionUsed = funcUsed;
        }
    }
}
