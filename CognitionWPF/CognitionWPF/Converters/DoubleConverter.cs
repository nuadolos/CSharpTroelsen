using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CognitionWPF.Converters
{
    /// <summary>
    /// Пользовательский конвертор для собственных целей
    /// </summary>
    public class DoubleConverter : IValueConverter
    {
        /// <summary>
        /// Передача значения от источника к цели
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //К цели возвращается целое число после конвертации
            double d = (double)value;
            return (int)d;
        }

        /// <summary>
        /// Передача значения от цели к источнику
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //К источнику возвращается тоже самое значение, что и вначале
            return value;
        }
    }
}
