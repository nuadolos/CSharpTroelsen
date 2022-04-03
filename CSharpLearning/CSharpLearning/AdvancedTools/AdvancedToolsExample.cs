using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.AdvancedTools
{
    internal class AdvancedToolsExample
    {
        public static void ExampleOperator()
        {
            User.CancelSubscriptionFailed += (sender, e) => { Console.WriteLine($"{e.message}\nУ {(sender as User).Login} проблемы..."); };
            User.PaymentFailed += (sender, e) => { Console.WriteLine($"{e.message}\nУ {(sender as User).Login} проблемы..."); };
            
            User Vlad = new User("Vladd", "vas", 150, 50);
            User maxBalance = new User() { Balance = 0};
            ListUsers listUsers = new ListUsers();

            foreach (User users in listUsers)
            {
                if (maxBalance < users)
                    maxBalance = users;
            }

            Console.WriteLine($"Максимальный баланс ({maxBalance.Balance}) у {maxBalance.Login}");
            Console.ReadKey();
            Console.Clear();

            Vlad += 200;
            Console.WriteLine($"Баланс: {Vlad.Balance}");
            Vlad.CancelSubscription();
            Console.WriteLine($"Баланс: {Vlad.Balance}");
            Vlad.Subscription = 200;
            Vlad.Payment(20);
            Console.WriteLine($"Баланс: {Vlad.Balance}");

            Console.ReadKey();
            Console.Clear();

            foreach (User users in listUsers)
            {
                for (int i = 0; i < listUsers.Count; i++)
                {
                    if (users == listUsers[i])
                        Console.WriteLine($"Два одинаковых пользователя! Быть не может, {users.Login}, ты не один!");
                }
            }
        }

        public static void ExampleСonverting()
        {
            User user = new User("АдрюшкинЛес", "впаы", 150, 2500);
            //implicit - Неявное преобразование
            Account acc = user;
            User user1 = new User();
            //и явное преобразование
            Account acc1 = (User)user1;
            Console.WriteLine("acc1:" + acc1.Login + "  " + acc1.Password);

            //explicit - только явное преобразование
            Account acc2 = new Account();
            User user2 = (User)acc2;
            Console.WriteLine("user2:" + user2.Login + "  " + user2.Password);

            //implicit - явное и неявное преобразование допустимо
            Account acc3 = new Account();
            string log = acc3;
            Console.WriteLine("log:" + log);

            //explicit - только явное преобразование
            string pas = "fsafs";
            Account acc4 = (Account)pas;
            Console.WriteLine("acc4:" + acc4.Password);
        }

        public static void ExampleExpandingMethod()
        {
            User us1 = new User();

            us1.DisplayElConsole();
            Console.WriteLine("\n\n");
            us1.DisplayDefiningAssembly();

            Console.ReadKey();
            Console.Clear();

            int myInt = 56721;
            myInt.DisplayElConsole();
            Console.WriteLine("\n\n");
            myInt.DisplayDefiningAssembly();
            Console.WriteLine("\n\n");
            Console.WriteLine("Число в обратном порядке:" + myInt.ReverseDigits());

            Console.ReadKey();
            Console.Clear();

            int[] intMas = new int[] { 52, 52, 743, 235, 743, 834, 865, 432, 151, 57 };
            intMas.PrintArrayOrList();
        }

        public static void ExampleAnonymousMethod()
        {
            var anonymous = new { Cash = 1000, Rep = true, Clothes = "Hoodies" };
            var anonymous1 = new { Cash = 1000, Rep = true, Clothes = "Hoodies" };

            if (anonymous == anonymous1)
            {
                Console.WriteLine("Они указывают на одну и ту же ячейку памяти!");
            }
            if (anonymous.Equals(anonymous1))
            {
                Console.WriteLine("Они действительно равны.");
            }
            if (anonymous.GetType().Name == anonymous1.GetType().Name)
            {
                Console.WriteLine("Они одного типа...");
            }

            Console.WriteLine("\n\n");

            anonymous.DisplayFullInformation();
            Console.WriteLine("\n\n");
            anonymous1.DisplayFullInformation();

            Console.ReadKey();
            Console.Clear();

            var anonymous2 = new { Money = 1000, Rep = false, Clothes = "Sweater" };
            var anonymous3 = anonymous2;

            if (anonymous2 == anonymous3)
            {
                Console.WriteLine("Они указывают на одну и ту же ячейку памяти!");
            }

            Console.WriteLine("\n\n");

            anonymous2.DisplayFullInformation();
            Console.WriteLine("\n\n");
            anonymous3.DisplayFullInformation();
        }

        public static void ExampleAnonymousMethod1(int index, string name, ConsoleColor color = ConsoleColor.Cyan)
        {
            var anonymousMethod = new { Id = index, Name = name, Color = color };

            Console.WriteLine($"Информация о anonymousMethod: {anonymousMethod.Id}\t{anonymousMethod.Name}\t{anonymousMethod.Color}");
           
            Console.BackgroundColor = anonymousMethod.Color;

            Console.ReadKey();
            Console.Clear();

            var nestedAnonym = new
            {
                User = new User() { Login = "Надюшка*_*"},
                Anonym = new { Color1 = ConsoleColor.Red, Color2 = ConsoleColor.Green, Color3 = ConsoleColor.Blue },
                Date = DateTime.Now.Date
            };

            Console.WriteLine($"Имя пользователя: {nestedAnonym.User.Login}\nВторой цвет: {nestedAnonym.Anonym.Color2}\nДата: {nestedAnonym.Date}");
        }
    }
}
