using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Interface
{
    interface ISupplier
    {
        string SupName { get; set; }
        DateTime DateShipment { get; set; }
        string Description { get; set; }

        void PrintDataSupplier();
        void StopDelivery();
    }
}
