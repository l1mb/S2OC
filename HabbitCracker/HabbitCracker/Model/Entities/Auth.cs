﻿using System.Security.Policy;

#nullable disable

namespace HabbitCracker.Model.Entities
{
    public partial class Auth
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public virtual Person Person { get; set; }
    }
}