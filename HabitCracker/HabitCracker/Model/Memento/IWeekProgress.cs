using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitCracker.Model.Entities;

namespace HabitCracker.Model.Memento
{
    internal class WeekMemento
    {
        public string State { get; set; }

        public WeekMemento(String state)
        {
            State = state;
        }
    }
}