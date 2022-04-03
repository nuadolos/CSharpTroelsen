using SkillBoxCourseCSharp.DelegateEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Multithreaded_Parallel_Async
{
    internal class DelegateAsyncExample
    {
        public delegate void MessageView(string message);

        public static void ExampleDelegateAsync()
        {
            MessageView mv = (message) => {
                Thread.Sleep(6000);
                Console.WriteLine(message);
                Thread.Sleep(6000);
            };

            //Вызывающий поток блокируется, пока не будет завершен BeginInvoke()
            IAsyncResult result = mv.BeginInvoke("Асинхронный делегат выполнен!", new AsyncCallback(MessageComplete), "Параметр, поддерживающий object");

            //Поток снова доступен
            Console.WriteLine("Main продолжает работать");

            //Ожидание выполнения MessageComplete и вызывающий поток не блокируется
            while(!result.IsCompleted)
            {
                Console.WriteLine("Ожидается сообщение...");
                Thread.Sleep(2000);
            }
        }

        static void MessageComplete(IAsyncResult iar)
        {
            Console.WriteLine($"MessageComplete() вызван {Thread.CurrentThread.ManagedThreadId} потоком");
            Console.WriteLine(iar.AsyncState.ToString());

            AsyncResult ar = (AsyncResult)iar;
            MessageView mv = (MessageView)ar.AsyncDelegate;
            mv.EndInvoke(iar);
        }
    }
}
