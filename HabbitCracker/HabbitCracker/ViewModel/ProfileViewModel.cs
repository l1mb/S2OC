using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using HabbitCracker.Model.Contexts;
using HabbitCracker.Model.Entities;

namespace HabbitCracker.ViewModel
{
    internal class ProfileViewModel : BaseViewModel
    {
        public string _pizdec;

        public string Pizdec
        {
            get => _pizdec;
            set => _pizdec = value;
        }

        public Person CurrentPerson { get; set; } = UserContext.GetInstance().UserPerson;
    }
}