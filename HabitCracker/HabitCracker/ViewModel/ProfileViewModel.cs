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
                if (CurrentPerson.Wallet == null)
                {
                    CurrentPerson.Wallet = new Wallet();
                    CoolerContext.GetInstance().People.First(p => p.Id == CurrentPerson.Id).Wallet =
                        new Wallet();
                    CoolerContext.GetInstance().SaveChanges();
                }
                return CurrentPerson.Wallet.Balance;
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