using System;
using System.Collections.Generic;
using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.TextFormatting;
using HabitCracker.View.Menu.Challenge;
using MaterialDesignThemes.Wpf;
using static HabitCracker.Model.Entities.CoolerContext;

namespace HabitCracker.ViewModel
{
    internal class ChallengeViewModel : BaseViewModel
    {
        private ObservableCollection<Challenge> _allchallenges = new ObservableCollection<Challenge>();
        private ObservableCollection<Challenge> _personchallenges = new ObservableCollection<Challenge>();

        private Page _currentPage;

        private string _eventName = "";

        public List<string> AllChallengesHeadersList
        {
            get
            {
                var lsit = new List<string>();
                foreach (var s in AllChallenges.Select(p => p.Title))
                {
                    lsit.Add(s);
                }

                return lsit;
            }
        }


        public String EventName
        {
            get => _eventName;
            set
            {
                _eventName = value;
                OnPropertyChanged(nameof(EventName));
            }
        }

        private DateTime? _eventDate;

        public DateTime? EventDateTime
        {
            get => _eventDate;
            set
            {
                _eventDate = value;
                OnPropertyChanged(nameof(EventDateTime));
            }
        }

        public RelayCommand AddEvent => new RelayCommand(obj =>
        {
            var t = new Event();
            t.EventName = EventName;
            t.Day = (DateTime) EventDateTime;
            t.EventProgress = new List<EventProgress>();
            t.Challenge = _selectedEventChallenge;
            CoolerContext.GetInstance().Events.Add(t);
            Events.Add(t);
            Events = sortEvents(Events);
            CoolerContext.GetInstance().SaveChanges();
            newWindow.Close();

        });

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

        private Challenge _selectedEventChallenge;

        public string SelectedEventChallenge
        {
            set { _selectedEventChallenge = AllChallenges.FirstOrDefault(p => p.Title == value); }
        }





        public RelayCommand AddSelectedToPersonChallenge => new RelayCommand(obj =>
        {
            if (SelectedChallenge == null) return;

            if (!SelectedChallenge.Challengers.Contains(UserContext.GetInstance().UserPerson))
            {
                PersonChallenges.Add(SelectedChallenge);
                GetInstance().Challenges.FirstOrDefault(p => p == SelectedChallenge)?.Challengers
                    .Add(UserContext.GetInstance().UserPerson);
                GetInstance().SaveChanges();
            }
            else
            {
                MessageBox.Show("Этот челлендж уже добавлен вами");
            }



        });

        private Window newWindow;

        public RelayCommand AddEventWindowSpawnCommand => new RelayCommand(obj =>
        {
            newWindow = new AddEvent();
            newWindow.DataContext = this;
            newWindow.ShowDialog();
        });



        private ObservableCollection<Event> GetEvents()
        {
            ObservableCollection<Event> events = new();
            foreach (var var in GetInstance().Events.Where(p => (p.Challenge == SelectedChallenge)).ToList())
            {
                if ((var.EventProgress==null)||(var.EventProgress.FirstOrDefault(p => p.Person == UserContext.GetInstance().UserPerson) == null))
                {

                    events.ToList().ForEach(p => p.EventProgress = new List<EventProgress>());
                    events.Add(var);
                }

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


        private ObservableCollection<Event> sortEvents(ObservableCollection<Event> events)
        {
            return new ObservableCollection<Event>(events.ToList().OrderBy(p => p.Day));
        }

        public RelayCommand EventIsDone => new(obj =>
        {
            if (SelectedEvent.Challenge==null)
            {
                MessageBox.Show("Не выбрано событие");
                return;
                
            }
            CoolerContext.GetInstance().Events.FirstOrDefault(p => p == SelectedEvent)?.EventProgress.Add(new EventProgress(){Event = SelectedEvent, Person = UserContext.GetInstance().UserPerson});
            Events.Remove(SelectedEvent);
            Events = sortEvents(Events);
            CoolerContext.GetInstance().SaveChanges();
        });

        public Challenge SelectedChallenge
        {
            get => _selectedChallenge;
            set
            {
                _selectedChallenge = value;
                Events = GetEvents();
                OnPropertyChanged(nameof(SelectedChallenge));
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

        private ObservableCollection<Challenge> UpdatePersonChallenges() => new ObservableCollection<Challenge>(CoolerContext.GetInstance().Challenges.Where(p => p.Challengers.Contains(UserContext.GetInstance().UserPerson)));



        private void RemoveEvents(Event @event)
        {
            CoolerContext.GetInstance().EventProgress.RemoveRange(CoolerContext.GetInstance().EventProgress.Where(p => p.Event ==@event).AsEnumerable());
            CoolerContext.GetInstance().Events.Remove(@event);
        }


        public void RemoveExpiredEvents()
        {
            List<Model.Entities.Event> events = new List<Model.Entities.Event>();

            DateTime currentTime = DateTime.Today;
            foreach (var @event in CoolerContext.GetInstance().Events.ToList())
            {
                if ((@event.Day - currentTime.Date).TotalDays <=-1)
                {
                    events.Add(@event);
                    RemoveEvents(@event);
                }
            }
            CoolerContext.GetInstance().SaveChanges();

        }


        public ChallengeViewModel()
        {
         RemoveExpiredEvents();   
            AllChallenges = ChallengeContext.GetInstance().GetChallenges();
            PersonChallenges = UpdatePersonChallenges();
        }
    }
}