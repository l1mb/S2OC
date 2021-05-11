using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using HabitCracker.View.Menu;
using HabitCracker.View.Menu.Challenge;
using static System.Configuration.ConfigurationManager;

namespace HabitCracker.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private Page _databasePage;
        private Page _profilePage;
        private Page _HabitPage;

        private Page _challengePage;

        private Page _currentPage;

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public RelayCommand SetDatabasePageCommand => new RelayCommand(obj =>
            {
                CurrentPage = _databasePage;
            }
        );

        public RelayCommand Logout => new RelayCommand(obj =>
        {
            UserContext.GetInstance().UserPerson = new Person();

            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Application.Current.Windows[0]?.Close();
        });

        public RelayCommand SetProfilePageCommand => new RelayCommand(obj =>
            {
                CurrentPage = _profilePage;
            }
        );

        public RelayCommand SetHabitPageCommand => new RelayCommand(obj =>
             {
                 CurrentPage = _HabitPage;
             }
        );

        public RelayCommand SetChallengesPageCommand => new RelayCommand(obj =>
        {
            CurrentPage = _challengePage;
        });

        public MainViewModel()
        {
            _databasePage = new View.Menu.DataBase();
            _profilePage = new View.Menu.Profile();
            _HabitPage = new View.Menu.Habits();
            _challengePage = new Challenges();
            CurrentPage = _profilePage;
        }
    }
}