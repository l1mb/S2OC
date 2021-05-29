using HabitCracker.Model.Contexts;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

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