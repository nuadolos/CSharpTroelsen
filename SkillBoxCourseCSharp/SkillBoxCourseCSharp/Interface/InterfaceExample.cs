using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Interface
{
    internal class InterfaceExample
    {
        public static void MainIEnumerator()
        {
            ListProduct prod = new ListProduct();

            //Получение объектов класса ListProduct,
            //используя GetEnumerator(), содержащийся в классе ListProduct
            //
            //Без реализации интерфейса IEnumerable использование оператора
            //foreach, указав переменную prod, невозможно!
            foreach (Product item in prod)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n\n\n");

            //Получение объектов класса ListProduct,
            //используя именнованный итератор ReservedEnumerator()
            foreach (Product item in prod.ReservedEnumerator())
            {
                Console.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// Пример, где ссылочные переменные также указывают на один и тот же объект
        /// </summary>
        public static void MainICloneable1()
        {
            ListProduct prod1 = new ListProduct();
            ListProduct prod2 = (ListProduct)prod1.Clone();

            foreach (var item in prod1)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("\n\n\n");
            foreach (var item in prod2)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n\n\n");
            prod2[2].Type = "aaaa";
            prod2[3].Name = "bbbb";

            foreach (var item in prod1)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("\n\n\n");
            foreach (var item in prod2)
            {
                Console.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// Пример, где ссылочные переменные указавыют на разные объекты в памяти
        /// </summary>
        public static void MainICloneable2()
        {
            var obj = new object();

            ListProduct prod1 = new ListProduct();
            ListProduct prod2 = (ListProduct)prod1.Clone(obj);

            foreach (var item in prod1)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("\n\n\n");
            foreach (var item in prod2)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n\n\n");
            prod2[2].Type = "aaaa";
            prod2[3].Name = "bbbb";

            foreach (var item in prod1)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("\n\n\n");
            foreach (var item in prod2)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public static void MainIComparable()
        {
            ListProduct prod1 = new ListProduct();
            Product[] arrayProd = prod1.lProduct.ToArray();

            foreach (var item in arrayProd)
            {
                Console.WriteLine(item.ToString());
            }

            //Array.Sort(arrayProd);//Чтобы выбрать другой метод CompareTo(),
                                  //необходимо его расскоментировать, но закомментировать другой.
            Array.Sort(arrayProd, Product.SortByArticle);
            Console.WriteLine("\n\n\n");

            foreach (var item in arrayProd)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
