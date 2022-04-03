using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp
{
    /// <summary>
    /// Частичный класс Workers, 
    /// хранящий в себе конструктор и частичный реализуемый метод отображения данных.
    /// При компиляции частичные классы Workers будут скомпилированы в одно.
    /// </summary>
    public partial class Workers
    {
        /// <summary>
        /// Конструктор, содержащий в себе 6 необязательных параметров,
        /// за счет которых содержит в себе стандартный и множество других конструкторов
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="age"></param>
        /// <param name="department"></param>
        /// <param name="salary"></param>
        /// <param name="countProject"></param>
        public Workers(string name = "", string surname = "", int age = 0, string department = "", int salary = 0, int countProject = 0)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Department = department;
            Salary = salary;
            CountProject = countProject;
        }

        /// <summary>
        /// Реализация частичного метода PrintEmployee
        /// </summary>
        partial void PrintEmployee()
        {
            Console.WriteLine($"Worker: {Name} {Surname} {Age} {Department} {Salary} {CountProject}");
        }
    }
}
