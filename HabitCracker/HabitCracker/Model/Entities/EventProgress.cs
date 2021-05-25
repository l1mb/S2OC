using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitCracker.Model.Entities
{
    public class EventProgress
    {   
        public int Id { get; set; }
        public Event Event { get; set; }

        public Person Person { get; set; }

    }
}
