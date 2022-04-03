using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.IO
{
    [Serializable]
    public class Home
    {
        [NonSerialized]
        private bool isOwn;

        public string Name { get; set; }
        public string Owner { get; set; }
        public string Address { get; set; }
        public int Size { get; set; }
        public bool IsOwn { get => isOwn; set => isOwn = value; }

        public Home() : this("Камень", "Президент", "Москва", 8, false)
        { }

        public Home(string name, string owner, string address, int size, bool isOwn)
        {
            Name = name;
            Owner = owner;
            Address = address;
            Size = size;
            IsOwn = isOwn;
        }

        public void Sale(string newOwner)
        {
            Owner = newOwner;
        }

        public override string ToString() => $"Name: {Name}; Owner: {Owner}; Address: {Address}; Size: {Size}; IsOwn: {IsOwn}";
    }
}
