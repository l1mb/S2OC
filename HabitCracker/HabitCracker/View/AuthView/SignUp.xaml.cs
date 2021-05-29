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

        public void PasswordCheck_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password.Password != PasswordCheck.Password)
            {
                PasswordCheck.Background = Brushes.LightCoral;
            }
            else
            {
                if ((!string.IsNullOrWhiteSpace(TextBox.Text)) && (Password.Password != "" && PasswordCheck.Password != ""))
                {
                    Gates.IsEnabled = true;
                }
                else
                {
                    Gates.IsEnabled = false;
                }
                PasswordCheck.Background = Brushes.Transparent;
            }
        }
    }
}