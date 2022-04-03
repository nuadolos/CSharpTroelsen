using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Json_XML_Partial
{
    internal class JsonExample
    {
        public static void JsonFormatExample()
        {
            List<Workers> lWorker = new List<Workers>();

            for (int i = 0; i< 5; i++)
            {
                lWorker.Add(new Workers($"Олег_{i}", $"Жириков_{i}", i + 20, $"Отдел_{i}", i* 10000, i));
            }

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

        }
    }
}
