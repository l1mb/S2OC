using HabitCracker.Model.Entities;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace HabitCracker.Model.Contexts
{
    internal class HabitContext
    {
        private static readonly Lazy<HabitContext> Instance = new(() => new HabitContext());

        public static HabitContext GetInstance()
        {
            return Instance.Value;
        }

        public ObservableCollection<Habit> Habits
        {
            get => GetHabits();
        }

        private readonly Entities.CoolerContext _dbContext;

        public HabitContext()
        {
            _dbContext = Entities.CoolerContext.GetInstance();
        }

        private ObservableCollection<Habit> GetHabits()
        {
            var Habits = new ObservableCollection<Habit>();

            foreach (var item in _dbContext.Habits.Where(p =>
                p.User == UserContext.GetInstance().UserPerson).ToList())
            {
                Habits.Add(item);
            }
            return Habits;
        }
    }
}