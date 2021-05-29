using System;
using System.Collections.Generic;

namespace HabitCracker.Model.Entities
{
    public class Event
    {
        public Event()
        {
            EventProgress = new HashSet<EventProgress>();
        }

        public int Id { get; set; }
        public DateTime Day { get; set; }
        public string EventName { get; set; }
        public Challenge Challenge { get; set; }

        public ICollection<EventProgress> EventProgress { get; set; }
    }
}