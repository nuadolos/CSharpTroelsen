using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Collection
{
    internal class CollectionExample
    {
        public static void MainCollectionList()
        {
            List<Order> orderList = new List<Order>()
            {
                new Order(),
                new Order() { Id = 1, ClientName = "Андрей", CoffeeName = "Капучино", Volume = 0.3, Price = 350},
                new Order(2, "Владимир", "Эспрессо", 0.2, 400),
                new Order(3, "Мария", "Латте", 0.3, 400),
                new Order(4, "Валерий", "Капучино", 0.5, 350),
            };

            foreach(Order order in orderList)
            {
                Console.WriteLine(order.ToString());
            }

            orderList.Add(new Order(5, "Дарья", "Латте", 1, 400));
            Console.WriteLine("\n\n\n");

            foreach (Order order in orderList)
            {
                Console.WriteLine(order.ToString());
            }
        }

        public static void MainCollectionQueue()
        {
            Queue<Order> orderQueue = new Queue<Order>();

            orderQueue.Enqueue(new Order());
            orderQueue.Enqueue(new Order() { Id = 1, ClientName = "Андрей", CoffeeName = "Капучино", Volume = 0.3, Price = 350 });
            orderQueue.Enqueue(new Order(2, "Владимир", "Эспрессо", 0.2, 400));
            orderQueue.Enqueue(new Order(3, "Мария", "Латте", 0.3, 400));
            orderQueue.Enqueue(new Order(4, "Валерий", "Капучино", 0.5, 350));

            while (orderQueue.Count > 0)
            {
                Console.WriteLine(orderQueue.Dequeue().ToString());
            }

            Console.WriteLine("Список закончился");
        }

        public static void MainCollectionStack()
        {
            Stack<Order> orderQueue = new Stack<Order>();

            orderQueue.Push(new Order());
            orderQueue.Push(new Order() { Id = 1, ClientName = "Андрей", CoffeeName = "Капучино", Volume = 0.3, Price = 350 });
            orderQueue.Push(new Order(2, "Владимир", "Эспрессо", 0.2, 400));
            orderQueue.Push(new Order(3, "Мария", "Латте", 0.3, 400));
            orderQueue.Push(new Order(4, "Валерий", "Капучино", 0.5, 350));

            while (orderQueue.Count > 0)
            {
                Console.WriteLine(orderQueue.Pop().ToString());
            }

            Console.WriteLine("Список закончился");
        }

        public static void MainCollectionSortedSet()
        {
            SortedSet<Order> orderSortedSet = new SortedSet<Order>(new SortByCoffeeName())
            {
                new Order(),
                new Order() { Id = 1, ClientName = "Андрей", CoffeeName = "Капучино", Volume = 0.3, Price = 350},
                new Order(2, "Владимир", "Эспрессо", 0.2, 400),
                new Order(3, "Мария", "Латте", 0.3, 400),
                new Order(4, "Валерий", "Капучино", 0.5, 350),
            };

            foreach (Order order in orderSortedSet)
            {
                Console.WriteLine(order.ToString());
            }

            orderSortedSet.Add(new Order(5, "Дарья", "Латте", 1, 400));
            Console.WriteLine("\n\n\n");

            foreach (Order order in orderSortedSet)
            {
                Console.WriteLine(order.ToString());
            }
        }

        public static void MainCollectionDictionary()
        {
            Dictionary<string,Order> orderDictionary = new Dictionary<string, Order>()
            {
                ["Sergey0"] = new Order(),
                ["Andrey1"] = new Order() { Id = 1, ClientName = "Андрей", CoffeeName = "Капучино", Volume = 0.3, Price = 350},
                ["Vova2"] = new Order(2, "Владимир", "Эспрессо", 0.2, 400),
                ["Marya3"] = new Order(3, "Мария", "Латте", 0.3, 400),
                ["Valera4"] = new Order(4, "Валерий", "Капучино", 0.5, 350),
            };

            Console.WriteLine(orderDictionary["Marya3"].ToString());
            Console.WriteLine("\n\n\n");
            Console.WriteLine(orderDictionary["Valera4"].ToString());
            Console.WriteLine("\n\n\n");
            Console.WriteLine(orderDictionary["Andrey1"].ToString());
        }
    }
}
