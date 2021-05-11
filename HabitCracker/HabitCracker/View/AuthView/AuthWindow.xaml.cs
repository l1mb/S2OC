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

namespace HabitCracker
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private bool flag = false;

        public AuthWindow()
        {
            InitializeComponent();
        }

        private void SignInClick(object sender, EventArgs eventArgs)
        {
            SwitchColors(SignUp, SignIn, ref flag);
        }

        private void SignUpClick(object sender, EventArgs eventArgs)
        {
            SwitchColors(SignIn, SignUp, ref flag);
        }

        private void SwitchColors(Button src, Button dest, ref bool flag)
        {
            var tempBackgroundBrush = dest.Background;
            var tempForegroundBrush = dest.Foreground;

            dest.Background = src.Background;
            dest.Foreground = src.Foreground;

            src.Background = tempBackgroundBrush;
            src.Foreground = tempForegroundBrush;
        }
    }
}