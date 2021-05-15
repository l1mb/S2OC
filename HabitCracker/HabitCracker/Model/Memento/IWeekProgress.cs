using System;

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