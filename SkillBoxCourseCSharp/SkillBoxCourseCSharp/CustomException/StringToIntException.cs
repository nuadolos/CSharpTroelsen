using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.CustomException
{

    [Serializable]
    public class StringToIntException : Exception
    {
        public StringToIntException() { }
        public StringToIntException(string message) : base(message) { }
        public StringToIntException(string message, Exception inner) : base(message, inner) { }
        protected StringToIntException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
