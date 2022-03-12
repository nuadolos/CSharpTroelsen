using System;
using System.Collections.Generic;
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

namespace CognitionWPF.CustomDependencyProp
{
    /// <summary>
    /// Логика взаимодействия для ShowNumberControl.xaml
    /// </summary>
    public partial class ShowNumberControl : UserControl
    {

        /// <summary>
        /// Свойство оболочки CLR, которое косвенно
        /// возвращает и устанавливает значение с использованием методов
        /// базового класса System.Windows.DependencyObject
        /// </summary>
        public int CurrentNumber
        {
            get { return (int)GetValue(CurrentNumberProperty); }
            set { SetValue(CurrentNumberProperty, value); }
        }

        /// <summary>
        /// Поле типа DependencyProperty, хранимое значение свойства CurrentNumber
        /// </summary>
        public static readonly DependencyProperty CurrentNumberProperty =
            //Метод регистрации объекта DependencyProperty с ссылкой на свойства CLR
            DependencyProperty.Register("CurrentNumber",
                //Тип данных
                typeof(int),
                //Класс, которому принадлежит данное свойство
                typeof(ShowNumberControl),
                //Установка стандартного значения и реагирования на изменение свойства
                new UIPropertyMetadata(100, 
                    new PropertyChangedCallback((depObj, args) =>
                    {
                        ShowNumberControl c = (ShowNumberControl)depObj;
                        Label label = c.numberDisplay;
                        label.Content = args.NewValue.ToString();
                    })),
                //Проверка допустимого значения свойства
                new ValidateValueCallback(value =>
                {
                    int intNumber = Convert.ToInt32(value);
                    return intNumber >= 0 && intNumber <= 500;
                }));


        public ShowNumberControl()
        {
            InitializeComponent();
        }
    }
}
