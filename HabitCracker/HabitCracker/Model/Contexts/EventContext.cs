using HabitCracker.Model.Entities;
using System;

namespace HabitCracker.Model.Contexts
{
    internal class EventContext
    {
        private static readonly Lazy<EventContext> Instance = new(() => new EventContext());

        public Event Event;

        public static EventContext GetInstance()
        {
            return Instance.Value;
        }

        public EventContext()
        {
            Event = new Event();
        }

        public void GetEvents()
        {
        }
    }
}