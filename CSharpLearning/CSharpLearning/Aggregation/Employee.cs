using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Aggregation
{
    /// <summary>
    /// Реализация модели включения/делегации
    /// </summary>
    public class Employee
    {
        /*
         * По-моему мнению плюсом данного подхода является неявная реализация вложенных типов в глазах пользователя.
         * То есть будь класс BenefitPackage реализован отдельным файлом *.cs,
         * тогда в клиенской части не составит труда создать экземпляр данного класса, что будет неуместно,
         * так как класс BenefitPackage необходим лишь для предоставления дополнительного функционала класса Employee.
        */

        private class BenefitPackage
        {
            public enum BenefitPackageLevel
            {
                Standard, Gold, Platinum
            }

            public string BPName { get; set; }

        }

        public virtual string FullName { get; set; }
        public int Age { get; set; }

        BenefitPackage bp;
        BenefitPackage.BenefitPackageLevel level;

        public Employee()
        {
            bp = new BenefitPackage();
            level = new BenefitPackage.BenefitPackageLevel();
        }
        
        public virtual void SetBPName(string name) => bp.BPName = name;
    }
}
