using HabbitCracker.Model.Contexts;
using HabbitCracker.Model.Entities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using HabbitCracker.View.Menu.Habbit;

namespace HabbitCracker.ViewModel
{
    internal class HabbitViewModel : BaseViewModel
    {
        private ObservableCollection<Habbit> _personHabbits = new ObservableCollection<Habbit>();
        private Habbit _selectedHabbit;
        private Person _person;
        private Random _rand = new Random();

        public int Rand
        {
            get => _rand.Next();
        }

        private Habbit _newHabbit = new();

        public Habbit NewHabbit
        {
            get => _newHabbit;
            set
            {
                _newHabbit = value;
                OnPropertyChanged(nameof(NewHabbit));
            }
        }

        public ObservableCollection<Habbit> PersonHabbits
        {
            get => _personHabbits;
            set
            {
                _personHabbits = value;
                OnPropertyChanged(nameof(PersonHabbits));
            }
        }

        public Habbit SelectedHabbit
        {
            get => _selectedHabbit;
            set
            {
                _selectedHabbit = value;
                OnPropertyChanged(nameof(SelectedHabbit));
            }
        }

        public RelayCommand ClearHabbitList => new RelayCommand(obj =>
            {
                PersonHabbits.Clear();
            }
        );

        public RelayCommand DeleteSelectedCommand => new RelayCommand(obj =>
        {
            PersonHabbits.Remove(SelectedHabbit);
        });

        public RelayCommand OpenNewHabbitCtorCommand => new RelayCommand(obj =>
         {
             Window window = new NewHabbit();
             window.Show();
             window.DataContext = this;
         });

        public RelayCommand AddNewHabbitCommand => new RelayCommand(obj =>
        {
            var tmpHabbit = new Habbit()
            {
                Id = Rand,
                Userid = UserContext.GetInstance().UserPerson.Id,
                CurrentStreak = 0,
                Description = NewHabbit.Description,
                CreateDate = DateTime.Now,
                Title = NewHabbit.Title,
                DaysCount = NewHabbit.DaysCount,
                User = UserContext.GetInstance().UserPerson
            };
            if (!String.IsNullOrWhiteSpace(NewHabbit.Title) && !string.IsNullOrWhiteSpace(NewHabbit.Description))
            {
                PersonHabbits.Add(tmpHabbit);
                CourseworkDbContext.GetInstance().Habbits.Add(tmpHabbit);

                CourseworkDbContext.GetInstance().SaveChanges();
            }
            PersonHabbits = _personHabbits;
            OnPropertyChanged(nameof(PersonHabbits));
        });

        public HabbitViewModel()
        {
            if (HabbitContext.GetInstance().Habbits.Count == 0)
            {
                _personHabbits.Add(new Habbit()
                {
                    Id = Rand,
                    Userid = UserContext.GetInstance().UserPerson.Id,
                    CurrentStreak = 0,
                    Description = "text about danger of smoking",
                    CreateDate = DateTime.Now,
                    Title = "Smoking",
                    User = UserContext.GetInstance().UserPerson
                });
                _personHabbits.Add(new Habbit()
                {
                    Id = Rand,
                    Userid = UserContext.GetInstance().UserPerson.Id,
                    CurrentStreak = 0,
                    Description = "text about danger of drinking",
                    CreateDate = DateTime.Now,
                    Title = "Drinking",
                    User = UserContext.GetInstance().UserPerson
                });
                _personHabbits.Add(new Habbit()
                {
                    Id = Rand,
                    Userid = UserContext.GetInstance().UserPerson.Id,
                    CurrentStreak = 0,
                    Description = "text about danger of using drugs",
                    CreateDate = DateTime.Now,
                    Title = "Druging",
                    User = UserContext.GetInstance().UserPerson
                });
                _personHabbits.Add(new Habbit()
                {
                    Id = Rand,
                    Userid = UserContext.GetInstance().UserPerson.Id,
                    CurrentStreak = 0,
                    Description = "text about danger of using seal",
                    CreateDate = DateTime.Now,
                    Title = "Sealing",
                    User = UserContext.GetInstance().UserPerson
                });
                _personHabbits.Add(new Habbit()
                {
                    Id = Rand,
                    Userid = UserContext.GetInstance().UserPerson.Id,
                    CurrentStreak = 0,
                    Description = "text about danger of using seal",
                    CreateDate = DateTime.Now,
                    Title = "Sealing",
                    User = UserContext.GetInstance().UserPerson
                });
            }
            else
            {
                PersonHabbits = HabbitContext.GetInstance().Habbits;
            }
        }
    }
}