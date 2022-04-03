using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace SkillBoxCourseCSharp.Dynamic
{
    internal class DynamicExample
    {
        /// <summary>
        /// Метод, демонстрирующий поведение свойства DnmObj при присвоение значения другого типа
        /// </summary>
        public static void ExampleDynamicClass()
        {
            DynamicClass dnmcClass1 = new DynamicClass("Хорошая погода!");
            Console.WriteLine(dnmcClass1);

            DynamicClass dnmcClass2 = new DynamicClass(new object());
            Console.WriteLine(dnmcClass2);

            DynamicClass dnmcClass3 = new DynamicClass(8743);
            Console.WriteLine(dnmcClass3);

            dnmcClass2.DnmObj = true;
            dnmcClass3.DnmObj = new Interface.Product();

            Console.WriteLine(dnmcClass2);
            Console.WriteLine(dnmcClass3);
        }

        /// <summary>
        /// Метод, раскрывающий полезность динамических объектов в позднем связывании
        /// </summary>
        public static void ExampleDynamicLateBinding()
        {
            try
            {
                //Загрузка локальной сборки PrivateLibrary
                Assembly asmObj = Assembly.Load("PrivateLibrary");

                //Получение метаданных для типа Car
                Type car = asmObj.GetType("PrivateLibrary.Car");

                //Создание экземпляра типа Car
                dynamic localCar = Activator.CreateInstance(car);

                //Использование метода SpeedUp динамического экземпляра класса Car
                for (int i = 0; i < 8; i++)
                    localCar.SpeedUp(i + 250 / (70 - (i + 2 * i + 20) / (i + 1)));

                //Так как в классе Car нет метода PowerOff, то произойдет ошибка!
                localCar.PowerOff();

                //Аналогично произойдет ошибка, так как в классе Car нет метода SpeedUp, который не содержит параметры
                localCar.SpeedUp();
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException er)
            {
                Console.WriteLine(er.Message);
            }
        }

        /// <summary>
        /// Метод для создания Excel файла с данными
        /// </summary>
        public static void ExampleInteractionAssembly()
        {
            Random rnd = new Random();

            List<LifetimeOfObjects.Directive> dirList = new List<LifetimeOfObjects.Directive>()
            {
                new LifetimeOfObjects.Directive("Учеба", true) { Id = 1},
                new LifetimeOfObjects.Directive("Телеграм", true) { Id = 2},
                new LifetimeOfObjects.Directive("Семейное", false) { Id = 3}
            };

            int fileId = 1;
            foreach (LifetimeOfObjects.Directive dir in dirList)
            {
                for (int i = 0; i < 5; i++)
                {
                    dir.AddFile(new LifetimeOfObjects.File($"{System.IO.Path.GetRandomFileName()}",
                        DateTime.Now.AddMinutes(rnd.Next(0, 100)), "PNG", rnd.Next(500, 10000))
                    { Id = fileId});

                    fileId++;
                }
            }

            //Загрузить Excel и создать пустую рабочую книгу
            Excel.Application excelApp = new Excel.Application();
            excelApp.Workbooks.Add();

            //Количество листов в рабочей книге
            excelApp.SheetsInNewWorkbook = 3;

            //Создание 3 листов для хранения различных данных
            Excel._Worksheet workSheetDirective = (Excel.Worksheet)excelApp.Worksheets.get_Item(1);
            Excel._Worksheet workSheetFile = (Excel.Worksheet)excelApp.Worksheets.get_Item(2);
            Excel._Worksheet workSheetDirectiveFile = (Excel.Worksheet)excelApp.Worksheets.get_Item(3);

            //Установка имени для каждого листа
            workSheetDirective.Name = "Directive";
            workSheetFile.Name = "File";
            workSheetDirectiveFile.Name = "DirFile";

            //Установка заголовок столбцов в первые ячейки
            workSheetDirective.Cells[1, "A"] = "ID";
            workSheetDirective.Cells[1, "B"] = "Название папки";
            workSheetDirective.Cells[1, "C"] = "Возможно добавление?";

            workSheetFile.Cells[1, "A"] = "ID";
            workSheetFile.Cells[1, "B"] = "Название файла";
            workSheetFile.Cells[1, "C"] = "Дата добавления";
            workSheetFile.Cells[1, "D"] = "Тип";
            workSheetFile.Cells[1, "E"] = "Размер";

            workSheetDirectiveFile.Cells[1, "A"] = "Папка";
            workSheetDirectiveFile.Cells[1, "B"] = "Файл";

            //Запись данных из List<LifetimeOfObjects.Directive> в листы Excel
            int row = 1;
            int rowFile = 1;
            int rowDF = 1;
            foreach (LifetimeOfObjects.Directive dir in dirList)
            {
                row++;
                workSheetDirective.Cells[row, "A"] = dir.Id;
                workSheetDirective.Cells[row, "B"] = dir.Title;
                workSheetDirective.Cells[row, "C"] = dir.CanAddFile;

                foreach (LifetimeOfObjects.File file in dir)
                {
                    rowFile++;
                    workSheetFile.Cells[rowFile, "A"] = file.Id;
                    workSheetFile.Cells[rowFile, "B"] = file.Name;
                    workSheetFile.Cells[rowFile, "C"] = file.DateAdded;
                    workSheetFile.Cells[rowFile, "D"] = file.Type;
                    workSheetFile.Cells[rowFile, "E"] = file.Size;

                    rowDF++;
                    workSheetDirectiveFile.Cells[rowDF, "A"] = dir.Id;
                    workSheetDirectiveFile.Cells[rowDF, "B"] = file.Id;
                }
            }

            //Авторазмер ячеек
            workSheetDirective.Range["A1", "C20"].EntireColumn.AutoFit();
            workSheetFile.Range["A1", "E20"].EntireColumn.AutoFit();
            workSheetDirectiveFile.Range["A1", "B20"].EntireColumn.AutoFit();

            //Схоранение файла и благополучное завершение работы Excel
            excelApp.Application.ActiveWorkbook.SaveAs($@"{Environment.CurrentDirectory}\FileSystem.xlsx");
            Console.WriteLine("Файл FileSystem сохранен в папке приложения");
            excelApp.Quit();

            //Подробнее о работе с Excel: https://razilov-code.ru/2017/12/13/microsoft-office-interop-excel/
        }
    }
}
