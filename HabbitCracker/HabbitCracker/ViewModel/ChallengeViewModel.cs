using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabbitCracker.Model.Contexts;
using HabbitCracker.Model.Entities;

namespace HabbitCracker.ViewModel
{
    internal class ChallengeViewModel : BaseViewModel
    {
        private ObservableCollection<Challenge> _challenges = new ObservableCollection<Challenge>();
        private ObservableCollection<Event> _events = new ObservableCollection<Event>();

        private Challenge _selectedChallenge = new Challenge();
        private Event _selectedEvent = new Event();

        public Challenge SelectedChallenge
        {
            get => _selectedChallenge;
            set
            {
                _selectedChallenge = value;
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

        public ObservableCollection<Challenge> Challenges
        {
            get => _challenges;
            set
            {
                _challenges = value;
                OnPropertyChanged(nameof(Challenges));
            }
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

        public ChallengeViewModel()
        {
            Challenges = ChallengeContext.GetInstance().getChallenges();
        }
    }
}