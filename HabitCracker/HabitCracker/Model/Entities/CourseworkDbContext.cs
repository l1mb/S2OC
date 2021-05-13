using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HabitCracker.Model.Entities
{
    public partial class CourseworkDbContext : DbContext
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
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            }
        }

        public virtual DbSet<Auth> Auths { get; set; }
        public virtual DbSet<Challenge> Challenges { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Habit> Habits { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<Weekprogress> Weekprogresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Auth>(entity =>
            {
                entity.ToTable("AUTH");

                entity.HasIndex(e => e.Login, "UQ__AUTH__E39E266580A496E9")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Login)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LOGIN");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Salt)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SALT");
            });

            modelBuilder.Entity<Challenge>(entity =>
            {
                entity.ToTable("CHALLENGE");

                entity.Property(e => e.Challengeid)
                    .ValueGeneratedNever()
                    .HasColumnName("CHALLENGEID");

                entity.Property(e => e.Challengerid).HasColumnName("CHALLENGERID");

                entity.Property(e => e.Creatorid).HasColumnName("CREATORID");

                entity.Property(e => e.Dayscount).HasColumnName("DAYSCOUNT");

                entity.Property(e => e.Eventid).HasColumnName("EVENTID");

                entity.Property(e => e.Tip)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIP");

                entity.Property(e => e.Title)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Challenges)
                    .HasForeignKey(d => d.Creatorid)
                    .HasConstraintName("FK__CHALLENGE__CREAT__5D60DB10");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("EVENT");

                entity.Property(e => e.Eventid)
                    .ValueGeneratedNever()
                    .HasColumnName("EVENTID");

                entity.Property(e => e.Challengeid).HasColumnName("CHALLENGEID");

                entity.Property(e => e.Day)
                    .HasColumnType("datetime")
                    .HasColumnName("DAY");

                entity.Property(e => e.Event1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EVENT");

                entity.Property(e => e.Isdone).HasColumnName("ISDONE");

                entity.HasOne(d => d.Challenge)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.Challengeid)
                    .HasConstraintName("FK__EVENT__CHALLENGE__6CA31EA0");
            });

            modelBuilder.Entity<Habit>(entity =>
            {
                entity.ToTable("HABIT");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Userid).HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Habits)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__HABIT__USERID__3CF40B7E");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("PERSON");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Idwallet).HasColumnName("IDWALLET");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Name)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Picture)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PICTURE");

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ROLE")
                    .HasDefaultValueSql("('Пользователь')");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Person)
                    .HasForeignKey<Person>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PERSON__ID__373B3228");

                entity.HasOne(d => d.IdwalletNavigation)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.Idwallet)
                    .HasConstraintName("FK__PERSON__IDWALLET__3A179ED3");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.Idwallet)
                    .HasName("PK__WALLET__D462E32F0F7CDBE4");

                entity.ToTable("WALLET");

                entity.Property(e => e.Idwallet)
                    .ValueGeneratedNever()
                    .HasColumnName("IDWALLET");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(17, 3)")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.Hash)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("HASH");
            });

            modelBuilder.Entity<Weekprogress>(entity =>
            {
                entity.ToTable("WEEKPROGRESS");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Habit).HasColumnName("HABIT");

                entity.Property(e => e.Weekday)
                    .HasColumnType("date")
                    .HasColumnName("WEEKDAY");

                entity.HasOne(d => d.HabitNavigation)
                    .WithMany(p => p.Weekprogresses)
                    .HasForeignKey(d => d.Habit)
                    .HasConstraintName("FK__WEEKPROGR__HABIT__6F7F8B4B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}