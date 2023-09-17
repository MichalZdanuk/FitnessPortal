using FitnessPortalAPI.Models.Friendship;
using FitnessPortalAPI.Models.Trainings;

namespace FitnessPortalAPI.Services.Interfaces
{
    public interface IFriendshipService
    {
        Task<int> SendFriendshipRequest(int userId, int userToBeRequestedId);
        Task<List<FriendshipDto>> GetFriendshipRequests(int userId);
        Task RejectFriendRequest(int userId, int requestId);
        Task AcceptFriendRequest(int userId, int requestId);
        Task<List<FriendDto>> GetFriends(int userId);
        Task RemoveFriendship(int userId, int userToBeRemovedId);
        Task<List<MatchingUserDto>> FindUsersWithPattern(string pattern);
    }
}
