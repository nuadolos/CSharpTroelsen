using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenTalkingServiceClient.ServiceReference1;

namespace BenTalkingServiceClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Http or Tcp: ");
            string HttpOrTcp = Console.ReadLine();

            if (HttpOrTcp.ToLower() == "http")
                BTServiceHttp();
            else if (HttpOrTcp.ToLower() == "tcp")
                BTServiceTcp();
            else
            {
                Console.WriteLine("Опечатка!! Перезапуск приложения...");
                Task.Delay(2000).Wait();
                Process.Start("BenTalkingServiceClient.exe");
            }
        }

        static void BTServiceHttp()
        {
            using (BenTalkingClient ben = new BenTalkingClient("BasicHttpBinding_BenTalking"))
            {
                while (true)
                {
                    Console.Write("Вопрос: ");
                    string question = Console.ReadLine();

                    Console.WriteLine($"Бен сказал: {ben.BenAnswerToQuestion(question)}\n");

                    if (question.ToLower() == "return")
                        break;
                }
            }
        }

        static void BTServiceTcp()
        {
            using (BenTalkingClient ben = new BenTalkingClient("NetTcpBinding_BenTalking"))
            {
                while (true)
                {
                    Console.Write("Вопрос: ");
                    string question = Console.ReadLine();

                    Console.WriteLine($"Бен сказал: {ben.BenAnswerToQuestion(question)}\n");

                    if (question.ToLower() == "return")
                        break;
                }
            }
        }
    }
}
