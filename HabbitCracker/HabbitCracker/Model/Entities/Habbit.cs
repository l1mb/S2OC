using System;


#nullable disable

namespace HabbitCracker.Model.Entities
{
    public partial class Habbit
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CurrentStreak { get; set; }

        public virtual Person IdNavigation { get; set; }
    }
}