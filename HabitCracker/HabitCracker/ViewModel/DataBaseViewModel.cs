using HabitCracker.Model.Contexts;

namespace HabitCracker.ViewModel
{
    internal class DataBaseViewModel : BaseViewModel
    {
        public Model.Entities.Auth AuthObject { get; set; } = new();
        public Model.Entities.Challenge ChallengeObject { get; set; } = new();
        public Model.Entities.Event EventObject { get; set; } = new();
        public Model.Entities.Habit HabitObject { get; set; } = new();
        public Model.Entities.Person PersonObject { get; set; } = new();
        public Model.Entities.Wallet WalletObject { get; set; } = new();

        private bool _isModerator;

        public bool IsModerator
        {
            get { return _isModerator; }
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