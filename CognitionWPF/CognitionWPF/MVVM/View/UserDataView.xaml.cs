using CognitionWPF.CustomCommand;
using CognitionWPF.CustomCommand.RelayCommands;
using CognitionWPF.MVVM.Model;
using CognitionWPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CognitionWPF.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для UserDataView.xaml
    /// </summary>
    public partial class UserDataView : Page
    {
        /// <summary>
        /// Модель представления, хранящая все переменные
        /// для привязки к элементам управления
        /// </summary>
        public UserDataViewModel ViewModel { get; set; } = new UserDataViewModel();

        public UserDataView()
        {
            InitializeComponent();
        }
    }
}
