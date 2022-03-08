using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ManufactureEFExample.Repos;

namespace CognitionWPF.BaseFunctions
{
    /// <summary>
    /// Логика взаимодействия для ControlsAndAPI.xaml
    /// </summary>
    public partial class ControlsAndAPI : Page
    {
        public ControlsAndAPI()
        {
            InitializeComponent();

            InkRadio.IsChecked = true;
            ColorsCombo.SelectedIndex = 0;
            DrawingCanvas.EditingMode = InkCanvasEditingMode.Ink;

            SetBindings();

            ConfigureGrid();
        }

        #region Ink API

        #region Изменения свойств DrawingCanvas

        /// <summary>
        /// Изменение режима редактирования через RadioButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButtonClicked(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton)?.Content.ToString())
            {
                case "Ink Mode":
                    DrawingCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "Erase Mode":
                    DrawingCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
                case "Select Mode":
                    DrawingCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
            }
        }

        /// <summary>
        /// Изменение цвета пера через ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorChanged(object sender, SelectionChangedEventArgs e)
        {
            string colorToUse = (ColorsCombo.SelectedItem as StackPanel).Tag.ToString();
            DrawingCanvas.DefaultDrawingAttributes.Color = (Color)ColorConverter.ConvertFromString(colorToUse);
        }

        #endregion

        #region Загрузка и сохранение файла с расширением .bin

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            using (Stream fs = new FileStream("StrokeData.bin", FileMode.Create))
            {
                DrawingCanvas.Strokes.Save(fs);
            }
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            using (Stream fs = new FileStream("StrokeData.bin", FileMode.Open, FileAccess.Read))
            {
                StrokeCollection strokes = new StrokeCollection(fs);
                DrawingCanvas.Strokes = strokes;
            }
        }

        #endregion

        #region Очистка поля для рисования

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Strokes.Clear();
        }

        #endregion

        #endregion

        #region Data Binding

        private void SetBindings()
        {
            Binding msgBtnBinding = new Binding();
            msgBtnBinding.Converter = new Converters.DoubleConverter();
            msgBtnBinding.Source = TestScrollBar;
            msgBtnBinding.Path = new PropertyPath("Value");
            MsgBtn.SetBinding(Button.ContentProperty, msgBtnBinding);
        }

        private void MsgBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Значение прокрутки: {(sender as Button)?.Content.ToString()}");
        }

        #endregion

        #region Data Grid

        private void ConfigureGrid()
        {
            using (var repo = new UserRepo())
            {
                UsersData.ItemsSource = repo.GetAll().Select(u => new { u.Id, u.Login, u.Fullname, u.Role.Name});
            }
        }

        #endregion
    }
}
