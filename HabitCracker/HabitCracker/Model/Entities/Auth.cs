namespace HabitCracker.Model.Entities
{
    public class Auth
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public Person Person { get; set; }
    }
}