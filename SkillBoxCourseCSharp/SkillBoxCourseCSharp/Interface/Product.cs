using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Interface
{
    internal class Product : ICloneable, IComparable, ISupplier
    {
        public string Name { get; set; }
        public int Article { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }

        public static IComparer SortByArticle { get => (IComparer)new ArticleComparer(); }

        #region Свойства, наследуюмые интерфейсом ISupplier

        public string SupName { get; set; }
        public DateTime DateShipment { get; set; }
        public string Description { get; set; }

        #endregion

        public Product() : this("Доширак", 526, "Еда", 0)
        { }
        public Product(string name, int article, string type, int price)
        {
            Name = name;
            Article = article;
            Type = type;
            Price = price;
        }

        //Переопределенный метод ToString() имеет стандартный формат,
        //придерживающийся принципу конструирования данного метода в типах из библиотек базовых классов .NET
        //public override string ToString() => $"[Name: {Name}; Article: {Article}; Type: {Type}; Price: {Price}]";
        public override string ToString() => $"Name: {Name}; Article: {Article}; Type: {Type}; Price: {Price}";

        /// <summary>
        /// Клонирование по умолчанию.
        /// </summary>
        /// <returns></returns>
        public object Clone() => this.MemberwiseClone();

        /// <summary>
        /// Сортировка объекта по свойству Price (цене).
        /// Демонстрирует взаимствование CompareTo() у переменной типа int
        /// this.Price - 1 объект, tempProd.Price - 2 объект.
        /// Происходит сравнивание, которое явно показано в следующем CompareTo() методе.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        //public int CompareTo(object obj)
        //{
        //    if (obj is Product tempProd)
        //        return this.Price.CompareTo(tempProd.Price);
        //    else
        //        throw new ArgumentException("Данный объект не является Product");
        //}

        /// <summary>
        /// Явная реализация метода CompareTo(), возвращающий
        /// 1 - 1 объект находится после 2 объекта,
        /// -1 - 1 объект находится до 2 объекта,
        /// 0 - 1 объект равен 2 объекту.
        /// Сортировка производится по свойству Name.Length.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public int CompareTo(object obj)
        {
            if (obj is Product tempProd)
            {
                if (this.Name.Length > tempProd.Name.Length)
                    return 1;
                else if (this.Name.Length < tempProd.Name.Length)
                    return -1;
                else
                    return 0;
            }
            else
                throw new ArgumentException("Ошибочка");
        }

        #region Методы, наследуюмые интерфейсом ISupplier

        public void PrintDataSupplier()
        {
            Console.WriteLine($"Supplier: {SupName}, {DateShipment}, {Description}");
        }

        public void StopDelivery()
        {
            Console.WriteLine("Поставка продуктов прекращена!");
        }

        #endregion
    }
}
