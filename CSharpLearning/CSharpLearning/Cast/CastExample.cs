using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillBoxCourseCSharp.Aggregation;

namespace SkillBoxCourseCSharp.Cast
{
    internal class CastExample
    {
        /*Чтобы не получить ошибку во время выполнения, которая указывает,
              что в ходе приведения объект возвращает null,
              необходимо провести проверку:
                var tempItem = AttachedProdLV.SelectedItem as AttachedProduct;

                if (tempItem != null)
                    ...

              Но если мы уверены, что приведение вернет ожидаемое значение ключевое слово "is" можно исключить.
        */
        static void Cast(string[] args)
        {
            object[] allTypes = new object[]
            {
            new Manager() { CreditCard = "1004", Age = 14, FullName = "Леонид"},
            new SalesPerson() { Login = "Bez20", Password = "chupapi", Age = 15, FullName = "Папик"},
            new object(),
            new Employee(),
            new Employee(),
            null
            };

            foreach (object item in allTypes)
            {
                if (item is Manager)
                {
                    Console.WriteLine("Object is Manager:\n" + ((Manager)item).FullName + ((Manager)item).CreditCard);
                }
                //Без двойного приведения
                if (item is SalesPerson sale)
                {
                    Console.WriteLine("Object is SalesPerson:\n" + sale.FullName + sale.Login + sale.Password);
                }
                //Отбрасование применяется при ненужности в использовании данной переменной
                if (item is object _)
                    Console.WriteLine("Object is object");
            }

            object oneType = new Manager { Age = 14, FullName = "Aboba" };

            switch (oneType)
            {
                case Manager man when man.Age > 12:
                    Console.WriteLine("Крутой");
                    break;
                case SalesPerson sale:
                    Console.WriteLine("Мужчина " + sale.FullName);
                    break;
                case object _:
                    Console.WriteLine("Переменную потеряли...");
                    break;
            }
        }
    }
}
