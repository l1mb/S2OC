using System.Collections.Generic;

#nullable disable

namespace HabbitCracker.Model.Entities
{
    public partial class Person
    {
        public Person()
        {
            Challenges = new HashSet<Challenge>();
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

        public override string ToString()
        {
            return $"{this.Name}, {this.Lastname}, {this.Id}";
        }
    }
}