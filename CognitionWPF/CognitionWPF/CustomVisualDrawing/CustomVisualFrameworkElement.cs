using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CognitionWPF.CustomVisualDrawing
{
    public class CustomVisualFrameworkElement : FrameworkElement
    {
        /// <summary>
        /// Коллекция всех визуальных объектов.
        /// </summary>
        private VisualCollection visuals;

        /// <summary>
        /// Возвращает количество визуальных элементов внутри коллекции
        /// </summary>
        protected override int VisualChildrenCount => visuals.Count;
        /// <summary>
        /// Возвращает визуальный элемент по указанному индексу
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= visuals.Count)
                return null;

            return visuals[index];
        }

        /// <summary>
        /// Заполнение коллекции объектами DrawingVisual.
        /// Аргумент конструктора представляет владельца визуальных объектов.
        /// </summary>
        public CustomVisualFrameworkElement()
        {
            visuals = new VisualCollection(this) { AddCircle(), AddRect() };
        }

        /// <summary>
        /// Возвращает объект DrawingVisual, содержащий нарисованый круг
        /// </summary>
        /// <returns></returns>
        private Visual AddCircle()
        {
            DrawingVisual drawingVisual = new DrawingVisual();

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawEllipse(Brushes.Blue,
                    new Pen(Brushes.Green, 3),
                    new Point(10, 0), 50, 50);
            }

            return drawingVisual;
        }

        /// <summary>
        /// Возвращает объект DrawingVisual, содержащий нарисованый прямоугольник
        /// </summary>
        /// <returns></returns>
        private Visual AddRect()
        {
            DrawingVisual drawingVisual = new DrawingVisual();

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(Brushes.Red,
                    new Pen(Brushes.GreenYellow, 4),
                    new Rect(50, 20, 100, 50));
            }

            return drawingVisual;
        }
    }
}
