using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.LifetimeOfObjects
{
    internal class LifetimeOfObjectsExample
    {
        public static void LtOfObjectsExample()
        {
            Console.WriteLine($"Используемая память под объекты: {GC.GetTotalMemory(false)}");

            Directive dir = new Directive("Фото", true, new List<File>()
            {
                new File("Семейныйф2018", DateTime.Now, "JPG", 612),
                new File("Аватарка20", DateTime.Now.AddMinutes(15), "JPG", 4512),
                new File("Вконтакте2", DateTime.Now.AddMinutes(22), "JPG", 6243),
                new File("Игры2017", DateTime.Now.AddMinutes(6), "JPG", 7234),
                new File("Футбол2020", DateTime.Now.AddMinutes(10), "JPG", 725),
            });
            dir.SignatureForEvents((sender, e) =>
            {
                Console.WriteLine($"{e.Message}\nТип функции: {e.FunctionUsed}\nЕго размер: {(sender as File).Size}");
            },
            (sender, e) =>
            {
                Console.WriteLine($"{e.Message}\nТип функции: {e.FunctionUsed}\nЕго размер: {(sender as File).Size}");
            });

            dir.AddFile(new File("ШРВЫа", DateTime.Now.AddMinutes(25), "PNG", 412));
            dir.AddFile(new File("Fucnsa", DateTime.Now.AddMinutes(52135), "PNG", 1251));
            dir.AddFile(new File("dsaas", DateTime.Now.AddMinutes(6213), "PNG", 743));
            dir.AddFile(new File("gdsha", DateTime.Now.AddMinutes(552), "PNG", 6231));
            dir.AddFile(new File("uyre61", DateTime.Now.AddMinutes(3512), "PNG", 5321));

            Console.WriteLine("\n\n");

            foreach (File files in dir)
            {
                Console.WriteLine(files?.ToString());
            }

            Console.WriteLine($"\n\nПоколение объекта dir[4]: {GC.GetGeneration(dir[4])}");

            //Иследование объектов поколения 0 и приднудительный вызов сборки мусора
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();

            Console.WriteLine($"\nПоколение объекта dir[3] после сборки мусора: {GC.GetGeneration(dir[3])}");
            Console.WriteLine($"\nКоличество оставшихся файлов после сборки мусора: {dir.CountFiles}\n");
            Console.WriteLine($"\nИспользуемая память после сборки мусора: {GC.GetTotalMemory(false)}\n\n");

            Console.WriteLine("Кол-во проведенных сборок мусора объектов поколения 0:" + GC.CollectionCount(0));
            Console.WriteLine("Кол-во проведенных сборок мусора объектов поколения 1:" + GC.CollectionCount(1));
            Console.WriteLine("Кол-во проведенных сборок мусора объектов поколения 2:" + GC.CollectionCount(2));
            Console.WriteLine("\n\n");

            //Используется для освобождения неуправляемых ресурсов
            dir.Dispose();
            //Деструктор, начинающийся на символ ~, выполняется при сборке мусора или при завершении приложения
            //для освобождения неуправляемых ресурсов

            Console.WriteLine("\n\n");
            dir.RemoveFile(dir[4]);
            dir.RemoveFile(dir[5]);
        }
    }
}
