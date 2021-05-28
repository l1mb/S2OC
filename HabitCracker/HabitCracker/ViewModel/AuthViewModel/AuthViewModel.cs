using HabitCracker.View.AuthView;
using System.Windows.Controls;

namespace HabitCracker.ViewModel.Auth
{
    internal class AuthViewModel : BaseViewModel
    {
        private Page _currentPage;
        private readonly Page _signInPage;
        private readonly Page _signUpPage;

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public RelayCommand SetSignUpPage => new(obj =>
        {
            CurrentPage = _signUpPage;
        }
        );

        public RelayCommand SetSignInPage => new(obj =>
        {
            CurrentPage = _signInPage;
        });

        public AuthViewModel()
        {
            _signInPage = new SignIn();
            _signUpPage = new SignUp();
            _currentPage = _signUpPage;
        }
    }
}