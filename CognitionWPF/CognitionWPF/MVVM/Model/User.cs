using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitionWPF.MVVM.Model
{
    /// <summary>
    /// Наблюдаемый класс User,
    /// неявно реализующий событие PropertyChanged
    /// с помощью инструмента PropertyChanged.Fody
    /// </summary>
    public partial class User : INotifyPropertyChanged
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password меньше 8 символов")]
        public string Password { get; set; }
        [StringLength(50)]
        public string Fullname { get; set; }
        public bool IsChanged { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
