using Microsoft.EntityFrameworkCore;
using System;

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
            //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["CoolerDaniel"].ConnectionString);
            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-O8V49SL;Initial Catalog=CoolerOopDatabase;Integrated Security=True;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // public class Author
            // {
            //     public AuthorBiography Biography { get; set; }
            //     public int AuthorId { get; set; }
            //     public string FirstName { get; set; }
            //     public string LastName { get; set; }
            // }
            // public class AuthorBiography
            // {
            //     public Author Author { get; set; }
            //     public DateTime DateOfBirth { get; set; }
            //     public int AuthorBiographyId { get; set; }
            //     public int AuthorRef { get; set; }
            //     public string Biography { get; set; }
            //     public string Nationality { get; set; }
            //     public string PlaceOfBirth { get; set; }
            // }

            //  modelBuilder.Entity<Author>()
            // .HasOne(a => a.Biography)
            // .WithOne(b => b.Author)
            // .HasForeignKey<AuthorBiography>(b => b.AuthorRef);
            var auth = modelBuilder.Entity<Auth>();
            var person = modelBuilder.Entity<Person>();
            var wallet = modelBuilder.Entity<Wallet>();
            var habitProgress = modelBuilder.Entity<HabitProgress>();
            var habit = modelBuilder.Entity<Habit>();

            habit.HasMany(p =>p.HabitProgress).WithOne(p =>p.Habit).OnDelete(DeleteBehavior.Cascade);
            habitProgress.HasOne(p => p.Habit).WithMany(p => p.HabitProgress).OnDelete(DeleteBehavior.Cascade);

            auth.HasOne(a => a.Person).WithOne(p => p.Auth).HasForeignKey<Person>(p => p.AuthRef);
            person.HasOne(p => p.Wallet).WithOne(w => w.Owner).HasForeignKey<Wallet>(w => w.PersonRef);
            person.Property(p => p.Role).HasDefaultValue("Пользователь");


            // auth.HasData(new[]
            // {
            //     new Auth
            //     {
            //         Id = ...,
            //         Login = ...,
            //         Password = ...,
            //         Salt = ...
            //     },
            //     new Auth
            //     {
            //         Id = ...,
            //         Login = ...,
            //         Password = ...,
            //         Salt = ...
            //     }
            // });

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