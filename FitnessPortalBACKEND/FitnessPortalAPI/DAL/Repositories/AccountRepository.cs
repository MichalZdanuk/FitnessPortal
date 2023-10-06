using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessPortalAPI.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FitnessPortalDbContext _dbContext;
        public AccountRepository(FitnessPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users
                                .Include(u => u.Role)
                                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _dbContext.Users
                                .Include(u => u.Role)
                                .Include(u => u.Friends)
                                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
