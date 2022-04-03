using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Multithreaded_Parallel_Async
{
    internal class AsyncAwaitExample
    {
        public static async Task ExampleAsyncMethod()
        {
            Console.WriteLine("Начало метода ExampleAsyncMethod()");
            //await извлекает внутреннее возвращающее значение,
            //которое содержится в объекте Task
            string message = await DoWorkAsync();
            Console.WriteLine(message);
            Console.WriteLine("Конец метода ExampleAsyncMethod()");

            //Если данный метод не был объявлен с кл. словом async и типом Task,
            //то для ожидания завершения асинхронного метода
            //необходимо использовать свойство Result, если переменная содержит возвращающее значение,
            //и метод Wait(), если возвращающий значение void,
            //чтобы приложение не закрылось
            string messageResult = DoWorkAsync().Result;
            Console.WriteLine(message);
        }

        public static async Task<string> DoWorkAsync()
        {
            return await Task.Run(() =>
            {
                Console.WriteLine("Дело пошло");
                Thread.Sleep(5000);
                Console.WriteLine("Идентификатор потока: " + Thread.CurrentThread.ManagedThreadId);
                return "Дело сделано";
            });
        }
    }
}
