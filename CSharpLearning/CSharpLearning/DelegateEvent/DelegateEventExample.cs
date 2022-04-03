using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.DelegateEvent
{
    internal class DelegateEventExample
    {
        public static void ExampleDelegateEvent()
        {
            List<Student> lSt = new List<Student>()
            {
                new Student(),
                new Student("Сорокин Андрей", 17, "г. Москва", 2, true, 4),
                new Student("Танкова Мария", 18, "г. Москва", 3, false, 4),
                new Student("Анитова Екатерина", 19, "г. Москва", 4, true, 4),
                new Student("Глончевский Валерий", 16, "г. Москва", 1, false, 3),
            };

            //1 вариант регистрация события - использование отдельного метода,
            //описанного в классе DelegateEventExample.
            //Он является статическим, так как сам класс - статический,
            //иначе произойдет ошибка на этапе компиляции
            Student.StJoinClub += St_StJoinClub;

            //2 вариант регистрации события - использование анонимного метода
            Student.StSuspended += delegate (object sender, StudentEventArgs e)
            {
                if (sender is Student tempSt)
                {
                    Console.WriteLine($"{e.message}\n" +
                        $"Это был {tempSt.FIO} {tempSt.Course} курса");
                }
            };

            //3 вариант регистрации события - использование лямбда-выражений
            //Тоже самое что и анонимный метод, сжатый до выражения
            //Student.StUnSuspended += (sender, e) => Console.WriteLine(e.message);
            //При необходимости написания больше одной строчки кода используется фигурные скобки
            Student.StUnSuspended += (sender, e) =>
            {
                if (sender is Student tempSt)
                    Console.WriteLine($"{e.message}\n" +
                    $"Это {tempSt.FIO} {tempSt.Course} курса, состоящий теперь в клубе {tempSt.Club}");
            };

            Random rnd = new Random();

            //Удалить метод St_StJoinClub из списка вызовов.
            //При его удалении не произойдет выведения на экран сообщения
            //и сам StJoinClub будет равен null
            //Student.StJoinClub -= St_StJoinClub;

            foreach (Student stJoinClub in lSt)
            {
                stJoinClub.JoinClub(rnd.Next(4));
                Console.WriteLine("\n");
            }

            Console.ReadKey();
            Console.Clear();

            foreach (Student stSuspend in lSt)
            {
                if (!stSuspend.BudgetGroup)
                    stSuspend.SuspendFromClasses();
                Console.WriteLine("\n");
            }

            Console.ReadKey();
            Console.Clear();

            foreach (Student stUnSuspend in lSt)
            {
                if (!stUnSuspend.BudgetGroup)
                    stUnSuspend.UnSuspendFromClasses();
                Console.WriteLine("\n");
            }
        }

        /// <summary>
        /// Сгенерированный метод при регистрации события StJoinClub
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void St_StJoinClub(object sender, StudentEventArgs e)
        {
            if (sender is Student tempSt)
            {
                Console.WriteLine($"{e.message}\n" +
                    $"Это {tempSt.FIO} {tempSt.Course} курса, состоящий теперь в клубе {tempSt.Club}");
            }
        }
    }
}
