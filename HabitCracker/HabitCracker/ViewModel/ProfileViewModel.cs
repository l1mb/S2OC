using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace HabitCracker.ViewModel
{
    internal class ProfileViewModel : BaseViewModel
    {
        public Decimal PersonBalance =>
            //{
            //    if (CurrentPerson.Wallet == null)
            //    {
            //        CurrentPerson.Wallet = new Wallet();
            //        CoolerContext.GetInstance().People.First(p => p.Id == CurrentPerson.Id).Wallet =
            //            new Wallet();
            //        CoolerContext.GetInstance().SaveChanges();
            //    }
            decimal.Round(CurrentPerson.Wallet.Balance, 2,MidpointRounding.AwayFromZero);

        //public string LastHabbit
        //{
        //    get
        //    {
        //    };
        //}

        public Person CurrentPerson { get; set; } = UserContext.GetInstance().UserPerson;


        [CanBeNull]
        public Tuple<Habit, DateTime> LastHabit
        {
            get
            {
                //{
                //    userPersonHabit.HabitProgress.OrderByDescending(p => p.Weekday).First().Weekday))

                List<Tuple<Habit, DateTime>> tempList = (from habit in CoolerContext.GetInstance().People.Include(p => p.Habits).ThenInclude<Person, Habit, ICollection<HabitProgress>>(p => p.HabitProgress).Single(p => p == UserContext.GetInstance().UserPerson).Habits where habit.HabitProgress.OrderByDescending(p => p.Weekday).FirstOrDefault() != null select new Tuple<Habit, DateTime>(habit, habit.HabitProgress.OrderByDescending(p => p.Weekday).FirstOrDefault().Weekday)).ToList();
                var t = tempList.OrderByDescending(p => p.Item2).FirstOrDefault();
                return t;
            }
        }


        public Tuple<Habit, int> BiggestHabbitStreak
        {
            get
            {
                var t = CurrentPerson.Habits.OrderByDescending(s => s.CurrentStreak).FirstOrDefault();
                return t != null ? new Tuple<Habit, int>(t, t.CurrentStreak) : null;
            }
        }


        public ProfileViewModel()
        {
            ;

        }
    }
}