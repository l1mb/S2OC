﻿// <auto-generated />
using System;
using HabitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HabitCracker.Migrations
{
    [DbContext(typeof(CoolerContext))]
    [Migration("20210525195841_events")]
    partial class events
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChallengePerson", b =>
                {
                    b.Property<int>("ChallengersId")
                        .HasColumnType("int");

                    b.Property<int>("ChallengesId")
                        .HasColumnType("int");

                    b.HasKey("ChallengersId", "ChallengesId");

                    b.HasIndex("ChallengesId");

                    b.ToTable("ChallengePerson");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Auth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Auths");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Challenge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DaysCount")
                        .HasColumnType("int");

                    b.Property<string>("Tip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChallengeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.HasIndex("PersonId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.EventProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("PersonId");

                    b.ToTable("EventProgress");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Habit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentStreak")
                        .HasColumnType("int");

                    b.Property<int>("DaysCount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Habits");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.HabitProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("HabitId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Weekday")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HabitId");

                    b.ToTable("HabitProgress");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthRef")
                        .HasColumnType("int");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Пользователь");

                    b.HasKey("Id");

                    b.HasIndex("AuthRef")
                        .IsUnique();

                    b.ToTable("People");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PersonRef")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonRef")
                        .IsUnique();

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("ChallengePerson", b =>
                {
                    b.HasOne("HabitCracker.Model.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("ChallengersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HabitCracker.Model.Entities.Challenge", null)
                        .WithMany()
                        .HasForeignKey("ChallengesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Event", b =>
                {
                    b.HasOne("HabitCracker.Model.Entities.Challenge", "Challenge")
                        .WithMany("Events")
                        .HasForeignKey("ChallengeId");

                    b.HasOne("HabitCracker.Model.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("Challenge");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.EventProgress", b =>
                {
                    b.HasOne("HabitCracker.Model.Entities.Event", "Event")
                        .WithMany("EventProgress")
                        .HasForeignKey("EventId");

                    b.HasOne("HabitCracker.Model.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("Event");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Habit", b =>
                {
                    b.HasOne("HabitCracker.Model.Entities.Person", "User")
                        .WithMany("Habits")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.HabitProgress", b =>
                {
                    b.HasOne("HabitCracker.Model.Entities.Habit", "Habit")
                        .WithMany("HabitProgress")
                        .HasForeignKey("HabitId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Habit");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Person", b =>
                {
                    b.HasOne("HabitCracker.Model.Entities.Auth", "Auth")
                        .WithOne("Person")
                        .HasForeignKey("HabitCracker.Model.Entities.Person", "AuthRef")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auth");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Wallet", b =>
                {
                    b.HasOne("HabitCracker.Model.Entities.Person", "Owner")
                        .WithOne("Wallet")
                        .HasForeignKey("HabitCracker.Model.Entities.Wallet", "PersonRef")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Auth", b =>
                {
                    b.Navigation("Person");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Challenge", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Event", b =>
                {
                    b.Navigation("EventProgress");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Habit", b =>
                {
                    b.Navigation("HabitProgress");
                });

            modelBuilder.Entity("HabitCracker.Model.Entities.Person", b =>
                {
                    b.Navigation("Habits");

                    b.Navigation("Wallet");
                });
#pragma warning restore 612, 618
        }
    }
}
