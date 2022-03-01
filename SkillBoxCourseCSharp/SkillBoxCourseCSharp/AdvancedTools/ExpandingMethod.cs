using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.AdvancedTools
{
    static class ExpandingMethod
    {
        /// <summary>
        /// Расширяющий метод, выводящий на экран значение объекта
        /// </summary>
        /// <param name="obj"></param>
        public static void DisplayElConsole(this object obj)
        {
            Console.WriteLine($"Объект любого типа: {obj}");
        }

        /// <summary>
        /// Расширяющий метод, позволяющий объекту любого типа
        /// отобразить сборку, в которой он определен
        /// </summary>
        /// <param name="obj"></param>
        public static void DisplayDefiningAssembly(this object obj)
        {
            Console.WriteLine($"{obj.GetType().Name} находится в {Assembly.GetAssembly(obj.GetType()).GetName().Name}");
        }

        /// <summary>
        /// Расширяющий метод, изменяющий порядок цифр в целочисленном числе
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int ReverseDigits(this int i)
        {
            char[] digits = i.ToString().ToCharArray();

            Array.Reverse(digits);

            string newDigits = new string(digits);

            return int.Parse(newDigits);
        }

        public static void PrintArrayOrList(this System.Collections.IEnumerable iterator)
        {
            for (int i = 0; i < 3; i++)
            {
                foreach (var item in iterator)
                {
                    Console.Write("\t" + item);
                }
                Console.WriteLine();
            }
        }

        public static void DisplayFullInformation(this object obj)
        {
            Console.WriteLine($"Тип объекта: {obj.GetType().Name}");
            Console.WriteLine($"Базовый класс типа {obj.GetType().Name} является {obj.GetType().BaseType}");
            Console.WriteLine($"Метод ToString(): {obj.ToString()}");
            Console.WriteLine($"Метод GetHashCode(): {obj.GetHashCode()}");
        }
    }
}
