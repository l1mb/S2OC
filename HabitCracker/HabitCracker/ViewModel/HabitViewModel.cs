using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using HabitCracker.Model.Entities.Валеты;
using HabitCracker.Model.Memento;
using HabitCracker.View.Menu.Habit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using HabitCracker.View.Menu;
using Microsoft.IdentityModel.Tokens;
using static Newtonsoft.Json.JsonConvert;

namespace HabitCracker.ViewModel
{
    internal class HabitViewModel : BaseViewModel
    {
        public Habit[] Habits = {
            new()
            {
                Description = "text about danger of smoking",
                CreateDate = DateTime.Now,
                Title = "Smoking",
                User = UserContext.GetInstance().UserPerson
            },
            new()
            {
                Description = "text about danger of drinking",
                CreateDate = DateTime.Now,
                Title = "Drinking",
                User = UserContext.GetInstance().UserPerson
            },
            new()
            {
                Description = "text about danger of using drugs",
                CreateDate = DateTime.Now,
                Title = "Druging",
                User = UserContext.GetInstance().UserPerson
            },
            new()
            {
                Description = "text about danger of using seal",
                CreateDate = DateTime.Now,
                Title = "Sealing",
                User = UserContext.GetInstance().UserPerson
            },
            new()
            {
                Description = "text about danger of using seal",
                CreateDate = DateTime.Now,
                Title = "Sealing",
                User = UserContext.GetInstance().UserPerson
            }
        };

        private void RemoveOverduedHabit()
        {
            List<Model.Entities.Habit> habits = new List<Model.Entities.Habit>();
            DateTime currentTime = DateTime.Today;
            foreach (var habit in CoolerContext.GetInstance().Habits)
            {
                if ((currentTime.Date-habit.CreateDate).TotalDays>habit.DaysCount)
                {
                    habits.Add(habit);
                 
                    RemoveHabits(habit);
                }
            }
            NotifyHabit(habits);
            CoolerContext.GetInstance().SaveChanges();
        }

        private void RemoveHabits(Habit habit)
        {
            CoolerContext.GetInstance().HabitProgress.RemoveRange(CoolerContext.GetInstance().HabitProgress.Where(p => p.Habit == habit).AsEnumerable());
            CoolerContext.GetInstance().Habits.Remove(habit);
            CoolerContext.GetInstance().SaveChangesAsync();
        }

        public void NotifyHabit(List<Model.Entities.Habit> habits)
        {
            string tempString = habits.Aggregate<Habit, string>(null, (current, habit) => current + (habit.Title + "\n"));
            if (!String.IsNullOrWhiteSpace(tempString))
            {
                MessageBox.Show($"Пока вы были в отключке, у вас закончились следующие привычки\n{ tempString}", "Просрочено", MessageBoxButton.OK);
            }
        }

        public Originator Originator = new();
        public CareTaker Caretaker = new();

        private bool _habitIsDone = false;

        public bool IsDone
        {
            get => _habitIsDone;
            set
            {
                _habitIsDone = value;
                OnPropertyChanged(nameof(IsDone));
            }
        }

        public bool Monday

        {
            get => DaysBools[0];

            set
            {
                DaysBools[0] = value;

                OnPropertyChanged(nameof(Monday));
            }
        }

        public bool Tuesday
        {
            get => DaysBools[1];
            set
            {
                DaysBools[1] = value;
                OnPropertyChanged(nameof(Tuesday));
            }
        }

        public bool Wednesday
        {
            get => DaysBools[2];
            set
            {
                DaysBools[2] = value;
                OnPropertyChanged(nameof(Wednesday));
            }
        }

        public bool Thursday
        {
            get => DaysBools[3];
            set
            {
                DaysBools[3] = value;
                OnPropertyChanged(nameof(Thursday));
            }
        }

        public bool Friday
        {
            get => DaysBools[4];
            set
            {
                DaysBools[4] = value;
                OnPropertyChanged(nameof(Friday));
            }
        }

        public bool Saturday
        {
            get => DaysBools[5];
            set
            {
                DaysBools[5] = value;
                OnPropertyChanged(nameof(Saturday));
            }
        }

        public bool Sunday
        {
            get => DaysBools[6];
            set
            {
                DaysBools[6] = value;
                OnPropertyChanged(nameof(Sunday));
            }
        }

        public bool[] DaysBools { get; set; } = new bool[7];

        private Window _addNewHabbitWindow;
        private ObservableCollection<Habit> _personHabits = new();
        private Habit _selectedHabit;
        private readonly Random _rand = new();

        public int Rand => _rand.Next();

        private Habit _newHabit = new();

        public Habit NewHabit
        {
            get => _newHabit;
            set
            {
                _newHabit = value;
                OnPropertyChanged(nameof(NewHabit));
            }
        }

        public ObservableCollection<Habit> PersonHabits
        {
            get => _personHabits;
            set
            {
                _personHabits = value;
                OnPropertyChanged(nameof(PersonHabits));
            }
        }

