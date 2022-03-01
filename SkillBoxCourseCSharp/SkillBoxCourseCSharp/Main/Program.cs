using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillBoxCourseCSharp.Aggregation;
using SkillBoxCourseCSharp.CustomException;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SkillBoxCourseCSharp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            #region Terminal utilities

            /* Полезные утилиты, вводимые в терминал:
             *  1. ildasm
             *  2. csc
             *  3. ilasm
             *      -/debug
             *      -/dll
             *      -/exe
             *      -/key
             *      -/output
             *  4. gacutil
             *      -/i - Устанавливает сборку сострогим именем в GAC
             *      -/u - Удаляет сборку из GAC
             *      -/l - Отображает список сборок (или конкретную сборку) в GAC
            */

            /*
             * Генерация строгого имени библиотеки:
             * 1. Перейти в свойства сборки
             * 2. Перейти во вкладку "Публикация"
             * 3. Поставить галку на "Подписать сборку"
             * 4. В выпадающем списке выбрать "Создать"
             * 5. Дать имя файла и нажать на "Ок" 
            */

            #endregion


            //Workers ww = new Workers();
            //ww.PublicDataWorker();

            //Console.ReadKey();

            //var testWorker = new Workers(
            //    name: "Необязательный параметры",
            //    surname: "Имба",
            //    salary: 15000
            //    );

            #region Entity Framework

            EntityFramework.EntityFrameworkExample.ExampleClassFuncEF();

            #endregion

            #region ADO.NET

            //ADO.ADONETExample.ExampleProviderFactory();
            //ADO.ADONETExample.ExampleSqlConnection();
            //ADO.ADONETExample.ExampleCRUDUser();

            #endregion

            #region File, Directory and Ser

            //IO.FileDirectoryExample.ExampleSystemDirectoryFileInfo();
            //IO.FileDirectoryExample.ExampleUserDirectoryFileInfo();
            //IO.FileDirectoryExample.ExampleStream();
            //IO.FileDirectoryExample.ExampleFileSystemWatcher();

            //IO.SerializeExample.ExampleSerialize();
            //IO.SerializeExample.ExampleDeserialize();

            #endregion

            #region Multithreaded, Parallel and Asynchronous programming

            //Multithreaded_Parallel_Async.DelegateAsyncExample.ExampleDelegateAsync();

            //Multithreaded_Parallel_Async.ThreadExample.ExampleThreadInfo();
            //Multithreaded_Parallel_Async.ThreadExample.ExampleThreadStart();
            //Multithreaded_Parallel_Async.ThreadExample.ExampleParametrizedThreadStart();
            //Multithreaded_Parallel_Async.ThreadExample.ExampleAutoResetEvent();
            //Multithreaded_Parallel_Async.ThreadExample.ExampleOtherTypes();
            //Multithreaded_Parallel_Async.ThreadExample.ExampleInterlocked();
            //Multithreaded_Parallel_Async.ThreadExample.ExampleTimer();
            //Multithreaded_Parallel_Async.ThreadExample.ExampleThreadPool();

            //Multithreaded_Parallel_Async.TaskExample.ExampleTPLAndParallelLINQ();
            //Multithreaded_Parallel_Async.TaskExample.ExampleTask();

            //Не указав await или метод Wait() выполняющий поток не блокируется
            //При этом вылезает лишь предупреждение
            //Multithreaded_Parallel_Async.AsyncAwaitExample.ExampleAsyncMethod().Wait();

            #endregion

            #region CIL

            //CIL.CILExample.ExampeCILCode();
            //CIL.CILExample.ExampleCreateAssembly();

            #endregion

            #region Process/Thread/Domain

            //Process_Thread_Domain.ProcessThreadDomainExample.ExampleProcess();
            //Process_Thread_Domain.ProcessThreadDomainExample.ExampleThreadAndModule();
            //Process_Thread_Domain.ProcessThreadDomainExample.ExampleStartAndKillProcess();
            //Process_Thread_Domain.ProcessThreadDomainExample.ExampleAppDomain();
            //Process_Thread_Domain.ProcessThreadDomainExample.ExampleContextualBorder();

            #endregion

            #region Dynamic

            //Dynamic.DynamicExample.ExampleDynamicClass();
            //Dynamic.DynamicExample.ExampleDynamicLateBinding();
            //Dynamic.DynamicExample.ExampleInteractionAssembly();

            #endregion

            #region Attribute

            //Attribute.AttributeExample.ExampleObsolete();//Влечет ошибку компиляции
            //Attribute.AttributeExample.ExampleReflectionCustomAttribute();
            //Attribute.AttributeExample.ExampleLateBindingCustomAttribute();

            #endregion

            #region Reflection

            //Reflection.ReflectionExample.ExampleReflection();
            //Reflection.ReflectionExample.ExampleDynamicLoading();
            //Reflection.ReflectionExample.ExampleLateBinding();

            #endregion

            #region Lifetime of objects

            //LifetimeOfObjects.LifetimeOfObjectsExample.LtOfObjectsExample();

            #endregion

            #region Library

            //Library.LibraryExample.PrivateLibraryExample();
            //Library.LibraryExample.TestLibraryExample();

            #endregion

            #region LINQ

            //LINQ.LINQExample.ExampleLINQ();
            //LINQ.LINQExample.ExampleLinqOfType();

            #endregion

            #region AdvancedTools

            //AdvancedTools.AdvancedToolsExample.ExampleOperator();
            //AdvancedTools.AdvancedToolsExample.ExampleСonverting();
            //AdvancedTools.AdvancedToolsExample.ExampleExpandingMethod();
            //AdvancedTools.AdvancedToolsExample.ExampleAnonymousMethod();
            //AdvancedTools.AdvancedToolsExample.ExampleAnonymousMethod1(1, "Бобер");

            #endregion

            #region Delegate and Event

            //DelegateEvent.DelegateEventExample.ExampleDelegateEvent();

            #endregion

            #region Collection

            //Collection.CollectionExample.MainCollectionList();
            //Collection.CollectionExample.MainCollectionQueue();
            //Collection.CollectionExample.MainCollectionStack();
            //Collection.CollectionExample.MainCollectionSortedSet();
            //Collection.CollectionExample.MainCollectionDictionary();

            #endregion

            #region Interface

            //Interface.InterfaceExample.MainIEnumerator();
            //Interface.InterfaceExample.MainICloneable1();
            //Interface.InterfaceExample.MainICloneable2();
            //Interface.InterfaceExample.MainIComparable();

            #endregion

            #region Exception

            //CustException.ExceptionStart();

            #endregion

            #region Формирование Json/XML-файлов

            //List<Workers> lWorker = new List<Workers>();

            //for (int i = 0; i < 5; i++)
            //{
            //    lWorker.Add(new Workers($"Олег_{i}", $"Жириков_{i}", i + 20, $"Отдел_{i}", i * 10000, i));
            //}

            #region Создание файла Json (Неструктурированного)

            //string json = JsonConvert.SerializeObject(lWorker);
            //File.WriteAllText(@"C:\Users\nuadolos\Desktop\jsonTest.json", json);

            #endregion

            #region Считывание файла Json

            //json = File.ReadAllText(@"C:\Users\nuadolos\Desktop\jsonTest.json");

            //lWorker = JsonConvert.DeserializeObject<List<Workers>>(json);

            //foreach (var printWorkers in lWorker)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine(printWorkers.Name);
            //    Console.WriteLine(printWorkers.Surname);
            //    Console.WriteLine(printWorkers.Age);
            //    Console.WriteLine(printWorkers.Department);
            //    Console.WriteLine(printWorkers.Salary);
            //    Console.WriteLine(printWorkers.CountProject);
            //}

            #endregion

            #region Создание структурированного файла Json

            //JObject mainTree = new JObject();

            //JArray arrayWorkers = new JArray();

            //foreach (var eachWorker in lWorker)
            //{
            //    JObject workers = new JObject();

            //    workers["Name"] = eachWorker.Name;
            //    workers["Surname"] = eachWorker.Surname;
            //    workers["Age"] = eachWorker.Age;
            //    workers["Department"] = eachWorker.Department;
            //    workers["Salary"] = eachWorker.Salary;
            //    workers["CountProject"] = eachWorker.CountProject;

            //    arrayWorkers.Add(workers);
            //}

            //mainTree["Workers"] = arrayWorkers;

            //string jsonWorkers = mainTree.ToString();

            //File.WriteAllText(@"C:\Users\nuadolos\Desktop\jsonTest.json", jsonWorkers);

            #endregion

            #region Считывание структурированного файла Json

            //string jsonReadFile = File.ReadAllText(@"C:\Users\nuadolos\Desktop\jsonTest.json");

            //Console.WriteLine(JObject.Parse(jsonReadFile)["Workers"].ToString());//Вывод ключа "Workers"

            //var allWorkers = JObject.Parse(jsonReadFile)["Workers"].ToArray();//Вывод значений ключей из массива Workers
            //foreach(var item in allWorkers)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine(item["Name"].ToString());
            //    Console.WriteLine(item["Surname"].ToString());
            //}

            #endregion


            #region Создание файла XML

            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Workers>));

            //using(Stream fStream = new FileStream(@"C:\Users\nuadolos\Desktop\xmlTest.xml", FileMode.Create, FileAccess.Write))
            //{
            //    xmlSerializer.Serialize(fStream, lWorker);
            //}

            #endregion

            #region Считывание файла XML

            //lWorker.Clear();

            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Workers>));

            //using(Stream fStream = new FileStream(@"C:\Users\nuadolos\Desktop\xmlTest.xml", FileMode.Open, FileAccess.Read))
            //{
            //    lWorker = xmlSerializer.Deserialize(fStream) as List<Workers>;
            //}

            //foreach (var printWorkers in lWorker)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine(printWorkers.Name);
            //    Console.WriteLine(printWorkers.Surname);
            //    Console.WriteLine(printWorkers.Age);
            //    Console.WriteLine(printWorkers.Department);
            //    Console.WriteLine(printWorkers.Salary);
            //    Console.WriteLine(printWorkers.CountProject);
            //}

            #endregion

            #region Создание файла сложного XML

            //XElement _System = new XElement("System");
            //XElement _Town = new XElement("Town");

            //int j = 0;
            //foreach (var item in lWorker)
            //{
            //    j++;

            //    XElement _Department = new XElement("Department");
            //    XElement _Worker = new XElement("Worker");

            //    XAttribute attrWName = new XAttribute("name", item.Name);
            //    XAttribute attrWSurname = new XAttribute("surname", item.Surname);
            //    XAttribute attrWAge = new XAttribute("age", item.Age);
            //    XAttribute attrWSalary = new XAttribute("salary", item.Salary);
            //    XAttribute attrWCountProject = new XAttribute("countProject", item.CountProject);

            //    _Worker.Add(attrWName, attrWSurname, attrWAge, attrWSalary, attrWCountProject);

            //    XAttribute attributeDepartment = new XAttribute("name", item.Department);

            //    _Department.Add(_Worker, attributeDepartment);
            //    _Town.Add(_Department);
            //}

            //XAttribute attributeCity = new XAttribute("name", "Москва");
            //_Town.Add(attributeCity);
            //_System.Add(_Town);
            //_System.Save(@"C:\Users\nuadolos\Desktop\xmlComplicatedTest.xml");

            #endregion

            #region Считывание файла сложного XML

            //string xmlRead = File.ReadAllText(@"C:\Users\nuadolos\Desktop\xmlComplicatedTest.xml");

            //var xmlList = XDocument.Parse(xmlRead)
            //    .Descendants("System")
            //    .Descendants("Town")
            //    .Descendants("Department")
            //    .ToList();

            ////Вывод на экран ветки Department
            //foreach (var item in xmlList)
            //{
            //    Console.WriteLine($"\n\n{item}");
            //}

            ////Вывод на экран поле name из ветки Department 
            //foreach (var item in xmlList)
            //{
            //    Console.WriteLine($"\n{item.Attribute("name").Value}");
            //}

            ////Вывод на экран значений поля name из подветки Worker из ветки Department
            //foreach (var item in xmlList)
            //{
            //    Console.WriteLine($"\nName - {item.Element("Worker").Attribute("name").Value}");

            //    //try
            //    //{
            //    //    for (int i = 1; string.Equals($"Department{i}", item.Element($"Department{i}").Name.ToString()); i++)
            //    //    {
            //    //        Console.WriteLine($"Name{i} - {item.Element($"Department{i}").Element("Worker").Attribute("name").Value}");
            //    //    }
            //    //}
            //    //catch (Exception)
            //    //{

            //    //}
            //}

            ////Вывод на экран из ветви Town значение поля name
            //string city = XDocument.Parse(xmlRead)
            //    .Element("System")
            //    .Element("Town")
            //    .Attribute("name").Value;

            //Console.WriteLine(city);

            #endregion

            #endregion

            #region Фрагменты кода

            //public int MyProperty { get; set; }//prop

            //private int myVar;//propfull

            //public int MyProperty1
            //{
            //    get { return myVar; }
            //    set { myVar = value; }
            //}

            //public int MyProperty2 { get; private set; }//propg

            //Просмотр всех фрагментов кода вызывается с помощью горячих кнопок: CTRL+K, CTRL+B

            #endregion

            Console.ReadKey();
        }
    }
}
