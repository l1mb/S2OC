using System;
using System.Security.AccessControl;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using HabbitCracker.View.Menu;
using HabbitCracker.View.Menu.Challenge;
using static System.Configuration.ConfigurationManager;

namespace HabbitCracker.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private Page _databasePage;
        private Page _profilePage;
        private Page _habbitPage;

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
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Application.Current.Windows[0]?.Close();
            foreach (var VARIABLE in Application.Current.Windows)
            {
                if (VARIABLE.ToString() == "HabbitCracker.MainWindow")
                {
                    ((Window)VARIABLE).Close();
                    var suka = VARIABLE as Window;
                    suka.Close();
                }
            }
        });

        public RelayCommand SetProfilePageCommand => new RelayCommand(obj =>
            {
                CurrentPage = _profilePage;
            }
        );

        public RelayCommand SetHabbitPageCommand => new RelayCommand(obj =>
             {
                 CurrentPage = _habbitPage;
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
            _habbitPage = new View.Menu.Habbits();
            _challengePage = new Challenges();
            CurrentPage = _profilePage;
        }
    }
}