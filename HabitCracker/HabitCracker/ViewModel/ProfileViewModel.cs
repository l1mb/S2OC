using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Windows;
using HabitCracker.View.AuthView;
using JetBrains.Annotations;
using Microsoft.Win32;

namespace HabitCracker.ViewModel
{
    internal class ProfileViewModel : BaseViewModel
    {
        public int AdCost = 50;
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

        public RelayCommand BuyAd => new RelayCommand(obj =>
        {
            if (CurrentPerson.Role=="Пользователь")
            {
                if (CurrentPerson.Wallet.Balance<AdCost)
                {
                    MessageBox.Show($"Тебе не хватает ещё {AdCost - CurrentPerson.Wallet.Balance} монет");
                }
                else
                {
                    CoolerContext.GetInstance().People.FirstOrDefault(p => p == CurrentPerson).Role = "Cooler Пользователь";
                    CoolerContext.GetInstance().People.FirstOrDefault(p => p == CurrentPerson).Wallet.Balance -= AdCost;
                    CoolerContext.GetInstance().SaveChanges();
                }
                
            }
            
        });


        public string PicSource
        {
            get
            {
                return String.IsNullOrWhiteSpace(CoolerContext.GetInstance().People.FirstOrDefault(p => p == CurrentPerson).Picture) ? @"\Resources\ProfilePic.jpg" : CoolerContext.GetInstance().People.FirstOrDefault(p => p == CurrentPerson).Picture;
            }
            set
            {
                CoolerContext.GetInstance().People.FirstOrDefault(p => p == CurrentPerson).Picture = value;
                OnPropertyChanged(nameof(PicSource));
            }
        }

        public RelayCommand ChangeProfilePic => new RelayCommand(obj =>
        {
            var fileDialog = new OpenFileDialog { Filter = "Image files(*.jpg)|*.jpg" };
            if (fileDialog.ShowDialog() != true) return;
            PicSource = fileDialog.FileName;
        });


        private Window _AdWindow ;

        public ProfileViewModel()
        {
            if (UserContext.GetInstance().UserPerson.Role=="Пользователь")
            {
                _AdWindow = new Greeting();
                _AdWindow.ShowDialog();

            }
        }
    }
}