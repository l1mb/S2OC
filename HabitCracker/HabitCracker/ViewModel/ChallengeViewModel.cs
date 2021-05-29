using HabitCracker.Model.Builders;
using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using HabitCracker.View.Menu.Challenge;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static HabitCracker.Model.Entities.CoolerContext;

namespace HabitCracker.ViewModel
{
    internal class ChallengeViewModel : BaseViewModel
    {
        private ObservableCollection<Challenge> _allchallenges = new();
        private ObservableCollection<Challenge> _personchallenges = new();
        private Window _newWindow;

        private Page _currentPage;

        private string _eventName = "";

        public string Role
        {
            get => UserContext.GetInstance().UserPerson.Role;
        }

        public List<string> AllChallengesHeadersList => AllChallenges.Select(p => p.Title).ToList();

        public string EventName
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

        public RelayCommand AddEvent
        {
            get
            {
                EventBuilder builder = new();
                return new RelayCommand(obj =>
                {
                    if (EventDateTime != null)
                    {
                        var t = builder.SetName(EventName).SetDate((DateTime)EventDateTime)
                            .SetChallenge(_selectedEventChallenge).GetEvent();
                        GetInstance().Events.Add(t);
                        Events.Add(t);
                    }

                    Events = SortEvents(Events);
                    GetInstance().SaveChanges();
                    _newWindow.Close();
                });
            }
        }

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        private ObservableCollection<Event> _events = new();

        private Challenge _selectedChallenge = new();
        private Event _selectedEvent = new();

        private Challenge _selectedEventChallenge;

        public string SelectedEventChallenge
        {
            set { _selectedEventChallenge = AllChallenges.FirstOrDefault(p => p.Title == value); }
        }

        public RelayCommand AddSelectedToPersonChallenge => new(obj =>
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

        public RelayCommand AddEventWindowSpawnCommand => new(obj =>
        {
            _newWindow = new AddEvent { DataContext = this };
            _newWindow.ShowDialog();
        });

        private ObservableCollection<Event> GetEvents()
        {
            ObservableCollection<Event> events = new();
            foreach (var var in GetInstance().Events.Where(p => p != null && p.Challenge == SelectedChallenge).ToList().Where(var => var.EventProgress?.FirstOrDefault(p => p.Person == UserContext.GetInstance().UserPerson) == null))
            {
                events.ToList().ForEach(p => p.EventProgress = new List<EventProgress>());
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

        private static ObservableCollection<Event> SortEvents(IEnumerable<Event> events) => new(events.ToList().OrderBy(p => p.Day));

        public RelayCommand EventIsDone => new(obj =>
        {
            if (SelectedEvent.Challenge == null)
            {
                MessageBox.Show("Не выбрано событие");
                return;
            }
            GetInstance().Events.FirstOrDefault(p => p == SelectedEvent)?.EventProgress.Add(new EventProgress { Event = SelectedEvent, Person = UserContext.GetInstance().UserPerson });
            Events.Remove(SelectedEvent);
            Events = SortEvents(Events);
            GetInstance().SaveChanges();
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

        private ObservableCollection<Challenge> UpdatePersonChallenges() => new(GetInstance().Challenges.Where(p => p.Challengers.Contains(UserContext.GetInstance().UserPerson)));

        private static void RemoveEvents(Event @event)
        {
            GetInstance().EventProgress.RemoveRange(GetInstance().EventProgress.Where(p => p.Event == @event).AsEnumerable());
            GetInstance().Events.Remove(@event);
        }

        public void RemoveExpiredEvents()
        {
            var currentTime = DateTime.Today;
            foreach (var @event in GetInstance().Events.ToList().Where(@event => (@event.Day - currentTime.Date).TotalDays <= -1))
            {
                RemoveEvents(@event);
            }
            GetInstance().SaveChanges();
        }

        public ChallengeViewModel()
        {
            RemoveExpiredEvents();
            AllChallenges = ChallengeContext.GetInstance().GetChallenges();
            PersonChallenges = UpdatePersonChallenges();
        }
    }
}