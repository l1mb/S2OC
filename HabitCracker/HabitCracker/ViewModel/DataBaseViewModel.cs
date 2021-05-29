using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;

namespace HabitCracker.ViewModel
{
    internal class DataBaseViewModel : BaseViewModel
    {
        public Model.Entities.Auth AuthObject { get; set; } = new();
        public Challenge ChallengeObject { get; set; } = new();
        public Event EventObject { get; set; } = new();
        public Habit HabitObject { get; set; } = new();
        public Person PersonObject { get; set; } = new();
        public Wallet WalletObject { get; set; } = new();

        public HabitProgress HabitProgressObject { get; set; } = new();

        private bool _isModerator;

        public bool IsModerator
        {
            get => _isModerator;
            set
            {
                _isModerator = value;
                OnPropertyChanged(nameof(IsModerator));
            }
        }

        public DataBaseViewModel()
        {
            if (UserContext.GetInstance().UserPerson.Role == "Модератор")
            {
                IsModerator = true;
            }
        }
    }
}