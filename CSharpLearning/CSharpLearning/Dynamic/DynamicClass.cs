using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Dynamic
{
    internal class DynamicClass
    {
        private dynamic dnmcObj;

        public dynamic DnmObj { get => dnmcObj; set => dnmcObj = value; }

        public DynamicClass(dynamic tempDnmObj)
        {
            DnmObj = tempDnmObj;
        }

        public override string ToString() => $"\nDnmObj = {DnmObj};\nDnmObj.GetType(): {DnmObj.GetType()}";
    }
}
