using FitnessPortalAPI.DAL.Repositories;
using FitnessPortalAPI.Entities;

namespace FitnessPortalAPI.Repositories
{
    public interface IFriendshipRepository
    {
        Task<FriendshipRequest?> GetFriendshipRequestAsync(int requestId);
        Task<IEnumerable<FriendshipRequest>> GetFriendshipRequestsForUserAsync(int userId);
        Task CreateFriendshipRequestAsync(FriendshipRequest friendshipRequest);
        Task RemoveFriendshipRequest(FriendshipRequest friendshipRequest);
        Task AddFriendAsync(User firstUser, User secondUser);
        Task RemoveFriendAsync(User userToRemove, User friendToRemove);
        Task<bool> AreUsersFriendsAsync(int firstUserId, int secondUserId);
        Task<User?> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> FindUsersWithPattern(int userId, string pattern);
        Task<bool> FriendshipRequestExistsAsync(int senderId, int receiverId);
        Task<IEnumerable<Training>> GetFriendTrainingsAsync(int friendId);
    }
}
