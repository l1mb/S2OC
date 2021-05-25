using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HabitCracker.View.AuthView
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void PasswordCheck_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password.Password != PasswordCheck.Password)
            {
                PasswordCheck.Background = Brushes.LightCoral;
            }
            else
                PasswordCheck.Background = Brushes.Transparent;
        }

        private void Login_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}