using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.LifetimeOfObjects
{
    public class Directive : IEnumerable<File>, IDisposable
    {
        public delegate void FilesHandler(object sender, FileEventArgs e);
        public static event FilesHandler AddedFile;
        public static event FilesHandler DeletedFile;
            
        private List<File> files;
        //Создание объекта при его вызове, иначе приложение не выделяет для него память
        private Lazy<List<File>> loadedFiles = new Lazy<List<File>>();

        public int Id { get; set; }
        public string Title { get; set; }
        public int CountFiles { get => files.Count; }
        public bool CanAddFile { get; set; }

        public File this[int index] { get => files[index]; }

        public Directive(string title, bool canAddFiles, List<File> filesList = null)
        {
            files = filesList ?? new List<File>();
            Title = title;
            CanAddFile = canAddFiles;
        }

        ~Directive() => Console.WriteLine("Произошла сборка мусора");

        public void SignatureForEvents(FilesHandler AddedMethod, FilesHandler DeletedMethod)
        {
            AddedFile = AddedMethod;
            DeletedFile = DeletedMethod;
        }

        public void AddFile(File fileToAdd)
        {
            AddedFile?.Invoke(fileToAdd, new FileEventArgs($"Файл {fileToAdd.Name} добавлен", "Добавление"));
            files.Add(fileToAdd);
        }

        public void RemoveFile(File fileToDelete)
        {
            DeletedFile?.Invoke(fileToDelete, new FileEventArgs($"Файл {fileToDelete.Name} удален", "Удаление"));
            files.Remove(fileToDelete);
        }

        public IEnumerator<File> GetEnumerator() => files.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => files.GetEnumerator();

        public void Dispose() => Console.WriteLine("Ручное особождение ресурсов");
    }
}
