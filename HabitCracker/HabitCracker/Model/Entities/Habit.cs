using System;
using System.Collections.Generic;

namespace HabitCracker.Model.Entities
{
    public class Habit
    {
        public Habit()
        {
            HabitProgress = new HashSet<HabitProgress>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DaysCount { get; set; }
        public DateTime CreateDate { get; set; }
        public int CurrentStreak { get; set; }

        public Person User { get; set; }
        public ICollection<HabitProgress> HabitProgress { get; private set; }
    }
}