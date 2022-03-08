using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;

namespace CognitionWPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        //С помощью командной строки ОС Windows можно запускать приложение с параметрами:
        //      1) Открыть командную строку
        //      2) С помощью команды cd найти папку debug проекта
        //      3) Ввести название и расширение .exe файла и через пробел прописать все необходимые параметры

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application.Current.Properties["AdminRights"] = false;

            //foreach (string arg in e.Args)
            //{
            //    if (arg.Equals("/adminrights", StringComparison.OrdinalIgnoreCase))
            //    {
            //        Application.Current.Properties["AdminRights"] = true;
            //        break;
            //    }
            //}

            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                Application.Current.Properties["AdminRights"] = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            string msg = $"Завершение ссесии в {((bool)Application.Current.Properties["AdminRights"] ? "административном" : "обычном")} режиме";
            MessageBox.Show(msg, "Закрытие приложения", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
