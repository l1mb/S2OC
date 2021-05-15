namespace HabitCracker.Model.Entities
{
    public class Wallet
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public int PersonRef { get; set; }

        public Person Owner { get; set; }
    }
}