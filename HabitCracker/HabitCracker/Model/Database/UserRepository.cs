using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitCracker.Model.Database;

using HabitCracker.Model.Database;

using HabitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabitCracker.Model
{
    internal class UserRepository : EFGenericRepository<Person>, IGenericRepository<Person>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}