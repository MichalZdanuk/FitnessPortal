using Microsoft.EntityFrameworkCore;

namespace FitnessPortalAPI.Entities
{
    public class FitnessPortalDbContext : DbContext
    {
        private string _connectionString =
            "Server=ZDANUKM;Database=FitnessPortalDb;Trusted_Connection=True;TrustServerCertificate=True;";
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BMI> BMIs { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

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

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

    
}
