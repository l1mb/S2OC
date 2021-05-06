#nullable disable

namespace HabbitCracker.Model.Entities
{
    public partial class Challenge
    {
        public int Challengeid { get; set; }
        public int? Creatorid { get; set; }
        public string Title { get; set; }
        public string Tip { get; set; }
        public int? Dayscount { get; set; }
        public int? Challengerid { get; set; }
        public int? Eventid { get; set; }

        public virtual Person Creator { get; set; }
        public virtual Event Event { get; set; }
    }
}