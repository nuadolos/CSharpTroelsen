using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Collection
{
    internal class Order
    {
        public int Id { get; set; }
        public string CoffeeName { get; set; }
        public string ClientName { get; set; }
        public double Volume { get; set; }
        public double Price { get; set; }
         
        public Order() : this (0, "Латте", "Сергей", 0.5, 400)
        { }

        public Order(int id, string clName, string cfName, double volume, double price)
        {
            Id = id;
            CoffeeName = cfName;
            ClientName = clName;
            Volume = volume;
            Price = price * volume;
        }

        public override string ToString() => $"Заказ {Id}: Имя клиента: {ClientName}\n\tКофе: {CoffeeName}, Объем: {Volume}, Цена: {Price}\n\n";
    }
}
