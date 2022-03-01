using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Reflection
{
    internal class ReflectionExample
    {
        public static void ExampleReflection()
        {
            //Получение информации о типе Student
            DelegateEvent.Student typeSong = new DelegateEvent.Student();
            Type t1 = typeSong.GetType();

            //Получение информации о типе Directive
            Type t2 = typeof(LifetimeOfObjects.Directive);

            //Получение информации о типе LINQ.Song
            Type t3 = Type.GetType("SkillBoxCourseCSharp.LINQ.Song", false, true);

            //Получение информации о типе Car внешней сборки PrivateLibrary
            Type t4 = Type.GetType("PrivateLibrary.Car, PrivateLibrary");

            //Получение информации о типе для вложенного перечисления внутри текущей сборки
            Type t5 = Type.GetType("SkillBoxCourseCSharp.DelegateEvent.Student+СlubsStudent");

            //Чтобы вывести метаданные обобщенного типа необходимо использовать
            //символ ` и количество параметров типа:
            //System.Collections.Generic.List`1 (List<T>)

            //Получение информации о всех доступных методах всех ранее присвоенных типов
            MethodInfo[][] typesMethods = new MethodInfo[][]
            {
                t1.GetMethods(),
                t2.GetMethods(),
                t3.GetMethods(),
                t4.GetMethods(),
                t5.GetMethods()
            };

            //Вывод всех данных о методе (возвращающий тип, название, входные параметры)
            foreach (MethodInfo[] methodMas in typesMethods)
            {
                foreach (MethodInfo method in methodMas)
                {
                    Console.WriteLine("\n" + method.ReflectedType);
                    Console.WriteLine(method);
                }
                Console.WriteLine("\n\n");
            }

            Console.ReadKey();
            Console.Clear();

            //Вывод данных (Простаранство имен.Интерфейс, сборка и его характеристики) 
            //о интерфейсах, поддерживающие типы Student и Directive
            Console.WriteLine($"Тип {t1.Name}");
            foreach (Type i in t1.GetInterfaces())
            {
                Console.WriteLine(i.AssemblyQualifiedName);
            }
            Console.WriteLine($"\n\nТип {t2.Name}");
            foreach (Type i in t2.GetInterfaces())
            {
                Console.WriteLine(i.AssemblyQualifiedName);
            }
            //Такие процедуры можно проводить еще с полями и со свойствами

            Console.ReadKey();
            Console.Clear();

            //Вывод дополнительных деталей
            DisplayOptionalDetails(t1);
            DisplayOptionalDetails(t2);
            DisplayOptionalDetails(t3);
            DisplayOptionalDetails(t4);
            DisplayOptionalDetails(t5);
        }

        public static void ExampleDynamicLoading()
        {
            Assembly[] assemblies = new Assembly[]
                {
                    //В Load указывается дружественное имя сборки, при условии
                    //что она находится в папке bin\Debug этой сборки
                    Assembly.Load("PrivateLibrary"),
                    Assembly.Load("SkillBoxCourseCSharp"),
                    //В LoadFile указывается полный путь к другой сборке, что является удобнее,
                    //так как нет необходимости копировать его в данную сборку
                    Assembly.LoadFrom(@"C:\Users\nuadolos\source\repos\TelegramBotUI\TelegramBotUI\bin\Debug\TelegramBotUI.exe"),
                    
                    //Загрузка разделенной сборки TestLibrary
                    Assembly.Load(@"TestLibrary, Version=1.1.0.0, Culture=neutral, PublicKeyToken=ef46268e3e614574")
                };

            //Представление последнего элемента assemblies в удобной объектной переменной
            AssemblyName asmTestLibrary = new AssemblyName()
            {
                Name = "TestLibrary",
                Version = new Version("1.1.0.0")
            };

            //assemblies[3] = Assembly.Load(asmTestLibrary);

            foreach (Assembly asm in assemblies)
            {
                DisplayTypesInAsm(asm);
            }

            //При ручном вводе необходима проверка на существование данной сборки

            Console.ReadKey();
            Console.Clear();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".dll, .exe Files (*.dll;*.exe)|*.dll;*.exe";

            do
            {
                Console.Write("Завершить работу? (Да/Нет)");

                if (Console.ReadLine().ToLower() == "да")
                    break;

                try
                {
                    if (ofd.ShowDialog() == true)
                    {
                        DisplayTypesInAsm(Assembly.LoadFrom(ofd.FileName));
                    }
                }
                catch (Exception er)
                {
                    Console.WriteLine(er.Message);
                }
            }
            while (true);
        }

        public static void ExampleLateBinding()
        {
            //Загрузить локальную копию PrivateLibrary
            Assembly asm = Assembly.Load("PrivateLibrary");

            //Получение метаданных для типа Car
            Type t = asm.GetType("PrivateLibrary.Car");

            //Создать экземпляр Car
            object objCar = Activator.CreateInstance(t);

            //Получение информации о методе SpeedUp
            MethodInfo methodSpeedUp = t.GetMethod("SpeedUp");

            //Определение параметров для метода SpeedUp
            object[] paramsSpeed = new object[] { 10 };

            //Вызов метода SpeedUp,
            //указав в качестве параметра массив объектов paramsSpeed,
            //содержащий необходимые значения для входных параметров
            methodSpeedUp.Invoke(objCar, paramsSpeed);
        }

        /// <summary>
        /// Метод для вывода дополнительных деталей о входном типе
        /// </summary>
        /// <param name="t"></param>
        public static void DisplayOptionalDetails(Type t)
        {
            Console.WriteLine("\n\n\n");
            Console.WriteLine($"\tДополнительные детали о типе {t.Name}");
            Console.WriteLine($"Базовый класс: {t.BaseType}");
            Console.WriteLine($"Абстрактный? - {t.IsAbstract}");
            Console.WriteLine($"Запечатанный? - {t.IsSealed}");
            Console.WriteLine($"Обощенный? - {t.IsGenericTypeDefinition}");
            Console.WriteLine($"Этот тип является классом? - {t.IsClass}");
            Console.WriteLine($"Является вложенным в другой тип? - {t.IsNested}");
        }

        /// <summary>
        /// Метод для отображения содержимого входной сборки
        /// </summary>
        /// <param name="asm"></param>
        public static void DisplayTypesInAsm(Assembly asm)
        {
            Console.WriteLine("\n\n\n");
            Console.WriteLine($"Имя сборки: {asm.FullName}\n");

            Console.WriteLine("\tСодержание");
            foreach (Type t in asm.GetTypes())
            {
                Console.WriteLine($"Тип: {t.Name}");
            }
        }
    }
}
