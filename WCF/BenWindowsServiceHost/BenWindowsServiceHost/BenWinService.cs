using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using BenTalkingServiceLib;
using System.ServiceModel;

namespace BenWindowsServiceHost
{
    public partial class BenWinService : ServiceBase
    {
        private ServiceHost benHost;

        public BenWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //Если хост не был закрыт, то закрыть его
            benHost?.Close();

            ////Создание хоста и ABC
            //benHost = new ServiceHost(typeof(BenTalkingService));
            //Uri address = new Uri("http://localhost:8080/BenTalkingService");
            //WSHttpBinding binding = new WSHttpBinding();
            //Type contract = typeof(IBenTalking);

            ////Добавление конечной точки
            //benHost.AddServiceEndpoint(contract, binding, address);

            //Создание хоста с указанием адреса
            benHost = new ServiceHost(typeof(BenTalkingService), new Uri("http://localhost:8080/BenTalkingService"));

            //Использование стандартной конечной точки
            benHost.AddDefaultEndpoints();

            //Открытие хоста
            benHost.Open();

            /*Для того, чтобы установить службу Windows необходимо:
             * 1) Во вкладке "Вид" выбрать "Терминал"
             * 2) Прописать команду "installutil BenWindowsServiceHost.exe"
             * P.S. нужны права администратора
             * 
             * Чтобы удалить данную службу необходимо:
             * 1) Открыть командную строку с правами администратора
             * 2) Прописать команду "sc delete (Название службы - BenService)"
             */
        }

        protected override void OnStop()
        {
            //Остановка хоста
            benHost?.Close();
        }
    }
}
