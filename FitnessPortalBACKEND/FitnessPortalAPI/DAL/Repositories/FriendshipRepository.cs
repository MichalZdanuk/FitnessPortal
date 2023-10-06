using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessPortalAPI.DAL.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly FitnessPortalDbContext _dbContext;

        public FriendshipRepository(FitnessPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddFriendAsync(User firstUser, User secondUser)
        {
            firstUser.Friends.Add(secondUser);
            secondUser.Friends.Add(firstUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AreUsersFriendsAsync(int firstUserId, int secondUserId)
        {
            return await _dbContext.Users
                            .AnyAsync(u => u.Id == firstUserId && u.Friends.Any(f => f.Id == secondUserId));
        }

        public async Task CreateFriendshipRequestAsync(FriendshipRequest friendshipRequest)
        {
            await _dbContext.FriendshipRequests.AddAsync(friendshipRequest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<FriendshipRequest?> GetFriendshipRequestAsync(int requestId)
        {
            return await _dbContext.FriendshipRequests
                            .Include(fr => fr.Sender)
                            .ThenInclude(u => u.Friends)
                            .Include(fr => fr.Receiver)
                            .ThenInclude(u => u.Friends)
                            .FirstOrDefaultAsync(fr => fr.Id== requestId);
        }

        public async Task<IEnumerable<FriendshipRequest>> GetFriendshipRequestsForUserAsync(int userId)
        {
            return await _dbContext.FriendshipRequests
                            .Include(fr => fr.Sender)
                            .Include(fr => fr.Receiver)
                            .Where(fr => fr.ReceiverId == userId)
                            .ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _dbContext.Users
                            .Include(u => u.Friends)
                            .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task RemoveFriendAsync(User userToRemove, User friendToRemove)
        {
            userToRemove.Friends.Remove(friendToRemove);
            friendToRemove.Friends.Remove(userToRemove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveFriendshipRequest(FriendshipRequest friendshipRequest)
        {
            _dbContext.FriendshipRequests.Remove(friendshipRequest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> FindUsersWithPattern(string pattern)
        {
            return await _dbContext.Users
                .Where(user => user.Email.Contains(pattern))
                .ToListAsync();
        }

        public async Task<bool> FriendshipRequestExistsAsync(int senderId, int receiverId)
        {
            return await _dbContext.FriendshipRequests
                .AnyAsync(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId);
        }
    }
}
