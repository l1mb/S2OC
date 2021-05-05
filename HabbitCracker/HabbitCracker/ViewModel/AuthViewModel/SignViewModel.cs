using System.Windows;
using HabbitCracker.Model.Entities;

namespace HabbitCracker.ViewModel.AuthViewModel
{
    internal class SignViewModel : BaseViewModel
    {
        public Model.Entities.Auth SAuth = new();
        public Model.Entities.Person SPerson = new();

        public Model.Entities.Auth SignAuth { get; set; }
        public Model.Entities.Person SignPerson { get; set; }

        public RelayCommand SignUpButtonClickCommand => new RelayCommand(obj =>
            {
                CourseworkDbContext.GetInstance().Auths.Add(SignAuth);
                Window window = new MainWindow();
                window.Show();

                if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();
            }
        );

        public RelayCommand SignInButtonClickCommand => new RelayCommand(obj => { }
        );
    }
}