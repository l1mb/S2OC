using System.Collections.Generic;

#nullable disable

namespace HabitCracker.Model.Entities
{
    public partial class Person
    {
        public Person()
        {
            Challenges = new HashSet<Challenge>();
            Habits = new HashSet<Habit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
        public string Picture { get; set; }
        public int? Idwallet { get; set; }

        public virtual Auth IdNavigation { get; set; }
        public virtual Wallet IdwalletNavigation { get; set; }
        public virtual ICollection<Challenge> Challenges { get; set; }
        public virtual ICollection<Habit> Habits { get; set; }
    }
}