using System.Collections.Generic;

#nullable disable

namespace HabitCracker.Model.Entities
{
    public partial class Wallet
    {
        public Wallet()
        {
            People = new HashSet<Person>();
        }

        public int Idwallet { get; set; }
        public decimal? Balance { get; set; }
        public string Hash { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}