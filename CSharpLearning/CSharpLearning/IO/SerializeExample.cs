using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SkillBoxCourseCSharp.IO
{
    public class SerializeExample
    {
        public static void ExampleSerialize()
        {
            List<Home> homes = new List<Home>()
        {
            new Home(),
            new Home("Руберцы", "Владислав", "Николаево", 66, true),
            new Home("Гробер", "Сергей", "Хабаровск", 66, true),
            new Home("Ламит", "Андрей", "Санкт-Петербург", 66, true),
            new Home("Восток-2", "Николай", "Владимир", 66, true),
        };

            BinaryFormatter binFormat = new BinaryFormatter();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Home>));

            using (Stream fStream = new FileStream("ponos.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                binFormat.Serialize(fStream, homes);
            }

            using (Stream fStream = new FileStream("normHome.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                xmlSerializer.Serialize(fStream, homes);
            }

            //string line = null;
            //using (StreamReader sr = new StreamReader(new FileStream("user.dat", FileMode.Open, FileAccess.Read, FileShare.None)))
            //{
            //    line = sr.ReadToEnd();
            //}
            //using (StreamWriter sw = new StreamWriter(new FileStream("user.dat", FileMode.Create, FileAccess.Write, FileShare.None), Encoding.UTF8))
            //{
            //    sw.WriteLine(line);
            //}
        }

        public static void ExampleDeserialize()
        {
            List<Home> homes = new List<Home>();

            BinaryFormatter binFormat = new BinaryFormatter();
            XmlSerializer xSerializer = new XmlSerializer(typeof(List<Home>));

            using (Stream fStream = new FileStream("ponos.txt",FileMode.Open, FileAccess.Read, FileShare.None))
            {
                if (binFormat.Deserialize(fStream) is List<Home> lHomes)
                {
                    homes = lHomes;
                }

                foreach (Home home in homes)
                {
                    Console.WriteLine(home?.ToString());
                }
            }

            Console.WriteLine();

            //Значения IsOwner в XML сохранились, так как атрибут [NonSerialized] никак на XmlSerializer не влияет
            //Если было закрытое поле, то тогда оно бы не записалось
            using (Stream fStream = new FileStream("normHome.xml", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                if (xSerializer.Deserialize(fStream) is List<Home> lHomes)
                {
                    homes = lHomes;
                }

                foreach (Home home in homes)
                {
                    Console.WriteLine(home?.ToString());
                }
            }
        }
    }
}
