using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Json_XML_Partial
{
    internal class XmlExample
    {
        public static void XmlFormatExample()
        {
            List<Workers> lWorker = new List<Workers>();

            for (int i = 0; i < 5; i++)
            {
                lWorker.Add(new Workers($"Олег_{i}", $"Жириков_{i}", i + 20, $"Отдел_{i}", i * 10000, i));
            }

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
        }
    }
}
