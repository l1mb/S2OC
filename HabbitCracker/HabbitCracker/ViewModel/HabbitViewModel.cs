using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabbitCracker.Model.Contexts;
using HabbitCracker.Model.Entities;

namespace HabbitCracker.ViewModel
{
    internal class HabbitViewModel : BaseViewModel
    {
        private ObservableCollection<Habbit> _personHabbits = new ObservableCollection<Habbit>();
        private Habbit _selectedHabbit;

        public ObservableCollection<Habbit> PersonHabbits
        {
            get => _personHabbits;
            set
            {
                _personHabbits = value;
                OnPropertyChanged(nameof(PersonHabbits));
            }
        }

        public Habbit SelectedHabbit
        {
            get => _selectedHabbit;
            set
            {
                _selectedHabbit = value;
                OnPropertyChanged(nameof(SelectedHabbit));
            }
        }

        public HabbitViewModel()
        {
            if (HabbitContext.GetInstance().Habbits.Count == 0)
            {
                _personHabbits.Add(new Habbit()
                {
                    Id = UserContext.GetInstance().UserPerson.Id,
                    CurrentStreak = 0,
                    Description = "text about danger of smoking",
                    CreateDate = DateTime.Now,
                    Title = "Smoking",
                    IdNavigation = UserContext.GetInstance().UserPerson
                });
                _personHabbits.Add(new Habbit()
                {
                    Id = UserContext.GetInstance().UserPerson.Id,
                    CurrentStreak = 0,
                    Description = "text about danger of drinking",
                    CreateDate = DateTime.Now,
                    Title = "Drinking",
                    IdNavigation = UserContext.GetInstance().UserPerson
                });

                _personHabbits.Add(new Habbit()
                {
                    Id = UserContext.GetInstance().UserPerson.Id,
                    CurrentStreak = 0,
                    Description = "text about danger of using drugs",
                    CreateDate = DateTime.Now,
                    Title = "Druging",
                    IdNavigation = UserContext.GetInstance().UserPerson
                });

                _personHabbits.Add(new Habbit()
                {
                    Id = UserContext.GetInstance().UserPerson.Id,
                    CurrentStreak = 0,
                    Description = "text about danger of using seal",
                    CreateDate = DateTime.Now,
                    Title = "Sealing",
                    IdNavigation = UserContext.GetInstance().UserPerson
                });
            }
            else
            {
                PersonHabbits = HabbitContext.GetInstance().Habbits;
            }
        }
    }
}