using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.IO
{
    internal class FileDirectoryExample
    {
        public static void ExampleSystemDirectoryFileInfo()
        {
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            Console.WriteLine("\tДиректория");
            Console.WriteLine($"Название: {directory.Name}");
            Console.WriteLine($"Последнее изменение: {directory.LastWriteTime.ToLongDateString()}");
            Console.WriteLine($"Дата создания: {directory.CreationTime.ToLongDateString()}");
            Console.WriteLine($"Атрибуты: {directory.Attributes}");
            Console.WriteLine($"Путь: {directory.FullName}");
            Console.WriteLine($"Корневой каталог: {directory.Root}");

            Console.ReadKey();
            Console.Clear();

            FileInfo file = new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}\testStringFile.txt");
            file.Create();

            Console.WriteLine("\tФайл");
            Console.WriteLine($"Название: {file.Name}");
            Console.WriteLine($"Последнее изменение: {file.LastWriteTime.ToLongDateString()}");
            Console.WriteLine($"Дата создания: {file.CreationTime.ToLongDateString()}");
            Console.WriteLine($"Атрибуты: {file.Attributes}");
            Console.WriteLine($"Расширение: {file.Extension}");
            Console.WriteLine($"Путь: {file.FullName}");

            Console.ReadKey();
            Console.Clear();

            DriveInfo[] drives = DriveInfo.GetDrives();

            Console.WriteLine("\tЗапоминающие устройства\n");
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine("Наименование: " + drive.Name);
                Console.WriteLine("Тип: " + drive.DriveType);

                if (drive.IsReady)
                {
                    double size = drive.TotalSize / 1_073_741_824;
                    double avFree = drive.AvailableFreeSpace / 1_073_741_824;
                    Console.WriteLine($"Свободное пространство: {size} ГБ");
                    Console.WriteLine($"Оставшееся свободное пространство: {avFree} ГБ");
                    Console.WriteLine($"Формат: {drive.DriveFormat}");
                    Console.WriteLine($"Метка тома: {drive.VolumeLabel}\n");
                }
            }
        }

        public static void ExampleUserDirectoryFileInfo()
        {
            Console.Write("Путь: ");
            string userPath = Console.ReadLine();

            DirectoryInfo directory = new DirectoryInfo(userPath);

            Console.WriteLine("\n\tДиректория");
            Console.WriteLine($"Название: {directory.Name}");
            Console.WriteLine($"Последнее изменение: {directory.LastWriteTime.ToLongDateString()}");
            Console.WriteLine($"Дата создания: {directory.CreationTime.ToLongDateString()}");
            Console.WriteLine($"Атрибуты: {directory.Attributes}");
            Console.WriteLine($"Путь: {directory.FullName}");
            Console.WriteLine($"Корневой каталог: {directory.Root}\n\n");

            FileInfo[] files = directory.GetFiles();
            Console.WriteLine($"\t\t\tФайлы папки {directory.Name}\n\t\tНазвание\t\tДата создания\t\tРасширение\n");
            foreach (FileInfo file in files)
            {
                Console.WriteLine($"{file.Name, 30}\t{file.CreationTime.ToLongDateString(), 20}\t{file.Extension, 10}");
            }
        }

        public static void ExampleStream()
        {
            long pozition;

            using (Stream stream1 = new FileStream("Bebrakids.txt", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                byte[] buffer = Encoding.UTF8.GetBytes("Люблю я бебру...\nПрощай мой мир...");
                stream1.WriteAsync(buffer, 0, buffer.Length);

                stream1.Position = 0;

                string fileTxt = Encoding.UTF8.GetString(buffer);
                Console.WriteLine("В файл была записана строка:\n" + fileTxt);

                pozition = stream1.Length;
            }

            using (StreamWriter stream2 = new StreamWriter(new FileStream("Bebrakids.txt", 
                FileMode.Open, FileAccess.Write, FileShare.None), Encoding.UTF8))
            {
                stream2.WriteLine("Здраствуй, бебра...Теперь и моя душа здесь навеки!");
                stream2.WriteLine("Как суров стал этот жестокий мир.");
            }

            using (StreamReader stream3 = new StreamReader("Bebrakids.txt", Encoding.UTF8, true))
            {
                Console.WriteLine("\n\nТекст с дополнениями:\n" /*+ stream3.ReadToEnd()*/);

                string input = null;
                while((input = stream3.ReadLine()) != null)
                {
                    Console.WriteLine(input);
                }
            }

            Console.ReadKey();
            Console.Clear();

            using (BinaryWriter stream4 = new BinaryWriter(new FileStream("Bebrakids.txt",
                FileMode.Open, FileAccess.Write, FileShare.None), Encoding.UTF8))
            {
                string str = "String is bebra..!";
                int randomNum = 8521;
                DateTime dateNow = DateTime.Now;
                double d = 52.52;

                stream4.Write(str);
                stream4.Write(randomNum);
                stream4.Write(dateNow.ToLongTimeString());
                stream4.Write(d);
            }

            using (BinaryReader stream5 = new BinaryReader(new FileStream("Bebrakids.txt",
                FileMode.Open, FileAccess.Read, FileShare.None), Encoding.UTF8))
            {
                Console.WriteLine(stream5.ReadString());
                Console.WriteLine(stream5.ReadInt32());
                Console.WriteLine(stream5.ReadString());
                Console.WriteLine(stream5.ReadDouble());
            }

            File.Delete("Bebrakids.txt");
        }

        #region FileSystemWatcher

        public static void ExampleFileSystemWatcher()
        {
            Task.Run(() =>
            {
                FileSystemWatcher watcher = new FileSystemWatcher();

                watcher.Path = @"C:\Users\nuadolos\Desktop\Ненужная помойка\фото очка\Авы";

                watcher.NotifyFilter = NotifyFilters.FileName |
                NotifyFilters.CreationTime | NotifyFilters.LastWrite
                | NotifyFilters.DirectoryName;

                watcher.Changed += new FileSystemEventHandler(OnChanged);
                watcher.Created += new FileSystemEventHandler(OnChanged);
                watcher.Deleted += new FileSystemEventHandler(OnChanged);
                watcher.Renamed += new RenamedEventHandler((source, e) => Console.WriteLine($"Файл {e.OldName} был переменован в {e.Name}"));

                watcher.EnableRaisingEvents = true;

                while(true) { };
            });
        }

        static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"Файл {e.Name} был {e.ChangeType}");
        }

        #endregion
    }
}
