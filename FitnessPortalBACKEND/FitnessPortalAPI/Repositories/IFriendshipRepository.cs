﻿namespace FitnessPortalAPI.Repositories;
public interface IFriendshipRepository
{
	Task<FriendshipRequest?> GetFriendshipRequestAsync(int requestId);
	Task<IEnumerable<FriendshipRequest>> GetFriendshipRequestsForUserAsync(int userId);
	Task CreateFriendshipRequestAsync(FriendshipRequest friendshipRequest);
	Task RemoveFriendshipRequestAsync(FriendshipRequest friendshipRequest);
	Task AddFriendAsync(User firstUser, User secondUser);
	Task RemoveFriendAsync(User userToRemove, User friendToRemove);
	Task<bool> AreUsersFriendsAsync(int firstUserId, int secondUserId);
	Task<User?> GetUserByIdAsync(int userId);
	Task<IEnumerable<User>> FindUsersWithPatternAsync(int userId, string pattern);
	Task<bool> FriendshipRequestExistsAsync(int senderId, int receiverId);
	Task<IEnumerable<Training>> GetFriendTrainingsAsync(int friendId);
}
