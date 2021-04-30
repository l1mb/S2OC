using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HabbitCracker.ViewModel.Auth
{
    internal class SignViewModel : BaseViewModel
    {
        public RelayCommand SignUpButtonClickCommand => new RelayCommand(obj =>
            {
                Window window = new MainWindow();
                window.Show();
                if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();
            }
        );

        public RelayCommand SignInButtonClickCommand => new RelayCommand(obj =>
            {
            }
        );
    }
}