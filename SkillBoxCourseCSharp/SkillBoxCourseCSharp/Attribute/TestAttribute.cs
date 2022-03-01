using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Attribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public sealed class TestAttribute : System.Attribute
    {
        public string AssemblyName { get; set; }
        public string Description { get; set; }

        public TestAttribute()
        { }
        public TestAttribute(string assemblyName, string description)
        {
            AssemblyName = assemblyName;
            Description = description;
        }
    }
}
