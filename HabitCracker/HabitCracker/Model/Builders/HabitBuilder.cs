using HabitCracker.Model.Entities;
using System;

namespace HabitCracker.Model.Builders
{
    internal class HabitBuilder
    {
        private readonly Habit _habit = new();

        public HabitBuilder SetStreak(int value)
        {
            _habit.CurrentStreak = value;
            return this;
        }

        public HabitBuilder SetDescription(string value)
        {
            _habit.Description = value;
            return this;
        }

        public HabitBuilder SetDate(DateTime value)
        {
            _habit.CreateDate = value;
            return this;
        }

        public HabitBuilder SetDays(int value)
        {
            _habit.DaysCount = value;
            return this;
        }

        public HabitBuilder SetTitle(string value)
        {
            _habit.Title = value;
            return this;
        }

        public HabitBuilder SetPerson(Person value)
        {
            _habit.User = value;
            return this;
        }

        public Habit GetHabit()
        {
            return _habit;
        }
    }
}