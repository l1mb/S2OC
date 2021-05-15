using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Ghbrjk");
        }
    }
}