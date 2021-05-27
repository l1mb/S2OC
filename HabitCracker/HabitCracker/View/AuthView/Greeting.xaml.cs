using System.Windows;

namespace HabitCracker.View.AuthView
{
    /// <summary>
    /// Логика взаимодействия для Greeting.xaml
    /// </summary>
    public partial class Greeting : Window
    {
        public Greeting()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}