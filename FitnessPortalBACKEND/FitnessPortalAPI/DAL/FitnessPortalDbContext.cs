﻿using FitnessPortalAPI.Constants;
using FitnessPortalAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessPortalAPI.DAL
{
    public class FitnessPortalDbContext : DbContext
    {
        private string _connectionString =
            "Server=ZDANUKM;Database=FitnessPortalDatabase;Trusted_Connection=True;TrustServerCertificate=True;";
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BMI> BMIs { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<FriendshipRequest> FriendshipRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasMaxLength(25);

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();

            // Configure the relationship between FriendRequest and User
            modelBuilder.Entity<FriendshipRequest>()
                .HasOne(fr => fr.Sender)
                .WithMany(u => u.SentFriendRequests)
                .HasForeignKey(fr => fr.SenderId)
                .OnDelete(DeleteBehavior.Restrict); // Choose the appropriate delete behavior

            modelBuilder.Entity<FriendshipRequest>()
                .HasOne(fr => fr.Receiver)
                .WithMany(u => u.ReceivedFriendRequests)
                .HasForeignKey(fr => fr.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict); // Choose the appropriate delete behavior

            modelBuilder.Entity<BMI>()
                .Property(b => b.BMICategory)
                .HasConversion(
                    v => v.ToString(),    // Convert enum to string
                    v => (BMICategory)Enum.Parse(typeof(BMICategory), v) // Convert string to enum
                );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
