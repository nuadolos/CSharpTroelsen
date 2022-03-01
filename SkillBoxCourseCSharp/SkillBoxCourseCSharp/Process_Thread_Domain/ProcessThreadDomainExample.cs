using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Process_Thread_Domain
{
    internal class ProcessThreadDomainExample
    {
        /// <summary>
        /// Отображение всех работающих на данный момент процессов
        /// </summary>
        public static void ExampleProcess()
        {
            Process[] process = Process.GetProcesses();

            var sortIdProcesses = from p in process
                                  orderby p.Id select p;

            foreach (Process processItem in sortIdProcesses)
            {
                Console.WriteLine($"-> PID: {processItem.Id}\tName: {processItem.ProcessName}");
            }
        }

        /// <summary>
        /// Отображение всех потоков процесса, найденного по его PID
        /// </summary>
        public static void ExampleThreadAndModule()
        {
            Process p = null;

            try
            {
                Console.Write("PID -> ");
                p = Process.GetProcessById(int.Parse(Console.ReadLine()));
            }
            catch (ArgumentException er)
            {
                Console.WriteLine(er.Message);
            }

            if (p == null)
                return;

            Console.WriteLine($"\n\tВсе потоки процесса {p.ProcessName}\n");

            ProcessThreadCollection theardsCol = p.Threads;
            
            foreach (ProcessThread thread in theardsCol)
            {
                Console.WriteLine($"-> Thread ID: {thread.Id}\t" +
                    $"Start Time: {thread.StartTime.ToShortTimeString()}\t" +
                    $"Priority: {thread.PriorityLevel}");
            }

            Console.ReadKey();
            Console.Clear();

            //Для работы данной части кода необходимо:
            // 1. Зайти в свойства проекта
            // 2. Нажать на вкладку "Сборка"
            // 3. Убрать галочку с "Предпочтительна 32-разрядная версия"

            Console.Write($"PID -> {p.Id}");
            Console.WriteLine($"\n\tВсе модули процесса {p.ProcessName}\n");

            ProcessModuleCollection modulesCol = p.Modules;

            foreach (ProcessModule module in p.Modules)
            {
                Console.WriteLine($"-> Module: {module.ModuleName}\tPath: {module.FileName}\n");
            }
        }

        /// <summary>
        /// Запуск выбранного .exe файла, его анализ и уничтожение
        /// </summary>
        public static void ExampleStartAndKillProcess()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Files .exe (*.exe)|*.exe";
            Process exeFile = null;

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();

                if (ofd.ShowDialog() == true)
                {
                    startInfo.FileName = ofd.FileName;
                }
                startInfo.WindowStyle = ProcessWindowStyle.Normal;

                //Запуск процесса
                exeFile = Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Используемые потоки процесса
            Console.WriteLine($"\n\tВсе потоки процесса {exeFile.ProcessName}\n");

            foreach (ProcessThread thread in exeFile.Threads)
            {
                Console.WriteLine($"-> Thread ID: {thread.Id}\t" +
                    $"Start Time: {thread.StartTime.ToShortTimeString()}\t" +
                    $"Priority: {thread.PriorityLevel}");
            }

            Console.WriteLine($"\n--> Для завершения процесса {exeFile.ProcessName} нажмите на любую кнопку");
            Console.ReadKey();

            try
            {
                //Завершение процесса (Если процесс уже был завершен, то возникнет ошибка)
                exeFile.Kill();
                Console.WriteLine("\nПроцесс завершен");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public static void ExampleAppDomain()
        {
            //Ссылка на стандартный домен приложения
            AppDomain[] appDomains = new AppDomain[]
            {
                AppDomain.CurrentDomain,
                AppDomain.CreateDomain("SecondDomain")
            };

            foreach (AppDomain domains in appDomains)
            {
                Console.WriteLine($"\nНазвание домена: {domains.FriendlyName}");

                //Регистрация события AssemblyLoad на отслеживание загруженных сборок в любой домен
                domains.AssemblyLoad += (s, e) => Console.WriteLine($"\nСборка {e.LoadedAssembly.GetName().Name} была загружена в домен!\n");
                domains.Load("PrivateLibrary");

                Console.WriteLine($"\tИдентификатор: {domains.Id}");
                Console.WriteLine($"\tЯвляется стандартным?: {(domains.IsDefaultAppDomain() ? "Да" : "Нет")}");
                Console.WriteLine($"\tБазовый католог: {domains.BaseDirectory}");
            }

            Assembly[] assembliesDomain = appDomains[0].GetAssemblies();
            Console.WriteLine($"\nВсе сборки, содержащиеся в домене {appDomains[0].FriendlyName}\n");

            //При использовании LINQ To Objects в домене появятся сборки System.dll и System.Core.dll
            var orderbyAssemblies = from asembly in assembliesDomain
                                    orderby asembly.GetName().Name select asembly;

            foreach (Assembly asm in orderbyAssemblies)
            {
                Console.WriteLine($"\t-> Сборка {asm.FullName}");
            }

            //Регистрация события DomainUnload на выгрузку домена из процесса или на завершение процесса
            appDomains[1].DomainUnload += (s, e) => Console.WriteLine($"\nДомен SecondDomain выгружен!\n");
            AppDomain.Unload(appDomains[1]);
        }

        public static void ExampleContextualBorder()
        {
            TestContext1 test1Ctx1 = new TestContext1();
            Console.WriteLine();

            TestContext1 test1Ctx2 = new TestContext1();
            Console.WriteLine();

            TestContext2 test2Ctx1 = new TestContext2();
            Console.WriteLine();

            TestContext2 test2Ctx2 = new TestContext2();
            Console.WriteLine();
        }

        /// <summary>
        /// Тестовый класс TestContext1 для метода ExampleContextualBorder()
        /// созданный объект которого является контекстно-связанным
        /// и находится в стандартном контексте 0
        /// </summary>
        public class TestContext1
        {
            public TestContext1()
            {
                Context ctx = Thread.CurrentContext;
                Console.WriteLine($"Объект {this.ToString()} находится в контексте {ctx.ContextID}");
                foreach (IContextProperty iCtx in ctx.ContextProperties)
                {
                    Console.WriteLine($"\t->Context Prop: {iCtx.Name}");
                }
            }
        }

        /// <summary>
        /// Тестовый класс TestContext2 для метода ExampleContextualBorder(),
        /// созданный объект которого является контекстно-связанным
        /// и загружен в уникальную контекстную границу с идентификатором 1,
        /// так как декорирован атрибут Synchronization и наследует класс ContextBoundObject
        /// </summary>
        [Synchronization]
        public class TestContext2 : ContextBoundObject
        {
            public TestContext2()
            {
                Context ctx = Thread.CurrentContext;
                Console.WriteLine($"Объект {this.ToString()} находится в контексте {ctx.ContextID}");
                foreach (IContextProperty iCtx in ctx.ContextProperties)
                {
                    Console.WriteLine($"\t->Context Prop: {iCtx.Name}");
                }
            }
        }
    }
}
