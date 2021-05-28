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
        private bool openGates;
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
            {
                if ((sender is TextBox tBox)&&!string.IsNullOrWhiteSpace(tBox.Text)&&(Password.Password!=""&& PasswordCheck.Password!=""))
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