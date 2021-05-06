#nullable disable

namespace HabbitCracker.Model.Entities
{
    public partial class Event
    {
        public int Eventid { get; set; }
        public string Day { get; set; }
        public string Event1 { get; set; }

        public virtual Challenge EventNavigation { get; set; }
    }
}