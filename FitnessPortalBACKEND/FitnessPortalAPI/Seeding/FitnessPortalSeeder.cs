using FitnessPortalAPI.Constants;
using FitnessPortalAPI.DAL;
using FitnessPortalAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace FitnessPortalAPI.Seeding
{
    public class FitnessPortalSeeder
    {
        private readonly FitnessPortalDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private Calculator _calculator;
        public FitnessPortalSeeder(FitnessPortalDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _calculator = new Calculator();
        }
        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                SeedRoles();
                SeedUsers();
                SeedArticles();
                SeedBMI();
                SeedTrainings();
            }
        }

        private void SeedRoles()
        {
            if (!_context.Roles.Any())
            {
                var roles = GetRoles();
                _context.Roles.AddRange(roles);
                _context.SaveChanges();
            }
        }

        private void SeedUsers()
        {
            if (!_context.Users.Any())
            {
                var users = GetUsersWithHashedPasswords();
                _context.Users.AddRange(users);
                _context.SaveChanges();
            }
        }

        private void SeedArticles()
        {
            if (!_context.Articles.Any())
            {
                var users = _context.Users.ToList();

                var articles = ArticlesSeeder.GetArticles(users);
                _context.Articles.AddRange(articles);
                _context.SaveChanges();
            }
        }

        private void SeedBMI()
        {
            if (!_context.BMIs.Any())
            {
                var users = _context.Users.ToList();

                var bmis = BMISeeder.GetBMIRecords(users);
                _context.BMIs.AddRange(bmis);
                _context.SaveChanges();
            }
        }

        private void SeedTrainings()
        {
            if (!_context.Trainings.Any())
            {
                var users = _context.Users.ToList();

                var trainings = TrainingsSeeder.GetSampleTrainings(users);
                _context.Trainings.AddRange(trainings);
                _context.SaveChanges();
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }

        private IEnumerable<User> GetUsersWithHashedPasswords()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Email = "john@doe.com",
                    Username = "JohnDoe",
                    DateOfBirth = new DateTime(1990, 5, 15),
                    Weight = 75,
                    Height = 180,
                    RoleId = 1,
                    PasswordHash = HashPassword("johndoe"),
                },
                new User()
                {
                    Email = "jane@smith.com",
                    Username = "JaneSmith",
                    DateOfBirth = new DateTime(1985, 8, 20),
                    Weight = 68,
                    Height = 170,
                    RoleId = 1,
                    PasswordHash = HashPassword("janesmith"),
                },
                new User()
                {
                    Email = "alice@johnson.com",
                    Username = "AliceJohnson",
                    DateOfBirth = new DateTime(1992, 10, 10),
                    Weight = 62,
                    Height = 165,
                    RoleId = 1,
                    PasswordHash = HashPassword("alicejohnson"),
                },
            };
            return users;
        }

        private string HashPassword(string password)
        {
            var user = new User();
            return _passwordHasher.HashPassword(user, password);
        }
    }
}
