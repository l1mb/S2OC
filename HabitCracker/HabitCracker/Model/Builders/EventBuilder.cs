using HabitCracker.Model.Entities;
using System;

namespace HabitCracker.Model.Builders
{
    internal class EventBuilder
    {
        private readonly Event _event = new();

        public EventBuilder SetDate(DateTime value)
        {
            _event.Day = value;
            return this;
        }

        public EventBuilder SetChallenge(Challenge value)
        {
            _event.Challenge = value;
            return this;
        }

        public EventBuilder SetName(string value)
        {
            _event.EventName = value;
            return this;
        }

        public Event GetEvent() => _event;
    }
}