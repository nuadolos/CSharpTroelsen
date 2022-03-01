using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.EntityFramework.Model
{
    public partial class ManufactureEntities
    {
        private static ManufactureEntities _context;
        public static ManufactureEntities Context
        {
            get
            {
                if (_context == null)
                    _context = new ManufactureEntities();

                return _context;
            }
        }
    }
}
