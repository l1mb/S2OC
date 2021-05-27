using System.Collections.Generic;

namespace HabitCracker.Model.Entities
{
    public class Person
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
        public int AuthRef { get; set; }
        public Auth Auth { get; set; }
        public Wallet Wallet { get; set; }
        public ICollection<Challenge> Challenges { get; set; }
        public ICollection<Habit> Habits { get; set; }
    }
}