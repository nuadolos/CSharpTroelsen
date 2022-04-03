using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp
{
    /// <summary>
    /// Частичный класс Workers, 
    /// хранящий в себе автоматические свойства и метод отображения данных.
    /// При компиляции частичные классы Workers будут скомпилированы в одно.
    /// </summary>
    public partial class Workers
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
        public int CountProject { get; set; }

        /// <summary>
        /// Тело частичного метода PrintEmployee указан в Workers.Full
        /// </summary>
        partial void PrintEmployee();
        public void PublicDataWorker()
        {
            PrintEmployee();
        }
    }
}
