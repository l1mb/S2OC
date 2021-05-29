namespace HabitCracker.Model.Entities
{
    public class EventProgress
    {
        public int Id { get; set; }
        public Event Event { get; set; }

        public Person Person { get; set; }
    }
}