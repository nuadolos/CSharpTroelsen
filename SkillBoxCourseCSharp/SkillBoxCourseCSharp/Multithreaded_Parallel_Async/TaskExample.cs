using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Multithreaded_Parallel_Async
{
    /// <summary>
    /// TPL - Библиотека параллельных задач
    /// </summary>
    internal class TaskExample
    {
        #region Parallel Data Processing

        //Пример остановки обработки данных при помощи графического интерфейса

        public static CancellationTokenSource cancelToken = new CancellationTokenSource();

        public static void ExampleTPLAndParallelLINQ()
        {
            ParallelOptions opts = new ParallelOptions();
            opts.CancellationToken = cancelToken.Token;
            opts.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

            string[] arrayStr = new string[20];

            for (int i = 0; i < arrayStr.Length; i++)
                arrayStr[i] = i.ToString();

            try
            {
                //Обработать данные массива в параллельном режиме
                Parallel.ForEach(arrayStr, str =>
                {
                    //Если использован cancelToken.Cancel(), то произойдет остановка обработки данных
                    opts.CancellationToken.ThrowIfCancellationRequested();

                    Thread.Sleep(1000);
                    Console.WriteLine("Идентификатор потока: " + Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine($"\t{str}\n");
                });
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                do
                {
                    //Если использован cancelToken.Cancel(), то произойдет остановка обработки данных
                    opts.CancellationToken.ThrowIfCancellationRequested();

                    Thread.Sleep(1000);

                    var strOne = from s in arrayStr.AsParallel().WithCancellation(cancelToken.Token)
                                 where s == "14"
                                 select s;

                    Console.WriteLine("Идентификатор потока: " + Thread.CurrentThread.ManagedThreadId);

                    foreach (string str in strOne)
                    {
                        Console.WriteLine(str);
                    }

                    Console.WriteLine($"Желаете продолжить?");
                    if (Console.ReadLine().ToLower() == "нет")
                        break;
                }
                while(true);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("End...Or not?");
        }

        private void Cancel_Click()
        {
            cancelToken.Cancel();
        }

        #endregion

        #region Task

        public static void ExampleTask()
        {
            //Запуск новой задачи в новом потоке, при этом выполняющий поток не блокируется
            Task.Factory.StartNew(() => TaskOne());

            Console.WriteLine("Is the process busy?");
        }

        private static void TaskOne()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Идентификатор потока: " + Thread.CurrentThread.ManagedThreadId);
        }

        #endregion
    }
}
