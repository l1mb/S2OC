using System;
using System.Security.AccessControl;
using System.Threading.Channels;
using System.Windows.Controls;
using static System.Configuration.ConfigurationManager;

namespace HabbitCracker.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private Page _databasePage;
        private Page _profilePage;

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

        public RelayCommand SetProfilePageCommand => new RelayCommand(obj =>
            {
                CurrentPage = _profilePage;
            }
        );

        public MainViewModel()
        {
            _databasePage = new View.DataBase();
            _profilePage = new View.Profile();
            CurrentPage = _profilePage;
        }
    }
}