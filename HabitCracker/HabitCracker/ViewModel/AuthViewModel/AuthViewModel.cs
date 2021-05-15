﻿using HabitCracker.View.AuthView;
using System.Windows.Controls;

namespace HabitCracker.ViewModel.Auth
{
    internal class AuthViewModel : BaseViewModel
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

        public RelayCommand SignUp => new RelayCommand(obj =>
         {
             //SqlCommand sql = new SqlCommand($"insert into Auth values ({})");
         }
        );

        public AuthViewModel()
        {
            _SignInPage = new SignIn();
            _SignUpPage = new SignUp();
            _currentPage = _SignUpPage;
        }
    }
}