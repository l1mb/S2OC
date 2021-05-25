using System;
using System.Collections.Generic;

namespace HabitCracker.Model.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public string EventName { get; set; }
        public bool IsDone { get; set; }

        public Person Person { get; set; }

        public Challenge Challenge { get; set; }

        public ICollection<EventProgress> EventProgress { get; set;}
    }
}