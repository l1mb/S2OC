using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabbitCracker.Model.Database;
using HabbitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabbitCracker.Model
{
    internal class UserRepository : EFGenericRepository<Person>, IGenericRepository<Person>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}