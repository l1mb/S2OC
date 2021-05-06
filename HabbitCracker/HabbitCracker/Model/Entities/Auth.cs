#nullable disable

namespace HabbitCracker.Model.Entities
{
    public partial class Auth
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public virtual Person Person { get; set; }
    }
}