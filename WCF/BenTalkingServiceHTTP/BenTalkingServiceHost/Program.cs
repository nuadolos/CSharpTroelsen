using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BenTalkingServiceLib;

namespace BenTalkingServiceHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost sHost = new ServiceHost(typeof(BenTalkingService)))
            {
                sHost.Open();
                DisplayHostInfo(sHost);

                Console.ReadKey();
            }
        }

        static void DisplayHostInfo(ServiceHost host)
        {
            Console.WriteLine($"\tИнформация о хосте");

            foreach (System.ServiceModel.Description.ServiceEndpoint endpoint in host.Description.Endpoints)
            {
                Console.WriteLine($"Адрес: {endpoint.Address}");
                Console.WriteLine($"Привязка: {endpoint.Binding.Name}");
                Console.WriteLine($"Контракт: {endpoint.Contract.Name}");
                Console.WriteLine("****************\n");
            }
        }
    }
}
