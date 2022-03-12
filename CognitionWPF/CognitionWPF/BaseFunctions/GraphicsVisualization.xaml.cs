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

namespace CognitionWPF.BaseFunctions
{
    /// <summary>
    /// Логика взаимодействия для GraphicsVisualization.xaml
    /// </summary>
    public partial class GraphicsVisualization : Page
    {
        public GraphicsVisualization()
        {
            InitializeComponent();
        }

        #region Shape

        private string _clickedShape = nameof(EllipseOpt);

        /// <summary>
        /// Выбор создаваемой фигуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShapeChanged(object sender, RoutedEventArgs e)
        {
            RadioButton opt = sender as RadioButton;

            _clickedShape = opt?.Name;
        }

        /// <summary>
        /// При нажатии на CanvasDrawingArea создается элемент, наследующий класс Shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasDrawingArea_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Shape shapeToRender = null;

            //Создание нового элемента управления, наследующий класс Shape
            switch (_clickedShape)
            {
                case nameof(EllipseOpt):
                    {
                        RadialGradientBrush brush = new RadialGradientBrush();
                        brush.GradientStops.Add(new GradientStop(
                            (Color)ColorConverter.ConvertFromString("#FF77F177"), 0));
                        brush.GradientStops.Add(new GradientStop(
                            (Color)ColorConverter.ConvertFromString("#FF11E611"), 1));
                        brush.GradientStops.Add(new GradientStop(
                            (Color)ColorConverter.ConvertFromString("#FF5A8E5A"), 0.545));

                        shapeToRender = new Ellipse() { Fill = brush, Width = 35, Height = 35 };
                        break;
                    }
                case nameof(RectangleOpt):
                    shapeToRender = new Rectangle() { Fill = Brushes.Red, Width = 35, Height = 35 };
                    break;
                case nameof(LineOpt):
                    shapeToRender = new Line()
                    {
                        Stroke = Brushes.Blue,
                        StrokeThickness = 10,
                        X1 = 0,
                        X2 = 50,
                        Y1 = 0,
                        Y2 = 50,
                        StrokeStartLineCap = PenLineCap.Triangle,
                        StrokeEndLineCap = PenLineCap.Round
                    };
                    break;
            }

            //Установка позиции элемента относительно указателя мышки
            Canvas.SetLeft(shapeToRender, e.GetPosition(CanvasDrawingArea).X);
            Canvas.SetTop(shapeToRender, e.GetPosition(CanvasDrawingArea).Y);

            //Добавление в контейнер выбранную фигуру
            CanvasDrawingArea.Children.Add(shapeToRender);
        }

        /// <summary>
        /// Удаление фигуры, на которую наведен курсор мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasDrawingArea_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Выявление позиции курсора мыши на элементе управления CanvasDrawingArea
            Point positionShape = e.GetPosition(CanvasDrawingArea);

            //Выяснение, был ли произведен щелчек по элементу внутри CanvasDrawingArea
            HitTestResult result = VisualTreeHelper.HitTest(CanvasDrawingArea, positionShape);

            //Если был произведен, то фигура, на которую щелкнули, удаляется
            if (result != null)
                CanvasDrawingArea.Children.Remove(result.VisualHit as Shape);
        }

        /// <summary>
        /// Поворот CanvasDrawingArea под случайный градус
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RotateCanvasBtn_Click(object sender, RoutedEventArgs e)
        {
            Random rndm = new Random();
            CanvasDrawingArea.LayoutTransform = null;
            
            RotateTransform rotateCanvas = new RotateTransform(rndm.Next(0, 360));
            CanvasDrawingArea.LayoutTransform = rotateCanvas;
        }

        #endregion

        #region Visual

        /// <summary>
        /// Создание битового изображения и помещение его в VisualNewImage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            int textFontSize = 24;

            //Массив отрисованных букв
            FormattedText[] captchaTexts = new FormattedText[5];
            
            for (int i = 0; i < captchaTexts.Length; i++)
            {
                captchaTexts[i] = new FormattedText("y",
                    new System.Globalization.CultureInfo("en-us"),
                    FlowDirection.LeftToRight,
                    new Typeface(this.FontFamily, FontStyles.Italic, FontWeights.DemiBold, FontStretches.UltraExpanded),
                    textFontSize,
                    Brushes.Green,
                    null,
                    VisualTreeHelper.GetDpi(this).PixelsPerDip);
            }

            //Визуальный объект для хранения отрисованных данных
            DrawingVisual drawingVisual = new DrawingVisual();

            //Отрисовка данных
            using(DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                //Задний фон в виде прямоугольника
                drawingContext.DrawRoundedRectangle(Brushes.Honeydew,
                    new Pen(Brushes.Black, 2),
                    new Rect(5, 5, 300, 50), 0, 0);

                Random random = new Random();
                double x = 10;

                //Произвести поворот и рисование каждой буквы
                foreach (FormattedText text in captchaTexts)
                {
                    drawingContext.PushTransform(new RotateTransform(random.Next(-10, 10)));
                    drawingContext.DrawText(text, new Point(x, 20));

                    x += 10;
                }
            }

            //Динамическое создание битового изображения,
            //используя данные в объекте DrawingVisual
            RenderTargetBitmap bmp = new RenderTargetBitmap(500, 100, 100, 90, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            VisualNewImage.Source = bmp;
        }

        /// <summary>
        /// Наклонение объектов DrawingVisual при нажатии на них
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomVisualFrameworkElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point positionMouse = e.GetPosition(this);

            VisualTreeHelper.HitTest(this, null,
                new HitTestResultCallback(result =>
                {
                    if (result.VisualHit is DrawingVisual res)
                    {
                        if (res.Transform == null)
                            res.Transform = new SkewTransform(7, 7);
                        else
                            res.Transform = null;
                    }

                    return HitTestResultBehavior.Stop;
                }),
                new PointHitTestParameters(positionMouse));
        }

        #endregion
    }
}
