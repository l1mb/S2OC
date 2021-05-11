using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitCracker.Model.Database;
using HabitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabitCracker.Model
{
    internal class HabitRepository : EFGenericRepository<Habit>, IGenericRepository<Habit>
    {
        public HabitRepository(DbContext context) : base(context)
        {
        }
    }
}