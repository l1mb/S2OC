using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabitCracker.Model.Contexts
{
    internal class HabitContext : DbContext
    {
        public static readonly Lazy<HabitContext> Instance = new(() => new HabitContext());

        public static HabitContext GetInstance()
        {
            return Instance.Value;
        }

        public ObservableCollection<Habit> Habits = new();

        private readonly Entities.CourseworkDbContext _dbContext;

        public HabitContext()
        {
            _dbContext = Entities.CourseworkDbContext.GetInstance();
            Habits = GetHabits();
        }

        public void GetCurrentContext()
        {
        }

        private ObservableCollection<Habit> GetHabits()
        {
            var Habits = new ObservableCollection<Habit>();

            foreach (var item in _dbContext.Habits.Where(p =>
                p.Userid == UserContext.GetInstance().UserPerson.Id).ToList())
            {
                Habits.Add(item);
            }
            return Habits;
        }
    }
}