using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF.Entities.Base
{
    public class EntityBase
    {
        /// <summary>
        /// Идентификатор для каждой таблицы
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Проверка параллелизма. Добавляет версию строки,
        /// чтобы пользователя не могли изменять одну и ту же запись.
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