        public Habit SelectedHabit
        {
            get => _selectedHabit;
            set
            {
                NullDays();
                _selectedHabit = value;
                Caretaker = Originator.FillDays(Model.Entities.CoolerContext.GetInstance().HabitProgress.Where(p => p.Habit == SelectedHabit));
                //тут могут быть некоторые косяки с выбором
                Switcher(Caretaker.GetCurrent());

                if (_selectedHabit != null)
                    IsDone = _selectedHabit.HabitProgress.Count(p => p.Weekday.Date == DateTime.Today.Date) != 0;
                OnPropertyChanged(nameof(SelectedHabit));
                OnPropertyChanged(nameof(Caretaker));
            }
        }

        public RelayCommand ClearHabitList => new(obj =>
            {
                foreach (var VARIABLE in PersonHabits)
                {
                    RemoveHabits(VARIABLE);
                }
                PersonHabits.Clear();

            }

        );

        public RelayCommand DeleteSelectedCommand => new(obj =>
        {
            PersonHabits.Remove(SelectedHabit);
            if (SelectedHabit!=null)
            {
                RemoveHabits(SelectedHabit);
                SelectedHabit = null;
            }

        });

        public RelayCommand OpenNewHabitCtorCommand => new(obj =>
        {
            _addNewHabbitWindow = new NewHabit();
            _addNewHabbitWindow.Show();
            _addNewHabbitWindow.DataContext = this;
        });

        public RelayCommand AddNewHabitCommand => new(obj =>
        {
            var tmpHabit = new Habit()
            {
                CurrentStreak = 0,
                Description = NewHabit.Description,
                CreateDate = DateTime.Now,
                Title = NewHabit.Title,
                DaysCount = NewHabit.DaysCount,
                User = UserContext.GetInstance().UserPerson
            };
            if (!string.IsNullOrWhiteSpace(NewHabit.Title) && !string.IsNullOrWhiteSpace(NewHabit.Description))
            {
                PersonHabits.Add(tmpHabit);
                CoolerContext.GetInstance().Habits.Add(tmpHabit);

                CoolerContext.GetInstance().SaveChanges();
            }
            _addNewHabbitWindow.Close();
            PersonHabits = _personHabits;
            OnPropertyChanged(nameof(PersonHabits));
        });

        public RelayCommand GetPreviousDays => new(obj =>
        {
            NullDays();
            Switcher(Caretaker.GetPrevious());
        });

        private void NullDays()
        {
            Monday = Tuesday = Wednesday = Thursday = Friday = Saturday = Sunday = false;
        }
        public RelayCommand GetNextDays => new(
            obj =>
            {
                NullDays();

                Switcher(Caretaker.GetNext());
            });

        private void Switcher(WeekMemento memento, DayOfWeek? day = null)
        {
           
            List<DayOfWeek> tmpList = new();
            if (day!=null)
            {
                tmpList.Add((DayOfWeek) day);
            }
            else if (memento == null)
            {
                return;
            }
            else
            {
                tmpList = DeserializeObject<List<DayOfWeek>>(memento.State);

            }


            foreach (var en in tmpList)
            {
                switch (en)
                {
                    case DayOfWeek.Monday:
                        {
                            Monday = true;
                        }
                        break;

                    case DayOfWeek.Tuesday:
                        {
                            Tuesday = true;
                        }
                        break;

                    case DayOfWeek.Wednesday:
                        {
                            Wednesday = true;
                        }
                        break;

                    case DayOfWeek.Thursday:
                        {
                            Thursday = true;
                        }
                        break;

                    case DayOfWeek.Friday:
                        {
                            Friday = true;
                        }
                        break;

                    case DayOfWeek.Saturday:
                        {
                            Saturday = true;
                        }
                        break;

                    case DayOfWeek.Sunday:
                        {
                            Sunday = true;
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public RelayCommand SaveHabits => new(obj =>
        {
            foreach (var habit in PersonHabits)
            {
                if (!CoolerContext.GetInstance().Habits.Contains(habit))
                {
                    CoolerContext.GetInstance().Habits.Add(habit);
                }
            }
        });

        public RelayCommand HabitIsDone => new(obj =>
            {
                IsDone = true;
                var w = new HabitProgress();
                w.Habit = SelectedHabit;

                SelectedHabit.CurrentStreak++;
                var tempGiveReward = Rewarder.GiveReward((int)SelectedHabit.CurrentStreak);

                Model.Entities.CoolerContext.GetInstance().Wallets
                        .First(p => p.PersonRef == UserContext.GetInstance().UserPerson.Id).Balance +=
                    tempGiveReward.Item1;
                MessageBox.Show(tempGiveReward.Item2);

                Switcher(Caretaker.GetCurrent(), DateTime.Today.DayOfWeek);
                w.Weekday = DateTime.Now;
                Model.Entities.CoolerContext.GetInstance().HabitProgress.Add(w);
                Model.Entities.CoolerContext.GetInstance().SaveChanges();
            });

        public HabitViewModel()
        {
            RemoveOverduedHabit();
            if (HabitContext.GetInstance().Habits.Count == 0)
            {
                _personHabits = new ObservableCollection<Habit>(Habits);
            }
            else
            {
                PersonHabits = HabitContext.GetInstance().Habits;
            }
        }
    }
}