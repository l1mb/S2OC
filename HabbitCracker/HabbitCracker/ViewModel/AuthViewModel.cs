using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HabbitCracker.ViewModel
{
    class AuthViewModel:BaseViewModel
    {
        private Page _currentPage;
        private Page _SignInPage
            ;
        private Page _SignUpPage;

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public RelayCommand SetSignUpPage => new RelayCommand(obj =>
        {
            CurrentPage = _SignUpPage;
        }
        );
        
        public RelayCommand SetSignInPage => new RelayCommand(obj =>
        {
            //CurrentPage = _databasePage;
            CurrentPage = _SignInPage;
        }
        );

        public AuthViewModel()
        {
            _SignInPage = new View.Auth.SignIn();
            _SignUpPage = new View.Auth.SignUp();
            _currentPage = _SignUpPage;

        }

        
    }
}
