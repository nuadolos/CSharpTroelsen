using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLibrary
{
    public class Car
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        public string Type { get; set; }

        public Car() : this("СборкаПомойки", 20, "Танк")
        { }

        public Car(string name, int speed, string type)
        {
            Name = name;
            Speed = speed;
            Type = type;
        }

        public void SpeedUp(int speed)
        {
            Speed += speed;
            Console.WriteLine($"Скорость: {Speed}");
        }

        public void SpeedDown(int speed)
        {
            Speed -= speed;
            Console.WriteLine($"Скорость: {Speed}");
        }
    }
}
