using CognitionWPF.BaseFunctions;
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

namespace CognitionWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new DepPropInAction());
            NavigationFrame.MainFrame = MainFrame;
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (NavigationFrame.MainFrame.CanGoBack)
                BackBtn.Visibility = Visibility.Visible;
            else
                BackBtn.Visibility = Visibility.Collapsed;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationFrame.MainFrame.GoBack();
        }

        #region Подтверждение выхода из приложения

        private void MainWin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string msg = "Вы действительно хотите закрыть приложение?";

            if (MessageBox.Show(msg, "Закрытие приложения",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region Определение позиции мыши на окне

        private void MainWin_MouseMove(object sender, MouseEventArgs e)
        {
            MousePosition.Text = $"Позиция мыши: {e.GetPosition(this).ToString()}";
        }

        #endregion

        #region Определение нажатия определенной клавиши

        private void MainWin_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressed.Text = $"Нажата кнопка: {e.Key.ToString()}";
        }

        #endregion
    }
}
