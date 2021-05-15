using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using System;
using System.Linq;

namespace HabitCracker.ViewModel
{
    internal class ProfileViewModel : BaseViewModel
    {
        public Decimal PersonBalance
        {
            get
            {
                return Model.Entities.CoolerContext.GetInstance().Wallets.First(p => p.Id == CurrentPerson.Wallet.Id).Balance;
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