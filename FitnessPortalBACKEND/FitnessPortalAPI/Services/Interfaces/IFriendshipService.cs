using FitnessPortalAPI.Models.Friendship;

namespace FitnessPortalAPI.Services.Interfaces
{
    public interface IFriendshipService
    {
        Task<int> SendFriendshipRequest(int userId, int userToBeRequestedId);
        Task<IEnumerable<FriendshipDto>> GetFriendshipRequests(int userId);
        Task RejectFriendshipRequest(int userId, int requestId);
        Task AcceptFriendshipRequest(int userId, int requestId);
        Task<IEnumerable<FriendDto>> GetFriendsForUser(int userId);
        Task RemoveFriendship(int userId, int userToBeRemovedId);
        Task<IEnumerable<MatchingUserDto>> FindUsersWithPattern(string pattern);
    }
}
