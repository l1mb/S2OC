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
    internal class HabbitRepository : EFGenericRepository<Habbit>, IGenericRepository<Habbit>
    {
        public HabbitRepository(DbContext context) : base(context)
        {
        }
    }
}