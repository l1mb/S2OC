using System.Collections.Generic;

#nullable disable

namespace HabbitCracker.Model.Entities
{
    public partial class Challenge
    {
        public Challenge()
        {
            Events = new HashSet<Event>();
        }

        public int Challengeid { get; set; }
        public int? Creatorid { get; set; }
        public string Title { get; set; }
        public string Tip { get; set; }
        public int? Dayscount { get; set; }
        public int? Challengerid { get; set; }

        public virtual Person Creator { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}