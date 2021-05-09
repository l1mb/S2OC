using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabbitCracker.Model.Contexts;
using HabbitCracker.Model.Entities;
using static HabbitCracker.Model.Entities.CourseworkDbContext;

namespace HabbitCracker.ViewModel
{
    internal class ChallengeViewModel : BaseViewModel
    {
        private ObservableCollection<Challenge> _challenges = new ObservableCollection<Challenge>();

        private ObservableCollection<Event> _events = new ObservableCollection<Event>();

        private Challenge _selectedChallenge = new Challenge();
        private Event _selectedEvent = new Event();

        private ObservableCollection<Event> getEvents()
        {
            ObservableCollection<Event> events = new();
            foreach (var var in CourseworkDbContext.GetInstance().Events.Where(p => p.Challengeid == SelectedChallenge.Challengeid).ToList())
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
                Events = getEvents();
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

        public ObservableCollection<Challenge> Challenges
        {
            get => ChallengeContext.GetInstance().getChallenges();
            set
            {
                _challenges = value;
                OnPropertyChanged(nameof(Challenges));
            }
        }

        public ChallengeViewModel()
        {
            Challenges = ChallengeContext.GetInstance().getChallenges();
        }
    }
}