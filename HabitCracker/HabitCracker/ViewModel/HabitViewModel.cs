using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using HabitCracker.Model.Memento;
using HabitCracker.View.Menu.Habit;
using Newtonsoft.Json;

namespace HabitCracker.ViewModel
{
    internal class HabitViewModel : BaseViewModel
    {
        public Originator Originator = new Originator();
        public CareTaker Caretaker = new CareTaker();
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
                Caretaker = Originator.FillDays(
                    CourseworkDbContext.GetInstance().Weekprogresses.Where(p => p.Habit == SelectedHabit.Id));
                Switcher(Caretaker.GetCurrent());
                OnPropertyChanged(nameof(SelectedHabit));
                OnPropertyChanged(nameof(Caretaker));
            }
        }

        public RelayCommand ClearHabitList => new RelayCommand(obj => { PersonHabits.Clear(); }
        );

        public RelayCommand DeleteSelectedCommand => new RelayCommand(obj => { PersonHabits.Remove(SelectedHabit); });

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

        private int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }

            return (date - firstMonthMonday).Days / 7 + 1;
        }

        //1 ........ end

        //orderby date, createMementos(weekdiff(date1, date2)),
        //найти понедельник, -7 дней, -7 дней

        public RelayCommand GetPreviousDays => new RelayCommand(obj =>
        {
            Monday = Tuesday = Wednesday = Thursday = Friday = Saturday = Sunday = false;
            Switcher(Caretaker.GetPrevious());
        });

        public RelayCommand GetNextDays => new RelayCommand(
            obj =>
            {
                Monday = Tuesday = Wednesday = Thursday = Friday = Saturday = Sunday = false;
                Switcher(Caretaker.GetNext());
            });

        private void Switcher(WeekMemento memento)
        {
            if (memento == null)
            {
                return;
            }
            foreach (var en in JsonConvert.DeserializeObject<List<DayOfWeek>>(memento.State))
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
                }
            }
        }

        public RelayCommand HabitIsDone => new RelayCommand(obj =>
            {
                //! код ниже пришел на смену слишком гениального
                //var tt = typeof(HabitViewModel).GetProperties().Where(p => p.PropertyType == typeof(bool)).ToArray();
                //и установки значения в свойства через рефлексию

                JsonConvert.DeserializeObject<List<DayOfWeek>>(Caretaker.GetCurrent().State);
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
                    else
                    {
                        w.Weekorder = t.OrderByDescending(p => p.Weekorder).First()?.Weekorder;
                    }
                }

                w.Weekday = DateTime.Now;
                CourseworkDbContext.GetInstance().Weekprogresses.Add(w);
                CourseworkDbContext.GetInstance().SaveChanges();
            });

        public HabitViewModel()
        {
            //!получается, что я все это время делал неправильное, я делал для определенной привычки, а нужно было просто для дней, может, так и лучше, вроде для общего случая легче

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