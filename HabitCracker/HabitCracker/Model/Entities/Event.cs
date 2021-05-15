using System;

namespace HabitCracker.Model.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public string EventName { get; set; }
        public bool IsDone { get; set; }

        public Challenge Challenge { get; set; }
    }
}