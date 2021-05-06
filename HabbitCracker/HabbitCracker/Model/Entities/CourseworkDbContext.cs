using System;
using System.Configuration;
using HabbitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HabbitCracker
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

        public CourseworkDbContext()
        {
        }

        public CourseworkDbContext(DbContextOptions<CourseworkDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auth> Auths { get; set; }
        public virtual DbSet<Challenge> Challenges { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Habbit> Habbits { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Auth>(entity =>
            {
                entity.ToTable("AUTH");

                entity.HasIndex(e => e.Login, "UQ__AUTH__E39E266508CF8D5F")
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

                entity.HasIndex(e => e.Eventid, "UQ__CHALLENG__6B1BA9B81F618BEB")
                    .IsUnique();

                entity.Property(e => e.Challengeid)
                    .ValueGeneratedNever()
                    .HasColumnName("CHALLENGEID");

                entity.Property(e => e.Challengerid).HasColumnName("CHALLENGERID");

                entity.Property(e => e.Creatorid).HasColumnName("CREATORID");

                entity.Property(e => e.Dayscount).HasColumnName("DAYSCOUNT");

                entity.Property(e => e.Eventid)
                    .IsRequired()
                    .HasColumnName("EVENTID");

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
                    .HasConstraintName("FK__CHALLENGE__CREAT__5F7E2DAC");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("EVENT");

                entity.Property(e => e.Eventid)
                    .ValueGeneratedNever()
                    .HasColumnName("EVENTID");

                entity.Property(e => e.Day)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("DAY");

                entity.Property(e => e.Event1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EVENT");

                entity.HasOne(d => d.EventNavigation)
                    .WithOne(p => p.Event)
                    .HasPrincipalKey<Challenge>(p => p.Eventid)
                    .HasForeignKey<Event>(d => d.Eventid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EVENT__EVENTID__625A9A57");
            });

            modelBuilder.Entity<Habbit>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("HABBIT");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Title)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__HABBIT__ID__4F47C5E3");
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
                    .HasColumnName("ROLE");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Person)
                    .HasForeignKey<Person>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PERSON__ID__4B7734FF");

                entity.HasOne(d => d.IdwalletNavigation)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.Idwallet)
                    .HasConstraintName("FK__PERSON__IDWALLET__4D5F7D71");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.Idwallet)
                    .HasName("PK__WALLET__D462E32FD9A60D3D");

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}