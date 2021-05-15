using System.Windows;
using System.Windows.Controls;

namespace HabitCracker.View.Menu
{
    /// <summary>
    /// Логика взаимодействия для Habits.xaml
    /// </summary>
    public partial class Habits : Page
    {
        public Habits()
        {
            InitializeComponent();
        }

        private void Selector_OnSelected(object sender, RoutedEventArgs e)
        {
            DisplayGrid.Visibility = Visibility.Visible;
            LetsAdd.Visibility = Visibility.Collapsed;
            SearchGrid.Visibility = Visibility.Visible;
        }
    }
}