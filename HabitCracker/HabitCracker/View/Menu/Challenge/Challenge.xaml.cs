using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HabitCracker.Model.Contexts;

namespace HabitCracker.View.Menu.Challenge
{
    /// <summary>
    /// Логика взаимодействия для Challenges.xaml
    /// </summary>
    public partial class Challenges : Page
    {
        public Challenges()
        {
            InitializeComponent();
            EventButton.Visibility = UserContext.GetInstance().UserPerson.Role is "Пользователь" or "Cooler пользователь" ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainChallenge.Visibility = Visibility.Visible;
            EventGrid.Visibility = Visibility.Visible;
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Person.Visibility = Visibility.Visible;
            All.Visibility = Visibility.Collapsed;
        }

        private void GoBack_OnClick(object sender, RoutedEventArgs e)
        {
            All.Visibility = Visibility.Visible;
            Person.Visibility = Visibility.Collapsed;
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

    }
}