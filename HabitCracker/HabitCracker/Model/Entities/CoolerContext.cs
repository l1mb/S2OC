using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HabitCracker.Model.Entities
{
    internal sealed class CoolerContext : DbContext
    {
        private static readonly Lazy<CourseworkDbContext> Instance = new(() => new CourseworkDbContext());

        public static CourseworkDbContext GetInstance()
        {
            return Instance.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["CoolerDaniel"].ConnectionString);
                optionsBuilder.UseSqlServer(
                    "Data Source=DESKTOP-O8V49SL;Initial Catalog=CoolerOopDatabase;Integrated Security=True");
            }
        }

        public DbSet<Auth> Auths { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Weekprogress> WeekProgresses { get; set; }
    }
}