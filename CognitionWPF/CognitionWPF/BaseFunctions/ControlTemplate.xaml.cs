using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace CognitionWPF.BaseFunctions
{
    /// <summary>
    /// Логика взаимодействия для ControlTemplate.xaml
    /// </summary>
    public partial class ControlTemplate : Page
    {
        private Control _ctrlToExamine = null;
        private string _dataToShow;

        public ControlTemplate()
        {
            InitializeComponent();
        }

        #region BitLogResources

        private void LoadImageSource()
        {
            //Нахождение фотографиЙ через относительный URI
            BitmapImage image1 = new BitmapImage(new Uri("/TestImages/Y4GOPzG14Jk.jpg", UriKind.Relative));
            Aboltus1.Source = image1;

            BitmapImage image2 = new BitmapImage(new Uri("/TestImages/eiDHLrlmY8s.jpg", UriKind.Relative));
            Aboltus2.Source = image2;

            BitmapImage image3 = new BitmapImage(new Uri("/TestImages/radost'8.jpg", UriKind.Relative));
            Aboltus3.Source = image3;
        }

        #region Dynamic/Static Resources

        /// <summary>
        /// Изменение градиента ресурса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeColorBtn_Click(object sender, RoutedEventArgs e)
        {
            //Меняет у каждого Border цвет, так как изменение происходит через ссылку
            var resource = (RadialGradientBrush)Resources["GradientBrushBorder"];
            resource.GradientStops[1] = new GradientStop(Colors.Red, 0);
        }

        /// <summary>
        /// Полное обновление градиента с помощью создания нового объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackColorBtn_Click(object sender, RoutedEventArgs e)
        {
            RadialGradientBrush gradient = new RadialGradientBrush();

            gradient.GradientStops.Add(new GradientStop(Colors.Firebrick, 1));
            gradient.GradientStops.Add(new GradientStop(Colors.Gold, 0.4));
            gradient.GradientStops.Add(new GradientStop(Colors.Navy, 0.6));
            gradient.GradientStops.Add(new GradientStop(Colors.Coral, 1));

            //Так как происходит привязка к новой ссылке, обновляется только динамический ресурс
            Resources["GradientBrushBorder"] = gradient;
        }

        #endregion

        #endregion

        #region CustomTemplate(DynamicStyle)

        /// <summary>
        /// Применение доступного стиля для кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StylesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string valueStyle = (StylesCombo.SelectedItem as ComboBoxItem)?.Content.ToString();
            var currentStyle = (Style)TryFindResource(valueStyle);
            StyleBtn.Style = currentStyle;
        }

        #endregion

        #region StandardControl

        /// <summary>
        /// Отображение полученной разметки XAML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemplateBtn_Click(object sender, RoutedEventArgs e)
        {
            _dataToShow = string.Empty;
            ShowTemplate();
            ResultTemplateTBox.Text = _dataToShow;
        }

        /// <summary>
        /// Получение разметки XAML стандартного шаблона введенного элемента управления
        /// </summary>
        private void ShowTemplate()
        {
            if (_ctrlToExamine != null)
                stackTemplatePanel.Children.Remove(_ctrlToExamine);

            try
            {
                //Загрузка сборки "PresentationFramework"
                Assembly asm = Assembly.Load("PresentationFramework," +
                    " Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");

                //Нахождение указанного элемента управления и изменение его свойств
                _ctrlToExamine = (Control)asm.CreateInstance(FullNameControlTBox.Text);
                _ctrlToExamine.Height = 200;
                _ctrlToExamine.Width = 200;
                _ctrlToExamine.Margin = new Thickness(5);

                //Добавление элемента управления на экран
                stackTemplatePanel.Children.Add(_ctrlToExamine);

                //Определение настройки XML для предохранения отступов
                var xmlSettings = new XmlWriterSettings { Indent = true };

                //Хранение разметки XAML
                var strBuilder = new StringBuilder();

                //Создание XmlWriter с учетом настроек
                var xWriter = XmlWriter.Create(strBuilder, xmlSettings);

                //Сохранение разметки XAML в объекте xWriter на основе ControlTemplate
                XamlWriter.Save(_ctrlToExamine.Template, xWriter);

                //Подготовленная разметка XAML свойтсва Template указанного элемента управления
                _dataToShow = strBuilder.ToString();
            }
            catch (Exception ex)
            {
                _dataToShow = ex.Message;
            }
        }

        #endregion

        #region Animation

        /// <summary>
        /// Выполнение анимации при загрузке страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimationPage_Loaded(object sender, RoutedEventArgs e)
        {
            #region CatAnimated

            //Бесконечная анимация вращения EllipseCatAnimation
            DoubleAnimation dblAnimRotateCat = new DoubleAnimation();
            dblAnimRotateCat.From = 0;
            dblAnimRotateCat.To = 360;
            dblAnimRotateCat.Duration = new Duration(TimeSpan.FromSeconds(4));
            dblAnimRotateCat.RepeatBehavior = RepeatBehavior.Forever;
            EllipseCatAnimation.RenderTransform = new RotateTransform();
            EllipseCatAnimation.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, dblAnimRotateCat);

            //Бесконечная анимация EllipseCatAnimation, при которой меняется видимость элемента
            DoubleAnimation dblAnimOpacity = new DoubleAnimation();
            dblAnimOpacity.From = 1;
            dblAnimOpacity.To = 0.5;
            dblAnimOpacity.Duration = new Duration(TimeSpan.FromSeconds(3));
            dblAnimOpacity.AutoReverse = true;
            dblAnimOpacity.RepeatBehavior = RepeatBehavior.Forever;
            EllipseCatAnimation.BeginAnimation(Ellipse.OpacityProperty, dblAnimOpacity);

            #endregion

            #region GirlAnimated

            //Анимация вращения изображения EllipseGirlAnimation, повторяющаяся 10 раз
            DoubleAnimation dblAnimRotateGirl = new DoubleAnimation();
            dblAnimRotateGirl.From = 0;
            dblAnimRotateGirl.To = 360;
            dblAnimRotateGirl.Duration = new Duration(TimeSpan.FromSeconds(4));
            dblAnimRotateGirl.RepeatBehavior = new RepeatBehavior(10);
            EllipseGirlAnimation.RenderTransform = new RotateTransform();
            EllipseGirlAnimation.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, dblAnimRotateGirl);

            #endregion

            #region AynamiAnimated

            //Анимация вращения изображения EllipseAynamiAnimation, повторяющаяся 30 секунд
            DoubleAnimation dblAnimRotateAynami = new DoubleAnimation();
            dblAnimRotateAynami.From = 0;
            dblAnimRotateAynami.To = 360;
            dblAnimRotateAynami.Duration = new Duration(TimeSpan.FromSeconds(4));
            dblAnimRotateAynami.RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(30));
            EllipseAynamiAnimation.RenderTransform = new RotateTransform();
            EllipseAynamiAnimation.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, dblAnimRotateAynami);

            #endregion
        }

        /// <summary>
        /// Метод представлен лишь в качестве примера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimationPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //ThicknessAnimation thicknessDogAnim = new ThicknessAnimation();

            //thicknessDogAnim.From = new Thickness(0, 0, 0, 5);
            //thicknessDogAnim.To = new Thickness(this.ActualWidth - 100, 0, 0, 5);
            //thicknessDogAnim.Duration = new Duration(TimeSpan.FromSeconds(5));
            //thicknessDogAnim.RepeatBehavior = RepeatBehavior.Forever;
            //thicknessDogAnim.AutoReverse = true;
            //RectangleDogAnimation.BeginAnimation(Border.MarginProperty, thicknessDogAnim);
        }

        #endregion
    }
}
