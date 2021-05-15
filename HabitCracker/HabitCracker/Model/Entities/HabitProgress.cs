using System;

namespace HabitCracker.Model.Entities
{
    public class HabitProgress
    {
        public int Id { get; set; }
        public DateTime Weekday { get; set; }

        public Habit Habit { get; set; }
    }
}