using System;

#nullable disable

namespace HabitCracker.Model.Entities
{
    public partial class Event
    {
        public int? Challengeid { get; set; }
        public int Eventid { get; set; }
        public DateTime? Day { get; set; }
        public string Event1 { get; set; }
        public bool? Isdone { get; set; }

        public virtual Challenge Challenge { get; set; }
    }
}