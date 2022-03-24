using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF.Entities
{
    [MetadataType(typeof(MetaData.UserMetaData))]
    public partial class User
    {
        [NotMapped]//Сообщает инфраструктуре EF о том,
                   //что поле явл. свойством, относящимся только к .NET
        public string LoginPassword { get => $"{Login} | {Password}"; }
        public override string ToString() => $"Id = {Id}, Login: {Login}, Password: {Password}, Fullname: {Fullname}, Role: {Role.Name}";
    }
}
