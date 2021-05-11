using System;
using System.Windows;
using System.Windows.Controls;
using HabitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HabitCracker.View.Menu
{
    /// <summary>
    /// Логика взаимодействия для DataBase.xaml
    /// </summary>
    public partial class DataBase : Page
    {
        private CourseworkDbContext db;

        public DataBase()
        {
            InitializeComponent();

            db = new CourseworkDbContext();
            LoadData();
            SetItemSource();

            //phonesGrid.ItemsSource = db.Phones.Local.ToBindingList(); // устанавливаем привязку к кэшу

            //this.Closing += MainWindow_Closing;
        }

        private void LoadData()
        {
            db.Auths.Load();
            db.People.Load();
            db.Challenges.Load();
            db.Habits.Load();
            db.Wallets.Load();
            db.Events.Load();
        }

        private void SetItemSource()
        {
            AuthGrid.ItemsSource = db.Auths.Local.ToBindingList();
            PersonGrid.ItemsSource = db.People.Local.ToBindingList();
            HabitGrid.ItemsSource = db.Habits.Local.ToBindingList();
            EventGrid.ItemsSource = db.Events.Local.ToBindingList();
            ChallengesGrid.ItemsSource = db.Challenges.Local.ToBindingList();
            WalletGrid.ItemsSource = db.Wallets.Local.ToBindingList();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            List<DataGrid> grids = new();

            grids.Add(AuthGrid);
            grids.Add(PersonGrid);
            grids.Add(EventGrid);
            grids.Add(ChallengesGrid);
            grids.Add(HabitGrid);
            grids.Add(WalletGrid);

            var selected = grids.Where(p => p.SelectedItems.Count > 0);

            foreach (var item in selected)
            {
                switch (item.Name)
                {
                    case "AuthGrid":
                        {
                            for (int i = 0; i < item.SelectedItems.Count; i++)
                            {
                                Auth auth = item.SelectedItems[i] as Auth;
                                if (auth != null)
                                {
                                    db.Auths.Remove(auth);
                                }
                            }
                        }
                        break;

                    case "ChallengeGrid":
                        {
                            for (int i = 0; i < item.SelectedItems.Count; i++)
                            {
                                Model.Entities.Challenge challenge = item.SelectedItems[i] as Model.Entities.Challenge;
                                if (challenge != null)
                                {
                                    db.Challenges.Remove(challenge);
                                }
                            }
                        }
                        break;

                    case "EventGrid":
                        {
                            for (int i = 0; i < item.SelectedItems.Count; i++)
                            {
                                Event @event = item.SelectedItems[i] as Event;
                                if (@event != null)
                                {
                                    db.Events.Remove(@event);
                                }
                            }
                        }
                        break;

                    case "PersonGrid":
                        {
                            for (int i = 0; i < item.SelectedItems.Count; i++)
                            {
                                Person person = item.SelectedItems[i] as Person;
                                if (person != null)
                                {
                                    db.People.Remove(person);
                                }
                            }
                        }
                        break;

                    case "WalletGrid":
                        {
                            for (int i = 0; i < item.SelectedItems.Count; i++)
                            {
                                Wallet wallet = item.SelectedItems[i] as Wallet;
                                if (wallet != null)
                                {
                                    db.Wallets.Remove(wallet);
                                }
                            }
                        }
                        break;

                    case "HabitGrid":
                        {
                            for (int i = 0; i < item.SelectedItems.Count;)
                            {
                                Model.Entities.Habit Habit = item.SelectedItems[i] as Model.Entities.Habit;
                                if (Habit != null)
                                {
                                    db.Habits.Remove(Habit);
                                }
                            }
                        }
                        break;
                }
            }

            //!tried to use generic methods, but something went wrong
            //also tried map datagrid with DbSet<"EntityName"> but do not finished it

            db.SaveChanges();
        }

        private void Remove<T>(DataGrid grid, int itemNumber) where T : class
        {
            T var = grid.SelectedItems[itemNumber] as T;
            if (var != null)
            {
                switch (nameof(T))
                {
                }
            }
        }
    }
}