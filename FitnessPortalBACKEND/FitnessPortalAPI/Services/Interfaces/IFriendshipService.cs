using FitnessPortalAPI.Models.Friendship;

namespace FitnessPortalAPI.Services.Interfaces;
public interface IFriendshipService
{
	Task<int> SendFriendshipRequestAsync(int userToBeRequestedId);
	Task<IEnumerable<FriendshipDto>> GetFriendshipRequestsAsync();
	Task RejectFriendshipRequestAsync(int requestId);
	Task AcceptFriendshipRequestAsync(int requestId);
	Task<IEnumerable<FriendDto>> GetFriendsAsync();
	Task RemoveFriendshipAsync(int userToBeRemovedId);
	Task<IEnumerable<MatchingUserDto>> FindUsersWithPatternAsync(string pattern);
	Task<FriendProfileDto> GetFriendStatisticsAsync(int friendId);
}
