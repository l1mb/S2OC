using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HabbitCracker
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private bool active;
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void SignInClick(object sender, EventArgs eventArgs)
        {
            SwitchColors(SignUp, SignIn);
        }

        private void SignUpClick(object sender, EventArgs eventArgs)
        {
            SwitchColors(SignIn, SignUp);
        }

        private void SwitchColors(Button src, Button dest)
        {
            var tempBackgroundBrush = dest.Background;
            var tempForegroundBrush = dest.Foreground;

            dest.Background = src.Background;
            dest.Foreground = src.Foreground;

            src.Background = tempBackgroundBrush;
            src.Foreground = tempForegroundBrush;
        }

        private void SignUpClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
