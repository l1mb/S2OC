using System.Collections.Generic;

#nullable disable

namespace HabbitCracker.Model.Entities
{
    public partial class Event
    {
        public Event()
        {
            Challenges = new HashSet<Challenge>();
        }

        public int Eventid { get; set; }
        public string Day { get; set; }
        public string Event1 { get; set; }

        public virtual ICollection<Challenge> Challenges { get; set; }
    }
}