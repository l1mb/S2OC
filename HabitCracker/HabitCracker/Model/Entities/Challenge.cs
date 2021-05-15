using System.Collections.Generic;

namespace HabitCracker.Model.Entities
{
    public class Challenge
    {
        public Challenge()
        {
            Events = new HashSet<Event>();
            Challengers = new List<Person>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Tip { get; set; }
        public int DaysCount { get; set; }
        public IList<Person> Challengers { get; private set; }
        public ICollection<Event> Events { get; private set; }
    }
}