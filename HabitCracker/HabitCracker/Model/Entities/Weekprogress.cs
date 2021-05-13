using System;

#nullable disable

namespace HabitCracker.Model.Entities
{
    public partial class Weekprogress
    {
        public int Id { get; set; }
        public int? Habit { get; set; }
        public DateTime? Weekday { get; set; }

        public virtual Habit HabitNavigation { get; set; }
    }
}