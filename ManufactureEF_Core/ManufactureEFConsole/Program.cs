using ManufactureEF.Entities;
using ManufactureEF_Core.Context;
using ManufactureEF_Core.DataInitializer;
using ManufactureEF_Core.Repos;

/*
 * Консольное приложение .NET Core
 * 
 * Реализацию следующих операторов верхнего уровня можно опустить:
 *      - Пространство имен названия проекта (в данном случае ManufactureEFConsole)
 *      - Класс Program
 *      - Метод Main - точка входа в приложение
 * 
 * При отсутствии операторов верхнего уровня
 *      весь написанный код неявно находится в статическом методе Main класса Program!
 */

//namespace ManufactureEFConsole
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
Console.WriteLine("\tManufactureContext\n");

using (var context = new ManufactureContext())
{
    Initializer.RecreateDatabase(context);
    Initializer.InitializeData(context);

    foreach (User user in context.User)
    {
        Console.WriteLine(user);
    }
}

Console.WriteLine("\n\tUserRepo\n");

using (var repo = new UserRepo())
{
    foreach (User user in repo.GetAll())
    {
        Console.WriteLine(user);
    }

    Console.ReadKey();
    Console.Clear();
    Console.WriteLine("\tGetAll()\n");

    foreach (User user in repo.GetAll(u => u.Fullname, false))
    {
        Console.WriteLine(user);
    }

    Console.ReadKey();
    Console.Clear();
    Console.WriteLine("\tGetRelatedData()\n");

    foreach (User user in repo.GetRelatedData())
    {
        Console.WriteLine(user);
        Console.WriteLine($"\t\tНаименование роли: {user.Role.Name}");
    }

    Console.ReadKey();
    Console.Clear();
    Console.WriteLine("\tGetDirectors()\n");

    User director = repo.GetOne(2);
    director.RoleId = 1;
    repo.Update(director);
    foreach (User user in repo.GetDirectors())
    {
        Console.WriteLine(user);
    }

    Console.ReadKey();
    Console.Clear();
    Console.WriteLine("\tSearch()\n");

    foreach (User user in repo.Search("Ан"))
    {
        Console.WriteLine(user);
    }
}

using (var context = new ManufactureContext())
{
    Initializer.ClearData(context);
}
//        }
//    }
//}
