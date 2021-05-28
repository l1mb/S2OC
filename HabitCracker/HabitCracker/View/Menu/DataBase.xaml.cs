using HabitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HabitCracker.View.Menu
{
    /// <summary>
    /// Логика взаимодействия для DataBase.xaml
    /// </summary>
    public partial class DataBase : Page
    {
        private readonly Model.Entities.CoolerContext db;

        public DataBase()
        {
            InitializeComponent();

            db = new Model.Entities.CoolerContext();
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
            db.HabitProgress.Load();
            db.EventProgress.Load();
        }

        private void SetItemSource()
        {
            AuthGrid.ItemsSource = db.Auths.Local.ToBindingList();
            PersonGrid.ItemsSource = db.People.Local.ToBindingList();
            HabitGrid.ItemsSource = db.Habits.Local.ToBindingList();
            EventGrid.ItemsSource = db.Events.Local.ToBindingList();
            ChallengesGrid.ItemsSource = db.Challenges.Local.ToBindingList();
            WalletGrid.ItemsSource = db.Wallets.Local.ToBindingList();
            HabitProgressGrid.ItemsSource = db.HabitProgress.Local.ToBindingList();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Инвалидная операция с базой данных, проверьте корректность введенных значений");
            }
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
                                for (int i = 0; i < item.SelectedItems.Count;)
                                {
                                    if (item.SelectedItems[i] is Auth auth)
                                    {
                                        db.Auths.Remove(auth);
                                    }
                                }
                            }
                            break;

                        case "ChallengesGrid":
                            {
                                for (int i = 0; i < item.SelectedItems.Count;)
                                {
                                    if (item.SelectedItems[i] is Model.Entities.Challenge challenge)
                                    {
                                        db.Challenges.Remove(challenge);
                                    }
                                }
                            }
                            break;

                        case "EventGrid":
                            {
                                for (int i = 0; i < item.SelectedItems.Count;)
                                {
                                    if (item.SelectedItems[i] is Event @event)
                                    {
                                        db.EventProgress.RemoveRange(db.EventProgress.Where(p=>p.Event==@event));
                                        db.Events.Remove(@event);
                                        
                                    }
                                }
                            }
                            break;

                        case "PersonGrid":
                            {
                                for (int i = 0; i < item.SelectedItems.Count;)
                                {
                                    if (item.SelectedItems[i] is Person person)
                                    {
                                        db.People.Remove(person);
                                    }
                                }
                            }
                            break;

                        case "WalletGrid":
                            {
                                for (int i = 0; i < item.SelectedItems.Count;)
                                {
                                    if (item.SelectedItems[i] is Wallet wallet)
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
                                    if (item.SelectedItems[i] is Model.Entities.Habit habit)
                                    {
                                        if (db.HabitProgress.Count(p => p.Habit == habit) != 0)
                                        {
                                            db.HabitProgress.Remove(db.HabitProgress.First(p => p.Habit == habit));

                                        }
                                        db.Habits.Remove(habit);
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
    }
}