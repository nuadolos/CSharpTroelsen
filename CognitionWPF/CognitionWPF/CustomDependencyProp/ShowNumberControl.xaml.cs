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
        //private int _currentNumber = 0;

        //public int CurrentNumber
        //{
        //    get => _currentNumber;
        //    set
        //    {
        //        _currentNumber = value;
        //        numberDisplay.Content = CurrentNumber.ToString();
        //    }
        //}

        public int CurrentNumber
        {
            get { return (int)GetValue(CurrentNumberProperty); }
            set { SetValue(CurrentNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentNumberProperty =
            DependencyProperty.Register("CurrentNumber",
                typeof(int),
                typeof(ShowNumberControl),
                new UIPropertyMetadata(100, 
                    new PropertyChangedCallback((depObj, args) =>
                    {
                        ShowNumberControl c = (ShowNumberControl)depObj;
                        Label label = c.numberDisplay;
                        label.Content = args.NewValue.ToString();
                    })),
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
