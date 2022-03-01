using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Collection
{
    internal class SortByCoffeeName : IComparer<Order>
    {
        public int Compare(Order x, Order y) => x.CoffeeName.CompareTo(y.CoffeeName);
    }
}
