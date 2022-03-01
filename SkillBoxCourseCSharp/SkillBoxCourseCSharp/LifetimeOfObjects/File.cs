using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.LifetimeOfObjects
{
    //Выявление атрибутов TestAttribute во время выполнения с помощью рефлексии
    [Attribute.TestAttribute(AssemblyName = "SkillBoxCourseCSharp.LifetimeOfObjects",
            Description = "Данный метод ExampleCustomAttribute предоставляет понять пользу атрибутов")]
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }

        public File(string name, DateTime dateAdded, string type, long size)
        {
            Name = name;
            DateAdded = dateAdded;
            Type = type;
            Size = size;
        }

        public override string ToString() => $"[ Name: {Name}, DateAdded: {DateAdded}, Type: {Type}, Size: {Size}";
    }
}
