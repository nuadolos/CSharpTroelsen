using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    public class LearnMaterial
    {
        /*Установка строго именованной сборки в GAC
         *  Для этого используется инструмент gacutil.exe:
         *  1) Открыть Visual Studio с правами администратора
         *  2) Открыть терминал
         *  3) Ввести путь к папке, где расположен .dll файл, используя ключ. слово cd
         *  4) Ввести gacutil с один из параметров и названием файла .dll
         *      /i - Устанавливает сборку сострогим именем в GAC
         *      /u - Удаляет сборку из GAC
         *      /l - Отображает список сборок (или конкретную сборку) в GAC
         *  
         *  Пример создания:
         *  cd C:\Users\nuadolos\source\repos\TestLibrary\TestLibrary\bin\Debug
         *  gacutil /i TestLibrary.dll
         *  gacutil /l TestLibrary
         *  gacutil /u TestLibrary
         */
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsLearning { get; set; }

        public LearnMaterial() : this(0, "", DateTime.Now, false)
        { }

        public LearnMaterial(int id, string name, DateTime startDate, bool isLearning)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            IsLearning = isLearning;
        }

        public void ResetLearn()
        {
            Id = 0;
            Name = string.Empty;
            StartDate = DateTime.Today;
            IsLearning = false;
        }
    }
}
