using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using HabitCracker.View.Menu;

namespace HabitCracker.ViewModel
{
    internal class ProfileViewModel : BaseViewModel
    {
        public string _pizdec;

        public string Pizdec
        {
            get => _pizdec;
            set => _pizdec = value;
        }

        public Decimal? PersonBalance
        {
            get
            {
                return CourseworkDbContext.GetInstance().Wallets.First(p => p.Idwallet == CurrentPerson.Idwallet).Balance;
            }
        }

        //public string LastHabbit
        //{
        //    get
        //    {
        //    };
        //}

        public Person CurrentPerson { get; set; } = UserContext.GetInstance().UserPerson;
    }
}