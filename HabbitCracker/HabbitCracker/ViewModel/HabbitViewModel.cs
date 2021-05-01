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
        private ObservableCollection<Habbit> _personHabbits;
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
                _personHabbits.Add(new Habbit() { Id = });
            }
        }
    }
}