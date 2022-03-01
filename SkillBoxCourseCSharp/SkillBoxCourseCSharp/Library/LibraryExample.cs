using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;
using PrivateLibrary;
using SkillBoxCourseCSharp.AdvancedTools;

namespace SkillBoxCourseCSharp.Library
{
    internal class LibraryExample
    {
        public static void PrivateLibraryExample()
        {
            /* Для подключения закрытой сборки необходимо:
             * 1) В обозревателе решений добавить ссылку на сборку, которую нужно подключить
             * 2) Перейти в раздел "Обзор" и нажать на кнопку "Обзор"
             * 3) Зайти в папку необходимой библиотеки
             * 4) Добраться до папки Debug
             * 5) Выбрать .dll файл
             * Для дальнейшего использования библиотеки нужно подключить простраство имен (Название .dll файла)
             * 
             * Файл .dll в этом случае будет находится в папке Debug данного проекта
             */
            Car testCar = new Car("Бульдозер", 10, "Трактор");
            Console.WriteLine($"{testCar.Name}, {testCar.Speed}, {testCar.Type}\n\n");

            for (int i = 0; i < 8; i++)
            {
                testCar.SpeedUp(i + 5);
                Console.WriteLine($"Машина разгоняется до скорости: {testCar.Speed}");
            }

            testCar.SpeedDown(20);
            Console.WriteLine($"Машина снизала до скорости: {testCar.Speed}");
        }

        public static void TestLibraryExample()
        {
            LearnMaterial learn = new LearnMaterial(1, "Создание библиотек", DateTime.Now, true);

            Console.WriteLine($"Экземпляр класса LearnMaterial из библиотки TestLibrary имеет:\n" +
                $"{learn.Name}\t{learn.StartDate.ToShortDateString()}\t{(learn.IsLearning ? "Изучается" : "Не изучается")}");

            Console.WriteLine("\n\n\n");
            learn.DisplayFullInformation();
            Console.WriteLine("\n\n\n");
            learn.DisplayDefiningAssembly();

            learn.ResetLearn();
            Console.WriteLine("\n\n");
            Console.WriteLine($"Экземпляр класса LearnMaterial из библиотки TestLibrary имеет:\n" +
                $"{learn.Name}\t{learn.StartDate.ToShortDateString()}\t{(learn.IsLearning ? "Изучается" : "Не изучается")}");
        }
    }
}
