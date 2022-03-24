using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF.Entities.MetaData
{
    public class UserMetaData
    {
        [Display(Name = "Логин | Пароль")]//Сообщает MVC имя, подлежащее
                                          //использованию вместо имени свойства
        public string LoginPassword;
    }
}
