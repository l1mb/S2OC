using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace HabitCracker.Model.Entities
{
    internal sealed class CoolerContext : DbContext
    {
        private static readonly Lazy<CoolerContext> Instance = new(() => new CoolerContext());

        public static CoolerContext GetInstance()
        {
            return Instance.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            optionsBuilder.UseSqlServer(
                ConfigurationManager.ConnectionStrings["CoolerDaniel"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var auth = modelBuilder.Entity<Auth>();
            var person = modelBuilder.Entity<Person>();
            var wallet = modelBuilder.Entity<Wallet>();
            var habitProgress = modelBuilder.Entity<HabitProgress>();
            var habit = modelBuilder.Entity<Habit>();

            habit.HasMany(p => p.HabitProgress).WithOne(p => p.Habit).OnDelete(DeleteBehavior.Cascade);
            habitProgress.HasOne(p => p.Habit).WithMany(p => p.HabitProgress).OnDelete(DeleteBehavior.Cascade);

            auth.HasOne(a => a.Person).WithOne(p => p.Auth).HasForeignKey<Person>(p => p.AuthRef);
            person.HasOne(p => p.Wallet).WithOne(w => w.Owner).HasForeignKey<Wallet>(w => w.PersonRef);
            person.Property(p => p.Role).HasDefaultValue("Пользователь");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Auth> Auths { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitProgress> HabitProgress { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<EventProgress> EventProgress { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
    }
}