using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF.Entities.MetaData
{
    public class UserMetaData
    {
        [Display(Name = "Логин | Пароль")]
        public string LoginPassword;
    }
}
