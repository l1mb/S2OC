using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using static HabitCracker.Model.Entities.CoolerContext;

namespace HabitCracker.ViewModel
{
    internal class ChallengeViewModel : BaseViewModel
    {
        private ObservableCollection<Challenge> _allchallenges = new ObservableCollection<Challenge>();
        private ObservableCollection<Challenge> _personchallenges = new ObservableCollection<Challenge>();

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

            //var tempChallenge = SelectedChallenge;
            //tempChallenge.Challengers.Add(UserContext.GetInstance().UserPerson);

            SelectedChallenge.Challengers.Add(UserContext.GetInstance().UserPerson);
            var t = SelectedChallenge;
            t.Id = 0;
            PersonChallenges.Add(t);
            GetInstance().Challenges.FirstOrDefault(p=>p == SelectedChallenge)?.Challengers.Add(UserContext.GetInstance().UserPerson);
            GetInstance().Challenges.Add(SelectedChallenge);
            GetInstance().SaveChanges();
        });

        private ObservableCollection<Event> GetEvents()
        {
            ObservableCollection<Event> events = new();
            foreach (var var in GetInstance().Events.Where(p => p.Challenge == SelectedChallenge).ToList())
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

        public ChallengeViewModel()
        {
            AllChallenges = ChallengeContext.GetInstance().GetChallenges();
            PersonChallenges = new ObservableCollection<Challenge>(UserContext.GetInstance().UserPerson.Challenges);
        }
    }
}