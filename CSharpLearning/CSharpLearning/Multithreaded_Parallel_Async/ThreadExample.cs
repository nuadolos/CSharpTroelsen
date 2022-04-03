using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SkillBoxCourseCSharp.Multithreaded_Parallel_Async
{
    internal class ThreadExample
    {
        public static void ExampleThreadInfo()
        {
            Thread currentThread = Thread.CurrentThread;
            currentThread.Name = "Новый поток";

            Console.WriteLine($"Название текущего домена: {Thread.GetDomain().FriendlyName}");
            Console.WriteLine($"Идентификатор текущего контекста: {Thread.CurrentContext.ContextID}");

            Console.WriteLine("\n\n\n\tИнформация о процессе");

            Console.WriteLine($"Название потока: {currentThread.Name}");
            Console.WriteLine($"Запущен ли поток?: {currentThread.IsAlive}");
            Console.WriteLine($"Приоритет: {currentThread.Priority}");
            Console.WriteLine($"Состояние: {currentThread.ThreadState}");
        }

        #region ThreadStart

        public static void ExampleThreadStart()
        {
            switch (Console.ReadLine())
            {
                //Работа производится в двух потоках
                case "1":
                    {
                        //Создание нового потока
                        Thread createThread = new Thread(new ThreadStart(MessageShow));
                        createThread.Name = "Second Thread";
                        createThread.Priority = ThreadPriority.BelowNormal;
                        createThread.Start();

                        break;
                    }
                default:
                    MessageShow();//Работа производится в одном потоке
                    break;
            }
            Console.WriteLine("Я быстрее!!!");
        }

        static void MessageShow()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Люблю вас...");
        }

        #endregion

        #region ParametrizedThreadStart

        public static void ExampleParametrizedThreadStart()
        {
            switch (Console.ReadLine())
            {
                //Работа производится в двух потоках
                case "1":
                    {
                        //Создание нового потока с одним параметром
                        Thread createThread = new Thread(new ParameterizedThreadStart(MessageShow));
                        createThread.Name = "Second Thread";
                        createThread.Priority = ThreadPriority.BelowNormal;
                        createThread.Start(512);

                        break;
                    }
                default:
                    MessageShow(28);//Работа производится в одном потоке
                    break;
            }
            Console.WriteLine("Я быстрее!!!");
        }

        static void MessageShow(object obj)
        {
            if (obj is int i)
            {
                Thread.Sleep(5000);
                Console.WriteLine("Переданный объект: цифра -" + i);
            }
        }

        #endregion

        #region AutoResetEvent

        private static AutoResetEvent waitHandle = new AutoResetEvent(false);
        public static void ExampleAutoResetEvent()
        {
            Thread createThread = new Thread(new ParameterizedThreadStart(IntView));
            createThread.Name = "Second Thread";
            createThread.Priority = ThreadPriority.BelowNormal;
            createThread.Start(512);

            //Ожидать, пока не поступит уведомление
            waitHandle.WaitOne();
            Console.WriteLine("Я быстрее!!!");
        }
        static void IntView(object obj)
        {
            if (obj is int i)
            {
                Thread.Sleep(5000);
                Console.WriteLine("Переданный объект: цифра -" + i);

                //Сообщить другому потоку о том, что работа завершина
                waitHandle.Set();
            }
        }

        #endregion

        #region lock/Monitor

        public static void ExampleOtherTypes()
        {
            Printer p = new Printer();
            Thread[] threads = new Thread[10];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ThreadStart(p.PrintNumbersMonitor))
                { Name = $"20{i}" };
            }

            foreach (Thread thread in threads)
                thread.Start();
        }

        //Если используется атрибут Synchronization, то все методы безопасны к потокам
        //то есть выполняются синхронно
        //[System.Runtime.Remoting.Contexts.Synchronization]
        private class Printer
        {
            //Маркер блокировки
            private object threadLock = new object();
            public void PrintNumbersLock()
            {
                //Этот код будет выполнен всеми потоками одновременно
                //так как не нуждается в синхронизации
                //Console.WriteLine("Я помню черные обои:");

                //Оператор lock используется для определения блока операторов, синхронизированный между потоками
                //т.е. каждый поток будет ждать окончания работы другого потока, но при этом делает заранее всю работу
                //Использование маркера блокировки
                lock (threadLock)
                {
                    Console.WriteLine($"-> Поток#{Thread.CurrentThread.Name} выполняет метод PrintNumbers()");
                    Console.Write("Цифры:");
                    for (int i = 0; i < 10; i++)
                    {
                        Random rndm = new Random();
                        Thread.Sleep(1000 * rndm.Next(5));
                        Console.Write($"\t{i}");
                    }
                    Console.WriteLine();
                }
            }

            public void PrintNumbersMonitor()
            {
                //Оператор lock - сокращение для работы с классом System.Threading.Monitor
                //Преимущество такого подхода - тип Monitor обеспечивает большую степень контроля,
                //используя статические методы типа Monitor
                Monitor.Enter(threadLock);

                try
                {
                    Console.WriteLine($"-> Поток#{Thread.CurrentThread.Name} выполняет метод PrintNumbers()");
                    Console.Write("Цифры:");
                    for (int i = 0; i < 10; i++)
                    {
                        Random rndm = new Random();
                        Thread.Sleep(1000 * rndm.Next(5));
                        Console.Write($"\t{i}");
                    }
                    Console.WriteLine();
                }
                finally
                {
                    Monitor.Exit(threadLock);
                }
            }
        }

        #endregion

        #region Interlocked

        public static void ExampleInterlocked()
        {
            int i = int.Parse(Console.ReadLine());

            Console.WriteLine("Число перед операциями: " + i);

            InterlockedFirst(ref i);
            Console.WriteLine("InterlockedFirst(ref int i): " + i);

            InterlockedSecond(ref i);
            Console.WriteLine("InterlockedSecond(ref int i): " + i);

            Console.WriteLine("Число после всех операций: " + i);
        }

        public static void InterlockedFirst(ref int i)
        {
            //Полученное число i увеличивается на единицу и меняет старое значение на новое
            Interlocked.Exchange(ref i, Interlocked.Increment(ref i));
        }

        public static void InterlockedSecond(ref int i)
        {
            //Если число i равен --i, то i равен 200
            //так как i является ссылочным, то i всегда равен 200
            Interlocked.CompareExchange(ref i, 200, Interlocked.Decrement(ref i));
        }

        #endregion

        #region Timer

        public static void ExampleTimer()
        {
            //Создание делегата для типа Timer
            TimerCallback tcb = new TimerCallback(PrintTimeNow);

            //Создание таймера в другом потоке
            Timer timer = new Timer(tcb, null, 0, 1000);
        }

        private static void PrintTimeNow(object obj)
        {
            Console.Clear();
            Console.WriteLine(DateTime.Now.ToLongTimeString());
            Console.WriteLine("Идентификатор потока: " + Thread.CurrentThread.ManagedThreadId);
        }

        #endregion

        #region ThreadPool

        public static void ExampleThreadPool()
        {
            Printer p = new Printer();
            WaitCallback wcb = new WaitCallback(WaitPrintNumbers);

            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(wcb, p);
            }
        }

        private static void WaitPrintNumbers(object obj)
        {
            (obj as Printer)?.PrintNumbersLock();
        }

        #endregion
    }
}
