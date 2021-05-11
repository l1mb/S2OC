using System;
using System.Collections.Generic;

#nullable disable

namespace HabitCracker.Model.Entities
{
    public partial class Habit
    {
        public Habit()
        {
            Weekprogresses = new HashSet<Weekprogress>();
        }

        public int Id { get; set; }
        public int? Userid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? DaysCount { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CurrentStreak { get; set; }

        public virtual Person User { get; set; }
        public virtual ICollection<Weekprogress> Weekprogresses { get; set; }
    }
}