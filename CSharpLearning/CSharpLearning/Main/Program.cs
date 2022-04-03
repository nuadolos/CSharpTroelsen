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

            #region Json

            //Json_XML_Partial.JsonExample.JsonFormatExample();

            #endregion

            #region Xml

            //Json_XML_Partial.XmlExample.XmlFormatExample();

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
