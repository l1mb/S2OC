using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabitCracker.Model.Contexts
{
    internal class EventContext : DbContext
    {
        public static readonly Lazy<EventContext> Instance = new(() => new EventContext());

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