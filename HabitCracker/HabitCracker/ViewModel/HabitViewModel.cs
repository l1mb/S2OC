using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using HabitCracker.View.Menu.Habit;

namespace HabitCracker.ViewModel
{
    internal class HabitViewModel : BaseViewModel
    {
        private ObservableCollection<bool> _days = new();

        private bool[] daysBools = new bool[7];

        public bool Monday

        {
            get => daysBools[0];

            set
            {
                daysBools[0] = value;
                OnPropertyChanged(nameof(Monday));
            }
        }

        public bool Tuesday
        {
            get => daysBools[1];
            set
            {
                daysBools[1] = value;
                OnPropertyChanged(nameof(Tuesday));
            }
        }

        public bool Wednesday
        {
            get => daysBools[2];
            set
            {
                daysBools[2] = value;
                OnPropertyChanged(nameof(Wednesday));
            }
        }

        public bool Thursday
        {
            get => daysBools[3];
            set
            {
                daysBools[3] = value;
                OnPropertyChanged(nameof(Thursday));
            }
        }

        public bool Friday
        {
            get => daysBools[4];
            set
            {
                daysBools[4] = value;
                OnPropertyChanged(nameof(Friday));
            }
        }

        public bool Saturday
        {
            get => daysBools[5];
            set
            {
                daysBools[5] = value;
                OnPropertyChanged(nameof(Saturday));
            }
        }

        public bool Sunday
        {
            get => daysBools[6];
            set
            {
                daysBools[6] = value;
                OnPropertyChanged(nameof(Saturday));
            }
        }

        public bool[] DaysBools
        {
            get => daysBools;
            set => daysBools = value;
        }

        private ObservableCollection<Habit> _personHabits = new ObservableCollection<Habit>();
        private Habit _selectedHabit;
        private Person _person;
        private Random _rand = new Random();

        public int Rand
        {
            get => _rand.Next();
        }

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
                _selectedHabit = value;
                OnPropertyChanged(nameof(SelectedHabit));
            }
        }

        public RelayCommand ClearHabitList => new RelayCommand(obj =>
            {
                PersonHabits.Clear();
            }
        );

        public RelayCommand DeleteSelectedCommand => new RelayCommand(obj =>
        {
            PersonHabits.Remove(SelectedHabit);
        });

        public RelayCommand OpenNewHabitCtorCommand => new RelayCommand(obj =>
         {
             Window window = new NewHabit();
             window.Show();
             window.DataContext = this;
         });

        public RelayCommand AddNewHabitCommand => new RelayCommand(obj =>
        {
            var tmpHabit = new Habit()
            {
                Id = Rand,
                Userid = UserContext.GetInstance().UserPerson.Id,
                CurrentStreak = 0,
                Description = NewHabit.Description,
                CreateDate = DateTime.Now,
                Title = NewHabit.Title,
                DaysCount = NewHabit.DaysCount,
                User = UserContext.GetInstance().UserPerson
            };
            if (!String.IsNullOrWhiteSpace(NewHabit.Title) && !string.IsNullOrWhiteSpace(NewHabit.Description))
            {
                PersonHabits.Add(tmpHabit);
                CourseworkDbContext.GetInstance().Habits.Add(tmpHabit);

                CourseworkDbContext.GetInstance().SaveChanges();
            }
            PersonHabits = _personHabits;
            OnPropertyChanged(nameof(PersonHabits));
        });

        public RelayCommand HabitIsDone => new RelayCommand(obj =>
        {
            var w = new Weekprogress();
            w.Id = Rand;
            w.Habit = SelectedHabit.Id;
            var t = CourseworkDbContext.GetInstance().Weekprogresses;
            if (t.Count(p => p.Habit == SelectedHabit.Id) == 0)
            {
                w.Weekorder = 0;
            }
            else
            {
                if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
                {
                    w.Weekorder = t.OrderByDescending(p => p.Weekorder).First().Weekorder + 1;
                }
            }

            w.Weekday = DateTime.Now;
            CourseworkDbContext.GetInstance().Weekprogresses.Add(w);
            CourseworkDbContext.GetInstance().SaveChanges();
        });

        public HabitViewModel()
        {
            if (HabitContext.GetInstance().Habits.Count == 0)
            {
                _personHabits.Add(new Habit()
                {
                    Id = Rand,
                    Userid = UserContext.GetInstance().UserPerson.Id,
                    CurrentStreak = 0,
                    Description = "text about danger of smoking",
                    CreateDate = DateTime.Now,
                    Title = "Smoking",
                    User = UserContext.GetInstance().UserPerson
                });
                _personHabits.Add(new Habit()
                {
                    Id = Rand,
                    Userid = UserContext.GetInstance().UserPerson.Id,
                    CurrentStreak = 0,
                    Description = "text about danger of drinking",
                    CreateDate = DateTime.Now,
                    Title = "Drinking",
                    User = UserContext.GetInstance().UserPerson
                });
                _personHabits.Add(new Habit()
                {
                    Id = Rand,
                    Userid = UserContext.GetInstance().UserPerson.Id,
                    CurrentStreak = 0,
                    Description = "text about danger of using drugs",
                    CreateDate = DateTime.Now,
                    Title = "Druging",
                    User = UserContext.GetInstance().UserPerson
                });
                _personHabits.Add(new Habit()
                {
                    Id = Rand,
                    Userid = UserContext.GetInstance().UserPerson.Id,
                    CurrentStreak = 0,
                    Description = "text about danger of using seal",
                    CreateDate = DateTime.Now,
                    Title = "Sealing",
                    User = UserContext.GetInstance().UserPerson
                });
                _personHabits.Add(new Habit()
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
                PersonHabits = HabitContext.GetInstance().Habits;
            }
        }

        public class BoolToBrushConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value == null)
                    return Brushes.Transparent;

                return System.Convert.ToBoolean(value)
                    ? Brushes.Red
                    : Brushes.Transparent;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}