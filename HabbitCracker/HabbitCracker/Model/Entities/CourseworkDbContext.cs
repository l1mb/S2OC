using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace HabbitCracker.Model.Entities
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
                RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder)entity, "AUTH");

                entity.HasIndex(e => e.Login, "UQ__AUTH__E39E26658CA13084")
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
                RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder)entity, "CHALLENGE");

                entity.HasIndex(e => e.Eventid, "UQ__CHALLENG__6B1BA9B84907165A")
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

                RelationalForeignKeyBuilderExtensions.HasConstraintName((ReferenceCollectionBuilder)entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Challenges)
                    .HasForeignKey(d => d.Creatorid), "FK__CHALLENGE__CREAT__2057CCD0");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder)entity, "EVENT");

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

                RelationalForeignKeyBuilderExtensions.HasConstraintName((ReferenceReferenceBuilder)entity.HasOne(d => d.EventNavigation)
                    .WithOne(p => p.Event)
                    .HasPrincipalKey<Challenge>(p => p.Eventid)
                    .HasForeignKey<Event>(d => d.Eventid)
                    .OnDelete(DeleteBehavior.ClientSetNull), "FK__EVENT__EVENTID__2334397B");
            });

            modelBuilder.Entity<Habbit>(entity =>
            {
                RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder)entity, "HABBIT");

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

                RelationalForeignKeyBuilderExtensions.HasConstraintName((ReferenceCollectionBuilder)entity.HasOne(d => d.User)
                    .WithMany(p => p.Habbits)
                    .HasForeignKey(d => d.Userid), "FK__HABBIT__USERID__1C873BEC");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder)entity, "PERSON");

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

                RelationalForeignKeyBuilderExtensions.HasConstraintName((ReferenceReferenceBuilder)entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Person)
                    .HasForeignKey<Person>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull), "FK__PERSON__ID__16CE6296");

                RelationalForeignKeyBuilderExtensions.HasConstraintName((ReferenceCollectionBuilder)entity.HasOne(d => d.IdwalletNavigation)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.Idwallet), "FK__PERSON__IDWALLET__19AACF41");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.Idwallet)
                    .HasName("PK__WALLET__D462E32FEBCE32E3");

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