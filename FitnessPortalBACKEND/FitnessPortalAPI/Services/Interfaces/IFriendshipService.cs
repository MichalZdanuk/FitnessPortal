using FitnessPortalAPI.Models.Friendship;

namespace FitnessPortalAPI.Services.Interfaces;
public interface IFriendshipService
{
	Task<int> SendFriendshipRequestAsync(int userId, int userToBeRequestedId);
	Task<IEnumerable<FriendshipDto>> GetFriendshipRequestsAsync(int userId);
	Task RejectFriendshipRequestAsync(int userId, int requestId);
	Task AcceptFriendshipRequestAsync(int userId, int requestId);
	Task<IEnumerable<FriendDto>> GetFriendsForUserAsync(int userId);
	Task RemoveFriendshipAsync(int userId, int userToBeRemovedId);
	Task<IEnumerable<MatchingUserDto>> FindUsersWithPatternAsync(int userId, string pattern);
	Task<FriendProfileDto> GetFriendStatisticsAsync(int userId, int friendId);
}
