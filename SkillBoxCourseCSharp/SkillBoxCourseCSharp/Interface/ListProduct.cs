using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Interface
{
    internal class ListProduct : IEnumerable, ICloneable
    {
        public List<Product> lProduct;
        public Random random;

        /// <summary>
        /// Индексатор, возвращающий объект класса Product
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Product this[int index]
        { 
            get
            {
                return lProduct[index];
            }
        }


        public ListProduct() 
        {
            random = new Random();

            lProduct = new List<Product>() 
            { 
                new Product(),
                new Product("Сок", 32, "Напиток", random.Next(100,1000)),
                new Product("Энергетик", 3, "Напиток", random.Next(100,1000)),
                new Product("Говядина", 63, "Еда", random.Next(100,1000))
            };
        }

        /// <summary>
        /// Реализация обобщенного метода ToArray(),
        /// формирующий обобщенный массив из обобщенного списка.
        /// Метод поддерживает ограничения по параметру типа (заполнителю).
        /// Сейчас метод может принимать параметр типа, являющийся структурой.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] ExampleT<T>() where T : struct /*class, new()*/
        {
            T[] array = new T[this.lProduct.Count];
            List<T> list = new List<T>();

            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }

            return array;
        }

        /// <summary>
        /// Реализованный объектом lProduct итератор.
        /// Метод предоставляет поддержку перечесления с помощью foreach.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            //Возвращать GetEnumerator объекта lProduct
            return lProduct.GetEnumerator();
        }

        /// <summary>
        /// Именованный итератор с возможностью возврата элементов в обратном порядке.
        /// </summary>
        /// <returns></returns>
        public IEnumerable ReservedEnumerator()
        {
            for (int i = lProduct.Count - 1; i >= 0; i--)
            {
                //Определяет возвращаемый элемент - сохраняет его текущее состояние
                yield return lProduct[i];

                if (i == 2)
                    //Запрещает вызов MoveNext, заканчивая возвращать доступные элементы
                    yield break;

                //Сама конструкция похожа на индексатор, но используется исключительно для перечислений
            }
        }

        /// <summary>
        /// Копирование по умолчанию.
        /// Копирование данным способом приведет к поверхностной копии,
        /// так как класс ListProduct содержит переменную ссылочного типа lProduct,
        /// тем самым это приведет к тому, 
        /// что копия lProduct будет указывать на тот же объект в памяти,
        /// что и первоначальный lProduct.
        /// Данный метод подходит к классу, где все переменные типа значений.
        /// </summary>
        /// <returns></returns>
        public object Clone() => this.MemberwiseClone();

        /// <summary>
        /// Глубокое (детальное) копирование.
        /// Все переменные ссылочного типа будут иметь новый экземпляр,
        /// но если это вручную укажет разработчик.
        /// Параметры для данного метода можно не указывать
        /// (Используется для перезагрузки в качестве примера).
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public object Clone(object _)
        {
            ListProduct lProd = (ListProduct)this.MemberwiseClone();

            lProd.lProduct = new List<Product>()
            {
                (Product)this.lProduct[0].Clone(),
                (Product)this.lProduct[1].Clone(),
                (Product)this.lProduct[2].Clone(),
                (Product)this.lProduct[3].Clone()
            };
            return lProd;
        }
    }
}
