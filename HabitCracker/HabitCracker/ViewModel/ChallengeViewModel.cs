using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using HabitCracker.View.Menu.Challenge;
using static HabitCracker.Model.Entities.CourseworkDbContext;

namespace HabitCracker.ViewModel
{
    internal class ChallengeViewModel : BaseViewModel
    {
        private ObservableCollection<Challenge> _allchallenges = new ObservableCollection<Challenge>();
        private ObservableCollection<Challenge> _personchallenges = new ObservableCollection<Challenge>();

        private Page _allChallengesPage;
        private Page _personChallengesPage;

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

        private ObservableCollection<Event> _events = new ObservableCollection<Event>();

        private Challenge _selectedChallenge = new Challenge();
        private Event _selectedEvent = new Event();

        public RelayCommand AddSelectedToPersonChallenge => new RelayCommand(obj =>
        {
            if (SelectedChallenge == null) return;
            var tempChallenge = SelectedChallenge;
            tempChallenge.Challengerid = UserContext.GetInstance().UserPerson.Id;

            SelectedChallenge.Challengerid = UserContext.GetInstance().UserPerson.Id;
            PersonChallenges.Add(SelectedChallenge);

            CourseworkDbContext.GetInstance().Challenges.Add(SelectedChallenge);
            CourseworkDbContext.GetInstance().SaveChanges();
        });

        private ObservableCollection<Event> GetEvents()
        {
            ObservableCollection<Event> events = new();
            foreach (var var in Model.Entities.CourseworkDbContext.GetInstance().Events.Where(p => p.Challengeid == SelectedChallenge.Challengeid).ToList())
            {
                events.Add(var);
            }

            return events;
        }

        public ObservableCollection<Event> Events
        {
            get => _events;
            set
            {
                _events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        public Challenge SelectedChallenge
        {
            get => _selectedChallenge;
            set
            {
                _selectedChallenge = value;
                OnPropertyChanged(nameof(SelectedChallenge));
                Events = GetEvents();
            }
        }

        public Event SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                _selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));
            }
        }

        public ObservableCollection<Challenge> AllChallenges
        {
            get => _allchallenges;
            set
            {
                _allchallenges = value;
                OnPropertyChanged(nameof(AllChallenges));
            }
        }

        public ObservableCollection<Challenge> PersonChallenges
        {
            get => _personchallenges;
            set
            {
                _personchallenges = value;
                OnPropertyChanged(nameof(PersonChallenges));
            }
        }

        public RelayCommand SetPersonPage => new RelayCommand(obj =>
        {
            CurrentPage = _personChallengesPage;
        });

        public RelayCommand SetAllPage => new RelayCommand(obj =>
         {
             CurrentPage = _allChallengesPage;
         });

        public ChallengeViewModel()
        {
            AllChallenges = ChallengeContext.GetInstance().getChallenges();
            PersonChallenges =
                new ObservableCollection<Challenge>(AllChallenges.Where(p =>
                    p.Challengerid == UserContext.GetInstance().UserPerson.Id));
        }
    }
}