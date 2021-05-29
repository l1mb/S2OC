using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HabitCracker.View
{
    [ValueConversion(typeof(DateTime), typeof(Brush))]
    public class DateTimeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = (DateTime)value;
            if (dateTime.Date < DateTime.Now)
                return true;
            if (dateTime.Date > DateTime.Now)
                return false;

            //return dateTime.Date == F

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}