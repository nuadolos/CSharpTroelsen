using ManufactureEF.Entities.MetaData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF.Entities
{
    [ModelMetadataType(typeof(UserMetaData))]
    public partial class User
    {
        [NotMapped]
        public string LoginPassword { get => $"{Login} {Password}"; }

        public override string ToString() => $"Логин: {Login}\tПароль: {Password}\tФИО: {Fullname}\tРоль: {RoleId}";
    }
}
