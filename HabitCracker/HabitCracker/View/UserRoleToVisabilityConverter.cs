using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using HabitCracker.Model.Contexts;

namespace HabitCracker.View
{
    [ValueConversion(typeof(DateTime), typeof(Visibility))]
    public class UserRoleToVisabilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return UserContext.GetInstance().UserPerson.Role == "Пользователь"
                ? Visibility.Collapsed
                : Visibility.Visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
