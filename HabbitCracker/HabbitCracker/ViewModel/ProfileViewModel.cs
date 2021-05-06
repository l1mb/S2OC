using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabbitCracker.Model.Contexts;
using HabbitCracker.Model.Entities;

namespace HabbitCracker.ViewModel
{
    internal class ProfileViewModel : BaseViewModel
    {
        public Person currentPerson = UserContext.GetInstance().UserPerson;
    }
}